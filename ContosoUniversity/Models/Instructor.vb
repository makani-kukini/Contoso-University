Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class Instructor
        Public Property ID As Integer

        <Required>
        <Display(Name:="Last Name")>
        <StringLength(50)>
        Public Property LastName As String

        <Required>
        <Column("FirstName")>
        <Display(Name:="First Name")>
        <StringLength(50)>
        Public Property FirstMidName As String

        <DataType(DataType.Date)>
        <Display(Name:="Hire Date")>
        Public Property HireDate As DateTime

        <Display(Name:="Full Name")>
        Public ReadOnly Property FullName As String
            Get
                Return LastName & ", " & FirstMidName
            End Get
        End Property

        Public Overridable Property Courses As ICollection(Of Course)
        Public Overridable Property OfficeAssignment As OfficeAssignment
    End Class
End Namespace
