Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports ContosoUniversity.Models
Imports ContosoUniversity.DAL

Namespace Migrations

    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of SchoolContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
            ContextKey = "ContosoUniversity.DAL.SchoolContext"
        End Sub

        Protected Overrides Sub Seed(context As ContosoUniversity.DAL.SchoolContext)
            Dim students = New List(Of Student) From {
                New Student() With {.FirstMidName = "Carson", .LastName = "Alexander", .EnrollmentDate = DateTime.Parse("2010-09-01")},
                New Student() With {.FirstMidName = "Meredith", .LastName = "Alonso", .EnrollmentDate = DateTime.Parse("2012-09-01")},
                New Student() With {.FirstMidName = "Arturo", .LastName = "Anand", .EnrollmentDate = DateTime.Parse("2013-09-01")},
                New Student() With {.FirstMidName = "Gytis", .LastName = "Barzdukas", .EnrollmentDate = DateTime.Parse("2012-09-01")},
                New Student() With {.FirstMidName = "Yan", .LastName = "Li", .EnrollmentDate = DateTime.Parse("2012-09-01")},
                New Student() With {.FirstMidName = "Peggy", .LastName = "Justice", .EnrollmentDate = DateTime.Parse("2011-09-01")},
                New Student() With {.FirstMidName = "Laura", .LastName = "Norman", .EnrollmentDate = DateTime.Parse("2013-09-01")},
                New Student() With {.FirstMidName = "Nino", .LastName = "Olivetto", .EnrollmentDate = DateTime.Parse("2005-09.01")}
            }

            students.ForEach(Sub(s) context.Students.AddOrUpdate(Function(p) p.LastName, s))
            context.SaveChanges()

            Dim instructors = New List(Of Instructor) From {
                New Instructor() With {.FirstMidName = "Kim", .LastName = "Abercrombie", .HireDate = DateTime.Parse("1995-03-11")},
                New Instructor() With {.FirstMidName = "Fadi", .LastName = "Fakhouri", .HireDate = DateTime.Parse("2002-07-06")},
                New Instructor() With {.FirstMidName = "Roger", .LastName = "Harui", .HireDate = DateTime.Parse("1998-07-01")},
                New Instructor() With {.FirstMidName = "Candace", .LastName = "Kapoor", .HireDate = DateTime.Parse("2001-01-15")},
                New Instructor() With {.FirstMidName = "Roger", .LastName = "Zheng", .HireDate = DateTime.Parse("2004-02-12")}
            }

            instructors.ForEach(Sub(s) context.Instructors.AddOrUpdate(Function(p) p.LastName, s))
            context.SaveChanges()

            Dim departments = New List(Of Department) From {
                New Department() With {.Name = "English", .Budget = 350000, .StartDate = DateTime.Parse("2007-09-01"), .InstructorID = instructors.Single(Function(i) i.LastName = "Abercrombie").ID},
                New Department() With {.Name = "Mathematics", .Budget = 100000, .StartDate = DateTime.Parse("2007-09-01"), .InstructorID = instructors.Single(Function(i) i.LastName = "Fakhouri").ID},
                New Department() With {.Name = "Engineering", .Budget = 350000, .StartDate = DateTime.Parse("2007-09-01"), .InstructorID = instructors.Single(Function(i) i.LastName = "Harui").ID},
                New Department() With {.Name = "Economics", .Budget = 100000, .StartDate = DateTime.Parse("2007-09-01"), .InstructorID = instructors.Single(Function(i) i.LastName = "Kapoor").ID}
            }

            departments.ForEach(Sub(s) context.Departments.AddOrUpdate(Function(p) p.Name, s))
            context.SaveChanges()

            Dim courses = New List(Of Course) From {
                New Course() With {.CourseID = 1050, .Title = "Chemistry", .Credits = 3, .DepartmentID = departments.Single(Function(s) s.Name = "Engineering").DepartmentID, .Instructors = New List(Of Instructor)()},
                New Course() With {.CourseID = 4022, .Title = "Microeconomics", .Credits = 3, .DepartmentID = departments.Single(Function(s) s.Name = "Economics").DepartmentID, .Instructors = New List(Of Instructor)()},
                New Course() With {.CourseID = 4041, .Title = "Macroeconomics", .Credits = 3, .DepartmentID = departments.Single(Function(s) s.Name = "Economics").DepartmentID, .Instructors = New List(Of Instructor)()},
                New Course() With {.CourseID = 1045, .Title = "Calculus", .Credits = 4, .DepartmentID = departments.Single(Function(s) s.Name = "Mathematics").DepartmentID, .Instructors = New List(Of Instructor)()},
                New Course() With {.CourseID = 3141, .Title = "Trigonometry", .Credits = 4, .DepartmentID = departments.Single(Function(s) s.Name = "Mathematics").DepartmentID, .Instructors = New List(Of Instructor)()},
                New Course() With {.CourseID = 2021, .Title = "Composition", .Credits = 3, .DepartmentID = departments.Single(Function(s) s.Name = "English").DepartmentID, .Instructors = New List(Of Instructor)()},
                New Course() With {.CourseID = 2042, .Title = "Literature", .Credits = 4, .DepartmentID = departments.Single(Function(s) s.Name = "English").DepartmentID, .Instructors = New List(Of Instructor)()}
            }

            courses.ForEach(Sub(c) context.Courses.AddOrUpdate(Function(p) p.CourseID, c))
            context.SaveChanges()

            Dim officeAssignments = New List(Of OfficeAssignment) From {
                New OfficeAssignment() With {.InstructorID = instructors.Single(Function(i) i.LastName = "Fakhouri").ID, .Location = "Smith 17"},
                New OfficeAssignment() With {.InstructorID = instructors.Single(Function(i) i.LastName = "Harui").ID, .Location = "Gowan 27"},
                New OfficeAssignment() With {.InstructorID = instructors.Single(Function(i) i.LastName = "Kapoor").ID, .Location = "Thompson 304"}
            }

            officeAssignments.ForEach(Sub(s) context.OfficeAssignments.AddOrUpdate(Function(p) p.InstructorID, s))
            context.SaveChanges()

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor")
            AddOrUpdateInstructor(context, "Chemistry", "Harui")
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng")
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng")

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri")
            AddOrUpdateInstructor(context, "Trigonometry", "Harui")
            AddOrUpdateInstructor(context, "Composition", "Abercrombie")
            AddOrUpdateInstructor(context, "Literature", "Abercrombie")

            context.SaveChanges()

            Dim enrollments As New List(Of Enrollment) From {
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Alexander").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Chemistry").CourseID,
                    .Grade = Grade.A
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Alexander").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Microeconomics").CourseID,
                    .Grade = Grade.C
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Alexander").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Macroeconomics").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Alonso").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Calculus").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Alonso").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Trigonometry").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Alonso").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Composition").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Anand").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Chemistry").CourseID
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Anand").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Microeconomics").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Barzdukas").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Chemistry").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Li").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Composition").CourseID,
                    .Grade = Grade.B
                },
                New Enrollment() With {
                    .StudentID = students.Single(Function(s) s.LastName = "Justice").ID,
                    .CourseID = courses.Single(Function(c) c.Title = "Literature").CourseID,
                    .Grade = Grade.B
                }
            }

            For Each e As Enrollment In enrollments
                Dim enrollmentInDatabase = context.Enrollments.Where(
                    Function(s) s.StudentID = e.StudentID AndAlso
                                s.Course.CourseID = e.CourseID).SingleOrDefault()

                If (enrollmentInDatabase Is Nothing) Then
                    context.Enrollments.Add(e)
                End If

            Next
            context.SaveChanges()
        End Sub

        Sub AddOrUpdateInstructor(ByVal context As SchoolContext, ByVal courseTitle As String, ByVal instructorName As String)
            Dim crs = context.Courses.SingleOrDefault(Function(c) c.Title = courseTitle)
            Dim inst = crs.Instructors.SingleOrDefault(Function(i) i.LastName = instructorName)
            If (inst Is Nothing) Then
                crs.Instructors.Add(context.Instructors.Single(Function(i) i.LastName = instructorName))
            End If
        End Sub

    End Class

End Namespace
