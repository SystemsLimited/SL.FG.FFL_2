using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using SL.FG.FFL.Layouts.SL.FG.FFL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace SL.FG.FFL.WebParts.WaiverForm
{
    [ToolboxItemAttribute(false)]
    public partial class WaiverForm : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public WaiverForm()
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


                    if (!String.IsNullOrEmpty(Page.Request.QueryString["IR01DI_ID"]))
                    {
                        this.hdnID.Value = Page.Request.QueryString["IR01DI_ID"];
                        this.hdnRecommendationListName.Value = "IIRRecommendationOnJob";
                        this.hdnParentList.Value = "IR01DI";
                        this.hdnFRID.Value = "FlashReportID";
                        this.hdnLink.Value = "?IR01DI_ID=";
                        this.hdnTaskName.Value = "01";


                    }
                    else if (!String.IsNullOrEmpty(Page.Request.QueryString["IR03DI_ID"]))
                    {

                        this.hdnID.Value = Page.Request.QueryString["IR03DI_ID"];
                        this.hdnRecommendationListName.Value = "IIRRecommendationOnJob";
                        this.hdnParentList.Value = "IR03DI";
                        this.hdnFRID.Value = "FlashReportID";
                        this.hdnLink.Value = "?IR03DI_ID=";
                        this.hdnTaskName.Value = "03";

                    }
                    else if (!String.IsNullOrEmpty(Page.Request.QueryString["IR05DI_ID"]))
                    {
                        this.hdnID.Value = Page.Request.QueryString["IR05DI_ID"];
                        this.hdnRecommendationListName.Value = "IIRRecommendation_OffJob";
                        this.hdnParentList.Value = "IR-5-Off";
                        this.hdnFRID.Value = "FRID";
                        this.hdnLink.Value = "?IR05DI_ID=";
                        this.hdnTaskName.Value = "05";
                    }



                    FillSection();
                    FillDepartment();

                    LoadPageOnUserBases(this.hdnID.Value);




                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->Page_Load)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);

            }

        }



        private void LoadPageOnUserBases(String RID)
        {

            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {



                        if (!String.IsNullOrEmpty(RID))
                        {
                            String WID = null;

                            WID = Check1StFromDraft(oSPWeb, RID);

                            String User = CheckUser(oSPWeb, RID, WID);

                            if (WID != null)
                            {
                                if (CheckAssignee(oSPWeb, WID))
                                {
                                    if (User != null && !checkResponsiblePerson(RID))
                                    {
                                        if (User.Equals("UM", StringComparison.OrdinalIgnoreCase))
                                        {

                                            LoadPageFromDraft(oSPWeb, WID);
                                            SetFieldsForUM();

                                        }
                                        else if (User.Equals("DM", StringComparison.OrdinalIgnoreCase))
                                        {

                                            LoadPageFromDraft(oSPWeb, WID);
                                            SetFieldsForDM();
                                        }
                                        else if (User.Equals("DO", StringComparison.OrdinalIgnoreCase))
                                        {

                                            LoadPageFromDraft(oSPWeb, WID);
                                            SetFieldsForDO();
                                        }
                                    }
                                    else
                                    {
                                        this.btnSaveAsDraft.Visible = false;
                                        this.btnSave.Visible = false;

                                        //LoadPageFromDraft(oSPWeb, WID);
                                    }

                                }
                                else
                                {
                                    string accessDeniedUrl = Utility.GetRedirectUrl("Access_Denied");

                                    if (!String.IsNullOrEmpty(accessDeniedUrl))
                                    {
                                        Page.Response.Redirect(accessDeniedUrl, false);
                                    }
                                }

                            }
                            else if (checkResponsiblePerson(RID))
                            {
                                Boolean IsWaiverAvailable = GetValuesFromRecomendationIfWaiverAvailable(RID);
                                if (!IsWaiverAvailable)
                                {
                                    this.waiver_div.Visible = false;
                                    this.waiverNotAvailable_lb.Visible = true;
                                    this.waiverNotAvailable_lb.InnerText = Utility.GetValueByKey("WaiverNotAvailable");
                                }
                            }
                            else
                            {
                                string accessDeniedUrl = Utility.GetRedirectUrl("Access_Denied");

                                if (!String.IsNullOrEmpty(accessDeniedUrl))
                                {
                                    Page.Response.Redirect(accessDeniedUrl, false);
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->LoadPageOnUserBases)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }


        private void SetFieldsForUM()
        {
            this.btnSaveAsDraft.Visible = false;
            this.btnSave.Visible = false;
            this.btnUMSave.Visible = true;
            this.btnUMDisapprive.Visible = true;
            this.UM_div.Attributes.Add("style", "display:mormal");
        }
        private void SetFieldsForDM()
        {
            this.btnSaveAsDraft.Visible = false;
            this.btnSave.Visible = false;
            this.btnDMSave.Visible = true;
            this.btnDMDisapprive.Visible = true;
            this.UM_div.Attributes.Add("style", "display:mormal");
            this.DM_div.Attributes.Add("style", "display:mormal");
        }
        private void SetFieldsForDO()
        {
            this.btnSaveAsDraft.Visible = false;
            this.btnSave.Visible = false;
            this.btnDOSave.Visible = true;
            this.btnDODisapprive.Visible = true;
            this.UM_div.Attributes.Add("style", "display:mormal");
            this.DM_div.Attributes.Add("style", "display:mormal");
            this.DO_div.Attributes.Add("style", "display:mormal");
        }

        private String CheckUser(SPWeb oSPWeb, String RID, String WID)
        {

            String User = null;
            try
            {

                //SPListItemCollection IR_1infoList = oWebSite.Lists["IR-1"].Items;
                string listName = "IRWaiverOff";
                // Fetch the List
                SPList list = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                String Status = null;

                if (WID != null && list != null)
                {

                    int WRID_ItemID = Convert.ToInt32(WID);

                    SPListItem ListItem = list.Items.GetItemById(WRID_ItemID);

                    if (ListItem != null)
                    {
                        Status = Convert.ToString(ListItem["Status"]);
                    }
                }

                SPListItemCollection RInfoList = oSPWeb.Lists[this.hdnRecommendationListName.Value].Items;

                if (RInfoList != null)
                {
                    SPListItem Item = RInfoList.GetItemById(Convert.ToInt32(RID));
                    SPUser CurrentUser = oSPWeb.CurrentUser;
                    String Department = Convert.ToString(Item["ResponsibleDepartment"]);
                    SPUser UMUser = null;
                    SPUser DMUser = null;
                    SPUser DOUser = null;

                    String Role = "Unit Manager";
                    String UMString = GetDepartmentItem(oSPWeb, Department, Role);
                    String[] UM = UMString.Split('~');

                    Role = "HOD";
                    String DMString = GetDepartmentItem(oSPWeb, Department, Role);
                    String[] DM = DMString.Split('~');

                    Role = "DO";
                    String DOString = GetDepartmentItem(oSPWeb, Department, Role);
                    String[] DO = DOString.Split('~');



                    if (UM != null)
                    {
                        String UMName = UM[0];
                        String UMEmail = UM[1];
                        UMUser = Utility.GetUser(oSPWeb, null, UMEmail, 0);
                    }
                    if (DM != null)
                    {

                        String DMName = DM[0];
                        String DMEmail = DM[1];
                        DMUser = Utility.GetUser(oSPWeb, null, DMEmail, 0);
                    }
                    if (DO != null)
                    {
                        String DOName = DO[0];
                        String DOEmail = DO[1];
                        DOUser = Utility.GetUser(oSPWeb, null, DOEmail, 0);
                    }

                    if (Utility.CompareUsers(CurrentUser, DMUser) && Status.Equals("UMApproved", StringComparison.OrdinalIgnoreCase))
                    {
                        User = "DM";
                    }
                    else if (Utility.CompareUsers(CurrentUser, UMUser) && Status.Equals("Inprogress", StringComparison.OrdinalIgnoreCase))
                    {
                        User = "UM";
                    }
                    else if (Utility.CompareUsers(CurrentUser, DOUser) && Status.Equals("DMApproved", StringComparison.OrdinalIgnoreCase))
                    {
                        User = "DO";
                    }


                }


            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->CheckUser)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return User;
        }

        private Boolean CheckAssignee(SPWeb oSPWeb, String WID)
        {
            Boolean assignee = false;
            try
            {
                SPListItemCollection WInfoListItems = oSPWeb.Lists["IRWaiverOff"].Items;

                if (WInfoListItems != null)
                {
                    SPListItem Item = WInfoListItems.GetItemById(Convert.ToInt32(WID));


                    String User = oSPWeb.CurrentUser.LoginName;
                    String[] Name = User.Split('|');
                    String currentUser = Name[1]; ;

                    if (currentUser != null)
                    {
                        String s = Convert.ToString(Item["Assignee"]);

                        string[] AssigneeList = s.Split(',');


                        foreach (string person in AssigneeList)
                        {
                            if (person == currentUser)
                                assignee = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->CheckAssignee)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
            return assignee;

        }

        private String GetDepartmentItem(SPWeb oSPWeb, String departmentID, String Role)
        {
            StringBuilder sb = new StringBuilder();
            try
            {

                String DepartmentName = null;

                string listName = "Department";

                SPListItemCollection DepartmentListColl = oSPWeb.Lists[listName].Items;

                if (DepartmentListColl != null)
                {
                    SPListItem Item = DepartmentListColl.GetItemById(Convert.ToInt32(departmentID));


                    DepartmentName = Convert.ToString(Item["Title"]);
                }


                // Fetch the List
                SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                SPQuery query = new SPQuery();
                SPListItemCollection spListItems;
                // Include only the fields you will use.
                query.ViewFields = "<FieldRef Name='HOD'/><FieldRef Name='HODEmail'/><FieldRef Name='DepartmentDescription'/>";
                query.ViewFieldsOnly = true;

                sb.Append("<Where><And><Eq><FieldRef Name='DepartmentDescription' /><Value Type='Note'>" + Role + "</Value></Eq><Eq><FieldRef Name='LinkTitle' /><Value Type='Computed'>" + DepartmentName + "</Value></Eq></And></Where>");
                query.Query = sb.ToString();
                spListItems = spList.GetItems(query);

                //List<ListItem> lstItems = new List<ListItem>();
                string email = null;
                string name = null;
                SPUser User = null;
                String UserLogin = null;
                String[] Name = null;
                sb = new StringBuilder();
                foreach (SPListItem spListItem in spListItems)
                {

                    name = Convert.ToString(spListItem["HOD"]);
                    email = Convert.ToString(spListItem["HODEmail"]);
                    User = Utility.GetUser(oSPWeb, null, email, 0);
                    UserLogin = User.LoginName;
                    Name = UserLogin.Split('|');
                    if (Name.Length > 1)
                        sb.Append(name).Append("~").Append(email).Append("~").Append(Name[1]);

                    break;
                }





            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->GetDepartmentItem)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
            return sb.ToString();
        }

        private Boolean checkResponsiblePerson(String RID)
        {
            Boolean Assignee = false;
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {
                        SPListItemCollection IR_1InfoList = oSPWeb.Lists[this.hdnRecommendationListName.Value].Items;
                        if (IR_1InfoList != null)
                        {
                            SPListItem Item = IR_1InfoList.GetItemById(Convert.ToInt32(RID));

                            if (Item != null)
                            {

                                String User = oSPWeb.CurrentUser.LoginName;
                                String[] Name = User.Split('|');
                                String currentUser = Name[1].ToLower();

                                if (currentUser != null)
                                {
                                    if (currentUser.ToLower() == Item["Assignee"].ToString().ToLower())
                                    {
                                        Assignee = true;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->checkResponsiblePerson)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return Assignee;

        }


        private Boolean GetValuesFromRecomendationIfWaiverAvailable(String RID)
        {
            Boolean WaiverAvailable = false;
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {
                        SPListItemCollection IR_1InfoList = oSPWeb.Lists[this.hdnRecommendationListName.Value].Items;
                        if (IR_1InfoList != null)
                        {
                            SPListItem Item = IR_1InfoList.GetItemById(Convert.ToInt32(RID));

                            if (Item != null)
                            {
                                if (Item["WaivedTargetDate1"] == null && String.IsNullOrEmpty(Convert.ToString(Item["WaivedTargetDate1"])))
                                {
                                    String FRID = null;
                                    this.Date_dtc.SelectedDate = System.DateTime.Now;


                                    FRID = GetValuesFromFR(oSPWeb, Convert.ToString(Item["IRID"]));

                                    if (!String.IsNullOrEmpty(FRID))
                                        this.SafetyIncidentReportNo_tf.Value = Convert.ToString(FRID);

                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["RecommendationNo"])))

                                        this.RecommendationNo_tf.Value = Convert.ToString(Item["RecommendationNo"]);


                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["ResponsibleSection"])))
                                    {

                                        this.Section_ddl.Items.FindByValue(Convert.ToString(Item["ResponsibleSection"])).Selected = true;

                                        this.Section_hdn.Value = Convert.ToString(Item["ResponsibleSection"]);
                                    }


                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["ResponsibleDepartment"])))
                                    {

                                        this.Department_ddl.Items.FindByValue(Convert.ToString(Item["ResponsibleDepartment"])).Selected = true;

                                        this.Department_hdn.Value = Convert.ToString(Item["ResponsibleDepartment"]);
                                    }


                                    if (Item["TargetDate"] != null && !String.IsNullOrEmpty(Convert.ToString(Item["TargetDate"])))
                                    {
                                        DateTime Date;
                                        bool bValid = DateTime.TryParse(Convert.ToString(Item["TargetDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                        if (!bValid)
                                        {
                                            Date = Convert.ToDateTime(Item["TargetDate"]);
                                        }

                                        this.RecommendationCompletionDueDate_dtc.SelectedDate = Date;
                                    }

                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["IIRDescription"])))

                                        this.DescriptionOfTheRecommendations_ta.Value = Convert.ToString(Item["IIRDescription"]);

                                    WaiverAvailable = true;

                                }
                                else if (Item["WaivedTargetDate2"] == null && String.IsNullOrEmpty(Convert.ToString(Item["WaivedTargetDate2"])))
                                {
                                    String FRID = null;
                                    this.Date_dtc.SelectedDate = System.DateTime.Now;


                                    FRID = GetValuesFromFR(oSPWeb, Convert.ToString(Item["IRID"]));

                                    if (!String.IsNullOrEmpty(FRID))
                                        this.SafetyIncidentReportNo_tf.Value = Convert.ToString(FRID);

                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["RecommendationNo"])))

                                        this.RecommendationNo_tf.Value = Convert.ToString(Item["RecommendationNo"]);


                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["ResponsibleSection"])))
                                    {

                                        this.Section_ddl.Items.FindByValue(Convert.ToString(Item["ResponsibleSection"])).Selected = true;

                                        this.Section_hdn.Value = Convert.ToString(Item["ResponsibleSection"]);
                                    }


                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["ResponsibleDepartment"])))
                                    {

                                        this.Department_ddl.Items.FindByValue(Convert.ToString(Item["ResponsibleDepartment"])).Selected = true;

                                        this.Department_hdn.Value = Convert.ToString(Item["ResponsibleDepartment"]);
                                    }


                                    if (Item["TargetDate"] != null && !String.IsNullOrEmpty(Convert.ToString(Item["TargetDate"])))
                                    {
                                        DateTime Date;
                                        bool bValid = DateTime.TryParse(Convert.ToString(Item["TargetDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                        if (!bValid)
                                        {
                                            Date = Convert.ToDateTime(Item["TargetDate"]);
                                        }

                                        this.RecommendationCompletionDueDate_dtc.SelectedDate = Date;
                                    }

                                    if (!String.IsNullOrEmpty(Convert.ToString(Item["IIRDescription"])))

                                        this.DescriptionOfTheRecommendations_ta.Value = Convert.ToString(Item["IIRDescription"]);

                                    WaiverAvailable = true;
                                }


                            }
                            else
                            {
                                WaiverAvailable = false;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->GetValuesFromRecomendationIfWaiverAvailable)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return WaiverAvailable;
        }

        private String GetValuesFromFR(SPWeb oSPWeb, String IR5ID)
        {
            String FRID = null;
            try
            {
                SPListItemCollection IR_1InfoList = oSPWeb.Lists[this.hdnParentList.Value].Items;
                if (IR_1InfoList != null)
                {
                    SPListItem Item = IR_1InfoList.GetItemById(Convert.ToInt32(IR5ID));

                    if (Item != null)
                    {
                        FRID = Convert.ToString(Item[this.hdnFRID.Value]);

                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->GetValuesFromFR)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
            return FRID;
        }


        private void FillDepartment()
        {

            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {
                        string listName = "Department";

                        // Fetch the List
                        SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                        SPQuery query = new SPQuery();
                        SPListItemCollection spListItems;
                        // Include only the fields you will use.
                        query.ViewFields = "<FieldRef Name='ID'/><FieldRef Name='Title'/>";
                        query.ViewFieldsOnly = true;
                        //query.RowLimit = 200; // Only select the top 200.
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<Where><Eq><FieldRef Name='DepartmentDescription' /><Value Type='Text'>HOD</Value></Eq></Where>");
                        query.Query = sb.ToString();
                        spListItems = spList.GetItems(query);

                        this.Department_ddl.DataSource = spListItems;
                        this.Department_ddl.DataTextField = "Title";
                        this.Department_ddl.DataValueField = "ID";
                        this.Department_ddl.DataBind();

                        this.Department_ddl.Items.Insert(0, new ListItem("Please Select", "0"));

                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->FillDepartment)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }


        }

        private void FillSection()
        {

            try
            {

                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {

                        string listName = "Section";

                        // Fetch the List
                        SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                        SPQuery query = new SPQuery();
                        SPListItemCollection spListItems;
                        // Include only the fields you will use.
                        query.ViewFields = "<FieldRef Name='ID'/><FieldRef Name='Title'/>";
                        query.ViewFieldsOnly = true;
                        //query.RowLimit = 200; // Only select the top 200.
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<OrderBy Override='True';><FieldRef Name='Title'/></OrderBy>");
                        query.Query = sb.ToString();
                        spListItems = spList.GetItems(query);

                        this.Section_ddl.DataSource = spListItems;
                        this.Section_ddl.DataTextField = "Title";
                        this.Section_ddl.DataValueField = "ID";
                        this.Section_ddl.DataBind();

                        this.Section_ddl.Items.Insert(0, new ListItem("Please Select", "0"));

                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->FillSection)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        private void DisableControls(bool disableAll)
        {
            btnSaveAsDraft.Visible = false;
            btnSave.Visible = false;
            btnUMSave.Visible = false;
            btnDMSave.Visible = false;
            btnDOSave.Visible = false;
            btnUMDisapprive.Visible = false;
            btnDMDisapprive.Visible = false;
            btnDODisapprive.Visible = false;
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    UpdateWaiverValues(spListItem, false, oWebSite, false, false, false);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnSave_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void btnUMSave_Click(object sender, EventArgs e)
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    UpdateWaiverValues(spListItem, false, oWebSite, true, false, false);

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
                }

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnUMSave_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void btnDMSave_Click(object sender, EventArgs e)
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    UpdateWaiverValues(spListItem, false, oWebSite, false, true, false);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnDMSave_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void btnDOSave_Click(object sender, EventArgs e)
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    UpdateWaiverValues(spListItem, false, oWebSite, false, false, true);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnDOSave_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void btnUMDisapprove_Click(object sender, EventArgs e)
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    WaiverRejection(spListItem, oWebSite, true, false, false);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnUMDisapprove_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void btnDMDisapprove_Click(object sender, EventArgs e)
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    WaiverRejection(spListItem, oWebSite, false, true, false);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnDMDisapprove_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        protected void btnDODisapprove_Click(object sender, EventArgs e)
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    WaiverRejection(spListItem, oWebSite, false, false, true);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnDODisapprove_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        private void WaiverRejection(SPListItem ListItem, SPWeb oWebSite, Boolean UM, Boolean DM, Boolean DO)
        {
            try
            {
                if (ListItem != null)
                {

                    String User = oWebSite.CurrentUser.LoginName;
                    String[] Name = User.Split('|');

                    if (Name.Length > 1)
                    {
                        ListItem["AuditedBy"] = Name[1];
                    }

                    ListItem["Status"] = "Completed";
                    ListItem.Update();

                    if (UM)
                    {

                        RejectionEmailByUM(ListItem);
                    }
                    else if (DM)
                    {

                        RejectionEmailByDM(ListItem);
                    }
                    else if (DO)
                    {

                        RejectionEmailByDO(ListItem);
                    }

                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->WaiverRejection)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
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
                            string listName = "IRWaiverOff";
                            // Fetch the List
                            SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                            String IROFFID = this.hdnID.Value;
                            int IROffItemID = Convert.ToInt32(IROFFID);

                            if (IROFFID != null && list != null)
                            {

                                String DR_ID = Check1StFromDraft(oWebSite, IROFFID);

                                int DR_ItemID = Convert.ToInt32(DR_ID);

                                SPListItem spListItem = null;

                                if (DR_ID != null)
                                    spListItem = list.Items.GetItemById(DR_ItemID);
                                else
                                    spListItem = list.Items.Add();

                                if (spListItem != null)
                                {

                                    UpdateWaiverValues(spListItem, true, oWebSite, false, false, false);

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

            }


            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnSaveAsDraft_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
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
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->btnCancel_Click)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);

            }
        }

        private String Check1StFromDraft(SPWeb oWebSite, String RID)
        {
            String ID = null;

            try
            {


                if (oWebSite != null)
                {
                    string listName = "IRWaiverOff";


                    SPList spList = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;

                    query.ViewFields = "<FieldRef Name='RID' /><FieldRef Name='ID' />";
                    query.ViewFieldsOnly = true;

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<Where><And><Eq><FieldRef Name='RID' /><Value Type='Text'>"+RID+"</Value></Eq><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq></And></Where>");
                    query.Query = sb.ToString();
                    spListItems = spList.GetItems(query);

                    if (spListItems != null)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            ID = item["ID"].ToString();

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->Check1StFromDraft)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
            return ID;
        }

        protected void UpdateWaiverValues(SPListItem ListItem, Boolean IsSaveAsDraft, SPWeb oWebSite, Boolean UM, Boolean DM, Boolean DO)
        {
            try
            {
                if (ListItem != null)
                {
                    String RID = this.hdnID.Value;

                    if (!String.IsNullOrEmpty(RID))
                        ListItem["RID"] = RID;

                    if (!String.IsNullOrEmpty(this.hdnTaskName.Value) && this.hdnTaskName.Value == "01")
                        ListItem["TaskName"] = "01";
                    else if (this.hdnTaskName.Value == "03")
                        ListItem["TaskName"] = "03";
                    else if (this.hdnTaskName.Value == "05")
                        ListItem["TaskName"] = "05";

                    if (!String.IsNullOrEmpty(Convert.ToString(this.SafetyIncidentReportNo_tf.Value)))
                        ListItem["SafetyIncidentReportNo"] = Convert.ToString(this.SafetyIncidentReportNo_tf.Value);


                    if (!String.IsNullOrEmpty(Convert.ToString(this.RecommendationNo_tf.Value)))
                        ListItem["RecommendationNo"] = Convert.ToString(this.RecommendationNo_tf.Value);


                    if (!String.IsNullOrEmpty(Convert.ToString(this.Section_hdn.Value)))
                        ListItem["Section"] = (Convert.ToString(this.Section_hdn.Value));


                    if (!String.IsNullOrEmpty(Convert.ToString(this.Department_hdn.Value)))
                        ListItem["IncidentDepartment"] = (Convert.ToString(this.Department_hdn.Value));


                    if (!String.IsNullOrEmpty(Convert.ToString(this.Date_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.Date_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["CurrentDate"] = date;
                        else
                            ListItem["CurrentDate"] = Convert.ToDateTime(this.Date_dtc.SelectedDate);
                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(this.RecommendationCompletionDueDate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.RecommendationCompletionDueDate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["RecommendationCompletionDueDate"] = date;
                        else
                            ListItem["RecommendationCompletionDueDate"] = Convert.ToDateTime(this.RecommendationCompletionDueDate_dtc.SelectedDate);
                    }


                    if (!String.IsNullOrEmpty(Convert.ToString(this.DescriptionOfTheRecommendations_ta.Value)))
                        ListItem["DescriptionOfRecommendation"] = Convert.ToString(this.DescriptionOfTheRecommendations_ta.Value);


                    if (!String.IsNullOrEmpty(Convert.ToString(this.RecommendationReason_ta.Value)))
                        ListItem["Reason"] = Convert.ToString(this.RecommendationReason_ta.Value);


                    if (!String.IsNullOrEmpty(Convert.ToString(this.NewTargetDate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.NewTargetDate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["NewTargetDate"] = date;
                        else
                            ListItem["NewTargetDate"] = Convert.ToDateTime(this.NewTargetDate_dtc.SelectedDate);
                    }




                    if (this.InitiatedBy_PeopleEditor.ResolvedEntities != null && this.InitiatedBy_PeopleEditor.ResolvedEntities.Count > 0)
                    {
                        PickerEntity entity = (PickerEntity)this.InitiatedBy_PeopleEditor.ResolvedEntities[0];

                        ListItem["InitiatedBy"] = entity.Claim.Value;
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.InitiatedDate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.InitiatedDate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["InitiatedDate"] = date;
                        else
                            ListItem["InitiatedDate"] = Convert.ToDateTime(this.InitiatedDate_dtc.SelectedDate);
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.InitiaterComments_ta.Value)))
                        ListItem["InitiaterComments"] = Convert.ToString(this.InitiaterComments_ta.Value);





                    if (this.UM_PeopleEditor.ResolvedEntities != null && this.UM_PeopleEditor.ResolvedEntities.Count > 0)
                    {
                        PickerEntity entity = (PickerEntity)this.UM_PeopleEditor.ResolvedEntities[0];

                        ListItem["UM"] = entity.Claim.Value;
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.UMDate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.UMDate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["UMReviewedDate"] = date;
                        else
                            ListItem["UMReviewedDate"] = Convert.ToDateTime(this.UMDate_dtc.SelectedDate);
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.UMComments_ta.Value)))
                        ListItem["UMComments"] = Convert.ToString(this.UMComments_ta.Value);



                    if (this.DM_PeopleEditor.ResolvedEntities != null && this.DM_PeopleEditor.ResolvedEntities.Count > 0)
                    {
                        PickerEntity entity = (PickerEntity)this.DM_PeopleEditor.ResolvedEntities[0];

                        ListItem["DM"] = entity.Claim.Value;
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.DMDate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.DMDate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["DMReviewedDate"] = date;
                        else
                            ListItem["DMReviewedDate"] = Convert.ToDateTime(this.DMDate_dtc.SelectedDate);
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.DMComments_ta.Value)))
                        ListItem["DMComments"] = Convert.ToString(this.DMComments_ta.Value);




                    if (this.DO_PeopleEditor.ResolvedEntities != null && this.DO_PeopleEditor.ResolvedEntities.Count > 0)
                    {
                        PickerEntity entity = (PickerEntity)this.DO_PeopleEditor.ResolvedEntities[0];

                        ListItem["DO"] = entity.Claim.Value;
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.DODate_dtc.SelectedDate)))
                    {
                        DateTime date;
                        bool bValid = DateTime.TryParse(this.DODate_dtc.SelectedDate.ToShortDateString(), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);


                        if (bValid)
                            ListItem["DOReviewedDate"] = date;
                        else
                            ListItem["DOReviewedDate"] = Convert.ToDateTime(this.DODate_dtc.SelectedDate);
                    }



                    if (!String.IsNullOrEmpty(Convert.ToString(this.DOComments_ta.Value)))
                        ListItem["DOComments"] = Convert.ToString(this.DOComments_ta.Value);





                    if (IsSaveAsDraft)
                    {

                        ListItem["IsSaveAsDraft"] = true;

                        ListItem["Status"] = "Inprogress";

                        if (Page.Request.QueryString["R_Off_ID"] != null)
                            ListItem["ROFFID"] = Page.Request.QueryString["R_Off_ID"];

                        String User = oWebSite.CurrentUser.LoginName;
                        String[] Name = User.Split('|');


                        if (Name.Length > 1)
                        {
                            ListItem["Assignee"] = Name[1];
                            ListItem["AuditedBy"] = Name[1];
                        }


                        if (!UM && !DM && !DO)
                        {
                            if (Name.Length > 1)
                            {
                                ListItem["SubmittedBy"] = Name[1];
                            }
                        }

                        ListItem.Update();

                    }
                    else
                    {


                        String User = oWebSite.CurrentUser.LoginName;
                        String[] Name = User.Split('|');
                        if (Name.Length > 1)
                        {
                            ListItem["AuditedBy"] = Name[1];
                        }
                        //ListItem["Status"] = "Comlete";
                        ListItem["IsSaveAsDraft"] = false;

                        ListItem["Status"] = "Inprogress";

                        ListItem.Update();

                        if (UM)
                        {
                            ListItem["Status"] = "UMApproved";
                            ListItem.Update();

                            SendEmailToDM(ListItem);
                        }
                        else if (DM)
                        {
                            ListItem["Status"] = "DMApproved";
                            ListItem.Update();

                            SendEmailToDO(ListItem);

                        }
                        else if (DO)
                        {
                            Boolean WaiverAvailable = false;
                            WaiverAvailable = AssignWaiver(oWebSite, ListItem);

                            if (WaiverAvailable)
                            {
                                ListItem["Status"] = "Completed";
                                ListItem.Update();
                                AssignWaiverEmailByDO(ListItem);
                            }
                            else
                            {

                                RejectionEmailByDO(ListItem);
                            }


                        }
                        else
                        {
                            User = oWebSite.CurrentUser.LoginName;
                            Name = User.Split('|');
                            if (Name.Length > 1)
                            {
                                ListItem["SubmittedBy"] = Name[1];
                            }

                            SendEmailToUM(ListItem);

                        }



                    }


                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->UpdateWaiverValues)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        private Boolean AssignWaiver(SPWeb oSPWeb, SPListItem WaivertItem)
        {
            Boolean isWaiverAvailable = false;

            try
            {
                String RID = this.hdnID.Value;

                SPListItemCollection RInfoList = oSPWeb.Lists[this.hdnRecommendationListName.Value].Items;
                if (RInfoList != null)
                {
                    SPListItem RecomendationItem = RInfoList.GetItemById(Convert.ToInt32(RID));

                    if (RecomendationItem != null)
                    {
                        if (RecomendationItem["WaivedTargetDate1"] == null)
                        {
                            RecomendationItem["WaivedTargetDate1"] = WaivertItem["NewTargetDate"];
                            RecomendationItem["Status"] = "Waived";
                            isWaiverAvailable = true;
                            RecomendationItem.Update();
                        }
                        else if (RecomendationItem["WaivedTargetDate2"] == null)
                        {
                            RecomendationItem["WaivedTargetDate2"] = WaivertItem["NewTargetDate"];
                            RecomendationItem["Status"] = "Second Waived";
                            isWaiverAvailable = true;
                            RecomendationItem.Update();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->AssignWaiver)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

            return isWaiverAvailable;
        }

        private void LoadPageFromDraft(SPWeb oSPWeb, String WRID)
        {

            try
            {

                if (oSPWeb != null)
                {

                    //SPListItemCollection IR_1infoList = oWebSite.Lists["IR-1"].Items;
                    string listName = "IRWaiverOff";
                    // Fetch the List
                    SPList list = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));


                    if (WRID != null && list != null)
                    {

                        int WRID_ItemID = Convert.ToInt32(WRID);

                        SPListItem ListItem = list.Items.GetItemById(WRID_ItemID);

                        if (ListItem != null)
                        {

                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["SafetyIncidentReportNo"])))
                                this.SafetyIncidentReportNo_tf.Value = Convert.ToString(ListItem["SafetyIncidentReportNo"]);



                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["RecommendationNo"])))
                                this.RecommendationNo_tf.Value = Convert.ToString(ListItem["RecommendationNo"]);



                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["Section"])))
                            {
                                this.Section_ddl.Items.FindByValue(Convert.ToString(ListItem["Section"])).Selected = true;
                                this.Section_hdn.Value = Convert.ToString(ListItem["Section"]);
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["IncidentDepartment"])))
                            {
                                this.Department_ddl.Items.FindByValue(Convert.ToString(ListItem["IncidentDepartment"])).Selected = true;
                                this.Department_hdn.Value = Convert.ToString(ListItem["IncidentDepartment"]);
                            }



                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["CurrentDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["CurrentDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["CurrentDate"]);
                                }

                                this.Date_dtc.SelectedDate = Date;
                            }




                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["RecommendationCompletionDueDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["RecommendationCompletionDueDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["RecommendationCompletionDueDate"]);
                                }

                                this.RecommendationCompletionDueDate_dtc.SelectedDate = Date;
                            }




                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DescriptionOfRecommendation"])))

                                this.DescriptionOfTheRecommendations_ta.Value = Convert.ToString(ListItem["DescriptionOfRecommendation"]);



                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["Reason"])))

                                this.RecommendationReason_ta.Value = Convert.ToString(ListItem["Reason"]);


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["NewTargetDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["NewTargetDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["NewTargetDate"]);
                                }

                                this.NewTargetDate_dtc.SelectedDate = Date;
                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["InitiatedBy"])))
                            {
                                PeopleEditor pe = new PeopleEditor();
                                PickerEntity UserEntity = new PickerEntity();
                                String username = Convert.ToString(ListItem["InitiatedBy"]);
                                //get Spuser
                                SPUser SPuser = Utility.GetUser(oSPWeb, username, null, 0);
                                if (SPuser != null)
                                {
                                    // CurrentUser is SPUser object
                                    UserEntity.DisplayText = SPuser.Name;
                                    UserEntity.Key = SPuser.LoginName;

                                    UserEntity = pe.ValidateEntity(UserEntity);

                                    // Add PickerEntity to People Picker control
                                    this.InitiatedBy_PeopleEditor.AddEntities(new List<PickerEntity> { UserEntity });


                                }

                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["InitiatedDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["InitiatedDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["InitiatedDate"]);
                                }

                                this.InitiatedDate_dtc.SelectedDate = Date;
                            }



                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["InitiaterComments"])))

                                this.InitiaterComments_ta.Value = Convert.ToString(ListItem["InitiaterComments"]);



                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["UM"])))
                            {
                                PeopleEditor pe = new PeopleEditor();
                                PickerEntity UserEntity = new PickerEntity();
                                String username = Convert.ToString(ListItem["UM"]);
                                //get Spuser
                                SPUser SPuser = Utility.GetUser(oSPWeb, username, null, 0);
                                if (SPuser != null)
                                {
                                    // CurrentUser is SPUser object
                                    UserEntity.DisplayText = SPuser.Name;
                                    UserEntity.Key = SPuser.LoginName;

                                    UserEntity = pe.ValidateEntity(UserEntity);

                                    // Add PickerEntity to People Picker control
                                    this.UM_PeopleEditor.AddEntities(new List<PickerEntity> { UserEntity });


                                }

                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["UMReviewedDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["UMReviewedDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["UMReviewedDate"]);
                                }

                                this.UMDate_dtc.SelectedDate = Date;
                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["UMComments"])))

                                this.UMComments_ta.Value = Convert.ToString(ListItem["UMComments"]);


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DM"])))
                            {
                                PeopleEditor pe = new PeopleEditor();
                                PickerEntity UserEntity = new PickerEntity();
                                String username = Convert.ToString(ListItem["DM"]);
                                //get Spuser
                                SPUser SPuser = Utility.GetUser(oSPWeb, username, null, 0);
                                if (SPuser != null)
                                {
                                    // CurrentUser is SPUser object
                                    UserEntity.DisplayText = SPuser.Name;
                                    UserEntity.Key = SPuser.LoginName;

                                    UserEntity = pe.ValidateEntity(UserEntity);

                                    // Add PickerEntity to People Picker control
                                    this.DM_PeopleEditor.AddEntities(new List<PickerEntity> { UserEntity });


                                }

                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DMReviewedDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["DMReviewedDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["DMReviewedDate"]);
                                }

                                this.DMDate_dtc.SelectedDate = Date;
                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DMComments"])))

                                this.DMComments_ta.Value = Convert.ToString(ListItem["DMComments"]);


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DO"])))
                            {
                                PeopleEditor pe = new PeopleEditor();
                                PickerEntity UserEntity = new PickerEntity();
                                String username = Convert.ToString(ListItem["DO"]);
                                //get Spuser
                                SPUser SPuser = Utility.GetUser(oSPWeb, username, null, 0);
                                if (SPuser != null)
                                {
                                    // CurrentUser is SPUser object
                                    UserEntity.DisplayText = SPuser.Name;
                                    UserEntity.Key = SPuser.LoginName;

                                    UserEntity = pe.ValidateEntity(UserEntity);

                                    // Add PickerEntity to People Picker control
                                    this.DO_PeopleEditor.AddEntities(new List<PickerEntity> { UserEntity });


                                }

                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DOReviewedDate"])))
                            {
                                DateTime Date;
                                bool bValid = DateTime.TryParse(Convert.ToString(ListItem["DOReviewedDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out Date);

                                if (!bValid)
                                {
                                    Date = Convert.ToDateTime(ListItem["DOReviewedDate"]);
                                }

                                this.DODate_dtc.SelectedDate = Date;
                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(ListItem["DOComments"])))

                                this.DOComments_ta.Value = Convert.ToString(ListItem["DOComments"]);


                        }
                    }

                }

            }

            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->LoadPageFromDraft)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void SendEmailToUM(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = this.hdnID.Value;
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("ToUMSubject");
                            string body = Utility.GetValueByKey("ToUMTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            String[] ArrUser = GetUser(oWebSite, "Unit Manager");

                            imiItem["Assignee"] = ArrUser[2];

                            imiItem.Update();

                            if (ArrUser.Length > 1)
                            {
                                message.To = ArrUser[1];
                                Email.SendEmail(message);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->SendEmailToUM)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void RejectionEmailByUM(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = this.hdnID.Value;
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("RejectionFromUMSubject");
                            string body = Utility.GetValueByKey("RejectionFromUMTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["AuditedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            SPUser spReceiver = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));

                            imiItem["Assignee"] = imiItem["SubmittedBy"];
                            imiItem.Update();

                            if (spReceiver != null)
                            {
                                message.To = spReceiver.Email;
                                Email.SendEmail(message);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->RejectionEmailByUM)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void SendEmailToDM(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = this.hdnID.Value;
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("ToDMSubject");
                            string body = Utility.GetValueByKey("ToDMTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            String[] ArrUser = GetUser(oWebSite, "HOD");

                            imiItem["Assignee"] = ArrUser[2];
                            imiItem.Update();

                            if (ArrUser.Length > 1)
                            {
                                message.To = ArrUser[1];
                                Email.SendEmail(message);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->SendEmailToDM)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        protected void RejectionEmailByDM(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = this.hdnID.Value;
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("RejectionFromDMSubject");
                            string body = Utility.GetValueByKey("RejectionFromDMTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["AuditedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            SPUser spReceiver = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));

                            imiItem["Assignee"] = imiItem["SubmittedBy"];
                            imiItem.Update();

                            if (spReceiver != null)
                            {
                                message.To = spReceiver.Email;
                                Email.SendEmail(message);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->RejectionEmailByDM)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void SendEmailToDO(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = this.hdnID.Value;
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("ToDOSubject");
                            string body = Utility.GetValueByKey("ToDOTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            String[] ArrUser = GetUser(oWebSite, "DO");

                            imiItem["Assignee"] = ArrUser[2];
                            imiItem.Update();
                            if (ArrUser.Length > 1)
                            {
                                message.To = ArrUser[1];
                                Email.SendEmail(message);

                            }
                            //message.To = "hafiz.muhammadusama@systemsltd.com";
                            //Email.SendEmail(message);

                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->SendEmailToDO)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }

        protected void RejectionEmailByDO(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = this.hdnID.Value;
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("RejectionFromDOSubject");
                            string body = Utility.GetValueByKey("RejectionFromDOTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            body = linkSB.ToString();

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["AuditedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            SPUser spReceiver = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));

                            imiItem["Assignee"] = imiItem["SubmittedBy"];
                            imiItem.Update();

                            if (spReceiver != null)
                            {
                                message.To = spReceiver.Email;
                                Email.SendEmail(message);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm>RejectionEmailByDO)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected void AssignWaiverEmailByDO(SPListItem imiItem)
        {
            try
            {
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWebSite = oSPsite.OpenWeb())
                    {

                        if (oWebSite != null)
                        {
                            String RID = Page.Request.QueryString["RID"];
                            string IR_1Link = Utility.GetRedirectUrl("WaiverForm");
                            string subject = Utility.GetValueByKey("AssigneWaiverDOSubject");
                            string body = Utility.GetValueByKey("AssigneWaiverDOTemplate");

                            StringBuilder linkSB = new StringBuilder();
                            linkSB.Append(IR_1Link)
                                        .Append(this.hdnLink.Value)
                                        .Append(RID);

                            //body = body.Replace("~|~", linkSB.ToString());
                            //body = linkSB.ToString();
                            body = "Waiver Assigned and New Target Date IS: " + Convert.ToString(imiItem["NewTargetDate"]);

                            SPUser spSender = Utility.GetUser(oWebSite, Convert.ToString(imiItem["AuditedBy"]));
                            Message message = new Message();
                            message.Subject = subject;
                            message.Body = body;
                            message.From = spSender.Email;

                            SPUser spReceiver = Utility.GetUser(oWebSite, Convert.ToString(imiItem["SubmittedBy"]));



                            if (spReceiver != null)
                            {
                                message.To = spReceiver.Email;
                                Email.SendEmail(message);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->AssignWaiverEmailByDO)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }

        }

        protected String[] GetUser(SPWeb oSPWeb, String Role)
        {
            String[] ArrUser = null;
            try
            {

                SPListItemCollection RInfoList = oSPWeb.Lists[this.hdnRecommendationListName.Value].Items;
                if (RInfoList != null)
                {
                    String RID = this.hdnID.Value;

                    SPListItem Item = RInfoList.GetItemById(Convert.ToInt32(RID));
                    SPUser CurrentUser = oSPWeb.CurrentUser;

                    String Department = Convert.ToString(Item["ResponsibleDepartment"]);

                    String DepItem = GetDepartmentItem(oSPWeb, Department, Role);
                    ArrUser = DepItem.Split('~');
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WaiverForm->GetUser)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
            return ArrUser;
        }
    }
}
