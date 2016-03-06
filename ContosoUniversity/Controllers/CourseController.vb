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

Namespace Controllers
    Public Class CourseController
        Inherits System.Web.Mvc.Controller

        Private db As New SchoolContext

        ' GET: Course
        Function Index(ByVal selectedDepartment As Integer?) As ActionResult
            Dim departments = db.Departments.OrderBy(Function(q) q.Name).ToList()
            ViewBag.SelectedDepartment = New SelectList(departments, "DepartmentID", "Name", selectedDepartment)
            Dim departmentID As Integer = selectedDepartment.GetValueOrDefault()

            Dim courses As IQueryable(Of Course) = db.Courses.
                Where(Function(c) Not selectedDepartment.HasValue OrElse c.DepartmentID = departmentID).
                OrderBy(Function(d) d.CourseID).
                Include(Function(d) d.Department)

            Return View(courses.ToList())
        End Function

        ' GET: Course/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim course As Course = db.Courses.Find(id)
            If IsNothing(course) Then
                Return HttpNotFound()
            End If
            Return View(course)
        End Function

        ' GET: Course/Create
        Function Create() As ActionResult
            PopulateDepartmentsDropDownList()
            Return View()
        End Function

        ' POST: Course/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="CourseID,Title,Credits,DepartmentID")> ByVal course As Course) As ActionResult
            Try
                If ModelState.IsValid Then
                    db.Courses.Add(course)
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                End If
            Catch dex As RetryLimitExceededException
                'Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.")
            End Try
            PopulateDepartmentsDropDownList(course.DepartmentID)
            Return View(course)
        End Function

        ' GET: Course/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim course As Course = db.Courses.Find(id)
            If IsNothing(course) Then
                Return HttpNotFound()
            End If
            PopulateDepartmentsDropDownList(course.DepartmentID)
            Return View(course)
        End Function

        ' POST: Course/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost(), ActionName("Edit")>
        <ValidateAntiForgeryToken()>
        Function EditPost(ByVal id As Integer?) As ActionResult
            If (id Is Nothing) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim courseToUpdate As Course = db.Courses.Find(id)
            If (TryUpdateModel(courseToUpdate, "", New String() {"Title", "Credits", "DepartmentID"})) Then
                Try
                    db.SaveChanges()

                    Return RedirectToAction("Index")
                Catch dex As RetryLimitExceededException
                    'Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.")
                End Try
            End If
            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID)
            Return View(courseToUpdate)
        End Function

        ' GET: Course/UpdateCourseCredits
        Function UpdateCourseCredits() As ActionResult
            Return View()
        End Function

        ' POST: Course/UpdateCourseCredits
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function UpdateCourseCredits(ByVal multiplier As Integer?) As ActionResult
            If (multiplier IsNot Nothing) Then
                ViewBag.RowsAffected = db.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier)
            End If
            Return View()
        End Function

        ' GET: Course/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim course As Course = db.Courses.Find(id)
            If IsNothing(course) Then
                Return HttpNotFound()
            End If
            Return View(course)
        End Function

        ' POST: Course/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim course As Course = db.Courses.Find(id)
            db.Courses.Remove(course)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Private Sub PopulateDepartmentsDropDownList(ByVal Optional selectedDepartment As Object = Nothing)
            Dim departmentsQuery = From d In db.Departments
                                   Order By d.Name
                                   Select d
            ViewBag.DepartmentID = New SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment)
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
