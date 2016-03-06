Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Namespace Models
    Public Class OfficeAssignment
        <Key>
        <ForeignKey("Instructor")>
        Public Property InstructorID As Integer

        <StringLength(50)>
        <Display(Name:="Office Location")>
        Public Property Location As String

        Public Overridable Property Instructor As Instructor
    End Class
End Namespace
