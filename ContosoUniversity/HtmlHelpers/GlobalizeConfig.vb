Imports System.Runtime.CompilerServices
Imports System.IO

Namespace HtmlHelpers
    Public Module GlobalizeConfig
        <Extension()>
        Public Function GlobalizeConfig(ByVal helper As HtmlHelper) As MvcHtmlString
            Dim currUICulture As String = System.Globalization.CultureInfo.CurrentUICulture.Name
            Dim currDirMapping As String = If(currUICulture = "en-US", "en", currUICulture)
            If Not (Directory.Exists(HttpContext.Current.Server.MapPath("~/Scripts/cldr-data/main/" + currDirMapping + "/"))) Then
                currDirMapping = "en-CA"
            End If

            Dim tb As TagBuilder = New TagBuilder("script")
            Dim sb As StringBuilder = New StringBuilder()
            sb.AppendLine()
            sb.AppendLine("$.when(")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/supplemental/likelySubtags.json""),")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/main/" + currDirMapping + "/numbers.json""),")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/supplemental/numberingSystems.json""),")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/main/" + currDirMapping + "/ca-gregorian.json""),")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/main/" + currDirMapping + "/timeZoneNames.json""),")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/supplemental/timeData.json""),")
            sb.AppendLine(vbTab & "$.get(""/Scripts/cldr-data/supplemental/weekData.json"")")
            sb.AppendLine(").then(function () {")
            sb.AppendLine(vbTab & "// Normalize $.get results, we only need the JSON, Not the request statuses.")
            sb.AppendLine(vbTab & "return [].slice.apply(arguments, [0]).map(function (result) {")
            sb.AppendLine(vbTab & vbTab & "return result[0];")
            sb.AppendLine(vbTab & "});")
            sb.AppendLine("}).then(Globalize.load).then(function () {")
            sb.AppendLine(vbTab & "// Initialise Globalize to the current UI culture")
            sb.AppendLine(vbTab & "Globalize.locale(""" + currDirMapping + """);")
            sb.AppendLine("})")

            tb.InnerHtml = sb.ToString()

            Return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal))
        End Function
    End Module
End Namespace
