Imports System
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure.Interception
Imports System.Data.Entity.SqlServer
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Reflection
Imports System.Linq
Imports ContosoUniversity.Logging

Namespace DAL
    Public Class SchoolInterceptorTransientErrors
        Inherits DbCommandInterceptor

        Private _counter As Integer = 0
        Private _logger As ILogger = New Logger()

        Public Overrides Sub ReaderExecuting(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of DbDataReader))
            Dim throwTransientErrors As Boolean = False

            If (command.Parameters.Count() > 0 AndAlso command.Parameters(0).Value.ToString() = "%Throw%") Then
                throwTransientErrors = True
                command.Parameters(0).Value = "%an%"
                command.Parameters(1).Value = "%an%"
            End If

            If (throwTransientErrors AndAlso _counter < 4) Then
                _logger.Information("Returning transient error for command: {0}", command.CommandText)
                _counter += 1
                interceptionContext.Exception = CreateDummySqlException()
            End If
        End Sub

        Private Function CreateDummySqlException() As Exception
            'The instance of SQL Server you attempted to connect to does Not support encryption
            Dim sqlErrorNumber = 20

            Dim sqlErrorCtor = GetType(SqlError).GetConstructors(BindingFlags.Instance Or BindingFlags.NonPublic).Where(Function(c) c.GetParameters().Count() = 7).Single()
            Dim sqlError = sqlErrorCtor.Invoke(New Object() {sqlErrorNumber, CType(0, Byte), CType(0, Byte), "", "", "", 1})

            Dim errorCollection = Activator.CreateInstance(GetType(SqlErrorCollection), True)
            Dim addMethod = GetType(SqlErrorCollection).GetMethod("Add", BindingFlags.Instance Or BindingFlags.NonPublic)
            addMethod.Invoke(errorCollection, {sqlError})

            Dim sqlExceptionCtor = GetType(SqlException).GetConstructors(BindingFlags.Instance Or BindingFlags.NonPublic).Where(Function(c) c.GetParameters.Count() = 4).Single()
            Dim sqlException = sqlExceptionCtor.Invoke(New Object() {"Dummy", errorCollection, Nothing, Guid.NewGuid()})

            Return sqlException
        End Function
    End Class
End Namespace