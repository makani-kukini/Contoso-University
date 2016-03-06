Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Course",
                Function(c) New With
                    {
                        .CourseID = c.Int(nullable := False),
                        .Title = c.String(),
                        .Credits = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CourseID)
            
            CreateTable(
                "dbo.Enrollment",
                Function(c) New With
                    {
                        .EnrollmentID = c.Int(nullable := False, identity := True),
                        .CourseID = c.Int(nullable := False),
                        .StudentID = c.Int(nullable := False),
                        .Grade = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.EnrollmentID) _
                .ForeignKey("dbo.Course", Function(t) t.CourseID, cascadeDelete := True) _
                .ForeignKey("dbo.Student", Function(t) t.StudentID, cascadeDelete := True) _
                .Index(Function(t) t.CourseID) _
                .Index(Function(t) t.StudentID)
            
            CreateTable(
                "dbo.Student",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .LastName = c.String(),
                        .FirstMidName = c.String(),
                        .EnrollmentDate = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student")
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course")
            DropIndex("dbo.Enrollment", New String() { "StudentID" })
            DropIndex("dbo.Enrollment", New String() { "CourseID" })
            DropTable("dbo.Student")
            DropTable("dbo.Enrollment")
            DropTable("dbo.Course")
        End Sub
    End Class
End Namespace
