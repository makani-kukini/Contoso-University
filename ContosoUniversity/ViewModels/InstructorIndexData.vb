Imports System.Collections.Generic
Imports ContosoUniversity.Models

Namespace ViewModels
    Public Class InstructorIndexData
        Public Property Instructors As IEnumerable(Of Instructor)
        Public Property Courses As IEnumerable(Of Course)
        Public Property Enrollments As IEnumerable(Of Enrollment)
    End Class
End Namespace