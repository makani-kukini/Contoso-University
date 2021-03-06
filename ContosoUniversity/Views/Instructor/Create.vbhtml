﻿@ModelType ContosoUniversity.Models.Instructor
@Imports ContosoUniversity.HtmlHelpers
@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Instructor</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        <div class="form-group">
            @Html.LabelFor(Function(model) model.LastName, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.LastName, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.LastName, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.FirstMidName, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.FirstMidName, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.FirstMidName, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.HireDate, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.HireDate, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.HireDate, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.OfficeAssignment.Location, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.OfficeAssignment.Location)
                @Html.ValidationMessageFor(Function(model) model.OfficeAssignment.Location)
            </div>
        </div>

         <div class="form-group">
             <div class="col-md-offset-2 col-md-10">
                 <table>
                     <tr>
                         @Code
                             Dim cnt As Integer = 0
                             Dim courses As List(Of ContosoUniversity.ViewModels.AssignedCourseData) = ViewBag.Courses

                             For Each course In courses
                                 cnt += 1
                                 If (cnt Mod 3 = 0) Then
                                     @:</tr><tr>
                                 End If
                                 @:<td>
                                     @<input type="checkbox"
                                             name="selectedCourses"
                                             value="@course.CourseID"
                                             @Html.Raw(If(course.Assigned, "checked=""checked""", "")) />
                                         @course.CourseID @: @course.Title
                                 @:</td>
                            Next
                            @:</tr>
                        End Code
                </table>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>  End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/globalization")
    @Scripts.Render("~/bundles/globalization.validate")

    @Html.GlobalizeConfig()
End Section
