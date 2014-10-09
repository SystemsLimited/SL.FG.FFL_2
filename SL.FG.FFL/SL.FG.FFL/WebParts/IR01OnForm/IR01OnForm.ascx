<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IR01OnForm.ascx.cs" Inherits="SL.FG.FFL.WebParts.IR01OnForm.IR01OnForm" %>


<link href="/_layouts/15/SL.FG.FFL/CSS/FGStyle.css" rel="stylesheet" />

<div class="container">
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    IR-I
                </div>
                <div class="panel-body">

                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-6">
                                <label>Incident Category<span style="color: red">&nbsp;*</span></label>
                                <br />
                                <select id="IncidentCategory_ddl" class="select2 col-lg-12 form-control" multiple="true" runat="server">
                                </select>
                                <textarea id="IncidentCategory_ta" class="form-control" visible="false" runat="server"></textarea>
                                <label id="IncidentCategory_msg" hidden style="color: red">You can't leave this empty.</label>
                                <input class="form-control" id="IncidentCategory_hdn" placeholder="Enter text" type="hidden" runat="server">
                            </div>
                            <div class="col-lg-6">
                                <label>Injury Category<span style="color: red">&nbsp;*</span></label>
                                <br />
                                <select id="InjuryCategory_ddl" class="select2 col-lg-12 form-control" multiple="true" runat="server">
                                </select>
                                <textarea id="InjuryCategory_ta" class="form-control" visible="false" runat="server"></textarea>
                                <label id="InjuryCategory_msg" hidden style="color: red">You can't leave this empty.</label>
                                <input class="form-control" id="InjuryCategory_hdn" placeholder="Enter text" type="hidden" runat="server">
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-6">
                                <label>Employee Type<span style="color: red">&nbsp;*</span></label>
                                <select id="EmployeeType_ddl" class="form-control" runat="server">
                                    <option value="0">Please Select</option>
                                    <option>FFL</option>
                                    <option>Contractor</option>
                                </select>
                                <label id="EmployeeType_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                            <div class="col-lg-6">
                                <div id="MOConsent_div" runat="server" style="display: none">
                                    <label>Consent Taken<span style="color: red">&nbsp;*</span></label>
                                    <select id="ConsentTaken_ddl" class="form-control" runat="server">
                                        <option value="0">Please Select</option>
                                        <option>Yes</option>
                                        <option>No</option>
                                        <option>N/A</option>
                                    </select>
                                    <label id="ConsentTaken_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="MOName_div" runat="server" style="display: none">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Date of Consent Taken<span style="color: red">&nbsp;*</span></label>

                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="Date_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" OnLoad="Date_dtc_DateChanged"/>
                                    </div>
                                    <label id="Date_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                                <div class="col-lg-6">



                                    <label>MO Name<span style="color: red">&nbsp;*</span></label>
                                    <SharePoint:ClientPeoplePicker runat="server" ID="MOName_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                                </div>
                                <label id="MOName_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>

                                <input class="form-control" id="MOName_tf" placeholder="Enter text" visible="false" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div id="MOFields_div" style="display: none" runat="server">
                            <div class="col-lg-6">

                                <label id="MORemarks_ldl" runat="server">MO Remarks<span id="MORemarks_str" runat="server" style="color: red">&nbsp;*</span></label>

                                <textarea id="MORemarks_ta" class="form-control" runat="server"></textarea>
                                <label id="MORemarks_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>

                            <div class="col-lg-6">
                                <label>Attachment</label>
                                <div>
                                    <table id="grdAttachments" runat="server">
                                    </table>
                                </div>
                                <asp:FileUpload ID="fileUploadControl" runat="server" AllowMultiple="true" />
                                <asp:HiddenField ID="hdnFilesNames" runat="server" Value="" />
                            </div>
                        </div>
                        <%--  <div class="col-lg-6">
                            <div id="HSEManagerName_div" style="display: none" runat="server">
                                <label>HSE Manager Name<span style="color: red">&nbsp;*</span></label>
                                <SharePoint:ClientPeoplePicker runat="server" ID="HSEManagerName_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                            </div>
                            <label id="HSEManagerName_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>

                            <input class="form-control" id="HSEManagerName_tf" placeholder="Enter text" visible="false" runat="server">
                        </div>--%>
                    </div>
                </div>

                <div class="panel panel-success">
                    <div class="panel-heading">
                        Incident Details
                    </div>
                    <div class="panel-body">
                        <div class="form-group row">
                            <div class="col-lg-6">
                                <label>Date of Incident<span style="color: red">&nbsp;*</span></label>
                                <div class="form-group">
                                    <SharePoint:DateTimeControl ID="DateOfIncident_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057"  OnLoad="DateOfIncident_dtc_DateChanged" />
                                </div>
                                <label id="DateOfIncident_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>Time of Incident<span style="color: red">&nbsp;*</span></label>
                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="TimeOfIncident_dtc" runat="server" TimeOnly="true" CssClassTextBox="form-control" AutoPostBack="false" />
                                    </div>
                                    <label id="TimeOfIncident_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-6">
                                <label>Unit/Area<span style="color: red">&nbsp;*</span></label>
                                <br />
                                <select id="Unit_Area_ddl" class="select2 col-lg-12 form-control" runat="server">
                                    <option value="0">Please Select</option>
                                </select>
                                <label id="Unit_Area_msg" hidden style="color: red">You can't leave this empty.</label>
                                <input class="form-control" id="Unit_Area_hdn" placeholder="Enter text" type="hidden" runat="server">
                            </div>
                            <div class="col-lg-6">
                                <label>Incident Score<span style="color: red">&nbsp;*</span></label>
                                <input id="IncidentScore_tf" class="form-control" placeholder="Enter text" runat="server">
                                <label id="IncidentScore_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-6 table-responsive">

                                <label>Submitted by<span style="color: red">&nbsp;*</span></label>
                                <div id="SubmittedBy_div" runat="server">
                                    <SharePoint:ClientPeoplePicker runat="server" ID="SubmittedBy_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" Enabled="false" />
                                </div>
                                <input class="form-control" id="SubmittedBy_tf" placeholder="Enter text" visible="false" runat="server">
                                <label id="SubmittedBy_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                            <div class="col-lg-6">
                                <label>Submission Date<span style="color: red">&nbsp;*</span></label>
                                <div class="form-group">
                                    <SharePoint:DateTimeControl ID="SubmissionDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" OnLoad="SubmissionDate_dtc_DateChanged" />
                                </div>
                                <label id="SubmissionDate_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-lg-12">
                                <label>Title<span style="color: red">&nbsp;*</span></label>
                                <input id="Title_tf" class="form-control" placeholder="Enter text" runat="server">
                                <label id="Title_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <label>Description<span style="color: red">&nbsp;*</span></label>
                                <textarea id="Description_ta" class="form-control" runat="server"></textarea>
                                <label id="Description_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <label>Action taken<span style="color: red">&nbsp;*</span></label>
                                <textarea id="ActionTaken_ta" class="form-control" runat="server"></textarea>
                                <label id="ActionTaken_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>
                        <div class="form-group row">

                            <div class="col-lg-6">
                                <div class="form-inline">
                                    <label>Is detailed report required on IR-03 Form? &nbsp;&nbsp;</label>
                                    <input id="ReportRequired_cb" type="checkbox" runat="server">
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-inline">
                                    <label>If so, is investigation team required? &nbsp;&nbsp;</label>
                                    <input id="TeamRequired_cb" type="checkbox" runat="server">
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="form-group pull-right">
            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save As Draft" OnClick="btnSaveAsDraft_Click" CssClass="btnSaveAsDraft" />
            <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" OnClientClick="return Save_Click();" CssClass="btnSave" />
            <asp:Button ID="btnMOSave" runat="server" Text="Submit" OnClick="btnMOSave_Click" Visible="false" OnClientClick="return MOSave_Click();" CssClass="btnSave" />
            <asp:Button ID="btnHSESave" runat="server" Text="Submit" Visible="false" OnClick="btnHSE_Click" OnClientClick="return Save_Click();" CssClass="btnSave" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btnCancel" />
        </div>

    </div>
