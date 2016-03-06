Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Course
        <DatabaseGenerated(DatabaseGeneratedOption.None)>
        <Display(Name:="Number")>
        Public Property CourseID As Integer

        <StringLength(50, MinimumLength:=3)>
        Public Property Title As String

        <Range(0, 5)>
        Public Property Credits As Integer

        Public Property DepartmentID As Integer

        Public Overridable Property Department As Department
        Public Overridable Property Enrollments As ICollection(Of Enrollment)
        Public Overridable Property Instructors As ICollection(Of Instructor)
    End Class
End Namespace