@ModelType ContosoUniversity.Models.Course
@Code
    ViewData("Title") = "UpdateCourseCredits"
End Code

<h2>UpdateCourseCredits</h2>

@If (ViewBag.RowsAffected Is Nothing) Then
    Using (Html.BeginForm())
        @Html.AntiForgeryToken()
        @<p>
            Enter a number to multiply every course's credits by: @Html.TextBox("multiplier")
        </p>
        @<p>
            <input type="submit" value="Update" />
        </p>
    End Using
Else
    @<p>
        Number of rows updated: @ViewBag.RowsAffected
    </p>
End If
<div>
    @Html.ActionLink("Back to List", "Index")
</div>