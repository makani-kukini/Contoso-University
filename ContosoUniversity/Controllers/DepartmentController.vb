Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ContosoUniversity.DAL
Imports ContosoUniversity.Models

Namespace Controllers
    Public Class DepartmentController
        Inherits System.Web.Mvc.Controller

        Private db As New SchoolContext

        ' GET: Department
        Async Function Index() As Task(Of ActionResult)
            Dim departments = db.Departments.Include(Function(d) d.Administrator)
            Return View(Await departments.ToListAsync())
        End Function

        ' GET: Department/Details/5
        Async Function Details(ByVal id As Integer?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            'Commenting out the original code to show how to use a raw SQL query.
            'Dim department As Department = Await db.Departments.FindAsync(id)

            'Create and execute raw SQL query.
            Dim query As String = "SELECT * FROM Department WHERE DepartmentID = @p0"
            Dim department As Department = Await db.Departments.SqlQuery(query, id).SingleOrDefaultAsync()

            If IsNothing(department) Then
                Return HttpNotFound()
            End If
            Return View(department)
        End Function

        ' GET: Department/Create
        Function Create() As ActionResult
            ViewBag.InstructorID = New SelectList(db.Instructors, "ID", "FullName")
            Return View()
        End Function

        ' POST: Department/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Create(<Bind(Include:="DepartmentID,Name,Budget,StartDate,InstructorID")> ByVal department As Department) As Task(Of ActionResult)
            If ModelState.IsValid Then
                db.Departments.Add(department)
                Await db.SaveChangesAsync()
                Return RedirectToAction("Index")
            End If
            ViewBag.InstructorID = New SelectList(db.Instructors, "ID", "FullName", department.InstructorID)
            Return View(department)
        End Function

        ' GET: Department/Edit/5
        Async Function Edit(ByVal id As Integer?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim department As Department = Await db.Departments.FindAsync(id)
            If IsNothing(department) Then
                Return HttpNotFound()
            End If
            ViewBag.InstructorID = New SelectList(db.Instructors, "ID", "FullName", department.InstructorID)
            Return View(department)
        End Function

        ' POST: Department/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Edit(ByVal id As Integer?, ByVal rowVersion As Byte()) As Task(Of ActionResult)
            Dim fieldsToBind As String() = New String() {"Name", "Budget", "StartDate", "InstructorID", "RowVersion"}

            If (id Is Nothing) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim departmentToUpdate = Await db.Departments.FindAsync(id)
            If (departmentToUpdate Is Nothing) Then
                Dim deletedDepartment As Department = New Department()
                TryUpdateModel(deletedDepartment, fieldsToBind)
                ModelState.AddModelError(String.Empty, "Unable to save changes. The department was deleted by another user.")
                ViewBag.InstructorID = New SelectList(db.Instructors, "ID", "FullName", deletedDepartment.InstructorID)
                Return View(deletedDepartment)
            End If

            If (TryUpdateModel(departmentToUpdate, fieldsToBind)) Then
                Try
                    db.Entry(departmentToUpdate).OriginalValues("RowVersion") = rowVersion
                    Await db.SaveChangesAsync()

                    Return RedirectToAction("Index")
                Catch ex As DbUpdateConcurrencyException
                    Dim entry = ex.Entries.Single()
                    Dim clientValues = DirectCast(entry.Entity, Department)
                    Dim databaseEntry = entry.GetDatabaseValues()
                    If (databaseEntry Is Nothing) Then
                        ModelState.AddModelError(String.Empty, "Unable to save changes. The department was deleted by another user.")
                    Else
                        Dim databaseValues = DirectCast(databaseEntry.ToObject(), Department)

                        If (databaseValues.Name <> clientValues.Name) Then
                            ModelState.AddModelError("Name", "Current value: " & databaseValues.Name)
                        End If
                        If (databaseValues.Budget <> clientValues.Budget) Then
                            ModelState.AddModelError("Budget", "Current value: " & String.Format("{0:c}", databaseValues.Budget))
                        End If
                        If (databaseValues.StartDate <> clientValues.StartDate) Then
                            ModelState.AddModelError("StartDate", "Current value: " & String.Format("{0:d}", databaseValues.StartDate))
                        End If
                        If (databaseValues.InstructorID <> clientValues.InstructorID) Then
                            ModelState.AddModelError("InstructorID", "Current value: " & db.Instructors.Find(databaseValues.InstructorID).FullName)
                        End If
                        ModelState.AddModelError(String.Empty, "The record you attempted to edit " &
                            "was modified by another user after you got the original value. The " &
                            "edit operation was cancelled and the current values in the database " &
                            "have been displayed. If you still want to edit this record, click " &
                            "the Save button again. Otherwise click the Back to List hyperlink.")
                        departmentToUpdate.RowVersion = databaseValues.RowVersion
                    End If
                Catch dex As RetryLimitExceededException
                    'Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see you system administrator.")
                End Try
            End If

            ViewBag.InstructorID = New SelectList(db.Instructors, "ID", "FullName", departmentToUpdate.InstructorID)
            Return View(departmentToUpdate)
        End Function

        ' GET: Department/Delete/5
        Async Function Delete(ByVal id As Integer?, ByVal concurrencyError As Boolean?) As Task(Of ActionResult)
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim department As Department = Await db.Departments.FindAsync(id)
            If IsNothing(department) Then
                If (concurrencyError.GetValueOrDefault()) Then
                    Return RedirectToAction("Index")
                End If
                Return HttpNotFound()
            End If

            If (concurrencyError.GetValueOrDefault()) Then
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete " &
                    "was modified by another user after you got the original values. " &
                    "The delete operation was cancelled and the current values in the " &
                    "database have been displayed. If you still want to delete this " &
                    "record, click the Delete button again. Otherwise " &
                    "click the Back to List hyperlink."
            End If

            Return View(department)
        End Function

        ' POST: Department/Delete/5
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Async Function Delete(ByVal department As Department) As Task(Of ActionResult)
            Try
                db.Entry(department).State = EntityState.Deleted
                Await db.SaveChangesAsync()
                Return RedirectToAction("Index")
            Catch ex As DbUpdateConcurrencyException
                Return RedirectToAction("Delete", New With {.concurrencyError = True, .id = department.DepartmentID})
            Catch dex As DataException
                'Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(String.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.")
                Return View(department)
            End Try
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
