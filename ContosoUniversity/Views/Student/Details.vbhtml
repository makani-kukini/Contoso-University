@ModelType ContosoUniversity.Models.Student
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>Student</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.LastName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LastName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FirstMidName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FirstMidName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EnrollmentDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EnrollmentDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Enrollments)
        </dt>

        <dd>
            <table class="table">
                <tr>
                    <th>Course Title</th>
                    <th>Grade</th>
                </tr>
                @For Each item In Model.Enrollments
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Course.Title)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Grade)
                        </td>
                    </tr>
                Next
            </table>
        </dd>
    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
