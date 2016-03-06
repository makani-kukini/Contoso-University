Imports ContosoUniversity.Models
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions

Namespace DAL
    Public Class SchoolContext
        Inherits DbContext

        Public Sub New()
            MyBase.New("SchoolContext")
        End Sub

        Public Property Courses As DbSet(Of Course)
        Public Property Departments As DbSet(Of Department)
        Public Property Enrollments As DbSet(Of Enrollment)
        Public Property Instructors As DbSet(Of Instructor)
        Public Property Students As DbSet(Of Student)
        Public Property OfficeAssignments As DbSet(Of OfficeAssignment)

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()

            modelBuilder.Entity(Of Course)().
                HasMany(Function(c) c.Instructors).
                WithMany(Function(i) i.Courses).
                Map(Function(t) t.MapLeftKey("CourseID").
                    MapRightKey("InstructorID").
                    ToTable("CourseInstructor"))
            modelBuilder.Entity(Of Department)().MapToStoredProcedures()
        End Sub
    End Class
End Namespace