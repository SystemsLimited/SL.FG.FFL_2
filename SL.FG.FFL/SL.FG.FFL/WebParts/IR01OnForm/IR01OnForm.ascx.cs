using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using SL.FG.FFL.Layouts.SL.FG.FFL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
namespace SL.FG.FFL.WebParts.IR01OnForm
{
    [ToolboxItemAttribute(false)]
    public partial class IR01OnForm : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public IR01OnForm()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    String IRID = Page.Request.QueryString["IRID"];

                    FillDropdowns();

                    if (!String.IsNullOrEmpty(IRID))
                    {
                        PageLoadOnUserBases();
                    }
                    else
                    {
                        this.btnSave.Visible = false;
                        this.btnSaveAsDraft.Visible = false;
                        this.btnMOSave.Visible = false;
                        this.btnHSESave.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->Page_Load)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);

            }
        }

        private void DisableControls(bool disableAll)
        {
            btnSaveAsDraft.Visible = false;
            btnSave.Visible = false;
            btnMOSave.Visible = false;
            btnHSESave.Visible = false;
        }

        private void PageLoadOnUserBases()
        {
            try
            {

                String IRID = Page.Request.QueryString["IRID"];
                if (!String.IsNullOrEmpty(IRID))
                {

                    using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oSPWeb = oSPsite.OpenWeb())
                        {

                            Boolean checkMo = CheckCurrentUserIsMo(oSPWeb);
                            Boolean checkHSEMember = CheckCurrentUserIsHSEMember(oSPWeb);
                            Boolean checkSupervisor = CheckCurrentUserIsSupervisor(oSPWeb);
                            Boolean checkAdmin = CheckCurrentUserIsAdmin(oSPWeb);

                            SPListItemCollection IR_1InfoList = oSPWeb.Lists["IR-1"].Items;
                            if (IR_1InfoList != null)
                            {
                                SPListItem imiItem = IR_1InfoList.GetItemById(Convert.ToInt32(IRID));
                                if (imiItem != null)
                                {
                                    Boolean checkSubmittedBy = CheckSubmitByCurrentUser(imiItem, oSPWeb);
                                    Boolean checkAudittedBy = CheckAuditByCurrentUser(imiItem, oSPWeb);
                                    Boolean SaveAsDraft = CheckSaveAsDraft(imiItem, oSPWeb);

                                    if (checkAdmin)
                                    {
                                        LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                        DisableUnableFieldsForAssigneeHSEMembers();
                                    }
                                    else if (CheckAssignee(imiItem, oSPWeb))
                                    {

                                        if (checkMo && checkAudittedBy && SaveAsDraft)
                                        {
                                            LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                            DisableFieldsForNewPerson();
                                        }

                                        else if (checkMo && checkSubmittedBy && checkAudittedBy)
                                        {
                                            LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                            DisableUnableFieldsForMO();
                                        }
                                        else if (checkMo && checkAudittedBy)
                                        {
                                            LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                            DisableFieldsForNewPerson();
                                        }
                                        else if (checkMo)
                                        {
                                            LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                            DisableUnableFieldsForMO();
                                        }
                                        else if (checkHSEMember && checkAudittedBy)
                                        {

                                            LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                            DisableFieldsForNewPerson();


                                        }
                                        //else if (checkHSEMember)
                                        //{

                                        //    LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                        //    DisableUnableFieldsForAssigneeHSEMembers();

                                        //}
                                        else
                                        {

                                            LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                            DisableFieldsForNewPerson();

                                        }

                                    }
                                    else if (checkHSEMember)
                                    {

                                        LoadPageFromDraft(imiItem, oSPWeb, IRID);
                                        DisableUnableFieldsForHSEMembers();
                                    }
                                    else
                                    {

                                        string accessDeniedUrl = Utility.GetRedirectUrl("Access_Denied");

                                        if (!String.IsNullOrEmpty(accessDeniedUrl))
                                        {
                                            Page.Response.Redirect(accessDeniedUrl, false);
                                        }

                                    }


                                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["Status"])))
                                    {
                                        string status = Convert.ToString(imiItem["Status"]);

                                        if (status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                                        {
                                            this.btnSave.Visible = false;
                                            this.btnSaveAsDraft.Visible = false;
                                            this.btnMOSave.Visible = false;
                                            this.btnHSESave.Visible = false;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    DisableFieldsForNewPerson();
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->PageLoadOnUserBases)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }


        protected void Date_dtc_DateChanged(object sender, EventArgs e)
        {
            this.Date_dtc.MinDate = DateTime.Now.Date; // starting date as DateTime object
        }

        protected void DateOfIncident_dtc_DateChanged(object sender, EventArgs e)
        {
            this.DateOfIncident_dtc.MaxDate = DateTime.Now.Date; // starting date as DateTime object
        }

        protected void SubmissionDate_dtc_DateChanged(object sender, EventArgs e)
        {
            this.SubmissionDate_dtc.MaxDate = DateTime.Now.Date; // starting date as DateTime object
        }
      
        private Boolean CheckSaveAsDraft(SPListItem ListItem, SPWeb oSPWeb)
        {
            Boolean IsSaveAsDraft = false;

            String User = oSPWeb.CurrentUser.LoginName;
            String[] Name = User.Split('|');
            String currentUser = Name[1];

            if (currentUser != null)
            {
                String s = Convert.ToString(ListItem["IsSaveAsDraft"]);

                if (s.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    IsSaveAsDraft = true;
                }
            }
            return IsSaveAsDraft;

        }
        private Boolean CheckAuditByCurrentUser(SPListItem ListItem, SPWeb oSPWeb)
        {
            Boolean SubmittedBy = false;

            String User = oSPWeb.CurrentUser.LoginName;
            String[] Name = User.Split('|');
            String currentUser = Name[1];

            if (currentUser != null)
            {
                String s = Convert.ToString(ListItem["AuditedBy"]);

                string[] AssigneeList = s.Split(',');


                foreach (string person in AssigneeList)
                {
                    if (person == currentUser)
                        SubmittedBy = true;
                }
            }
            return SubmittedBy;

        }

        private Boolean CheckSubmitByCurrentUser(SPListItem ListItem, SPWeb oSPWeb)
        {
            Boolean SubmittedBy = false;

            String User = oSPWeb.CurrentUser.LoginName;
            String[] Name = User.Split('|');
            String currentUser = Name[1];

            if (currentUser != null)
            {
                String s = Convert.ToString(ListItem["SubmittedBy"]);

                string[] AssigneeList = s.Split(',');


                foreach (string person in AssigneeList)
                {
                    if (person == currentUser)
                        SubmittedBy = true;
                }
            }
            return SubmittedBy;

        }


        private Boolean CheckAssignee(SPListItem ListItem, SPWeb oSPWeb)
        {
            Boolean assignee = false;

            String User = oSPWeb.CurrentUser.LoginName;
            String[] Name = User.Split('|');
            String currentUser = Name[1];

            if (currentUser != null)
            {
                String s = Convert.ToString(ListItem["Assignee"]);

                string[] AssigneeList = s.Split(',');


                foreach (string person in AssigneeList)
                {
                    if (person == currentUser)
                        assignee = true;
                }
            }
            return assignee;

        }

        private void FillDropdowns()
        {
            using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb oSPWeb = oSPsite.OpenWeb())
                {
                    FillArea(oSPWeb);
                    FillIncidentCategory(oSPWeb);
                    FillInjuryCategory(oSPWeb);
                }
            }
        }

        private bool CheckCurrentUserIsMo(SPWeb oSPWeb)
        {
            Boolean IsMO = false;
            try
            {
                //string groupName = Utility.GetValueByKey("MasterGroup");
                //var spGroup = oSPWeb.Groups[groupName];
                //if (spGroup != null)
                //{
                //    isMember = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                //}

                string groupName = Utility.GetValueByKey("MOGroup");
                var spGroup = oSPWeb.Groups[groupName];

                //var spGroup = oSPWeb.Groups.GetByName("MO Group");

                if (spGroup != null)
                {
                    IsMO = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->CheckCurrentUserIsMo)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return IsMO;
        }

        private bool CheckCurrentUserIsHSEMember(SPWeb oSPWeb)
        {
            Boolean IsHSEMember = false;
            try
            {
                string groupName = Utility.GetValueByKey("MasterGroup");
                var spGroup = oSPWeb.Groups[groupName];

                if (spGroup != null)
                {
                    IsHSEMember = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->CheckCurrentUserIsHSEMember)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return IsHSEMember;
        }

        private bool CheckCurrentUserIsSupervisor(SPWeb oSPWeb)
        {
            Boolean IsSupervisor = false;
            try
            {
                //var spGroup = oSPWeb.Groups.GetByName("Supervisor");
                //if (spGroup != null)
                //{
                //    IsSupervisor = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                //}

                string groupName = Utility.GetValueByKey("SupervisorGroup");
                var spGroup = oSPWeb.Groups[groupName];

                if (spGroup != null)
                {
                    IsSupervisor = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->CheckCurrentUserIsSupervisor)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return IsSupervisor;
        }

        private bool CheckCurrentUserIsAdmin(SPWeb oSPWeb)
        {
            Boolean IsSupervisor = false;
            try
            {
                //var spGroup = oSPWeb.Groups.GetByName("Supervisor");
                //if (spGroup != null)
                //{
                //    IsSupervisor = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                //}

                string groupName = Utility.GetValueByKey("AdminGroup");
                var spGroup = oSPWeb.Groups[groupName];

                if (spGroup != null)
                {
                    IsSupervisor = oSPWeb.IsCurrentUserMemberOfGroup(spGroup.ID);
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->CheckCurrentUserIsAdmin)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return IsSupervisor;
        }

        private void DisableFieldsForNewPerson()
        {

            this.MORemarks_ta.Disabled = true;
            this.MORemarks_ta.Visible = false;
            this.MORemarks_ldl.Visible = false;
            this.MORemarks_str.Visible = false;
            this.btnMOSave.Visible = false;
            //  this.btnSupervisorSave.Visible = false;
        }

        private void DisableUnableFieldsForMO()
        {


            this.IncidentCategory_ddl.Visible = false;

            this.IncidentCategory_ta.Visible = true;

            this.IncidentCategory_ta.Disabled = true;

            this.InjuryCategory_ddl.Visible = false;

            this.InjuryCategory_ta.Visible = true;

            this.InjuryCategory_ta.Disabled = true;

            this.EmployeeType_ddl.Disabled = true;

            this.ConsentTaken_ddl.Disabled = true;

            this.MOName_div.Attributes.Add("style", "display:none");

            this.MOFields_div.Attributes.Add("style", "display:normal");

            this.MOName_tf.Visible = true;

            this.MOName_tf.Disabled = true;

            this.Date_dtc.Enabled = false;

            this.MOName_PeopleEditor.Enabled = false;

            this.DateOfIncident_dtc.Enabled = false;

            this.TimeOfIncident_dtc.Enabled = false;

            this.Unit_Area_ddl.Disabled = true;

            this.IncidentScore_tf.Disabled = true;

            this.SubmissionDate_dtc.Enabled = false;

            this.Title_tf.Disabled = true;

            this.Description_ta.Disabled = true;

            this.ActionTaken_ta.Disabled = true;

            this.SubmittedBy_div.Attributes.Add("style", "display:none");

            this.SubmittedBy_tf.Visible = true;

            this.SubmittedBy_tf.Disabled = true;

            this.ReportRequired_cb.Disabled = true;

            this.TeamRequired_cb.Disabled = true;

            this.btnSaveAsDraft.Visible = false;

            this.btnSave.Visible = false;

            this.btnMOSave.Visible = true;


        }

        private void DisableUnableFieldsForAssigneeHSEMembers()
        {

            //this.IncidentCategory_ddl.Visible = false;

            //this.IncidentCategory_ta.Visible = true;

            //this.IncidentCategory_ta.Disabled = true;

            //this.InjuryCategory_ddl.Visible = false;

            //this.InjuryCategory_ta.Visible = true;

            //this.InjuryCategory_ta.Disabled = true;

            //this.EmployeeType_ddl.Disabled = true;

            //this.ConsentTaken_ddl.Disabled = true;

            //this.MOName_div.Attributes.Add("style", "display:none");

            //this.MOName_tf.Visible = true;

            //this.MOName_tf.Disabled = true;

            //this.Date_dtc.Enabled = false;

            //this.MOName_PeopleEditor.Enabled = false;

            //this.DateOfIncident_dtc.Enabled = false;

            //this.TimeOfIncident_dtc.Enabled = false;

            //this.Unit_Area_ddl.Disabled = true;

            //this.IncidentScore_tf.Disabled = true;



            //this.SubmissionDate_dtc.Enabled = false;

            //this.Title_tf.Disabled = true;

            //this.Description_ta.Disabled = true;

            //this.ActionTaken_ta.Disabled = true;

            //this.SubmittedBy_div.Attributes.Add("style", "display:none");

            //this.SubmittedBy_tf.Visible = true;

            //this.SubmittedBy_tf.Disabled = true;

            //this.ReportRequired_cb.Disabled = true;

            //this.TeamRequired_cb.Disabled = true;

            this.btnSaveAsDraft.Visible = false;

            this.btnSave.Visible = false;

            this.btnHSESave.Visible = true;

            this.MOName_div.Attributes.Add("style", "display:normal");

            this.fileUploadControl.Visible = false;

            this.MORemarks_ldl.Visible = true;

            this.MORemarks_ta.Visible = true;

            this.MORemarks_ta.Disabled = true;

            this.hdnFilesNames.Visible = true;


        }

        private void DisableUnableFieldsForHSEMembers()
        {

            this.IncidentCategory_ddl.Visible = false;

            this.IncidentCategory_ta.Visible = true;

            this.IncidentCategory_ta.Disabled = true;

            this.InjuryCategory_ddl.Visible = false;

            this.InjuryCategory_ta.Visible = true;

            this.InjuryCategory_ta.Disabled = true;

            this.EmployeeType_ddl.Disabled = true;

            this.ConsentTaken_ddl.Disabled = true;

            this.MOName_div.Attributes.Add("style", "display:none");

            this.MOName_tf.Visible = true;

            this.MOName_tf.Disabled = true;

            this.Date_dtc.Enabled = false;

            this.fileUploadControl.Visible = false;
            this.MOName_PeopleEditor.Enabled = false;

            this.DateOfIncident_dtc.Enabled = false;

            this.TimeOfIncident_dtc.Enabled = false;

            this.Unit_Area_ddl.Disabled = true;

            this.IncidentScore_tf.Disabled = true;

            this.SubmissionDate_dtc.Enabled = false;

            this.Title_tf.Disabled = true;

            this.Description_ta.Disabled = true;

            this.ActionTaken_ta.Disabled = true;

            this.SubmittedBy_div.Attributes.Add("style", "display:none");

            this.SubmittedBy_tf.Visible = true;

            this.SubmittedBy_tf.Disabled = true;

            this.ReportRequired_cb.Disabled = true;

            this.TeamRequired_cb.Disabled = true;

            this.btnSaveAsDraft.Visible = false;

            this.btnSave.Visible = false;

            //  this.btnHSESave.Visible = true;

            this.MORemarks_ldl.Visible = true;

            this.MORemarks_ta.Visible = true;

            this.MORemarks_ta.Disabled = true;



        }


        //private void DisableUnableFieldsForSupervisor()
        //{

        //    this.IncidentCategory_ddl.Visible = false;

        //    this.IncidentCategory_ta.Visible = true;

        //    this.IncidentCategory_ta.Disabled = true;

        //    this.InjuryCategory_ddl.Visible = false;

        //    this.InjuryCategory_ta.Visible = true;

        //    this.InjuryCategory_ta.Disabled = true;

        //    this.EmployeeType_ddl.Disabled = true;

        //    this.ConsentTaken_ddl.Disabled = true;

        //    this.MOName_div.Attributes.Add("style", "display:none");

        //    this.MOName_tf.Visible = true;

        //    this.MOName_tf.Disabled = true;

        //    this.Date_dtc.Enabled = false;

        //    this.MOName_PeopleEditor.Enabled = false;

        //    this.DateOfIncident_dtc.Enabled = false;

        //    this.TimeOfIncident_dtc.Enabled = false;

        //    this.Unit_Area_ddl.Disabled = true;

        //    this.IncidentScore_tf.Disabled = true;

        //    this.btnHSESave.Attributes.Add("style", "display:none");

        //    this.SubmissionDate_dtc.Enabled = false;

        //    this.Title_tf.Disabled = true;

        //    this.Description_ta.Disabled = true;

        //    this.ActionTaken_ta.Disabled = true;

        //    this.SubmittedBy_div.Attributes.Add("style", "display:none");

        //    this.SubmittedBy_tf.Visible = true;

        //    this.SubmittedBy_tf.Disabled = true;

        //    this.ReportRequired_cb.Disabled = true;

        //    this.TeamRequired_cb.Disabled = true;

        //    this.btnSaveAsDraft.Visible = false;

        //    this.btnSave.Visible = false;

        //    this.btnMOSave.Visible = false;

        //    this.MORemarks_ldl.Visible = true;

        //    this.MORemarks_ta.Visible = true;

        //    this.MORemarks_ta.Disabled = true;

        //    this.HSEManagerName_div.Attributes.Add("style", "display:none");

        //    this.btnHSESave.Visible = true;


        //}

        private void FillArea(SPWeb oSPWeb)
        {


            try
            {

                string listName = "Area";

                // Fetch the List
                SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                SPQuery query = new SPQuery();
                SPListItemCollection spListItems;
                // Include only the fields you will use.
                query.ViewFields = "<FieldRef Name='ID'/><FieldRef Name='Title'/>";
                query.ViewFieldsOnly = true;
                //query.RowLimit = 200; // Only select the top 200.
                StringBuilder sb = new StringBuilder();
                sb.Append("<OrderBy Override='TRUE';><FieldRef Name='Title'/></OrderBy>");
                query.Query = sb.ToString();
                spListItems = spList.GetItems(query);

                this.Unit_Area_ddl.DataSource = spListItems;
                this.Unit_Area_ddl.DataTextField = "Title";
                this.Unit_Area_ddl.DataValueField = "Title";
                this.Unit_Area_ddl.DataBind();

                this.Unit_Area_ddl.Items.Insert(0, new ListItem("Please Select", "0"));

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->FillArea)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        private void FillIncidentCategory(SPWeb oSPWeb)
        {


            try
            {
                string listName = "IncidentCategory";

                // Fetch the List
                SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                SPQuery query = new SPQuery();
                SPListItemCollection spListItems;
                // Include only the fields you will use.
                query.ViewFields = "<FieldRef Name='ID'/><FieldRef Name='Title'/>";
                query.ViewFieldsOnly = true;
                //query.RowLimit = 200; // Only select the top 200.
                StringBuilder sb = new StringBuilder();
                sb.Append("<OrderBy Override='TRUE';><FieldRef Name='Title'/></OrderBy>");
                query.Query = sb.ToString();
                spListItems = spList.GetItems(query);

                this.IncidentCategory_ddl.DataSource = spListItems;
                this.IncidentCategory_ddl.DataTextField = "Title";
                this.IncidentCategory_ddl.DataValueField = "Title";
                this.IncidentCategory_ddl.DataBind();

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->FillIncidentCategory)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }


        private void FillInjuryCategory(SPWeb oSPWeb)
        {
            try
            {
                string listName = "InjuryCategory";

                // Fetch the List
                SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                SPQuery query = new SPQuery();
                SPListItemCollection spListItems;
                // Include only the fields you will use.
                query.ViewFields = "<FieldRef Name='ID'/><FieldRef Name='Title'/>";
                query.ViewFieldsOnly = true;
                //query.RowLimit = 200; // Only select the top 200.
                StringBuilder sb = new StringBuilder();
                sb.Append("<OrderBy Override='TRUE';><FieldRef Name='Title'/></OrderBy>");
                query.Query = sb.ToString();
                spListItems = spList.GetItems(query);

                this.InjuryCategory_ddl.DataSource = spListItems;
                this.InjuryCategory_ddl.DataTextField = "Title";
                this.InjuryCategory_ddl.DataValueField = "Title";
                this.InjuryCategory_ddl.DataBind();
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->FillInjuryCategory)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }


        private void LoadPageFromDraft(SPListItem imiItem, SPWeb oSPWeb, String IRID)
        {

            try
            {


                if (imiItem != null)
                {
                    if (imiItem.Attachments.Count > 0)
                    {
                        foreach (String attachmentname in imiItem.Attachments)
                        {
                            String attachmentAbsoluteURL =
                            imiItem.Attachments.UrlPrefix // gets the containing directory URL
                            + attachmentname;
                            // To get the SPSile reference to the attachment just use this code
                            SPFile attachmentFile = oSPWeb.GetFile(attachmentAbsoluteURL);

                            StringBuilder sb = new StringBuilder();

                            HtmlTableRow tRow = new HtmlTableRow();

                            HtmlTableCell removeLink = new HtmlTableCell();
                            HtmlTableCell fileLink = new HtmlTableCell();

                            sb.Append(String.Format("<a href='{0}/{1}' target='_blank'>{2}</a>", oSPWeb.Url, attachmentFile.Url, attachmentname));
                            removeLink.InnerHtml = "<span class='btn-danger removeLink' style='padding:3px; margin-right:3px; border-radius:2px;'><i class='glyphicon glyphicon-remove'></i></span><span class='fileName' style='display:none;'>" + attachmentFile.Name + "</span>";

                            fileLink.InnerHtml = sb.ToString();

                            tRow.Cells.Add(removeLink);
                            tRow.Cells.Add(fileLink);

                            this.grdAttachments.Rows.Add(tRow);
                        }

                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["IncidentScore"])))
                    {

                        this.IncidentScore_tf.Value = Convert.ToString(imiItem["IncidentScore"]);

                        this.IncidentScore_tf.Disabled = true;
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["IncidentCategory"])))
                    {
                        String s = Convert.ToString(imiItem["IncidentCategory"]);

                        string[] IncidentCategoryItem = s.Split(',');

                        this.IncidentCategory_hdn.Value = s;

                        this.IncidentCategory_ta.Value = s;

                        foreach (string Item in IncidentCategoryItem)
                        {
                            this.IncidentCategory_ddl.Items.FindByValue(Item).Selected = true;

                        }

                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["InjuryCategory"])))
                    {
                        String s = Convert.ToString(imiItem["InjuryCategory"]);



                        string[] InjuryCategoryItem = s.Split(',');

                        this.InjuryCategory_hdn.Value = s;

                        this.InjuryCategory_ta.Value = s;
                        foreach (string Item in InjuryCategoryItem)
                        {
                            this.InjuryCategory_ddl.Items.FindByValue(Item).Selected = true;



                        }

                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["EmployeeType"])))

                        this.EmployeeType_ddl.Value = Convert.ToString(imiItem["EmployeeType"]);




                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["ConsentTaken"])))

                        this.ConsentTaken_ddl.Value = Convert.ToString(imiItem["ConsentTaken"]);




                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["DateOfConsentTaken"])))
                    {
                        DateTime Date;
                        bool bValid = DateTime.TryParse(Convert.ToString(imiItem["DateOfConsentTaken"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                        if (!bValid)
                        {
                            Date = Convert.ToDateTime(imiItem["DateOfConsentTaken"]);
                        }

                        this.Date_dtc.SelectedDate = Date;
                    }





                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["MOName"])))
                    {


                        PeopleEditor pe = new PeopleEditor();
                        PickerEntity UserEntity = new PickerEntity();
                        String username = Convert.ToString(imiItem["MOName"]);
                        //get Spuser
                        SPUser SPuser = GetUser(oSPWeb, username, null, 0);
                        if (SPuser != null)
                        {
                            // CurrentUser is SPUser object
                            UserEntity.DisplayText = SPuser.Name;
                            UserEntity.Key = SPuser.LoginName;

                            UserEntity = pe.ValidateEntity(UserEntity);

                            // Add PickerEntity to People Picker control
                            this.MOName_PeopleEditor.AddEntities(new List<PickerEntity> { UserEntity });

                            this.MOName_tf.Value = SPuser.Name;
                        }



                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["DateOfIncident"])))
                    {
                        DateTime Date;
                        bool bValid = DateTime.TryParse(Convert.ToString(imiItem["DateOfIncident"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                        if (!bValid)
                        {
                            Date = Convert.ToDateTime(imiItem["DateOfIncident"]);
                        }

                        this.DateOfIncident_dtc.SelectedDate = Date;
                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["TimeOfIncident"])))

                        this.TimeOfIncident_dtc.SelectedDate = Convert.ToDateTime(imiItem["TimeOfIncident"]);


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["Unit_x002f_Area"])))
                    {
                        this.Unit_Area_ddl.Items.FindByValue(Convert.ToString(imiItem["Unit_x002f_Area"])).Selected = true;
                        this.Unit_Area_hdn.Value = Convert.ToString(imiItem["Unit_x002f_Area"]);
                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["IncidentScore"])))
                        this.IncidentScore_tf.Value = Convert.ToString(imiItem["IncidentScore"]);



                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["SubmittedBy"])))
                    {
                        PeopleEditor pe = new PeopleEditor();
                        PickerEntity UserEntity = new PickerEntity();
                        String username = Convert.ToString(imiItem["SubmittedBy"]);
                        //get Spuser
                        SPUser SPuser = GetUser(oSPWeb, username, null, 0);
                        if (SPuser != null)
                        {
                            // CurrentUser is SPUser object
                            UserEntity.DisplayText = SPuser.Name;
                            UserEntity.Key = SPuser.LoginName;

                            UserEntity = pe.ValidateEntity(UserEntity);

                            // Add PickerEntity to People Picker control
                            this.SubmittedBy_PeopleEditor.AddEntities(new List<PickerEntity> { UserEntity });

                            this.SubmittedBy_tf.Value = SPuser.Name;
                        }

                    }

                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["DateOFSubmission"])))
                    {
                        DateTime Date;
                        bool bValid = DateTime.TryParse(Convert.ToString(imiItem["DateOFSubmission"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                        if (!bValid)
                        {
                            Date = Convert.ToDateTime(imiItem["DateOFSubmission"]);
                        }

                        this.SubmissionDate_dtc.SelectedDate = Date;
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["TitleOfIncident"])))

                        this.Title_tf.Value = Convert.ToString(imiItem["TitleOfIncident"]);


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["IncidentDescription"])))

                        this.Description_ta.Value = Convert.ToString(imiItem["IncidentDescription"]);

                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["ActionTaken"])))

                        this.ActionTaken_ta.Value = Convert.ToString(imiItem["ActionTaken"]);


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["IsDetailedReportRequired?"])))

                        if (Convert.ToString(imiItem["IsDetailedReportRequired?"]) == "Yes")
                            this.ReportRequired_cb.Checked = true;
                        else
                            this.ReportRequired_cb.Checked = false;
                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["IsInvestigationTeamRequired?"])))

                        if (Convert.ToString(imiItem["IsInvestigationTeamRequired?"]) == "Yes")
                            this.TeamRequired_cb.Checked = true;
                        else
                            this.TeamRequired_cb.Checked = false;


                    if (!String.IsNullOrEmpty(Convert.ToString(imiItem["MORemarks"])))

                        this.MORemarks_ta.Value = Convert.ToString(imiItem["MORemarks"]);

                }

            }

            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm>LoadPageFromDraft)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        private SPUser GetUser(SPWeb oSPWeb, string username = null, string email = null, int userId = 0)
        {
            SPUser spUser = null;
            try
            {
                if (oSPWeb != null)
                {
                    if (!String.IsNullOrEmpty(username))
                    {
                        string temp = "i:0#.w|" + username;
                        spUser = oSPWeb.AllUsers[temp];
                    }
                    else if (!String.IsNullOrEmpty(email))
                    {
                        spUser = oSPWeb.AllUsers.GetByEmail(email);
                    }
                    else if (userId > 0)
                    {
                        spUser = oSPWeb.AllUsers.GetByID(userId);
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->GetUser)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return spUser;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            //SPListItemCollection IR_1infoList = oWebSite.Lists["IR-1"].Items;

                            string listName = "IR-1";

                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));
                            SPListItem spListItem = null;

                            String IRID = Page.Request.QueryString["IRID"];
                            int ItemID = Convert.ToInt32(IRID);

                            if (ItemID != 0 && list != null)
                            {

                                spListItem = list.Items.GetItemById(ItemID);

                                if (spListItem != null)
                                {

                                    UpdateIR_1Values(spListItem, false, oWebSite, false);

                                }
                            }

                            else if (list != null)
                            {
                                spListItem = list.Items.Add();


                                if (spListItem != null)
                                {

                                    UpdateIR_1Values(spListItem, false, oWebSite, false);

                                }
                            }
                        }
                        string redirectUrl = Utility.GetRedirectUrl("WorkQueue_Redirect");

                        DisableControls(true);
                        if (!String.IsNullOrEmpty(redirectUrl))
                        {
                            Page.Response.Redirect(redirectUrl, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->btnSave_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string redirectUrl = Utility.GetRedirectUrl("WorkQueue_Redirect");

                DisableControls(true);
                if (!String.IsNullOrEmpty(redirectUrl))
                {
                    Page.Response.Redirect(redirectUrl, false);
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->btnCancel_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);

            }
        }

        protected void btnSaveAsDraft_Click(object sender, EventArgs e)
        {

            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            //SPListItemCollection IR_1infoList = oWebSite.Lists["IR-1"].Items;

                            string listName = "IR-1";

                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));
                            SPListItem spListItem = null;
                            String IRID = Page.Request.QueryString["IRID"];
                            int ItemID = Convert.ToInt32(IRID);

                            if (ItemID != 0 && list != null)
                            {

                                spListItem = list.Items.GetItemById(ItemID);

                                if (spListItem != null)
                                {

                                    UpdateIR_1Values(spListItem, true, oWebSite, false);

                                }
                            }


                            else if (list != null)
                            {
                                spListItem = list.Items.Add();


                                if (spListItem != null)
                                {

                                    UpdateIR_1Values(spListItem, true, oWebSite, false);

                                }
                            }
                        }
                        string redirectUrl = Utility.GetRedirectUrl("WorkQueue_Redirect");

                        DisableControls(true);
                        if (!String.IsNullOrEmpty(redirectUrl))
                        {
                            Page.Response.Redirect(redirectUrl, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm>btnSaveAsDraft_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        protected void btnHSE_Click(object sender, EventArgs e)
        {

            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            //SPListItemCollection IR_1infoList = oWebSite.Lists["IR-1"].Items;

                            string listName = "IR-1";

                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));
                            SPListItem spListItem = null;

                            String IRID = Page.Request.QueryString["IRID"];
                            int ItemID = Convert.ToInt32(IRID);

                            if (ItemID != 0 && list != null)
                            {

                                spListItem = list.Items.GetItemById(ItemID);

                                if (spListItem != null)
                                {

                                    UpdateIR_1Values(spListItem, false, oWebSite, true);

                                }
                            }

                            else if (list != null)
                            {
                                spListItem = list.Items.Add();


                                if (spListItem != null)
                                {

                                    UpdateIR_1Values(spListItem, false, oWebSite, true);

                                }
                            }
                        }
                        string redirectUrl = Utility.GetRedirectUrl("WorkQueue_Redirect");

                        DisableControls(true);
                        if (!String.IsNullOrEmpty(redirectUrl))
                        {
                            Page.Response.Redirect(redirectUrl, false);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->btnHSE_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }


        }

        protected void btnMOSave_Click(object sender, EventArgs e)
        {

            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            //SPListItemCollection IR_1infoList = oWebSite.Lists["IR-1"].Items;

                            string listName = "IR-1";

                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));
                            SPListItem spListItem = null;
                            String IRID = Page.Request.QueryString["IRID"];
                            int ItemID = Convert.ToInt32(IRID);

                            if (ItemID != 0 && list != null)
                            {

                                spListItem = list.Items.GetItemById(ItemID);

                                if (spListItem != null)
                                {
                                    if (!String.IsNullOrEmpty(Convert.ToString(this.MORemarks_ta.Value)))
                                        spListItem["MORemarks"] = Convert.ToString(this.MORemarks_ta.Value);


                                    if (!String.IsNullOrEmpty(this.hdnFilesNames.Value))
                                    {
                                        var fileNames = hdnFilesNames.Value.Split('~');

                                        foreach (var item in fileNames)
                                        {
                                            if (!String.IsNullOrEmpty(item))
                                            {
                                                spListItem.Attachments.Delete(item);
                                            }
                                        }
                                    }
                                    //-->>
                                    if (this.fileUploadControl.HasFiles)
                                    {
                                        foreach (var uploadedFile in fileUploadControl.PostedFiles)
                                        {
                                            Stream fs = uploadedFile.InputStream;
                                            byte[] _bytes = new byte[fs.Length];
                                            fs.Position = 0;
                                            fs.Read(_bytes, 0, (int)fs.Length);
                                            fs.Close();
                                            fs.Dispose();

                                            spListItem.Attachments.Add(uploadedFile.FileName, _bytes);
                                        }
                                    }

                                    String User = oWebSite.CurrentUser.LoginName;
                                    String[] Name = User.Split('|');

                                    if (Name.Length > 1)
                                    {
                                        spListItem["SubmittedBy"] = Name[1];
                                        spListItem["AuditedBy"] = Name[1];
                                    }
                                    spListItem["Status"] = "Completed";


                                    spListItem.Update();

                                    //  SendEmailToSupervisor(spListItem);
                                    SendEmailToHSE(spListItem);

                                    //UpdateIR_1Values(spListItem, true, oWebSite);

                                }
                            }


                            //else if (list != null)
                            //{
                            //    spListItem = list.Items.Add();


                            //    if (spListItem != null)
                            //    {
                            //        if (!String.IsNullOrEmpty(Convert.ToString(this.MORemarks_ta.Value)))
                            //            spListItem["MORemarks"] = Convert.ToString(this.MORemarks_ta.Value);

                            //            spListItem.Update();

                            //    }
                            //}
                        }
                        string redirectUrl = Utility.GetRedirectUrl("WorkQueue_Redirect");

                        DisableControls(true);
                        if (!String.IsNullOrEmpty(redirectUrl))
                        {
                            Page.Response.Redirect(redirectUrl, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->btnMOSave_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }
        //-->

        protected void UpdateIR_1Values(SPListItem imiItem, Boolean IsSaveAsDraft, SPWeb oWebSite, Boolean HSE)
        {
            try
            {
                bool IsInjury = false;

                if (imiItem != null)
                {


                    if (!String.IsNullOrEmpty(Convert.ToString(this.IncidentCategory_hdn.Value)))
                    {
                        imiItem["IncidentCategory"] = Convert.ToString(this.IncidentCategory_hdn.Value);

                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(this.InjuryCategory_hdn.Value)))
                    {
                        imiItem["InjuryCategory"] = Convert.ToString(this.InjuryCategory_hdn.Value);

                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(this.EmployeeType_ddl.SelectedIndex)) && this.EmployeeType_ddl.SelectedIndex > 0)
                        imiItem["EmployeeType"] = Convert.ToString(this.EmployeeType_ddl.Items[this.EmployeeType_ddl.SelectedIndex]);


                    if (!String.IsNullOrEmpty(Convert.ToString(this.ConsentTaken_ddl.SelectedIndex)) && this.ConsentTaken_ddl.SelectedIndex > 0)
                        imiItem["ConsentTaken"] = Convert.ToString(this.ConsentTaken_ddl.Items[this.ConsentTaken_ddl.SelectedIndex]);



                    if (!String.IsNullOrEmpty(Convert.ToString(this.Date_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.Date_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            imiItem["DateOfConsentTaken"] = date;
                        else
                            imiItem["DateOfConsentTaken"] = Convert.ToDateTime(this.Date_dtc.SelectedDate);
                    }




                    if (this.MOName_PeopleEditor.ResolvedEntities != null && this.MOName_PeopleEditor.ResolvedEntities.Count > 0)
                    {
                        PickerEntity MOentity = (PickerEntity)this.MOName_PeopleEditor.ResolvedEntities[0];

                        imiItem["MOName"] = MOentity.Claim.Value;
                    }




                    if (!String.IsNullOrEmpty(Convert.ToString(this.DateOfIncident_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.DateOfIncident_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            imiItem["DateOfIncident"] = date;
                        else
                            imiItem["DateOfIncident"] = Convert.ToDateTime(this.DateOfIncident_dtc.SelectedDate);
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.TimeOfIncident_dtc.SelectedDate)))
                        imiItem["TimeOfIncident"] = this.TimeOfIncident_dtc.SelectedDate.ToShortTimeString();
                    else
                        imiItem["TimeOfIncident"] = null;

                    if (!String.IsNullOrEmpty(Convert.ToString(this.Unit_Area_hdn.Value)))
                        imiItem["Unit/Area"] = (Convert.ToString(this.Unit_Area_hdn.Value));

                    if (!String.IsNullOrEmpty(Convert.ToString(this.IncidentScore_tf.Value)))
                        imiItem["IncidentScore"] = Convert.ToString(this.IncidentScore_tf.Value);


                    if (this.SubmittedBy_PeopleEditor.ResolvedEntities != null && this.SubmittedBy_PeopleEditor.ResolvedEntities.Count > 0)
                    {
                        PickerEntity entity = (PickerEntity)this.SubmittedBy_PeopleEditor.ResolvedEntities[0];

                        imiItem["SubmittedBy"] = entity.Claim.Value;
                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(this.SubmissionDate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.SubmissionDate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            imiItem["DateOFSubmission"] = date;
                        else
                            imiItem["DateOFSubmission"] = Convert.ToDateTime(this.SubmissionDate_dtc.SelectedDate);
                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(this.Title_tf.Value)))
                        imiItem["TitleOfIncident"] = Convert.ToString(this.Title_tf.Value);

                    if (!String.IsNullOrEmpty(Convert.ToString(this.Description_ta.Value)))
                        imiItem["IncidentDescription"] = Convert.ToString(this.Description_ta.Value);

                    if (!String.IsNullOrEmpty(Convert.ToString(this.ActionTaken_ta.Value)))
                        imiItem["ActionTaken"] = Convert.ToString(this.ActionTaken_ta.Value);

                    if (!String.IsNullOrEmpty(Convert.ToString(this.ReportRequired_cb.Checked)))
                        if (Convert.ToInt32(this.ReportRequired_cb.Checked) == 1)
                            imiItem["IsDetailedReportRequired?"] = "Yes";
                        else
                            imiItem["IsDetailedReportRequired?"] = "No";


                    if (!String.IsNullOrEmpty(Convert.ToString(this.TeamRequired_cb.Checked)))
                        if (Convert.ToInt32(this.TeamRequired_cb.Checked) == 1)
                            imiItem["IsInvestigationTeamRequired?"] = "Yes";
                        else
                            imiItem["IsInvestigationTeamRequired?"] = "No";


                    String User = oWebSite.CurrentUser.LoginName;
                    String[] Name = User.Split('|');

                    if (Name.Length > 1)
                        imiItem["AuditedBy"] = Name[1];

                    if (IsSaveAsDraft)
                    {
                        if (Name.Length > 1)
                            imiItem["Assignee"] = Name[1];

                        imiItem["IsSaveAsDraft"] = true;

                        imiItem["Status"] = "Inprogress";

                        imiItem.Update();
                    }
                    else if (HSE)
                    {
                        imiItem.Update();
                    }
                    else
                    {


                        imiItem["IsSaveAsDraft"] = false;



                        if (!String.IsNullOrEmpty(Convert.ToString(this.IncidentCategory_hdn.Value)))
                        {
                            String s = Convert.ToString(this.IncidentCategory_hdn.Value);

                            string[] IncidentCategoryItem = s.Split(',');

                            foreach (String Item in IncidentCategoryItem)
                            {
                                if (Item == "Injury")
                                    IsInjury = true;
                            }

                        }






                        if (IsInjury)
                        {
                            SPUser spMO = Utility.GetUser(oWebSite, Convert.ToString(imiItem["MOName"]));

                            User = spMO.LoginName;
                            Name = User.Split('|');
                            if (Name.Length > 1)
                                imiItem["Assignee"] = Name[1];

                            imiItem.Update();

                            SendEmailToMO(imiItem);
                        }
                        else
                        {

                            imiItem["Status"] = "Completed";

                            imiItem.Update();
                            // SendEmailToSupervisor(imiItem);
                            SendEmailToHSE(imiItem);



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->UpdateIR_1Values)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        //-->

        protected void SendEmailToMO(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            string IR_1Link = Utility.GetRedirectUrl("IR1FormLink");
                            string subject = Utility.GetValueByKey("MOEmailSubject");
                            string body = Utility.GetValueByKey("MOEmailTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append("?IRID=")
                                        .Append(imiItem.ID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();
                            Message message = new Message();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));

                            SPUser spMO = Utility.GetUser(oWebSite, Convert.ToString(imiItem["MOName"]));

                            message.To = spMO.Email;
                            message.From = spSender.Email;
                            message.Subject = subject;
                            message.Body = body;

                            Email.SendEmail(message);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->SendEmailToMO)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void SendEmailToSupervisor(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            string listName = "Area";

                            SPList spList = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            SPQuery query = new SPQuery();
                            SPListItemCollection spListItems;

                            query.ViewFields = "<FieldRef Name='HOA' /><FieldRef Name='HOAEmail' /><FieldRef Name='Title' />";
                            query.ViewFieldsOnly = true;

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Convert.ToString(imiItem["Unit/Area"]) + "</Value></Eq></Where><OrderBy><FieldRef Name='Title' /></OrderBy>");
                            query.Query = sb.ToString();
                            spListItems = spList.GetItems(query);

                            String SupervisorEmail = null;
                            String SupervisorName = null;

                            foreach (SPListItem IR_1item in spListItems)
                            {
                                SupervisorEmail = IR_1item["HOAEmail"].ToString();
                                SupervisorName = IR_1item["HOA"].ToString();
                            }

                            string IR_1Link = Utility.GetRedirectUrl("IR1FormLink");
                            string subject = Utility.GetValueByKey("SupervisorEmailSubject");
                            string body = Utility.GetValueByKey("SupervisorEmailTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append("?IRID=")
                                        .Append(imiItem.ID);

                            body = linkSB.ToString();


                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));
                            imiItem["Assignee"] = SupervisorName;
                            imiItem.Update();

                            Message message = new Message();
                            message.To = SupervisorEmail;
                            message.From = spSender.Email;
                            message.Subject = subject;
                            message.Body = body;

                            Email.SendEmail(message);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->SendEmailToSupervisor)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void SendEmailToHSE(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            string IR_1Link = Utility.GetRedirectUrl("FlashReportLink");
                            string subject = Utility.GetValueByKey("HSEEmailSubject");
                            string body = Utility.GetValueByKey("HSEEmailTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append("?IRID=")
                                        .Append(imiItem.ID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;


                            List<SPUser> Users = new List<SPUser>();


                            Users = GetGroupMembers("MasterGroup");

                            StringBuilder AssigneeUsers = new StringBuilder();

                            //if (!String.IsNullOrEmpty(Convert.ToString(imiItem["Assignee"])))
                            //{
                            //    AssigneeUsers.Append(Convert.ToString(imiItem["Assignee"])).Append(",");
                            //}




                            foreach (SPUser user in Users)
                            {
                                String User = user.LoginName;
                                String[] Name = User.Split('|');


                                if (Name.Length > 1)
                                    AssigneeUsers.Append(Name[1]).Append(",");
                            }

                            AssigneeUsers.Length = AssigneeUsers.Length - 1;

                            imiItem["Assignee"] = AssigneeUsers.ToString();





                            imiItem.Update();

                            AddDummyEntryInFlashReportOffListToAssignTaskToHSE(oWebSite, imiItem, imiItem["Assignee"].ToString());

                            foreach (SPUser user in Users)
                            {

                                String User = user.LoginName;
                                String[] Name = User.Split('|');
                                if (Name.Length > 1)
                                {
                                    SPUser spMO = Utility.GetUser(oWebSite, Name[1]);
                                    message.To = spMO.Email;
                                    Email.SendEmail(message);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->SendEmailToHSE)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected List<SPUser> GetGroupMembers(String GroupName)
        {
            List<SPUser> Users = new List<SPUser>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWebSite = oSPsite.OpenWeb())
                        {

                            String groupName = Utility.GetValueByKey(GroupName);
                            SPGroup Group = oWebSite.Groups[groupName];

                            foreach (SPUser user in Group.Users)
                            {
                                // add all the group users to the list
                                Users.Add(user);
                            }

                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->GetGroupMembers)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return Users;
        }

        protected void AddDummyEntryInFlashReportOffListToAssignTaskToHSE(SPWeb oWebSite, SPListItem IR1Item, String AllHSEMembers)
        {

            try
            {
                if (oWebSite != null)
                {
                    string listName = "FlashReport";

                    SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                    if (list != null)
                    {
                        SPListItem FRListItem = list.Items.Add();

                        if (FRListItem != null)
                        {

                            FRListItem["Assignee"] = AllHSEMembers;
                            FRListItem["IsSaveAsDraft"] = false;
                            FRListItem["Status"] = "Inprogress";
                            FRListItem["IRID"] = IR1Item["ID"];
                            //IR5ListItem["Unit/Area"] = FRItem["Unit/Section"];
                            //IR5ListItem["TargetDate"] = FRItem["TargetDate"];
                            //IR5ListItem["DateOfIncident"] = FRItem["DateOfIncident"];                            
                            //IR5ListItem["TimeOfIncident"] = FRItem["TimeOfIncident"];
                            //IR5ListItem["SubmittedBy"] = FRItem["SubmittedBy"];

                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["DateOfIncident"])))
                                FRListItem["SubmittedBy"] = IR1Item["SubmittedBy"];

                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["DateOfIncident"])))

                                FRListItem["DateOfIncident"] = Convert.ToDateTime(IR1Item["DateOfIncident"]);

                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["TimeOfIncident"])))

                                FRListItem["TimeOfIncident"] = Convert.ToDateTime(IR1Item["TimeOfIncident"]);


                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["Unit_x002f_Area"])))
                            {

                                FRListItem["Unit/Section"] = Convert.ToString(IR1Item["Unit_x002f_Area"]);
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["Unit_x002f_Area"])))
                            {

                                FRListItem["ResponcibleSection/Unit"] = Convert.ToString(IR1Item["Unit_x002f_Area"]);
                            }




                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["DateOFSubmission"])))
                            {
                                FRListItem["IR-1ReceivingDate"] = Convert.ToDateTime(IR1Item["DateOFSubmission"]);
                            }


                            FRListItem["FlashIssueDate"] = System.DateTime.Now;


                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["Description"])))

                                FRListItem["DescriptionOfIncident"] = Convert.ToString(IR1Item["Description"]);


                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["ActionTaken"])))

                                FRListItem["ActionTaken"] = Convert.ToString(IR1Item["ActionTaken"]);


                            if (!String.IsNullOrEmpty(Convert.ToString(IR1Item["IncidentScore"])))

                                FRListItem["IncidentScore"] = Convert.ToString(IR1Item["IncidentScore"]);

                            FRListItem.Update();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->AddDummyEntryInFlashReportOffListToAssignTaskToHSE)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }
        protected void AddDummyEntryInIR01ListToAssignTaskTo(SPWeb oWebSite, SPListItem IR02ListItem)
        {

            try
            {
                if (oWebSite != null)
                {
                    String listName = "IR-1";

                    SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                    if (list != null)
                    {
                        SPListItem IR1ListItem = list.Items.Add();

                        if (IR1ListItem != null)
                        {
                            Message message = new Message();
                            String UserName = null;
                            String User = oWebSite.CurrentUser.LoginName;
                            String[] Name = User.Split('|');
                            if (Name.Length > 1)
                            {
                                UserName = Utility.GetUsername(Name[1], true);
                            }

                            IR1ListItem["Assignee"] = UserName;
                            IR1ListItem["IsSaveAsDraft"] = false;
                            IR1ListItem["Status"] = "Inprogress";
                            IR1ListItem["IR2ID"] = IR02ListItem["ID"];
                            IR1ListItem["IncidentScore"] = IR02ListItem["Total"];
                            IR1ListItem.Update();


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->AddDummyEntryInIR01ListToAssignTaskTo)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        protected void GetValuesFromIR02AndAddItemInIR01()
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            string listName = "IR02";
                            // Fetch the List
                            SPList SPListIR02 = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            SPQuery query = new SPQuery();

                            query.ViewFields = "<ViewFields><FieldRef Name='ID' /><FieldRef Name='Total' /><FieldRef Name='Created' /></ViewFields>";
                            query.ViewFieldsOnly = true;

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<Where><Eq><FieldRef Name='Author' /><Value Type='Integer'><UserID /></Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>");
                            query.Query = sb.ToString();
                            SPListItemCollection spListItems = SPListIR02.GetItems(query);
                            query.RowLimit = 1;

                            if (spListItems != null && spListItems.Count > 0)
                            {
                                foreach (SPListItem item in spListItems)
                                {
                                    AddDummyEntryInIR01ListToAssignTaskTo(oWebSite, item);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IR-1OnJobForm->GetValuesFromIR02AndAddItemInIR01)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }
    }
}
