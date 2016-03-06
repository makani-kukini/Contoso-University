Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Student
        Public Property ID As Integer

        <Required>
        <StringLength(50)>
        <Display(Name:="Last Name")>
        Public Property LastName As String

        <Required>
        <StringLength(50, ErrorMessage:="First name cannot be longer than 50 characters.")>
        <Column("FirstName")>
        <Display(Name:="First Name")>
        Public Property FirstMidName As String

        <DataType(DataType.Date)>
        <Display(Name:="Enrollment Date")>
        Public Property EnrollmentDate As DateTime

        <Display(Name:="Full Name")>
        Public ReadOnly Property FullName As String
            Get
                Return LastName & ", " & FirstMidName
            End Get
        End Property

        Public Overridable Property Enrollments As ICollection(Of Enrollment)
    End Class
End Namespace
