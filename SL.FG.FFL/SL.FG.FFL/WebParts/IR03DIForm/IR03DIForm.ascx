﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IR03DIForm.ascx.cs" Inherits="SL.FG.FFL.WebParts.IR03DIForm.IR03DIForm" %>


<script type="text/javascript">
    function isActionConfirmed(action) {

        var message = "IR03 Detailed Investigation: Are you sure you want to perform this action?";

        if (typeof action != 'undefined' && action != null && action != "") {
            if (action == "Save") {
                message = "Do you want to Save IR03 Detailed Investigation?";
            }
            else if (action == "SaveAsDraft") {
                message = "Do you want to Save IR03 Detailed Investigation as Draft?";
            }
            else if (action == "Approve") {
                message = "Do you want to Approve IR03 Detailed Investigation?";
            }
            else if (action == "Reject") {
                message = "Do you want to Reject IR03 Detailed Investigation?";
            }
            else if (action == "Forward") {
                message = "Do you want to Forward IR03 Detailed Investigation?";
            }
            else if (action == "Submit") {
                message = "Do you want to Submit IR03 Detailed Investigation?";
            }
        }

        var confirm = window.confirm(message);
        if (!confirm) {
            return false;
        }
        return true;
    }

    function convertStringToDate(str) {
        try {
            var temp = str.split('/');

            if (temp.length > 2) {
                var d = new Date(temp[2], (parseInt(temp[1]) - 1), temp[0]);
                return d;
            }
        }
        catch (ex) {
        }
        return null;
    }

    //Extract Email from content
    function extractEmails(text) {
        return text.match(/([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9._-]+)/gi);
    }

    //Extract Username from content
    function extractUsernames(option) {
        var username;
        if (option == "1") {
            username = $("[id$=responsiblePerson_PeopleEditor] span.ms-entity-resolved").attr("title");
        }
        return username;
    }

    function ValidationHandler(control, controlName) {
        alert(controlName + ":  " + "Please Provide value for the required field.");
        control.focus();
    }

    function ValidationSummary(message, controls) {
        if (message != "" && controls != "") {
            alert("Message: " + message + "\n\n" + controls);
        }
    }

    function SaveIRDIDetails(isSavedAsDraft, action) {
        var errorFlag = false;
        var controlList = '';

        var message = '';

        if ($('.errorMsg').length > 0) {
            $('.errorMsg').remove();
        }


        if (action != "SaveAsDraft") {
            var hdnActualOrPotentialInquiry = $('[id$=hdnActualOrPotentialInquiry]').val();
            var hdnProcessSafetyIncidents = $('[id$=hdnProcessSafetyIncidents]').val();
            var hdnMiscellaneousIncidents = $('[id$=hdnMiscellaneousIncidents]').val();
            var hdnBasicActivityInProgress = $('[id$=hdnBasicActivityInProgress]').val();
            var hdnRiskBasedElements = $('[id$=hdnRiskBasedElements]').val();
            var hdnCulturalBasedElements = $('[id$=hdnCulturalBasedElements]').val();

            var errorMsg = 'Please Select atleast one';

            if (hdnActualOrPotentialInquiry == "") {
                errorFlag = true;

                var tempControl = $('[id$=hdnActualOrPotentialInquiry]').parent().parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
                tempControl.focus();
            }
            if (hdnProcessSafetyIncidents == "") {
                errorFlag = true;

                var tempControl = $('[id$=hdnProcessSafetyIncidents]').parent().parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
                tempControl.focus();
            }
            if (hdnMiscellaneousIncidents == "") {
                errorFlag = true;

                var tempControl = $('[id$=hdnMiscellaneousIncidents]').parent().parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
                tempControl.focus();
            }
            if (hdnBasicActivityInProgress == "") {
                errorFlag = true;

                var tempControl = $('[id$=hdnBasicActivityInProgress]').parent().parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
                tempControl.focus();
            }
            if (hdnRiskBasedElements == "") {
                errorFlag = true;

                var tempControl = $('[id$=hdnRiskBasedElements]').parent().parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
                tempControl.focus();
            }
            if (hdnCulturalBasedElements == "") {
                errorFlag = true;

                var tempControl = $('[id$=hdnCulturalBasedElements]').parent().parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
                tempControl.focus();
            }

            if ($("[id$=reportDueOnDateDate]").val() != "") {
                try {
                    var targetDate = convertStringToDate($("[id$=reportDueOnDateDate]").val());
                    var currentDate = new Date();

                    errorMsg = 'Please provide value for this required field.';

                    if (targetDate != null && targetDate < currentDate && $('[id$=lateSubmissionReasons_ta]').val() == "") {
                        errorFlag = true;

                        var tempControl = $('[id$=lateSubmissionReasons_ta]').parent();

                        var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                        $(tempControl).append(spanTemp);
                        tempControl.focus();
                    }
                }
                catch (ex) {
                }
            }

            errorMsg = 'Please enter atleast one';

            var countKeyFindings = $('[id$=noOfKeyFindings_span]').text();
            var countPeopleIntervieweds = $('[id$=noOfPeopleInterviewed_span]').text();
            var countRootCauses = $('[id$=noOfRootCauses_span]').text();

            if (countKeyFindings == "0") {
                errorFlag = true;

                var tempControl = $('[id$=keyFindings_tf]').parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
            }

            if (countPeopleIntervieweds == "0") {
                errorFlag = true;

                var tempControl = $('[id$=peopleInterviewed_tf]').parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
            }

            if (countRootCauses == "0") {
                errorFlag = true;

                var tempControl = $('[id$=rootCauses_tf]').parent().parent().parent();

                var spanTemp = '<span class="errorMsg">' + errorMsg + '</span>';

                $(tempControl).append(spanTemp);
            }
        }

        if (errorFlag == false) {

            if (!isActionConfirmed(action)) {
                return false;
            }

            var recommendationList = '';
            $("[id$=recommendationDetails_table] tr.recommendationItem").each(function () {
                $this = $(this)
                var recommendationId = $this.find("span.recommendationId").html();
                var recommendationNo = $this.find("span.recommendationNo").html();
                var description = $this.find("span.description").html();
                var username = $this.find("span.username").html();
                var email = $this.find("span.email").html();
                var sectionId = $this.find("span.sectionId").html();
                var sectionName = $this.find("span.sectionName").html();
                var departmentId = $this.find("span.departmentId").html();
                var departmentName = $this.find("span.departmentName").html();
                var targetDate = $this.find("span.targetDate").html();
                var observationSpot = $this.find("span.concurrenceOfRP").html();
                var status = $this.find("span.status").html();

                if (typeof recommendationId == 'undefined') {
                    recommendationId = 0;
                }
                if (typeof recommendationNo == 'undefined') {
                    recommendationNo = "";
                }
                if (typeof description == 'undefined') {
                    description = "";
                }
                if (typeof username == 'undefined') {
                    username = "";
                }
                if (typeof email == 'undefined') {
                    email = "";
                }
                if (typeof sectionId == 'undefined') {
                    sectionId = '0';
                }
                if (typeof sectionName == 'undefined') {
                    sectionName = "";
                }

                if (typeof departmentId == 'undefined') {
                    departmentId = '0';
                }
                if (typeof departmentName == 'undefined') {
                    departmentName = "";
                }
                if (typeof targetDate == 'undefined') {
                    targetDate = "";
                }
                if (typeof concurrenceOfRP == 'undefined') {
                    concurrenceOfRP = "no";
                }
                if (typeof status == 'undefined') {
                    status = "";
                }

                recommendationList = recommendationList +
                    recommendationId + "*|*" +
                    description + "*|*" +
                    username + "*|*" +
                    email + "*|*" +
                    sectionId + "*|*" +
                    sectionName + "*|*" +
                    departmentId + "*|*" +
                    departmentName + "*|*" +
                    targetDate + "*|*" +
                    concurrenceOfRP + "*|*" +
                    status + "*|*" +
                    recommendationNo + "*|*" +
                    isSavedAsDraft + "~|~";
            });

            $("[id$=hdnRecommendationList]").val(recommendationList);

            var keyFindingsList = '';
            $("[id$=keyFindings_table] tr.keyFindingsItem").each(function () {
                $this = $(this)
                var keyFindings = $this.find("span.keyFindingsDescription").html();

                keyFindingsList = keyFindingsList + keyFindings + "~|~";
            });

            $("[id$=hdnKeyFindingsList]").val(keyFindingsList);


            var peopleInterviewedList = '';
            $("[id$=peopleInterviewed_table] tr.peopleInterviewedItem").each(function () {
                $this = $(this)
                var peopleInterviewed = $this.find("span.peopleInterviewedDescription").html();

                peopleInterviewedList = peopleInterviewedList + peopleInterviewed + "~|~";
            });

            $("[id$=hdnPeopleInterviewedList]").val(peopleInterviewedList);

            var rootCausesList = '';
            $("[id$=rootCauses_table] tr.rootCausesItem").each(function () {
                $this = $(this)
                var rootCauses = $this.find("span.rootCausesDescription").html();

                rootCausesList = rootCausesList + rootCauses + "~|~";
            });

            $("[id$=hdnRootCausesList]").val(rootCausesList);




            return true;
        }
        else {
            //ValidationSummary(message, controlList);
            alert("Validation incomplete: Please verify the data.");
            return false;
        }
    }