</div>


<script src="/_layouts/15/SL.FG.FFL/Scripts/jQuery.js"></script>

<script src="/_layouts/15/SL.FG.FFL/Scripts/Validation/IR01On.js"></script>



<script type="text/javascript">

    $(document).ready(function () {

        $("[id$=IncidentCategory_ddl]").each(function () {

            if ($("[id$=IncidentCategory_ddl] option:selected").val() != "null" && $("[id$=IncidentCategory_ddl] option:selected").val() != "undefined") {

                var value = $(this).val();
                if (value != "" && value != "undefined") {
                    if (value.indexOf("Injury") >= 0) {

                        $("[id$=MOConsent_div]").attr("style", "display:normal");

                        $("[id$=ConsentTaken_ddl]").each(function () {

                            var value = $(this).val();

                            if (value.indexOf("Yes") >= 0) {

                                $("[id$=MOName_div]").attr("style", "display:normal");
                            }
                        });
                    }
                }
            }
        });

    });

    $('[id$=IncidentCategory_ddl]').on('change', function () {

        $("[id$=IncidentCategory_ddl]").each(function () {

            $("[id$=IncidentCategory_hdn]").val($(this).val());

            var value = $(this).val();

            if (value.indexOf("Injury") >= 0) {

                $("[id$=MOConsent_div]").show("fast");

                $("[id$=ConsentTaken_ddl]").each(function () {

                    var value = $(this).val();

                    if (value.indexOf("Yes") >= 0) {

                        $("[id$=MOName_div]").show("fast");
                    }
                    else {
                        $("[id$=MOName_div]").hide("fast");

                    }

                });

            }
            else {
                $("[id$=MOConsent_div]").hide("fast");
                $("[id$=MOName_div]").hide("fast");
            }

        });

    })

    $('[id$=ConsentTaken_ddl]').on('change', function () {

        $("[id$=ConsentTaken_ddl]").each(function () {

            var value = $(this).val();

            if (value.indexOf("Yes") >= 0) {

                $("[id$=MOName_div]").show("fast");
            }
            else {
                $("[id$=MOName_div]").hide("fast");

            }

        });

    })




    $('[id$=InjuryCategory_ddl]').on('change', function () {

        $("[id$=InjuryCategory_ddl]").each(function () {

            $("[id$=InjuryCategory_hdn]").val($(this).val());

        });


    })


    $('[id$=Unit_Area_ddl]').on('change', function () {

        $("[id$=Unit_Area_hdn]").val($(this).val());

    })


</script>

