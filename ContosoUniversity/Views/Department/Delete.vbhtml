@ModelType ContosoUniversity.Models.Department
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<p class="error">@ViewBag.ConcurrencyErrorMessage</p>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()
        @Html.HiddenFor(Function(model) model.DepartmentID)
        @Html.HiddenFor(Function(model) model.RowVersion)

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
