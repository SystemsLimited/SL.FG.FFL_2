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

        <!--Rizwan -->
        <!--Start -->
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
                            <h5>MSA Recommendation</h5>
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
                                data-target="#collapse12">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse12" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput12" placeholder="Search..." class="form-control" />
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
                                data-target="#collapse13">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse13" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput13" placeholder="Search..." class="form-control" />
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
                            <h5>IR Recommendations On Job</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse11">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse11" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput11" placeholder="Search..." class="form-control" />
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
        <!--End WorkQueue -->


        <!--Usama -->
        <!--Start -->
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-9">
                            <h5>IIR 01 Off The Job Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse30">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse30" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput30" placeholder="Search..." class="form-control" />
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
                            <h5>Flash Report Off The Job Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse32">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse32" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput32" placeholder="Search..." class="form-control" />
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
                            <h5>IR05 Off Job Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse34">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse34" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput34" placeholder="Search..." class="form-control" />
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
                            <h5>IR05 Off Job Recommendation Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse36">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse36" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput36" placeholder="Search..." class="form-control" />
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
                            <h5>On The Job Waiver Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse38">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse38" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput38" placeholder="Search..." class="form-control" />
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
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-9">
                            <h5>Off The Job Waiver Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse39">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse39" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput39" placeholder="Search..." class="form-control" />
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
        <div class="col-lg-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-lg-9">
                            <h5>IIR-01 On The Job Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse40">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse40" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput40" placeholder="Search..." class="form-control" />
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
                            <h5>Flash Report On The Job Task</h5>
                        </div>
                        <div class="col-lg-3">
                            <span class="panel-title pull-right"
                                data-toggle="collapse"
                                data-target="#collapse42">
                                <i class='glyphicon glyphicon-sort'></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="collapse42" class="panel-collapse collapse">
                    <div class="panel-body" style="height: 200px; overflow-y: scroll;">
                        <div class="row">
                            <div style="margin: 10px;">
                                <input type="text" id="searchInput42" placeholder="Search..." class="form-control" />
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
         <!--End WorkQueue -->

    </div>
</div>

<script src="/_layouts/15/SL.FG.FFL/Scripts/jQuery.js"></script>

<script src="/_layouts/15/SL.FG.FFL/Scripts/WorkQueue/WorkQueue.js"></script>






