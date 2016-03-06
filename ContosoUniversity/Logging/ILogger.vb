Imports System

Namespace Logging
    Public Interface ILogger
        Sub Information(ByVal message As String)
        Sub Information(ByVal fmt As String, ByVal ParamArray vars As Object())
        Sub Information(ByVal exception As Exception, ByVal fmt As String, ByVal ParamArray vars As Object())

        Sub Warning(ByVal message As String)
        Sub Warning(ByVal fmt As String, ByVal ParamArray vars As Object())
        Sub Warning(ByVal exception As Exception, ByVal fmt As String, ByVal ParamArray vars As Object())

        Sub [Error](ByVal message As String)
        Sub [Error](ByVal fmt As String, ByVal ParamArray vars As Object())
        Sub [Error](ByVal exception As Exception, ByVal fmt As String, ByVal ParamArray vars As Object())

        Sub TraceApi(ByVal componentName As String, ByVal method As String, ByVal timeSpan As TimeSpan)
        Sub TraceApi(ByVal componentName As String, ByVal method As String, ByVal timeSpan As TimeSpan, ByVal properties As String)
        Sub TraceApi(ByVal componentName As String, ByVal method As String, ByVal timeSpan As TimeSpan, ByVal fmt As String, ByVal ParamArray vars As Object())
    End Interface
End Namespace
