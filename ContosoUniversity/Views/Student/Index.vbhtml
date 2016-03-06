@ModelType PagedList.IPagedList(Of ContosoUniversity.Models.Student)
@Imports PagedList.Mvc
<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />

@Code
    ViewData("Title") = "Students"
End Code

<h2>Students</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>

@Using (Html.BeginForm("Index", "Student", FormMethod.Get))
    @<p>
        Find by name: @Html.TextBox("SearchString", TryCast(ViewBag.CurrentFilter, String))
        <input type="submit" value="Search"/>
    </p>
End Using

<table Class="table">
    <tr>
    <th>
            @Html.ActionLink("Last Name", "Index", New With {.sortOrder = ViewBag.NameSortParam, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th>
            First Name
        </th>
        <th>
            @Html.ActionLink("Enrollment Date", "Index", New With {.sortOrder = ViewBag.DateSortParam, .currentFilter = ViewBag.CurrentFilter})
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.LastName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FirstMidName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.EnrollmentDate)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.ID}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.ID}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.ID})
        </td>
    </tr>
Next

</table>
<br />
Page @(If(Model.PageCount < Model.PageNumber, 0, Model.PageNumber)) of @Model.PageCount

@Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter}))