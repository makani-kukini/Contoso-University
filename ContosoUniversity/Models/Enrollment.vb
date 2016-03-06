Imports System.ComponentModel.DataAnnotations
Namespace Models
    Public Enum Grade
        A
        B
        C
        D
        E
        F
    End Enum

    Public Class Enrollment
        Public Property EnrollmentID As Integer

        Public Property CourseID As Integer

        Public Property StudentID As Integer

        <DisplayFormat(NullDisplayText:="No grade")>
        Public Property Grade As Grade?

        Public Overridable Property Course As Course
        Public Overridable Property Student As Student
    End Class
End Namespace