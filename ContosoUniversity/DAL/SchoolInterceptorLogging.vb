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
    Public Class SchoolInterceptorLogging
        Inherits DbCommandInterceptor

        Private _logger As ILogger = New Logger()
        Private ReadOnly _stopwatch As Stopwatch = New Stopwatch()

        Public Overrides Sub ScalarExecuting(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of Object))
            MyBase.ScalarExecuting(command, interceptionContext)
            _stopwatch.Restart()
        End Sub

        Public Overrides Sub ScalarExecuted(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of Object))
            _stopwatch.Stop()
            If (interceptionContext.Exception IsNot Nothing) Then
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText)
            Else
                _logger.TraceApi("SQL Database", "SchoolInterceptor.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText)
            End If

            MyBase.ScalarExecuted(command, interceptionContext)
        End Sub

        Public Overrides Sub NonQueryExecuting(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of Integer))
            MyBase.NonQueryExecuting(command, interceptionContext)
            _stopwatch.Restart()
        End Sub

        Public Overrides Sub NonQueryExecuted(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of Integer))
            _stopwatch.Stop()
            If (interceptionContext.Exception IsNot Nothing) Then
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText)
            Else
                _logger.TraceApi("SQL Database", "SchoolInterceptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText)
            End If

            MyBase.NonQueryExecuted(command, interceptionContext)
        End Sub

        Public Overrides Sub ReaderExecuting(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of DbDataReader))
            MyBase.ReaderExecuting(command, interceptionContext)
            _stopwatch.Restart()
        End Sub

        Public Overrides Sub ReaderExecuted(command As DbCommand, interceptionContext As DbCommandInterceptionContext(Of DbDataReader))
            _stopwatch.Stop()
            If (interceptionContext.Exception IsNot Nothing) Then
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText)
            Else
                _logger.TraceApi("SQL Database", "SchoolInterceptor.ReaderExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText)
            End If

            MyBase.ReaderExecuted(command, interceptionContext)
        End Sub
    End Class
End Namespace
