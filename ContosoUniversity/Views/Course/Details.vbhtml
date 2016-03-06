@ModelType ContosoUniversity.Models.Course
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Course</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            Department
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Department.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Title)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Title)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Credits)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Credits)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.CourseID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
