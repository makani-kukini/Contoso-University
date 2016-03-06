@ModelType ContosoUniversity.ViewModels.InstructorIndexData
@Code
    ViewData("Title") = "Instructors"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>Last Name</th>
        <th>First Name</th>
        <th>Hire Date</th>
        <th>Office</th>
        <th>Courses</th>
        <th></th>
    </tr>

@For Each item In Model.Instructors
    Dim selectedRow As String = ""
    If (item.ID = ViewBag.InstructorID) Then
        selectedRow = "success"
    End If
    @<tr class="@selectedRow">
        <td>
            @Html.DisplayFor(Function(modelItem) item.LastName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.FirstMidName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.HireDate)
        </td>
        <td>
            @If (item.OfficeAssignment IsNot Nothing) Then
                @item.OfficeAssignment.Location
            End If
        </td>
        <td>
            @Code
                For Each course In item.Courses
                    @course.CourseID @: @course.Title <br />
                Next
            End Code
        </td>
        <td>
            @Html.ActionLink("Select", "Index", New With {.id = item.ID}) |
            @Html.ActionLink("Edit", "Edit", New With {.id = item.ID}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.ID}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.ID})
        </td>
    </tr>
                Next

</table>

@If (Model.Courses IsNot Nothing) Then
    @<h3>Courses Taught by Selected Instructor</h3>
    @<table class="table">
        <tr>
            <th></th>
            <th>Number</th>
            <th>Title</th>
            <th>Department</th>
        </tr>

        @For Each item In Model.Courses
            Dim selectedRow As String = ""
            If (item.CourseID = ViewBag.CourseID) Then
                selectedRow = "success"
            End If
            @<tr class="@selectedRow">
                <td>
                    @Html.ActionLink("Select", "Index", New With {.courseID = item.CourseID})
                </td>
                <td>
                    @item.CourseID
                </td>
                <td>
                    @item.Title
                </td>
                <td>
                    @item.Department.Name
                </td>
            </tr>
        Next
    </table>
End If

@If (Model.Enrollments IsNot Nothing) Then
    @<h3>
        Students Enrolled in Selected Course
    </h3>
    @<table class="table">
        <tr>
            <th>Name</th>
            <th>Grade</th>
        </tr>
        @For Each item In Model.Enrollments
            @<tr>
                <td>
                    @item.Student.FullName
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Grade)
                </td>
            </tr>
        Next
    </table>
End If