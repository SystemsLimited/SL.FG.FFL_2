<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WaiverForm.ascx.cs" Inherits="SL.FG.FFL.WebParts.WaiverForm.WaiverForm" %>


<link href="/_layouts/15/SL.FG.FFL/CSS/FGStyle.css" rel="stylesheet" />

<div class="container">
    <div class="row">
        <div class="col-sm-12" id="waiver_div" runat="server">
            <div class="panel panel-success">
                <div class="panel-heading">
                    Safety Incident Recommendations Waiver (IR-7 )
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-6">
                                <label>Safety incident report no<span style="color: red">&nbsp;*</span></label>
                                <input type="text" id="SafetyIncidentReportNo_tf" class="form-control" placeholder="Enter text" runat="server" disabled>
                                 <label id="SafetyIncidentReportNo_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                            <div class="col-lg-6">
                                <label>Recommendation no<span style="color: red">&nbsp;*</span></label>
                                <input type="text" id="RecommendationNo_tf" class="form-control" placeholder="Enter text" runat="server" disabled>
                                 <label id="RecommendationNo_msg" hidden style="color: red">You can't leave this empty.</label>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-6">
                                <label>Unit/Area<span style="color: red">&nbsp;*</span></label>
                                <br />
                                <select id="Section_ddl" class="select2 col-lg-12 form-control" runat="server" disabled>
                                    <option value="0">Please Select</option>
                                </select>
                                <input class="form-control" id="Section_hdn" placeholder="Enter text" type="hidden" runat="server" />
                                <input class="form-control" id="Hidden1" placeholder="Enter text" type="hidden" runat="server" />
                                <label id="Section_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                            <div class="col-lg-6">
                                <label>Department</label>
                                <br />
                                <select id="Department_ddl" class="select2 col-lg-12 form-control" runat="server" disabled>
                                    <option value="0">Please Select</option>
                                </select>
                                <input class="form-control" id="Department_hdn" placeholder="Enter text" type="hidden" runat="server">
                                <label id="Department_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-6">
                                <label>Date<span style="color: red">&nbsp;*</span></label>

                                <div class="form-group">
                                    <SharePoint:DateTimeControl Enabled="false" ID="Date_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                 <label id="Date_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                               
                            </div>
                            <div class="col-lg-6">
                                <label>Recommendation completion due date<span style="color: red">&nbsp;*</span></label>

                                <div class="form-group">
                                    <SharePoint:DateTimeControl Enabled="false" ID="RecommendationCompletionDueDate_dtc"  runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" IsRequiredField="true" UseTimeZoneAdjustment="false" LocaleId="2057"/>
                                 <label id="RecommendationCompletionDueDate_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Description of the recommendations<span style="color: red">&nbsp;*</span></label>

                                <textarea id="DescriptionOfTheRecommendations_ta" class="form-control" runat="server"></textarea>
                                <label id="DescriptionOfTheRecommendations_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>

                            <div class="col-lg-12">
                                <label>Recommendation reason<span style="color: red">&nbsp;*</span></label>

                                <textarea id="RecommendationReason_ta" class="form-control" runat="server"></textarea>
                                 <label id="RecommendationReason_msg" hidden style="color: red">You can't leave this empty.</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-6">
                                <label>New Target Date<span style="color: red">&nbsp;*</span></label>

                                <div class="form-group">
                                    <SharePoint:DateTimeControl ID="NewTargetDate_dtc"  runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" IsRequiredField="true" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                <label id="NewTargetDate_msg" hidden style="color: red">You can't leave this empty.</label>
                                <label id="NewTargetDateCompare_msg" hidden style="color: red">NewTarget date must be greater than Recommendation Completion Due Date.</label>
                                </div>
                                
                            </div>
                            <div class="col-lg-6">
                            </div>
                        </div>
                    </div>
                    <div id="Initiator_div" runat="server">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Initiated by<span style="color: red">&nbsp;*</span></label>
                                    <SharePoint:ClientPeoplePicker runat="server" ID="InitiatedBy_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                                <label id="InitiatedBy_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                                
                                <div class="col-lg-6">
                                    <label>Initiated Date<span style="color: red">&nbsp;*</span></label>

                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="InitiatedDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" />
                                     <label id="InitiatedDate_msg" hidden style="color: red">You can't leave this empty.</label>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>Initiator comments<span style="color: red">&nbsp;*</span></label>

                                    <textarea id="InitiaterComments_ta" class="form-control" runat="server"></textarea>
                                    <label id="InitiaterComments_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="UM_div" style="display: none" runat="server">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Reviewed by UM<span style="color: red">&nbsp;*</span></label>
                                    <SharePoint:ClientPeoplePicker runat="server" ID="UM_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                                <label id="UM_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                                
                                <div class="col-lg-6">
                                    <label>Date<span style="color: red">&nbsp;*</span></label>

                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="UMDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                     <label id="UMDate_msg" hidden style="color: red">You can't leave this empty.</label>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>Comments<span style="color: red">&nbsp;*</span></label>

                                    <textarea id="UMComments_ta" class="form-control" runat="server"></textarea>
                                    <label id="UMComments_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DM_div" style="display: none" runat="server">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Reviewed by DM<span style="color: red">&nbsp;*</span></label>
                                    <SharePoint:ClientPeoplePicker runat="server" ID="DM_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                                  <label id="DM_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                              

                                <div class="col-lg-6">
                                    <label>Date<span style="color: red">&nbsp;*</span></label>

                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="DMDate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                    <label id="DMDate_msg" hidden style="color: red">You can't leave this empty.</label>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>Comments<span style="color: red">&nbsp;*</span></label>

                                    <textarea id="DMComments_ta" class="form-control" runat="server"></textarea>
                                    <label id="DMComments_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DO_div" style="display: none" runat="server">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-6">
                                    <label>Reviewed by DO<span style="color: red">&nbsp;*</span></label>
                                    <SharePoint:ClientPeoplePicker runat="server" ID="DO_PeopleEditor" Rows="1" VisibleSuggestions="3" AllowMultipleEntities="false" PrincipalAccountType="User" />
                                 <label id="DO_PeopleEditor_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                               

                                <div class="col-lg-6">
                                    <label>Date<span style="color: red">&nbsp;*</span></label>

                                    <div class="form-group">
                                        <SharePoint:DateTimeControl ID="DODate_dtc" runat="server" DateOnly="true" CssClassTextBox="form-control" AutoPostBack="false" UseTimeZoneAdjustment="false" LocaleId="2057" />
                                    <label id="DODate_msg" hidden style="color: red">You can't leave this empty.</label>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-lg-12">
                                    <label>Comments<span style="color: red">&nbsp;*</span></label>

                                    <textarea id="DOComments_ta" class="form-control" runat="server"></textarea>
                                     <label id="DOComments_msg" hidden style="color: red">You can't leave this empty.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <asp:HiddenField ID="hdnID" runat="server" Value="" />
                <asp:HiddenField ID="hdnRecommendationListName" runat="server" Value="" />
                <asp:HiddenField ID="hdnParentList" runat="server" Value="" />
                <asp:HiddenField ID="hdnFRID" runat="server" Value="" />
                <asp:HiddenField ID="hdnLink" runat="server" Value="" />
                <asp:HiddenField ID="hdnTaskName" runat="server" Value="" />
                <div class="form-group pull-right">
                    <asp:Button ID="btnSaveAsDraft" class="btn btn-info" Visible="false" runat="server" Text="Save As Draft" OnClick="btnSaveAsDraft_Click" CssClass="btnSaveAsDraft" />
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Submit" OnClick="btnSave_Click"  OnClientClick="return Save_Click();" CssClass="btnSave" />
                    <asp:Button ID="btnCancel" class="btn btn-default" runat="server" Text="Cancel" CssClass="btnCancel" OnClick="btnCancel_Click" />
                    <asp:Button ID="btnUMSave" class="btn btn-primary" Visible="false" runat="server" Text="Reviewed And Approved"  OnClientClick="return Save_Click();" OnClick="btnUMSave_Click" CssClass="btnApprove" />
                    <asp:Button ID="btnDMSave" class="btn btn-primary" Visible="false" runat="server" Text="Reviewed And Approved"  OnClientClick="return Save_Click();" OnClick="btnDMSave_Click" CssClass="btnApprove" />
                    <asp:Button ID="btnDOSave" class="btn btn-primary" Visible="false" runat="server" Text="Reviewed And Approved"  OnClientClick="return Save_Click();" OnClick="btnDOSave_Click" CssClass="btnApprove" />
                    <asp:Button ID="btnUMDisapprive" class="btn btn-primary" Visible="false" runat="server" Text="Disapprove" OnClick="btnUMDisapprove_Click" CssClass="btnReject" />
                    <asp:Button ID="btnDMDisapprive" class="btn btn-primary" Visible="false" runat="server" Text="Disapprove" OnClick="btnDMDisapprove_Click" CssClass="btnReject" />
                    <asp:Button ID="btnDODisapprive" class="btn btn-primary" Visible="false" runat="server" Text="Disapprove" OnClick="btnDODisapprove_Click" CssClass="btnReject" />
                </div>
            </div>
        </div>
        <div class="col-sm-12" id="notWaiver_div" style="display: none">
            <span style="color: red">
                <label id="waiverNotAvailable_lb" runat="server" visible="false"></label>
            </span>
        </div>
    </div>

</div>


<script src="/_layouts/15/SL.FG.FFL/Scripts/jQuery.js"></script>
<script src="/_layouts/15/SL.FG.FFL/Scripts/Validation/Waiver.js"></script>
