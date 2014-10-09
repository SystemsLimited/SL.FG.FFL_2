<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkQueue.ascx.cs" Inherits="SL.FG.FFL.WebParts.WorkQueue.WorkQueue" %>


<link href="/_layouts/15/SL.FG.FFL/CSS/FGStyle.css" rel="stylesheet" />

<div class="container">
    <div class="row">
        <div id="message_div" runat="server" class="messageDiv">
        </div>
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-9">
                            <h5>MSA</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse_01">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse_01" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>MSA Schedule</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse3">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse3" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput3" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdMSAScheduled" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>MSA (Saved as draft)</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse1">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse1" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput1" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdMSATask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>MSA Recommendations</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse2">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse2" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput2" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdMSARecommendationTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-9">
                            <h5>IR-On-Job</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse_02">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse_02" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR01</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_21">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_21" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_21" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIR01Task" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>Flash Report</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_22">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_22" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_22" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdFlashReportTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR01 Detailed Investigation</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_23">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_23" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_23" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIR01DITasks" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR03 Detailed Investigation</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_24">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_24" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_24" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIR03DITasks" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR Recommendations</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_25">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_25" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_25" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIRRecommendationsOnJob" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>Waiver Tasks</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_26">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_26" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_26" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdWaiverOnJobTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-9">
                            <h5>IR-Off-Job</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse_03">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse_03" class="panel-collapse collapse">
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR01</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_31">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_31" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_31" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIIROffJobTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>Flash Report</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_32">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_32" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_32" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdFROffJobTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR05 Detailed Investigation</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_33">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_33" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_33" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIR05OffJobTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>IR Recommendations</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_34">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_34" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_34" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdIR5OffJobRecomendationTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9">
                                            <h5>Waiver Tasks</h5>
                                        </div>
                                        <div class="col-lg-3">
                                            <span class="panel-title pull-right"
                                                data-toggle="collapse"
                                                data-target="#collapse_35">
                                                <i class='glyphicon glyphicon-sort'></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div id="collapse_35" class="panel-collapse collapse">
                                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                                        <div class="row">
                                            <div style="margin: 10px;">
                                                <input type="text" id="searchInput_35" placeholder="Search..." class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdWaiverOffJobTask" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle" GridLines="Both" HeaderStyle-BackColor="AliceBlue" Width="100%" CellPadding="10" CellSpacing="10">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<script src="/_layouts/15/SL.FG.FFL/Scripts/jQuery.js"></script>

<script src="/_layouts/15/SL.FG.FFL/Scripts/WorkQueue/WorkQueue.js"></script>






