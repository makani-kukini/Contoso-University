Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class MaxLengthOnNames
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Student", "LastName", Function(c) c.String(maxLength := 50))
            AlterColumn("dbo.Student", "FirstMidName", Function(c) c.String(maxLength := 50))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Student", "FirstMidName", Function(c) c.String())
            AlterColumn("dbo.Student", "LastName", Function(c) c.String())
        End Sub
    End Class
End Namespace
