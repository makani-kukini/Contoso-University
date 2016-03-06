Imports ContosoUniversity.DAL
Imports ContosoUniversity.ViewModels
Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Private db As SchoolContext = New SchoolContext()
    Function Index() As ActionResult
        Return View()
    End Function

    Function About() As ActionResult
        'Commenting out LINQ to show how to do the same thing in SQL.
        'Dim data As IQueryable(Of EnrollmentDateGroup) = From student In db.Students
        '                                                 Group By EnrollmentDate = student.EnrollmentDate Into dateGroup = Group
        '                                                 Select New EnrollmentDateGroup() With {.EnrollmentDate = EnrollmentDate, .StudentCount = dateGroup.Count()}

        'SQL version of the above LINQ code.
        Dim query As String = "SELECT EnrollmentDate, COUNT(*) AS StudentCount " &
            "FROM Student " &
            "GROUP BY EnrollmentDate"
        Dim data As IEnumerable(Of EnrollmentDateGroup) = db.Database.SqlQuery(Of EnrollmentDateGroup)(query)
        Return View(data.ToList())
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        db.Dispose()
        MyBase.Dispose(disposing)
    End Sub
End Class
