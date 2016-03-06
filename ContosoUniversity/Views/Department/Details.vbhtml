@ModelType ContosoUniversity.Models.Department
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Department</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            Administrator
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Administrator.FullName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Budget)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Budget)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.StartDate)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.DepartmentID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
