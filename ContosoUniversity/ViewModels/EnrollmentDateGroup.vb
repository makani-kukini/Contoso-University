Imports System
Imports System.ComponentModel.DataAnnotations

Namespace ViewModels
    Public Class EnrollmentDateGroup
        <DataType(DataType.Date)>
        Public Property EnrollmentDate As DateTime?
        Public Property StudentCount As Integer
    End Class
End Namespace
