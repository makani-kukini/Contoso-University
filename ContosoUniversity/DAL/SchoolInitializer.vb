Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.Entity
Imports ContosoUniversity.Models

Namespace DAL
    Public Class SchoolInitializer
        Inherits System.Data.Entity.DropCreateDatabaseIfModelChanges(Of SchoolContext)

        Protected Overrides Sub Seed(context As SchoolContext)
            Dim students = New List(Of Student) From {
                New Student() With {.FirstMidName = "Carson", .LastName = "Alexander", .EnrollmentDate = DateTime.Parse("2005-09-01")},
                New Student() With {.FirstMidName = "Meredith", .LastName = "Alonso", .EnrollmentDate = DateTime.Parse("2002-09-01")},
                New Student() With {.FirstMidName = "Arturo", .LastName = "Anand", .EnrollmentDate = DateTime.Parse("2003-09-01")},
                New Student() With {.FirstMidName = "Gytis", .LastName = "Barzdukas", .EnrollmentDate = DateTime.Parse("2002-09-01")},
                New Student() With {.FirstMidName = "Yan", .LastName = "Li", .EnrollmentDate = DateTime.Parse("2002-09-01")},
                New Student() With {.FirstMidName = "Peggy", .LastName = "Justice", .EnrollmentDate = DateTime.Parse("2001-09-01")},
                New Student() With {.FirstMidName = "Laura", .LastName = "Norman", .EnrollmentDate = DateTime.Parse("2003-09-01")},
                New Student() With {.FirstMidName = "Nino", .LastName = "Olivetto", .EnrollmentDate = DateTime.Parse("2005-09.01")}
            }

            students.ForEach(Sub(s) context.Students.Add(s))
            context.SaveChanges()

            Dim courses = New List(Of Course) From {
                New Course() With {.CourseID = 1050, .Title = "Chemistry", .Credits = 3},
                New Course() With {.CourseID = 4022, .Title = "Microeconomics", .Credits = 3},
                New Course() With {.CourseID = 4041, .Title = "Macroeconomics", .Credits = 3},
                New Course() With {.CourseID = 1045, .Title = "Calculus", .Credits = 4},
                New Course() With {.CourseID = 3141, .Title = "Trigonometry", .Credits = 4},
                New Course() With {.CourseID = 2021, .Title = "Composition", .Credits = 3},
                New Course() With {.CourseID = 2042, .Title = "Literature", .Credits = 4}
            }

            courses.ForEach(Sub(c) context.Courses.Add(c))
            context.SaveChanges()

            Dim enrollments = New List(Of Enrollment) From {
                New Enrollment() With {.StudentID = 1, .CourseID = 1050, .Grade = Grade.A},
                New Enrollment() With {.StudentID = 1, .CourseID = 4022, .Grade = Grade.C},
                New Enrollment() With {.StudentID = 1, .CourseID = 4041, .Grade = Grade.B},
                New Enrollment() With {.StudentID = 2, .CourseID = 1045, .Grade = Grade.B},
                New Enrollment() With {.StudentID = 2, .CourseID = 3141, .Grade = Grade.F},
                New Enrollment() With {.StudentID = 2, .CourseID = 2021, .Grade = Grade.F},
                New Enrollment() With {.StudentID = 3, .CourseID = 1050},
                New Enrollment() With {.StudentID = 4, .CourseID = 1050},
                New Enrollment() With {.StudentID = 4, .CourseID = 4022, .Grade = Grade.F},
                New Enrollment() With {.StudentID = 5, .CourseID = 4041, .Grade = Grade.C},
                New Enrollment() With {.StudentID = 6, .CourseID = 1045},
                New Enrollment() With {.StudentID = 7, .CourseID = 3141, .Grade = Grade.A}
            }

            enrollments.ForEach(Sub(e) context.Enrollments.Add(e))
            context.SaveChanges()
        End Sub
    End Class
End Namespace