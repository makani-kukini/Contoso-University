Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DepartmentRowVersion
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Department", "RowVersion", Function(c) c.Binary(nullable := False, fixedLength := true, timestamp := True, storeType := "rowversion"))
            AlterStoredProcedure(
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
                    "SELECT t0.[DepartmentID], t0.[RowVersion]" & vbCrLf & _
                    "FROM [dbo].[Department] AS t0" & vbCrLf & _
                    "WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            )
            
            AlterStoredProcedure(
                "dbo.Department_Update",
                Function(p) New With
                    {
                        .DepartmentID = p.Int(),
                        .Name = p.String(maxLength := 50),
                        .Budget = p.Decimal(precision := 19, scale := 4, storeType := "money"),
                        .StartDate = p.DateTime(),
                        .InstructorID = p.Int(),
                        .RowVersion_Original = p.Binary(maxLength := 8, fixedLength := true, storeType := "rowversion")
                    },
                body :=
                    "UPDATE [dbo].[Department]" & vbCrLf & _
                    "SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID" & vbCrLf & _
                    "WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))" & vbCrLf & _
                    "" & vbCrLf & _
                    "SELECT t0.[RowVersion]" & vbCrLf & _
                    "FROM [dbo].[Department] AS t0" & vbCrLf & _
                    "WHERE @@ROWCOUNT > 0 AND t0.[DepartmentID] = @DepartmentID"
            )
            
            AlterStoredProcedure(
                "dbo.Department_Delete",
                Function(p) New With
                    {
                        .DepartmentID = p.Int(),
                        .RowVersion_Original = p.Binary(maxLength := 8, fixedLength := true, storeType := "rowversion")
                    },
                body :=
                    "DELETE [dbo].[Department]" & vbCrLf & _
                    "WHERE (([DepartmentID] = @DepartmentID) AND (([RowVersion] = @RowVersion_Original) OR ([RowVersion] IS NULL AND @RowVersion_Original IS NULL)))"
            )
            
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Department", "RowVersion")
            Throw New NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.")
        End Sub
    End Class
End Namespace
