@ModelType IEnumerable(Of ContosoUniversity.Models.Course)
@Code
    ViewData("Title") = "Courses"
End Code

<h2>Courses</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
@Using (Html.BeginForm())
    @<p>Select Department: @Html.DropDownList("SelectedDepartment", "All")
    <input type="submit" value="Filter" /></p>
End Using
<Table Class="table">
    <tr>
    <th>
            @Html.DisplayNameFor(Function(model) model.CourseID)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Title)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Credits)
        </th>
        <th>
            Department
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.CourseID)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Title)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Credits)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Department.Name)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.CourseID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.CourseID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.CourseID })
        </td>
    </tr>
Next

</table>
