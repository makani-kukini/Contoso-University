@ModelType IEnumerable(Of ContosoUniversity.Models.Department)
@Code
    ViewData("Title") = "Departments"
End Code

<h2>Departments</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Budget)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.StartDate)
        </th>
        <th>
            Administrator
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Budget)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.StartDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Administrator.FullName)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.DepartmentID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.DepartmentID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.DepartmentID })
        </td>
    </tr>
Next

</table>
