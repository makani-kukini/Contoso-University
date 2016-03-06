Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ContosoUniversity.DAL
Imports ContosoUniversity.Models
Imports ContosoUniversity.ViewModels

Namespace Controllers
    Public Class InstructorController
        Inherits System.Web.Mvc.Controller

        Private db As New SchoolContext

        ' GET: Instructor
        Function Index(ByVal id As Integer?, ByVal courseID As Integer?) As ActionResult
            Dim viewModel As InstructorIndexData = New InstructorIndexData()
            viewModel.Instructors = db.Instructors.
                Include(Function(i) i.OfficeAssignment).
                Include(Function(i) i.Courses.Select(Function(c) c.Department)).
                OrderBy(Function(i) i.LastName)

            If (id IsNot Nothing) Then
                ViewBag.InstructorID = id.Value
                viewModel.Courses = viewModel.Instructors.Where(
                    Function(i) i.ID = id.Value).Single().Courses
            End If

            If (courseID IsNot Nothing) Then
                ViewBag.CourseID = courseID.Value
                'Lazy Loading
                'viewModel.Enrollments = viewModel.Courses.Where(
                '   Function(x) x.CourseID = courseID).Single().Enrollments
                'Explicit Loading
                Dim selectedCourse As Course = viewModel.Courses.Where(Function(x) x.CourseID = courseID).Single()
                db.Entry(selectedCourse).Collection(Function(x) x.Enrollments).Load()
                For Each enrollment As Enrollment In selectedCourse.Enrollments
                    db.Entry(enrollment).Reference(Function(x) x.Student).Load()
                Next

                viewModel.Enrollments = selectedCourse.Enrollments
            End If

            Return View(viewModel)
        End Function

        ' GET: Instructor/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim instructor As Instructor = db.Instructors.Find(id)
            If IsNothing(instructor) Then
                Return HttpNotFound()
            End If
            Return View(instructor)
        End Function

        ' GET: Instructor/Create
        Function Create() As ActionResult
            Dim instructor As Instructor = New Instructor()
            instructor.Courses = New List(Of Course)()
            PopulateAssignedCourseData(instructor)
            Return View()
        End Function

        ' POST: Instructor/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="LastName,FirstMidName,HireDate,OfficeAssignment")> ByVal instructor As Instructor, ByVal selectedCourses As String()) As ActionResult
            If (selectedCourses IsNot Nothing) Then
                instructor.Courses = New List(Of Course)()
                For Each course In selectedCourses
                    Dim courseToAdd = db.Courses.Find(Integer.Parse(course))
                    instructor.Courses.Add(courseToAdd)
                Next
            End If

            If ModelState.IsValid Then
                db.Instructors.Add(instructor)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            PopulateAssignedCourseData(instructor)
            Return View(instructor)
        End Function

        ' GET: Instructor/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim instructor As Instructor = db.Instructors.
                Include(Function(i) i.OfficeAssignment).
                Include(Function(i) i.Courses).
                Where(Function(i) i.ID = id).
                Single()
            PopulateAssignedCourseData(instructor)
            If IsNothing(instructor) Then
                Return HttpNotFound()
            End If
            Return View(instructor)
        End Function

        ' POST: Instructor/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(ByVal id As Integer?, ByVal selectedCourses As String()) As ActionResult
            If (id Is Nothing) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim instructorToUpdate As Instructor = db.Instructors.
                Include(Function(i) i.OfficeAssignment).
                Include(Function(i) i.Courses).
                Where(Function(i) i.ID = id).
                Single()

            If (TryUpdateModel(instructorToUpdate, "", New String() {"LastName", "FirstMidName", "HireDate", "OfficeAssignment"})) Then
                Try
                    If (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location)) Then
                        instructorToUpdate.OfficeAssignment = Nothing
                    End If

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate)

                    db.SaveChanges()

                    Return RedirectToAction("Index")
                Catch dex As RetryLimitExceededException
                    'Log the error (uncomment dex variable name And add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.")
                End Try
            End If
            PopulateAssignedCourseData(instructorToUpdate)
            Return View(instructorToUpdate)
        End Function

        ' GET: Instructor/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim instructor As Instructor = db.Instructors.Find(id)
            If IsNothing(instructor) Then
                Return HttpNotFound()
            End If
            Return View(instructor)
        End Function

        ' POST: Instructor/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim instructor As Instructor = db.Instructors.
                Include(Function(i) i.OfficeAssignment).
                Where(Function(i) i.ID = id).
                Single()

            db.Instructors.Remove(instructor)

            Dim department = db.Departments.
                Where(Function(d) d.InstructorID = id).
                SingleOrDefault()
            If (department IsNot Nothing) Then
                department.InstructorID = Nothing
            End If

            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Private Sub UpdateInstructorCourses(ByVal selectedCourses As String(), ByVal instructorToUpdate As Instructor)
            If (selectedCourses Is Nothing) Then
                instructorToUpdate.Courses = New List(Of Course)()
                Return
            End If

            Dim selectedCoursesHS = New HashSet(Of String)(selectedCourses)
            Dim instructorCourses = New HashSet(Of Integer)(instructorToUpdate.Courses.Select(Function(c) c.CourseID))
            For Each course In db.Courses
                If (selectedCoursesHS.Contains(course.CourseID.ToString())) Then
                    If Not (instructorCourses.Contains(course.CourseID)) Then
                        instructorToUpdate.Courses.Add(course)
                    End If
                Else
                    If (instructorCourses.Contains(course.CourseID)) Then
                        instructorToUpdate.Courses.Remove(course)
                    End If
                End If
            Next
        End Sub

        Private Sub PopulateAssignedCourseData(ByVal instructor As Instructor)
            Dim allCourses = db.Courses
            Dim instructorCourses = New HashSet(Of Integer)(instructor.Courses.Select(Function(c) c.CourseID))
            Dim viewModel = New List(Of AssignedCourseData)()
            For Each course In allCourses
                viewModel.Add(New AssignedCourseData With
                    {
                    .CourseID = course.CourseID,
                    .Title = course.Title,
                    .Assigned = instructorCourses.Contains(course.CourseID)
                    })
            Next
            ViewBag.Courses = viewModel
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
