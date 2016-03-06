Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive*"))

        Dim bundle As ScriptBundle = New ScriptBundle("~/bundles/globalization").Include(
                    "~/Scripts/cldr.js",
                    "~/Scripts/cldr/event.js",
                    "~/Scripts/cldr/supplemental.js",
                    "~/Scripts/globalize.js",
                    "~/Scripts/globalize/number.js",
                    "~/Scripts/globalize/date.js")
        bundle.Orderer = New AsIsBundleOrderer()
        bundles.Add(bundle)
        bundles.Add(New ScriptBundle("~/bundles/globalization.validate").Include(
                    "~/Scripts/jquery.validate.globalize*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/respond.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css"))
    End Sub
End Module

