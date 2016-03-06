Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ComplexDataModel
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Department",
                Function(c) New With
                    {
                        .DepartmentID = c.Int(nullable := False, identity := True),
                        .Name = c.String(maxLength := 50),
                        .Budget = c.Decimal(nullable := False, storeType := "money"),
                        .StartDate = c.DateTime(nullable := False),
                        .InstructorID = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.DepartmentID) _
                .ForeignKey("dbo.Instructor", Function(t) t.InstructorID) _
                .Index(Function(t) t.InstructorID)
            
            CreateTable(
                "dbo.Instructor",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .LastName = c.String(nullable := False, maxLength := 50),
                        .FirstName = c.String(nullable := False, maxLength := 50),
                        .HireDate = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID)
            
            CreateTable(
                "dbo.OfficeAssignment",
                Function(c) New With
                    {
                        .InstructorID = c.Int(nullable := False),
                        .Location = c.String(maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.InstructorID) _
                .ForeignKey("dbo.Instructor", Function(t) t.InstructorID) _
                .Index(Function(t) t.InstructorID)
            
            CreateTable(
                "dbo.CourseInstructor",
                Function(c) New With
                    {
                        .CourseID = c.Int(nullable := False),
                        .InstructorID = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.CourseID, t.InstructorID }) _
                .ForeignKey("dbo.Course", Function(t) t.CourseID, cascadeDelete := True) _
                .ForeignKey("dbo.Instructor", Function(t) t.InstructorID, cascadeDelete := True) _
                .Index(Function(t) t.CourseID) _
                .Index(Function(t) t.InstructorID)

            'Create a department for the previous data in the dbo.Course table to point to
            'Sql("INSERT INTO dbo.Department (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())")
            'The defaultValue for the FK points to the department created above
            'AddColumn("dbo.course", "DepartmentID", Function(c) c.Int(nullable:=False, defaultValue:=1))
            AddColumn("dbo.Course", "DepartmentID", Function(c) c.Int(nullable:=False))
            AlterColumn("dbo.Course", "Title", Function(c) c.String(maxLength := 50))
            AlterColumn("dbo.Student", "LastName", Function(c) c.String(nullable := False, maxLength := 50))
            AlterColumn("dbo.Student", "FirstName", Function(c) c.String(nullable := False, maxLength := 50))
            CreateIndex("dbo.Course", "DepartmentID")
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor")
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course")
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department")
            DropForeignKey("dbo.Department", "InstructorID", "dbo.Instructor")
            DropForeignKey("dbo.OfficeAssignment", "InstructorID", "dbo.Instructor")
            DropIndex("dbo.CourseInstructor", New String() { "InstructorID" })
            DropIndex("dbo.CourseInstructor", New String() { "CourseID" })
            DropIndex("dbo.OfficeAssignment", New String() { "InstructorID" })
            DropIndex("dbo.Department", New String() { "InstructorID" })
            DropIndex("dbo.Course", New String() { "DepartmentID" })
            AlterColumn("dbo.Student", "FirstName", Function(c) c.String(maxLength := 50))
            AlterColumn("dbo.Student", "LastName", Function(c) c.String(maxLength := 50))
            AlterColumn("dbo.Course", "Title", Function(c) c.String())
            DropColumn("dbo.Course", "DepartmentID")
            DropTable("dbo.CourseInstructor")
            DropTable("dbo.OfficeAssignment")
            DropTable("dbo.Instructor")
            DropTable("dbo.Department")
        End Sub
    End Class
End Namespace
