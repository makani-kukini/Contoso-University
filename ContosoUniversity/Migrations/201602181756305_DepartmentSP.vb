Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DepartmentSP
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateStoredProcedure(
                "dbo.Department_Insert",
                Function(p) New With
                    {
                        .Name = p.String(maxLength := 50),
                        .Budget = p.Decimal(precision := 19, scale := 4, storeType := "money"),
                        .StartDate = p.DateTime(),
                        .InstructorID = p.Int()
                    },
                body :=
                    "INSERT [dbo].[Department]([Name], [Budget], [StartDate], [InstructorID])" & vbCrLf & _
                    "VALUES (@Name, @Budget, @StartDate, @InstructorID)" & vbCrLf & _
                    "" & vbCrLf & _
                    "DECLARE @DepartmentID int" & vbCrLf & _
                    "SELECT @DepartmentID = [DepartmentID]" & vbCrLf & _
                    "FROM [dbo].[Department]" & vbCrLf & _
                    "WHERE @@ROWCOUNT > 0 AND [DepartmentID] = scope_identity()" & vbCrLf & _
                    "" & vbCrLf & _
                    "SELECT t0.[DepartmentID]" & vbCrLf & _
                    "FROM [dbo].[Department] AS t0" & vbCrLf & _
                    "WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            )
            
            CreateStoredProcedure(
                "dbo.Department_Update",
                Function(p) New With
                    {
                        .DepartmentID = p.Int(),
                        .Name = p.String(maxLength := 50),
                        .Budget = p.Decimal(precision := 19, scale := 4, storeType := "money"),
                        .StartDate = p.DateTime(),
                        .InstructorID = p.Int()
                    },
                body :=
                    "UPDATE [dbo].[Department]" & vbCrLf & _
                    "SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID" & vbCrLf & _
                    "WHERE ([DepartmentID] = @DepartmentID)"
            )
            
            CreateStoredProcedure(
                "dbo.Department_Delete",
                Function(p) New With
                    {
                        .DepartmentID = p.Int()
                    },
                body :=
                    "DELETE [dbo].[Department]" & vbCrLf & _
                    "WHERE ([DepartmentID] = @DepartmentID)"
            )
            
        End Sub
        
        Public Overrides Sub Down()
            DropStoredProcedure("dbo.Department_Delete")
            DropStoredProcedure("dbo.Department_Update")
            DropStoredProcedure("dbo.Department_Insert")
        End Sub
    End Class
End Namespace