</script>

<link href="/_layouts/15/SL.FG.FFL/CSS/FGStyle.css" rel="stylesheet" />

<style type="text/css">
    .editRecommendation {
        display: none !important;
    }

    .editKeyFindings {
        display: none !important;
    }

    .editPeopleInterviewed {
        display: none !important;
    }

    .editRootCauses {
        display: none !important;
    }

    [id$=responsiblePerson_PeopleEditor_TopSpan] {
        border-radius: 5px !important;
        width: 100%;
    }

    .panel-title:hover {
        cursor: pointer;
    }
</style>


<div class="container containerMaxWidth">
    <div class="row">
        <div class="col-lg-12">
            <div id="message_div" runat="server" class="messageDiv">
            </div>
            <div class="panel panel-success">
                <div class="panel-heading">
                    Detailed Incident Report
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Title</label>
                        <input type='text' class="form-control" id="incidentTitle_tf" runat="server" disabled />
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-6">
                            <label>Date of Incident</label>
                            <div class="form-group">
                                <SharePoint:DateTimeControl ID="incidentDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <label>Time of Incident</label>
                            <div class="form-group">
                                <SharePoint:DateTimeControl ID="incidentTime_dtc" runat="server" TimeOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Unit/Area<span style="color: red">&nbsp;*</span></label>
                                <asp:DropDownList ID="incidentUnitArea_ddl" runat="server" CssClass="form-control" AutoPostBack="false" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Incident Score</label>
                                <input type='text' class="form-control" id="incidentScore_tf" runat="server" disabled />
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-6">
                            <label>Employee Type<span style="color: red">&nbsp;*</span></label>
                            <select id="EmployeeType_ddl" class="form-control" runat="server" disabled>
                                <option>FFL</option>
                                <option>Contractor</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-4">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Actual or Potential Inquiry
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-12">
                                            <select id="actualOrPotentialInquiry_ddl" class="select2 col-lg-12" multiple="true" runat="server">
                                                <option>Fatality</option>
                                                <option>LTI</option>
                                                <option>RWC</option>
                                                <option>MTC</option>
                                            </select>
                                            <asp:HiddenField ID="hdnActualOrPotentialInquiry" runat="server" Value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Process Safety Incidents
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-12">
                                            <select id="processSafetyIncidents_ddl" class="select2 col-lg-12" multiple="true" runat="server">
                                                <option>Process Safety Incident/ Process Release</option>
                                                <option>Inoperable Safety Equipment</option>
                                                <option>Operational Upset</option>
                                                <option>Human Error</option>
                                                <option>Violation if PSM Systems/Site HSE Policies</option>
                                                <option>Fire</option>
                                            </select>
                                            <asp:HiddenField ID="hdnProcessSafetyIncidents" runat="server" Value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Miscellaneous Incidents
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-12">
                                            <select id="miscellaneousIncidents_ddl" class="select2 col-lg-12" multiple="true" runat="server">
                                                <option>Vehicle Incident</option>
                                                <option>Equipment Failure</option>
                                                <option>Electrical Failure</option>
                                                <option>Environmental Incidents</option>
                                                <option>Others</option>
                                            </select>
                                            <asp:HiddenField ID="hdnMiscellaneousIncidents" runat="server" Value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Description</label>
                        <textarea class="form-control" id="incidentDescription_ta" runat="server"> </textarea>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-6">
                            <label>Date of Occurence of Incident</label>
                            <div class="form-group">
                                <SharePoint:DateTimeControl ID="incidentDateOfOccurence" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <label>Report due on(according to Flash report target Date)</label>
                            <div class="form-group">
                                <SharePoint:DateTimeControl ID="reportDueOnDate" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Reasons for Sending the report late(if applicable)</label>
                        <textarea class="form-control" id="lateSubmissionReasons_ta" runat="server"></textarea>
                    </div>
                    <div class="form-group row">
                        <div class="col-lg-4">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Basic Activity in Progress
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-12">
                                            <select id="basicActivityInProgress_ddl" class="select2 col-lg-12" multiple="true" runat="server">
                                                <option>Normal Operation</option>
                                                <option>Lifting(Mech.)</option>
                                                <option>Chemical Handling</option>
                                                <option>Maintenance</option>
                                                <option>Plant Startup</option>
                                                <option>Lifting Manual</option>
                                                <option>Material Handling</option>
                                                <option>Loading/Unloading</option>
                                                <option>Plant Shutdown</option>
                                                <option>Commissioning</option>
                                                <option>Cleaning</option>
                                                <option>Others</option>
                                            </select>
                                            <asp:HiddenField ID="hdnBasicActivityInProgress" runat="server" Value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-lg-4">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Risk Based Elements
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-12">
                                            <select id="riskBasedElements_ddl" class="select2 col-lg-12" multiple="true" runat="server">
                                                <option>Process Safety Information</option>
                                                <option>Process Hazard Analysis</option>
                                                <option>Management of Change - Technology</option>
                                                <option>Emergency Preparedness & Contingency</option>
                                                <option>Quality Assurance</option>
                                                <option>Pre-Startup Safety Review</option>
                                                <option>Mechanical Integrity</option>
                                                <option>Management of Change - Facilities</option>
                                                <option>Management of Change - Personal</option>
                                                <option>Contractor Safety</option>
                                            </select>
                                            <asp:HiddenField ID="hdnRiskBasedElements" runat="server" Value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    Cultural Based Elements
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-12">
                                            <select id="culturalBasedElements_ddl" class="select2 col-lg-12" multiple="true" runat="server">
                                                <option>Management Commitment</option>
                                                <option>Policies and Principles</option>
                                                <option>Integrated Organiation Structure</option>
                                                <option>Line Management Accountability & Responsibility</option>
                                                <option>Goals Objectives & Plans</option>
                                                <option>Safety Personnel</option>
                                                <option>Procedures & Performance Standards</option>
                                                <option>Training & Development</option>
                                                <option>Effective Communication</option>
                                                <option>Motivation and awareness</option>
                                                <option>Audits & Observations</option>
                                                <option>Incident Investigation</option>
                                            </select>
                                            <asp:HiddenField ID="hdnCulturalBasedElements" runat="server" Value="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <div class="panel panel-success">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <label>Sequence of Events</label>
                                            <div class="form-group">
                                                <div class='input-group'>
                                                    <input type='text' class="form-control" id="rootCauses_tf" runat="server" placeholder="Please Enter..." />
                                                    <span id="rootCauses_span" class="input-group-addon">&nbsp;Add
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group table-responsive" style="overflow-x: scroll;">
                                            <table id="rootCauses_table" class="table" runat="server">
                                                <tr>
                                                    <th>No</th>
                                                    <th>Description</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </table>
                                            Total: <span id="noOfRootCauses_span" runat="server"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <div class="panel panel-success">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                 <label>Key Findings</label>
                                                <div class='input-group'>
                                                    <input type='text' class="form-control" id="keyFindings_tf" runat="server" placeholder="Please Enter..." />
                                                    <span id="keyFindings_span" class="input-group-addon">&nbsp;Add
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group table-responsive" style="overflow-x: scroll;">
                                            <table id="keyFindings_table" class="table" runat="server">
                                                <tr>
                                                    <th>No</th>
                                                    <th>Description</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </table>
                                            Total: <span id="noOfKeyFindings_span" runat="server"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <div class="panel panel-success">
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Investigations/Interviews</label>
                                                <div class='input-group'>
                                                    <input type='text' class="form-control" id="peopleInterviewed_tf" runat="server" placeholder="Please Enter..." />
                                                    <span id="peopleInterviewed_span" class="input-group-addon">&nbsp;Add
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group table-responsive" style="overflow-x: scroll;">
                                            <table id="peopleInterviewed_table" class="table" runat="server">
                                                <tr>
                                                    <th>No</th>
                                                    <th>Description</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </table>
                                            Total: <span id="noOfPeopleInterviewed_span" runat="server"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Conclusion</label>
                            <textarea class="form-control" id="conclusion_ta" runat="server"> </textarea>
                        </div>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-9">
                                        <h5>Recommendation Entry</h5>
                                    </div>
                                    <div class="col-lg-3">
                                        <span id="panel-title3" class="panel-title pull-right"
                                            data-toggle="collapse"
                                            data-target="#collapse3">
                                            <i class='glyphicon glyphicon-sort'></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div id="collapse3" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <label>Description</label>
                                        <textarea class="form-control" id="description_ta"></textarea>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-6 table-responsive">
                                            <label>Responsible Person<span style="color: red">&nbsp;*</span></label>
                                            <SharePoint:ClientPeoplePicker runat="server" ID="responsiblePerson_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                                            <input id="responsiblePersonUsername_hd" type="hidden" value="" />
                                            <input id="recommendationId_hd" type="hidden" value="0" />
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>Responsible Department<span style="color: red">&nbsp;*</span></label>
                                                <select name="responsibleDepartment_ddl" class="form-control" id="responsibleDepartment_ddl" runat="server">
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>Responsible Unit/Section</label>
                                                <select name="responsibleSection_ddl" class="form-control" id="responsibleSection_ddl" runat="server">
                                                    <option value='0'>Please Select</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>Responsible Person Email</label>
                                                <input id="responsiblePersonEmail_tf" type='text' class="form-control disabled" disabled />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-6">
                                            <label>Do you have concurrence of responsible individuals on action items completion dates</label>
                                            <div class="form-group">
                                                <div class="form-inline col-lg-6">
                                                    <label>Yes</label>
                                                    <input type="radio" name="concurrenceOfRP" id="concurrenceOfRPYes_rb" value="Yes">
                                                </div>
                                                <div class="form-inline col-lg-6">
                                                    <label>No</label>
                                                    <input type="radio" name="concurrenceOfRP" id="concurrenceOfRPNo_rb" value="No" checked>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Target Date<span style="color: red">&nbsp;*</span></label>
                                            <div class="form-group">
                                                <SharePoint:DateTimeControl ID="targetDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-lg-6">
                                            <label>Status</label>
                                            <select class="form-control" id="status_ddl" disabled>
                                                <option>Pending</option>
                                                <option>In Progress</option>
                                                <option>Completed</option>
                                            </select>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>&nbsp</label>
                                            <div class="form-group">
                                                <input type="button" value="Add" class="btnAdd pull-right" id="addRecommendation_btn" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-body">
                                            <div class="form-group table-responsive" style="overflow-x: scroll;">
                                                <table class="table" id="recommendationDetails_table" runat="server">
                                                    <tr>
                                                        <th>No</th>
                                                        <th>Description</th>
                                                        <th>Responsible Person</th>
                                                        <th>Responsible Unit</th>
                                                        <th>Responsible Department</th>
                                                        <th>Target Date</th>
                                                        <th>Concurrence Of RP</th>
                                                        <th>Status</th>
                                                        <th>Actions</th>
                                                    </tr>
                                                </table>
                                                No. of Recommendations <span id="noOfRecommendations_span" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label>Investigated By</label>
                                    <input type='text' class="form-control" id="investigatedBy_tf" runat="server" disabled />
                                </div>
                                <div class="col-lg-6">
                                    <label>Investigation Date<span style="color: red">&nbsp;*</span></label>
                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="investigationDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label>Approval Authority</label>
                                    <input type='text' class="form-control" id="approvedBy_tf" runat="server" disabled />
                                </div>
                                <div class="col-lg-6" id="approvalDate_div" runat="server" visible="false">
                                    <label>Approval Date<span style="color: red">&nbsp;*</span></label>
                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="approvalDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <span class="errorMsg" id="LateSubmition_spn">Late Submission</span>
                            </div>
                        </div>
                        <div class="panel panel-success" id="HSEDepartment_div" runat="server" visible="false">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-9">
                                        <h5>HSE Department</h5>
                                    </div>
                                    <div class="col-lg-3">
                                        <span class="panel-title pull-right"
                                            data-toggle="collapse"
                                            data-target="#collapse4">
                                            <i class='glyphicon glyphicon-sort'></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div id="collapse4" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>Report viewed (HSE Engineer/Unit Manager)</label>
                                                <textarea class="form-control" id="rvf_reportViewed_ta" runat="server" rows="3" maxlength="30"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>UM HSE Comments</label>
                                        <textarea class="form-control" id="UM_HSE_Comments_ta" runat="server" rows="3"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success" id="IRRCQuality_div" runat="server" visible="false">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-9">
                                        <h5>IRRC Quality Score</h5>
                                    </div>
                                    <div class="col-lg-3">
                                        <span class="panel-title pull-right"
                                            data-toggle="collapse"
                                            data-target="#collapse5">
                                            <i class='glyphicon glyphicon-sort'></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div id="collapse5" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-lg-6">
                                            <label>IRRC Quality Score</label>
                                            <textarea class="form-control" id="IRRCQualityScore_ta" runat="server" rows="3" maxlength="30"></textarea>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>Quality Assessed By</label>
                                            <textarea class="form-control" id="IRRCQualityAccessedBy_ta" runat="server" rows="3" maxlength="30"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-9">
                                        <h5>Contributing Human Factor for the Incident</h5>
                                    </div>
                                    <div class="col-lg-3">
                                        <span class="panel-title pull-right"
                                            data-toggle="collapse"
                                            data-target="#collapse6">
                                            <i class='glyphicon glyphicon-sort'></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div id="collapse6" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <img src="/_layouts/15/SL.FG.FFL/Images/img1.jpg" alt="Contributing Human Factor for the Incident" class="img-responsive" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <asp:HiddenField ID="hdnSentFrom" runat="server" Value="" />
    <asp:HiddenField ID="hdnIsChangesAllowed" runat="server" Value="1" />
    <asp:HiddenField ID="hdnApprovalAuthority" runat="server" Value="" />
    <asp:HiddenField ID="hdnFRID" runat="server" Value="" />
    <asp:HiddenField ID="hdnIR1ID" runat="server" Value="" />
    <asp:HiddenField ID="hdnIR03DI_Id" runat="server" Value="" />
    <asp:HiddenField ID="hdnIdList" runat="server" Value="" />
    <asp:HiddenField ID="hdnKeyFindingsList" runat="server" Value="" />
    <asp:HiddenField ID="hdnPeopleInterviewedList" runat="server" Value="" />
    <asp:HiddenField ID="hdnRootCausesList" runat="server" Value="" />
    <asp:HiddenField ID="hdnRecommendationList" runat="server" Value="" />

    <div class="col-lg-6" id="FRTagetDate_div" runat="server" style="display: none;">
        <SharePoint:DateTimeControl ID="FRTargetDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" Enabled="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
    </div>

    <br />
    <br />
    <div class="form-group pull-right">
        <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save As Draft" OnClick="btnSaveAsDraft_Click" OnClientClick="return SaveIRDIDetails(true, 'SaveAsDraft');" CssClass="btnSaveAsDraft" Visible="false" />
        <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" OnClientClick="return SaveIRDIDetails(true, 'Submit');" CssClass="btnSave" Visible="false" />
        <asp:Button ID="btnLastSave" runat="server" Text="Save" OnClick="btnLastSave_Click" OnClientClick="return SaveIRDIDetails(false, 'Save');" CssClass="btnSave" Visible="false" />
        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" OnClientClick="return SaveIRDIDetails(true, 'Approve');" CssClass="btnApprove" Visible="false" />
        <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" OnClientClick="return SaveIRDIDetails(true, 'Reject');" CssClass="btnReject" Visible="false" />
        <asp:Button ID="btnForward" runat="server" Text="Review/Forward" OnClick="btnForward_Click" OnClientClick="return SaveIRDIDetails(false, 'Forward');" CssClass="btnForward" Visible="false" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="return isActionConfirmed();" CssClass="btnCancel" />
    </div>
</div>


<script src="/_layouts/15/SL.FG.FFL/Scripts/MicrosoftAjax.js" type="text/javascript">
</script>
<script
    type="text/javascript"
    src="/_layouts/15/sp.runtime.js">
</script>
<script
    type="text/javascript"
    src="/_layouts/15/sp.js">
</script>

<script src="/_layouts/15/SL.FG.FFL/Scripts/IRR03DI/IRR03DIForm_JSOM.js"></script>

<script src="/_layouts/15/SL.FG.FFL/Scripts/jQuery.js"></script>

<script src="/_layouts/15/SL.FG.FFL/Scripts/IRR03DI/IRR03DIForm.js"></script>

