Imports System.Web.Optimization
Public Class AsIsBundleOrderer
    Implements IBundleOrderer

    Public Function OrderFiles(context As BundleContext, files As IEnumerable(Of BundleFile)) As IEnumerable(Of BundleFile) Implements IBundleOrderer.OrderFiles
        Return files
    End Function
End Class
