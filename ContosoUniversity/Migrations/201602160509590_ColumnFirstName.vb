Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ColumnFirstName
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameColumn(table := "dbo.Student", name := "FirstMidName", newName := "FirstName")
        End Sub
        
        Public Overrides Sub Down()
            RenameColumn(table := "dbo.Student", name := "FirstName", newName := "FirstMidName")
        End Sub
    End Class
End Namespace
