using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Administration;
using SL.FG.FFL.Layouts.SL.FG.FFL.Common;
using System.Text;
using System.Globalization;

namespace SL.FG.FFL.WebParts.WorkQueue
{
    [ToolboxItemAttribute(false)]
    public partial class WorkQueue : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public WorkQueue()
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
                    using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oSPWeb = oSPsite.OpenWeb())
                        {
                            SPUser currentUser = oSPWeb.CurrentUser;

                            if (currentUser != null)
                            {
                                //Rizwan
                                //Start
                                FillMSAScheduleWorkQueue(oSPWeb, currentUser);
                                FillMSAWorkQueue(oSPWeb, currentUser);
                                FillMSARecommendationWorkQueue(oSPWeb, currentUser);
                                FillIRRecommendationOnJobWorkQueue(oSPWeb, currentUser);
                                FillIR01DIWorkQueue(oSPWeb, currentUser);
                                FillIR03DIWorkQueue(oSPWeb, currentUser);
                                //End

                                //Hafiz Usama
                                //Start
                                IR01TaskWorkQueue(oSPWeb, currentUser);
                                FRTaskWorkQueue(oSPWeb, currentUser);
                                IR01OffJobTaskWorkQueue(oSPWeb, currentUser);
                                FROffJobTaskWorkQueue(oSPWeb, currentUser);
                                IR05OffJobTaskWorkQueue(oSPWeb, currentUser);
                                FillIRRecommendationOffJobWorkQueue(oSPWeb, currentUser);
                                WaiverOffJobTaskWorkQueue(oSPWeb, currentUser);
                                WaiverOnJobTaskWorkQueue(oSPWeb, currentUser);
                                //End
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->PageLoad)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);

                message_div.InnerHtml = " { " + ex.Message + " } " + "  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }

        //Utility Functions
        //Start

        private bool getHOD(SPWeb oSPWeb, string departmentID, string responsiblepersonEmailAddress)
        {
            if (oSPWeb != null)
            {
                SPList spList = oSPWeb.Lists["Department"];
                //SPQuery spQuery = new SPQuery();
                //spQuery.Query = "<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + departmentID +"</Value></Eq></Where>";
                SPListItem listItem = spList.GetItemById(Convert.ToInt32(departmentID));
                if (listItem != null)
                {
                    if (Convert.ToString(listItem["HODEmail"]).Equals(responsiblepersonEmailAddress, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //End
        private void FillMSAScheduleWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("AreaAudited", typeof(string));
                dt.Columns.Add("StartTime", typeof(string));
                dt.Columns.Add("EndTime", typeof(string));
                dt.Columns.Add("LinkFileName", typeof(string));

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSPSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb pSPWeb = oSPSite.OpenWeb())
                        {
                            SPList spList = pSPWeb.Lists["MSA Schedule"];
                            SPQuery spQuery = new SPQuery();

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<Where>")
                                .Append("<And>")
                                 .Append("<Eq>")
                                 .Append("<FieldRef Name='FFLScheduleName' />")
                                 .Append("<Value Type='User'>" + currentUser.Name + "</Value>")
                                 .Append("</Eq>")
                                 .Append("</Where>");

                            string strQuery = "<Where><And><And><Eq><FieldRef Name='FFLScheduleName' LookupId='TRUE' /><Value Type='Integer' >" + currentUser.ID + "</Value></Eq><Geq><FieldRef Name='EventDate' /><Value Type='DateTime' IncludeTimeValue='FALSE'><Today /></Value></Geq></And><Eq><FieldRef Name='MSAStatus' /><Value Type='Choice'>Not Started</Value></Eq></And></Where>";
                            //"<Where><Eq><FieldRef Name='FFLScheduleName' LookupId='TRUE' /><Value Type='Integer' >" + currentUser.ID + "</Value></Eq></Where>";
                            spQuery.Query = strQuery; //sb.ToString(); // <UserID />



                            SPListItemCollection spListItems = spList.GetItems(spQuery);
                            if (spListItems != null && spListItems.Count > 0)
                            {
                                DataRow dr;
                                string recommendationLink = Utility.GetRedirectUrl("MSAFormLink");

                                SPFieldUrlValue link;

                                foreach (SPListItem item in spListItems)
                                {
                                    dr = dt.NewRow();
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "FFLScheduleName:1: " + Convert.ToString(item["FFLScheduleName"]) + "End", "");
                                    string name = Convert.ToString(item["FFLScheduleName"]);
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "FFLScheduleName:2: " + Convert.ToString(item["FFLScheduleName"]) + "End", "");

                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "FFLArea:1: " + Convert.ToString(item[item.Fields["FFLArea"].InternalName]) + "End", "");
                                    if (Convert.ToString(item["FFLArea"]) != null)
                                    {
                                        string[] areas = Convert.ToString(item["FFLArea"]).Split('#');
                                        if (areas.Length > 1)
                                        {
                                            dr["AreaAudited"] = areas[1];
                                        }
                                    }

                                    //dr["AreaAudited"] = item["FFLArea"] != null ? Convert.ToString(item["FFLArea"]) : "";
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "EventDate:1: " + Convert.ToString(item["EventDate"]) + "End", "");
                                    if (!String.IsNullOrEmpty(Convert.ToString(item["EventDate"])))
                                    {
                                        DateTime StartTime;
                                        bool bValid = DateTime.TryParse(Convert.ToString(item["EventDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out StartTime);

                                        if (bValid)
                                        {
                                            dr["StartTime"] = StartTime.ToShortDateString();
                                        }
                                        else
                                        {
                                            dr["StartTime"] = Convert.ToDateTime(StartTime).ToShortDateString();
                                        }
                                    }
                                    else
                                    {
                                        dr["StartTime"] = "";
                                    }
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "EndDate:1: " + Convert.ToString(item["EndDate"]) + "End", "");
                                    if (!String.IsNullOrEmpty(Convert.ToString(item["EndDate"])))
                                    {
                                        DateTime EndTime;
                                        bool bValid = DateTime.TryParse(Convert.ToString(item["EndDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out EndTime);

                                        if (bValid)
                                        {
                                            dr["EndTime"] = EndTime.ToShortDateString();
                                        }
                                        else
                                        {
                                            dr["EndTime"] = Convert.ToDateTime(EndTime).ToShortDateString(); ;
                                        }
                                    }
                                    else
                                    {
                                        dr["EndTime"] = "";
                                    }
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "MSAFormLink:1: " + Convert.ToString(item["MSAFormLink"]) + "End", "");
                                    link = new SPFieldUrlValue(Convert.ToString(item["MSAFormLink"]));
                                    dr["LinkFileName"] = link.Url;  //.Format("{0}?SID=" + item["ID"], recommendationLink);


                                    dt.Rows.Add(dr);
                                }

                                BoundField bf = new BoundField();

                                //RecommendationNo Column
                                bf = new BoundField();
                                bf.DataField = "AreaAudited";
                                bf.HeaderText = "Area To Be Audited";
                                grdMSAScheduled.Columns.Add(bf);

                                bf = new BoundField();
                                bf.DataField = "StartTime";
                                bf.HeaderText = "Start Date";
                                grdMSAScheduled.Columns.Add(bf);

                                bf = new BoundField();
                                bf.DataField = "EndTime";
                                bf.HeaderText = "End Date";
                                grdMSAScheduled.Columns.Add(bf);



                                HyperLinkField hyperlinkField = new HyperLinkField();
                                hyperlinkField.HeaderText = "View MSA";
                                hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                                hyperlinkField.Text = "View";
                                grdMSAScheduled.Columns.Add(hyperlinkField);


                                grdMSAScheduled.DataSource = dt;
                                grdMSAScheduled.DataBind();
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL->FillMSASchedule)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FillMSAWorkQueue(SPWeb oSPWeb_, SPUser currentUser)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oSPWeb_EP = oSPsite.OpenWeb())
                        {
                            string getName = string.Empty;


                            DataTable dt = new DataTable();

                            dt.Columns.Add("AreaAudited", typeof(string));
                            dt.Columns.Add("StartTime", typeof(string));
                            dt.Columns.Add("MSADate", typeof(string));
                            dt.Columns.Add("EndTime", typeof(string));
                            dt.Columns.Add("AuditedBy", typeof(string));
                            dt.Columns.Add("LinkFileName", typeof(string));

                            string listName = "MSA";
                            // Fetch the List
                            SPList splistMSARecommendation = oSPWeb_EP.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb_EP.Url, listName));

                            SPQuery query = new SPQuery();
                            SPListItemCollection spListItems;
                            // Include only the fields you will use.


                            string vf = "<FieldRef Name='ID' /><FieldRef Name='Author' /><FieldRef Name='MSADate' /><FieldRef Name='AuditedBy' /><FieldRef Name='AreaAudited' /><FieldRef Name='AccompaniedBy' /><FieldRef Name='StartTime' /><FieldRef Name='EndTime' />";

                            query.ViewFields = vf;
                            query.ViewFieldsOnly = true;

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<Where>")
                                .Append("<And>")
                                .Append("<Eq>")
                                 .Append("<FieldRef Name='IsSavedAsDraft' />")
                                 .Append("<Value Type='Boolean'>1</Value>")
                                 .Append("</Eq>")
                                 .Append("<Eq>")
                                 .Append("<FieldRef Name='Author' />")
                                 .Append("<Value Type='User'>" + currentUser.Name + "</Value>")
                                 .Append("</Eq>")
                                 .Append("</And>")
                                 .Append("</Where>");


                            query.Query = sb.ToString();
                            spListItems = splistMSARecommendation.GetItems(query);

                            DataRow dr;


                            if (spListItems != null && spListItems.Count > 0)
                            {
                                foreach (SPListItem item in spListItems)
                                {

                                    dr = dt.NewRow();

                                    SPUser author = null;
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("-1", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, "Ex-A", "Ex-A");
                                    if (item["Author"] != null)
                                    {
                                        string authorStr = Convert.ToString(item["Author"]);

                                        var temp = authorStr.Split('#');

                                        if (temp.Length > 1)
                                        {
                                            temp = temp[0].Split(';');

                                            if (temp.Length > 1)
                                            {
                                                author = Utility.GetUser(oSPWeb_EP, null, null, Int32.Parse(temp[0]));
                                            }
                                        }


                                    }

                                    dr["AreaAudited"] = item["AreaAudited"] != null ? Convert.ToString(item["AreaAudited"]) : "";

                                    dr["StartTime"] = item["StartTime"] != null ? Convert.ToString(item["StartTime"]) : "";

                                    dr["EndTime"] = item["EndTime"] != null ? Convert.ToString(item["EndTime"]) : "";

                                    string auditedBy = item["AuditedBy"] != null ? Convert.ToString(item["AuditedBy"]) : "";

                                    if (!String.IsNullOrEmpty(auditedBy))
                                    {
                                        SPUser auditedByUser = Utility.GetUser(oSPWeb_EP, auditedBy);

                                        if (auditedByUser != null)
                                        {
                                            dr["AuditedBy"] = auditedByUser.Name;
                                        }
                                    }


                                    DateTime date;
                                    bool bValid = DateTime.TryParse(Convert.ToString(item["MSADate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);

                                    if (bValid)
                                    {
                                        dr["MSADate"] = date.ToShortDateString();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            dr["MSADate"] = Convert.ToDateTime(Convert.ToString(item["MSADate"])).ToShortDateString();
                                        }
                                        catch (Exception ex)
                                        {
                                            SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-MSAD:" + Convert.ToString(item["MSADate"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                        }
                                    }


                                    string recommendationLink = Utility.GetRedirectUrl("MSAFormLink");


                                    dr["LinkFileName"] = string.Format("{0}?MSAID=" + item["ID"], recommendationLink);



                                    if (author != null && Utility.CompareUsername(author.LoginName, currentUser.LoginName))
                                    {
                                        dt.Rows.Add(dr);
                                    }

                                }
                            }

                            BoundField bf = new BoundField();


                            //RecommendationNo Column
                            bf = new BoundField();
                            bf.DataField = "AreaAudited";
                            bf.HeaderText = "Area Audited";
                            grdMSATask.Columns.Add(bf);

                            bf = new BoundField();
                            bf.DataField = "StartTime";
                            bf.HeaderText = "Start Time";
                            grdMSATask.Columns.Add(bf);

                            bf = new BoundField();
                            bf.DataField = "EndTime";
                            bf.HeaderText = "End Time";
                            grdMSATask.Columns.Add(bf);

                            bf = new BoundField();
                            bf.DataField = "MSADate";
                            bf.HeaderText = "MSA Date";
                            grdMSATask.Columns.Add(bf);

                            bf = new BoundField();
                            bf.DataField = "AuditedBy";
                            bf.HeaderText = "Audited By";
                            grdMSATask.Columns.Add(bf);



                            HyperLinkField hyperlinkField = new HyperLinkField();
                            hyperlinkField.HeaderText = "View MSA";
                            hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                            hyperlinkField.Text = "View";
                            grdMSATask.Columns.Add(hyperlinkField);


                            grdMSATask.DataSource = dt;
                            grdMSATask.DataBind();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSAWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FillMSARecommendationWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("ItemID", typeof(int));
                    dt.Columns.Add("RecommendationNo", typeof(string));
                    dt.Columns.Add("ResponsiblePerson", typeof(string));
                    dt.Columns.Add("TargetDate", typeof(string));
                    dt.Columns.Add("TaskName", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));
                    dt.Columns.Add("LinkDisplayText", typeof(string));

                    string listName = "MSARecommendation";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='MSARecommendationDescription'/>")
                        .Append("<FieldRef Name='TargetDate'/>")
                        .Append("<FieldRef Name='ResponsiblePerson'/>")
                        .Append("<FieldRef Name='ApprovedBy'/>")
                        .Append("<FieldRef Name='AssigneeEmail'/>")
                        .Append("<FieldRef Name='RecommendationNo'/>");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    query.Query = "<Where><And><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq></And><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            dr["ItemID"] = item["ID"];
                            dr["RecommendationNo"] = item["RecommendationNo"] != null ? Convert.ToString(item["RecommendationNo"]) : "";

                            string rpUsername = item["ResponsiblePerson"] != null ? Convert.ToString(item["ResponsiblePerson"]) : "";

                            SPUser responsiblePerson = null;

                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                responsiblePerson = Utility.GetUser(oSPWeb, rpUsername);

                                if (responsiblePerson != null)
                                {
                                    dr["ResponsiblePerson"] = responsiblePerson.Name;
                                }
                            }


                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["TargetDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["TargetDate"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["TargetDate"] = Convert.ToDateTime(Convert.ToString(item["TargetDate"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-TRGD:" + Convert.ToString(item["TargetDate"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            dr["TaskName"] = "MSA Recommendation";

                            string recommendationLink = Utility.GetRedirectUrl("MSARecommendationFormLink");


                            dr["LinkFileName"] = string.Format("{0}?MSARID=" + item["ID"], recommendationLink);

                            if (String.IsNullOrEmpty(Convert.ToString(item["ApprovedBy"])))
                            {
                                if (getHOD(oSPWeb, Convert.ToString(item["ID"]), responsiblePerson.Email))
                                {
                                    dr["LinkDisplayText"] = "View (for approval)";
                                }
                                else
                                {
                                    dr["LinkDisplayText"] = "View (for submission)";
                                }
                            }
                            else
                            {
                                if (Convert.ToString(item["ApprovedBy"]).Equals(Convert.ToString(item["AssigneeEmail"]), StringComparison.OrdinalIgnoreCase))
                                {
                                    dr["LinkDisplayText"] = "View (for approval)";
                                }
                                else
                                {
                                    dr["LinkDisplayText"] = "View (for submission)";
                                }
                            }


                            dt.Rows.Add(dr);
                        }
                    }

                    BoundField bf = new BoundField();
                    bf.DataField = "ItemID";
                    bf.HeaderText = "ID #";
                    grdMSARecommendationTask.Columns.Add(bf);

                    //RecommendationNo Column
                    bf = new BoundField();
                    bf.DataField = "RecommendationNo";
                    bf.HeaderText = "Recommendation No";
                    grdMSARecommendationTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "ResponsiblePerson";
                    bf.HeaderText = "Responsible Person";
                    grdMSARecommendationTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TargetDate";
                    bf.HeaderText = "Target Date";
                    grdMSARecommendationTask.Columns.Add(bf);

                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View Recommendations";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    //hyperlinkField.Text = "View";
                    hyperlinkField.DataTextField = "LinkDisplayText";
                    grdMSARecommendationTask.Columns.Add(hyperlinkField);


                    grdMSARecommendationTask.DataSource = dt;
                    grdMSARecommendationTask.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FillIRRecommendationOffJobWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("ItemID", typeof(int));
                    dt.Columns.Add("RecommendationNo", typeof(string));
                    dt.Columns.Add("ResponsiblePerson", typeof(string));
                    dt.Columns.Add("TargetDate", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IIRRecommendation_OffJob";
                    // Fetch the List
                    SPList splistIIRRecommendationOnJob = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='IIRDescription'/>")
                        .Append("<FieldRef Name='TargetDate'/>")
                        .Append("<FieldRef Name='ResponsiblePerson'/>")
                        .Append("<FieldRef Name='RecommendationNo'/>")
                        .Append("<FieldRef Name='IsFromIR01DI'/>");


                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    query.Query = "<Where><And><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq></And><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistIIRRecommendationOnJob.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            dr["ItemID"] = item["ID"];
                            dr["RecommendationNo"] = item["RecommendationNo"] != null ? Convert.ToString(item["RecommendationNo"]) : "";

                            string rpUsername = item["ResponsiblePerson"] != null ? Convert.ToString(item["ResponsiblePerson"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser responsiblePerson = Utility.GetUser(oSPWeb, rpUsername);

                                if (responsiblePerson != null)
                                {
                                    dr["ResponsiblePerson"] = responsiblePerson.Name;
                                }
                            }

                            bool isFromIR01DI = true;

                            if (item["IsFromIR01DI"] != null)
                            {
                                isFromIR01DI = Convert.ToBoolean(item["IsFromIR01DI"]);
                            }

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["TargetDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["TargetDate"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["TargetDate"] = Convert.ToDateTime(Convert.ToString(item["TargetDate"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-TRGD:" + Convert.ToString(item["TargetDate"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            string recommendationLink = Utility.GetRedirectUrl("IRRecommendationFormLink");



                            dr["LinkFileName"] = string.Format("{0}?IR05DI_ID=" + item["ID"], recommendationLink);


                            dt.Rows.Add(dr);
                        }
                    }

                    BoundField bf = new BoundField();
                    bf.DataField = "ItemID";
                    bf.HeaderText = "ID #";
                    grdIR5OffJobRecomendationTask.Columns.Add(bf);

                    //RecommendationNo Column
                    bf = new BoundField();
                    bf.DataField = "RecommendationNo";
                    bf.HeaderText = "Recommendation No";
                    grdIR5OffJobRecomendationTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "ResponsiblePerson";
                    bf.HeaderText = "Responsible Person";
                    grdIR5OffJobRecomendationTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TargetDate";
                    bf.HeaderText = "Target Date";
                    grdIR5OffJobRecomendationTask.Columns.Add(bf);


                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View Recommendations";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIR5OffJobRecomendationTask.Columns.Add(hyperlinkField);


                    grdIR5OffJobRecomendationTask.DataSource = dt;
                    grdIR5OffJobRecomendationTask.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillIIRRecommendationOnJobWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FillIRRecommendationOnJobWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("ItemID", typeof(int));
                    dt.Columns.Add("RecommendationNo", typeof(string));
                    dt.Columns.Add("ResponsiblePerson", typeof(string));
                    dt.Columns.Add("TargetDate", typeof(string));
                    dt.Columns.Add("TaskName", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IIRRecommendationOnJob";
                    // Fetch the List
                    SPList splistIIRRecommendationOnJob = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='IIRDescription'/>")
                        .Append("<FieldRef Name='TargetDate'/>")
                        .Append("<FieldRef Name='ResponsiblePerson'/>")
                        .Append("<FieldRef Name='RecommendationNo'/>")
                        .Append("<FieldRef Name='IsFromIR01DI'/>");


                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    query.Query = "<Where><And><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq></And><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistIIRRecommendationOnJob.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            dr["ItemID"] = item["ID"];
                            dr["RecommendationNo"] = item["RecommendationNo"] != null ? Convert.ToString(item["RecommendationNo"]) : "";

                            string rpUsername = item["ResponsiblePerson"] != null ? Convert.ToString(item["ResponsiblePerson"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser responsiblePerson = Utility.GetUser(oSPWeb, rpUsername);

                                if (responsiblePerson != null)
                                {
                                    dr["ResponsiblePerson"] = responsiblePerson.Name;
                                }
                            }

                            bool isFromIR01DI = true;

                            if (item["IsFromIR01DI"] != null)
                            {
                                isFromIR01DI = Convert.ToBoolean(item["IsFromIR01DI"]);
                            }

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["TargetDate"]), new CultureInfo("en-GB"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["TargetDate"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["TargetDate"] = Convert.ToDateTime(Convert.ToString(item["TargetDate"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-TRGD:" + Convert.ToString(item["TargetDate"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            string recommendationLink = Utility.GetRedirectUrl("IRRecommendationFormLink");

                            if (isFromIR01DI)
                            {
                                dr["TaskName"] = "IR01";
                                dr["LinkFileName"] = string.Format("{0}?IR01DI_ID=" + item["ID"], recommendationLink);
                            }
                            else
                            {
                                dr["TaskName"] = "IR03";
                                dr["LinkFileName"] = string.Format("{0}?IR03DI_ID=" + item["ID"], recommendationLink);
                            }

                            dt.Rows.Add(dr);
                        }
                    }

                    BoundField bf = new BoundField();
                    bf.DataField = "ItemID";
                    bf.HeaderText = "ID #";
                    grdIRRecommendationsOnJob.Columns.Add(bf);

                    //RecommendationNo Column
                    bf = new BoundField();
                    bf.DataField = "RecommendationNo";
                    bf.HeaderText = "Recommendation No";
                    grdIRRecommendationsOnJob.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "ResponsiblePerson";
                    bf.HeaderText = "Responsible Person";
                    grdIRRecommendationsOnJob.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TargetDate";
                    bf.HeaderText = "Target Date";
                    grdIRRecommendationsOnJob.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TaskName";
                    bf.HeaderText = "Task Name";
                    grdIRRecommendationsOnJob.Columns.Add(bf);

                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View Recommendations";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIRRecommendationsOnJob.Columns.Add(hyperlinkField);


                    grdIRRecommendationsOnJob.DataSource = dt;
                    grdIRRecommendationsOnJob.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillIIRRecommendationOnJobWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FillIR01DIWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("IncidentTitle", typeof(string));
                    dt.Columns.Add("IncidentDescription", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IR01DI";
                    // Fetch the List
                    SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='FlashReportID' />")
                        .Append("<FieldRef Name='IncidentTitle' />")
                        .Append("<FieldRef Name='IncidentDescription' />")
                        .Append("<FieldRef Name='IsSavedAsDraft' />")
                        .Append("<FieldRef Name='IsSubmitted' />")
                        .Append("<FieldRef Name='IsApproved' />")
                        .Append("<FieldRef Name='IsClosed' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName, true);
                    String currentEmail = currentUser.Email;

                    query.Query = "<Where><And><Contains><FieldRef Name='AssigneeEmail' /><Value Type='Note'>" + currentEmail + "</Value></Contains> <Eq><FieldRef Name='IsClosed' /><Value Type='Boolean'>0</Value></Eq></And></Where>";

                    spListItems = spList.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            string Link = Utility.GetRedirectUrl("IR01DIFormLink");

                            bool IsSavedAsDraft = false;
                            bool IsSubmitted = false;
                            bool IsApproved = false;

                            if (item["IsSavedAsDraft"] != null)
                            {
                                IsSavedAsDraft = Convert.ToBoolean(item["IsSavedAsDraft"]);
                            }
                            if (item["IsSubmitted"] != null)
                            {
                                IsSubmitted = Convert.ToBoolean(item["IsSubmitted"]);
                            }
                            if (item["IsApproved"] != null)
                            {
                                IsApproved = Convert.ToBoolean(item["IsApproved"]);
                            }

                            if (item["FlashReportID"] != null && IsSavedAsDraft == true && IsSubmitted == false && IsApproved == false)
                            {
                                dr["LinkFileName"] = string.Format("{0}?FRID=" + item["FlashReportID"], Link);
                            }
                            else
                            {
                                dr["LinkFileName"] = string.Format("{0}?IR01DI_Id=" + item["ID"], Link);
                            }

                            if (item["IncidentTitle"] != null)
                            {
                                dr["IncidentTitle"] = item["IncidentTitle"];
                            }

                            if (item["IncidentDescription"] != null)
                            {
                                dr["IncidentDescription"] = item["IncidentDescription"];
                            }

                            dt.Rows.Add(dr);
                        }
                    }

                    //RecommendationNo Column
                    BoundField bf = new BoundField();

                    bf = new BoundField();
                    bf.DataField = "IncidentTitle";
                    bf.HeaderText = "Incident Title";
                    grdIR01DITasks.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "IncidentDescription";
                    bf.HeaderText = "Description";
                    grdIR01DITasks.Columns.Add(bf);

                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIR01DITasks.Columns.Add(hyperlinkField);


                    grdIR01DITasks.DataSource = dt;
                    grdIR01DITasks.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillIR01DIWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FillIR03DIWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("IncidentTitle", typeof(string));
                    dt.Columns.Add("IncidentDescription", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IR03DI";
                    // Fetch the List
                    SPList spList = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='FlashReportID' />")
                        .Append("<FieldRef Name='IncidentTitle' />")
                        .Append("<FieldRef Name='IncidentDescription' />")
                        .Append("<FieldRef Name='IsSavedAsDraft' />")
                        .Append("<FieldRef Name='IsSubmitted' />")
                        .Append("<FieldRef Name='IsApproved' />")
                        .Append("<FieldRef Name='IsClosed' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName, true);
                    String currentEmail = currentUser.Email;

                    query.Query = "<Where><And><Contains><FieldRef Name='AssigneeEmail' /><Value Type='Note'>" + currentEmail + "</Value></Contains> <Eq><FieldRef Name='IsClosed' /><Value Type='Boolean'>0</Value></Eq></And></Where>";

                    spListItems = spList.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            string Link = Utility.GetRedirectUrl("IR03DIFormLink");

                            bool IsSavedAsDraft = false;
                            bool IsSubmitted = false;
                            bool IsApproved = false;

                            if (item["IsSavedAsDraft"] != null)
                            {
                                IsSavedAsDraft = Convert.ToBoolean(item["IsSavedAsDraft"]);
                            }
                            if (item["IsSubmitted"] != null)
                            {
                                IsSubmitted = Convert.ToBoolean(item["IsSubmitted"]);
                            }
                            if (item["IsApproved"] != null)
                            {
                                IsApproved = Convert.ToBoolean(item["IsApproved"]);
                            }

                            if (item["FlashReportID"] != null && IsSavedAsDraft == true && IsSubmitted == false && IsApproved == false)
                            {
                                dr["LinkFileName"] = string.Format("{0}?FRID=" + item["FlashReportID"], Link);
                            }
                            else
                            {
                                dr["LinkFileName"] = string.Format("{0}?IR03DI_Id=" + item["ID"], Link);
                            }

                            if (item["IncidentTitle"] != null)
                            {
                                dr["IncidentTitle"] = item["IncidentTitle"];
                            }

                            if (item["IncidentDescription"] != null)
                            {
                                dr["IncidentDescription"] = item["IncidentDescription"];
                            }

                            dt.Rows.Add(dr);
                        }
                    }

                    //RecommendationNo Column
                    BoundField bf = new BoundField();

                    bf = new BoundField();
                    bf.DataField = "IncidentTitle";
                    bf.HeaderText = "Incident Title";
                    grdIR03DITasks.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "IncidentDescription";
                    bf.HeaderText = "Description";
                    grdIR03DITasks.Columns.Add(bf);

                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIR03DITasks.Columns.Add(hyperlinkField);


                    grdIR03DITasks.DataSource = dt;
                    grdIR03DITasks.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillIR03DIWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        //End Work Queue

        //Hafiz Usama
        //Start
        private void IR01OffJobTaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();


                    dt.Columns.Add("IncidentCategory", typeof(string));
                    dt.Columns.Add("InjuryCategory", typeof(string));
                    dt.Columns.Add("DateOfIncident", typeof(string));
                    dt.Columns.Add("TimeOfIncident", typeof(string));
                    dt.Columns.Add("TitleOfIncident", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IR-1-Off";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='IncidentCategory' />")
                        .Append("<FieldRef Name='InjuryCategory' />")
                        .Append("<FieldRef Name='DateOfIncident' />")
                        .Append("<FieldRef Name='TimeOfIncident' />")
                        .Append("<FieldRef Name='TitleOfIncident' />")
                        .Append("<FieldRef Name='SubmittedBy' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            if (!String.IsNullOrEmpty(Convert.ToString(item["IncidentCategory"])))
                            {
                                dr["IncidentCategory"] = item["IncidentCategory"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["InjuryCategory"])))
                            {
                                dr["InjuryCategory"] = item["InjuryCategory"];
                            }

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["DateOfIncident"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["DateOfIncident"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["DateOfIncident"] = Convert.ToDateTime(Convert.ToString(item["DateOfIncident"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["DateOfIncident"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TimeOfIncident"])))
                            {
                                dr["TimeOfIncident"] = item["TimeOfIncident"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TitleOfIncident"])))
                            {
                                dr["TitleOfIncident"] = item["TitleOfIncident"];
                            }


                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string recommendationLink = Utility.GetRedirectUrl("IR_1OffFormLink");

                            dr["LinkFileName"] = string.Format("{0}?IRID=" + item["ID"], recommendationLink);

                            dt.Rows.Add(dr);
                        }
                    }

                    //RecommendationNo Column
                    BoundField bf = new BoundField();
                    bf.DataField = "IncidentCategory";
                    bf.HeaderText = "Incident Category";
                    grdIIROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "InjuryCategory";
                    bf.HeaderText = "Injury Category";
                    grdIIROffJobTask.Columns.Add(bf);


                    bf = new BoundField();
                    bf.DataField = "DateOfIncident";
                    bf.HeaderText = "Date Of Incident";
                    grdIIROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TimeOfIncident";
                    bf.HeaderText = "Time Of Incident";
                    grdIIROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TitleOfIncident";
                    bf.HeaderText = "Title Of Incident";
                    grdIIROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "SubmittedBy";
                    bf.HeaderText = "Submitted By";
                    grdIIROffJobTask.Columns.Add(bf);


                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View IR-01";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIIROffJobTask.Columns.Add(hyperlinkField);


                    grdIIROffJobTask.DataSource = dt;
                    grdIIROffJobTask.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FROffJobTaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();



                    dt.Columns.Add("DateOfIncident", typeof(string));
                    dt.Columns.Add("TimeOfIncident", typeof(string));
                    dt.Columns.Add("DescriptionOfIncident", typeof(string));
                    dt.Columns.Add("ActionTaken", typeof(string));
                    dt.Columns.Add("Unit_x002f_Section", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "FlashReportOff";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='DateOfIncident' />")
                        .Append("<FieldRef Name='TimeOfIncident' />")
                        .Append("<FieldRef Name='DescriptionOfIncident' />")
                        .Append("<FieldRef Name='ActionTaken' />")
                        .Append("<FieldRef Name='Unit_x002f_Section' />")
                        .Append("<FieldRef Name='SubmittedBy' />")
                        .Append("<FieldRef Name='IRID' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            if (!String.IsNullOrEmpty(Convert.ToString(item["DescriptionOfIncident"])))
                            {
                                dr["DescriptionOfIncident"] = item["DescriptionOfIncident"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["ActionTaken"])))
                            {
                                dr["ActionTaken"] = item["ActionTaken"];
                            }

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["DateOfIncident"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["DateOfIncident"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["DateOfIncident"] = Convert.ToDateTime(Convert.ToString(item["DateOfIncident"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["DateOfIncident"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TimeOfIncident"])))
                            {
                                dr["TimeOfIncident"] = item["TimeOfIncident"];
                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(item["Unit_x002f_Section"])))
                            {
                                dr["Unit_x002f_Section"] = item["Unit_x002f_Section"];
                            }


                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string Link = Utility.GetRedirectUrl("FlashReportOffLink");

                            dr["LinkFileName"] = string.Format("{0}?IRID=" + item["IRID"], Link);

                            dt.Rows.Add(dr);
                        }
                    }

                    //RecommendationNo Column
                    BoundField bf = new BoundField();

                    bf = new BoundField();
                    bf.DataField = "DateOfIncident";
                    bf.HeaderText = "Date Of Incident";
                    grdFROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TimeOfIncident";
                    bf.HeaderText = "Time Of Incident";
                    grdFROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "DescriptionOfIncident";
                    bf.HeaderText = "Description Of Incident";
                    grdFROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "ActionTaken";
                    bf.HeaderText = "Action Taken";
                    grdFROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "Unit_x002f_Section";
                    bf.HeaderText = "Unit Section";
                    grdFROffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "SubmittedBy";
                    bf.HeaderText = "Submitted By";
                    grdFROffJobTask.Columns.Add(bf);


                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View Flash Report";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdFROffJobTask.Columns.Add(hyperlinkField);


                    grdFROffJobTask.DataSource = dt;
                    grdFROffJobTask.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void IR05OffJobTaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("DateOfIncident", typeof(string));
                    dt.Columns.Add("TimeOfIncident", typeof(string));
                    dt.Columns.Add("TitleOfIncident", typeof(string));
                    dt.Columns.Add("Unit_x002f_Area", typeof(string));
                    dt.Columns.Add("IncidentCategory", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IR-5-Off";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='DateOfIncident' />")
                        .Append("<FieldRef Name='TimeOfIncident' />")
                        .Append("<FieldRef Name='TitleOfIncident' />")
                        .Append("<FieldRef Name='Unit_x002f_Area' />")
                        .Append("<FieldRef Name='IncidentCategory' />")
                        .Append("<FieldRef Name='SubmittedBy' />")
                        .Append("<FieldRef Name='FRID' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();



                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["DateOfIncident"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["DateOfIncident"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["DateOfIncident"] = Convert.ToDateTime(Convert.ToString(item["DateOfIncident"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["DateOfIncident"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TimeOfIncident"])))
                            {
                                dr["TimeOfIncident"] = item["TimeOfIncident"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TitleOfIncident"])))
                            {
                                dr["TitleOfIncident"] = item["TitleOfIncident"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["Unit_x002f_Area"])))
                            {
                                dr["Unit_x002f_Area"] = item["Unit_x002f_Area"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["IncidentCategory"])))
                            {
                                dr["IncidentCategory"] = item["IncidentCategory"];
                            }

                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string Link = Utility.GetRedirectUrl("IR_5FormLink");

                            dr["LinkFileName"] = string.Format("{0}?FRID=" + item["FRID"], Link);

                            dt.Rows.Add(dr);
                        }
                    }


                    //RecommendationNo Column
                    BoundField bf = new BoundField();

                    bf = new BoundField();
                    bf.DataField = "DateOfIncident";
                    bf.HeaderText = "Date Of Incident";
                    grdIR05OffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TimeOfIncident";
                    bf.HeaderText = "Time Of Incident";
                    grdIR05OffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TitleOfIncident";
                    bf.HeaderText = "Title Of Incident";
                    grdIR05OffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "Unit_x002f_Area";
                    bf.HeaderText = "Unit Area";
                    grdIR05OffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "IncidentCategory";
                    bf.HeaderText = "Incident Category";
                    grdIR05OffJobTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "SubmittedBy";
                    bf.HeaderText = "Submitted By";
                    grdIR05OffJobTask.Columns.Add(bf);


                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View IR-05";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIR05OffJobTask.Columns.Add(hyperlinkField);


                    grdIR05OffJobTask.DataSource = dt;
                    grdIR05OffJobTask.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void WaiverOffJobTaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    String Task = null;



                    dt.Columns.Add("SafetyIncidentReportNo", typeof(string));
                    dt.Columns.Add("RecommendationNo", typeof(string));
                    dt.Columns.Add("Section", typeof(string));
                    dt.Columns.Add("IncidentDepartment", typeof(string));
                    dt.Columns.Add("RecommendationCompletionDueDate", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IRWaiverOff";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='SafetyIncidentReportNo' />")
                        .Append("<FieldRef Name='RecommendationNo' />")
                        .Append("<FieldRef Name='Section' />")
                        .Append("<FieldRef Name='IncidentDepartment' />")
                        .Append("<FieldRef Name='RecommendationCompletionDueDate' />")
                        .Append("<FieldRef Name='SubmittedBy' />")
                        .Append("<FieldRef Name='TaskName' />")
                        .Append("<FieldRef Name='RID' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            String TaskName = Task = Convert.ToString(item["TaskName"]);

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["RecommendationCompletionDueDate"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["RecommendationCompletionDueDate"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["RecommendationCompletionDueDate"] = Convert.ToDateTime(Convert.ToString(item["RecommendationCompletionDueDate"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["RecommendationCompletionDueDate"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["SafetyIncidentReportNo"])))
                            {
                                dr["SafetyIncidentReportNo"] = item["SafetyIncidentReportNo"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["RecommendationNo"])))
                            {
                                dr["RecommendationNo"] = item["RecommendationNo"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["Section"])))
                            {
                                dr["Section"] = item["Section"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["IncidentDepartment"])))
                            {
                                dr["IncidentDepartment"] = item["IncidentDepartment"];
                            }

                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string Link = Utility.GetRedirectUrl("WaiverFormLink");
                            //if(TaskName == "01")
                            //    dr["LinkFileName"] = string.Format("{0}?IR01DI_ID=" + item["RID"], Link);
                            //else if (TaskName == "03")
                            //    dr["LinkFileName"] = string.Format("{0}?IR03DI_ID=" + item["RID"], Link);
                            //else
                            if (TaskName == "05")
                                dr["LinkFileName"] = string.Format("{0}?IR05DI_ID=" + item["RID"], Link);
                            dt.Rows.Add(dr);
                        }
                    }


                    //RecommendationNo Column
                    if (Task == "05")
                    {
                        BoundField bf = new BoundField();

                        bf = new BoundField();
                        bf.DataField = "RecommendationCompletionDueDate";
                        bf.HeaderText = "Previous Target Date";
                        grdWaiverOffJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "SafetyIncidentReportNo";
                        bf.HeaderText = "Safety Incident Report No";
                        grdWaiverOffJobTask.Columns.Add(bf);

                        bf.DataField = "RecommendationNo";
                        bf.HeaderText = "Recommendation No";
                        grdWaiverOffJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "IncidentDepartment";
                        bf.HeaderText = "Department";
                        grdWaiverOffJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "Section";
                        bf.HeaderText = "Section";
                        grdWaiverOffJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "SubmittedBy";
                        bf.HeaderText = "Submitted By";
                        grdWaiverOffJobTask.Columns.Add(bf);


                        HyperLinkField hyperlinkField = new HyperLinkField();
                        hyperlinkField.HeaderText = "View Waiver";
                        hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                        hyperlinkField.Text = "View";
                        grdWaiverOffJobTask.Columns.Add(hyperlinkField);


                        grdWaiverOffJobTask.DataSource = dt;
                        grdWaiverOffJobTask.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void WaiverOnJobTaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();

                    String Task = null;


                    dt.Columns.Add("SafetyIncidentReportNo", typeof(string));
                    dt.Columns.Add("RecommendationNo", typeof(string));
                    dt.Columns.Add("Section", typeof(string));
                    dt.Columns.Add("IncidentDepartment", typeof(string));
                    dt.Columns.Add("RecommendationCompletionDueDate", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IRWaiverOff";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='SafetyIncidentReportNo' />")
                        .Append("<FieldRef Name='RecommendationNo' />")
                        .Append("<FieldRef Name='Section' />")
                        .Append("<FieldRef Name='IncidentDepartment' />")
                        .Append("<FieldRef Name='RecommendationCompletionDueDate' />")
                        .Append("<FieldRef Name='SubmittedBy' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Eq><FieldRef Name='IsSaveAsDraft' /><Value Type='Text'>True</Value></Eq></And><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();


                            String TaskName = Task = Convert.ToString(item["TaskName"]);

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["RecommendationCompletionDueDate"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["RecommendationCompletionDueDate"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["RecommendationCompletionDueDate"] = Convert.ToDateTime(Convert.ToString(item["RecommendationCompletionDueDate"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["RecommendationCompletionDueDate"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["SafetyIncidentReportNo"])))
                            {
                                dr["SafetyIncidentReportNo"] = item["SafetyIncidentReportNo"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["RecommendationNo"])))
                            {
                                dr["RecommendationNo"] = item["RecommendationNo"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["Section"])))
                            {
                                dr["Section"] = item["Section"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["IncidentDepartment"])))
                            {
                                dr["IncidentDepartment"] = item["IncidentDepartment"];
                            }

                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string Link = Utility.GetRedirectUrl("WaiverFormLink");

                            if (TaskName == "01")
                                dr["LinkFileName"] = string.Format("{0}?IR01DI_ID=" + item["RID"], Link);
                            else if (TaskName == "03")
                                dr["LinkFileName"] = string.Format("{0}?IR03DI_ID=" + item["RID"], Link);

                            //dr["LinkFileName"] = string.Format("{0}?IR03DI_ID=" + item["ID"], Link);

                            dt.Rows.Add(dr);
                        }
                    }
                    if (Task == "01" || Task == "03")
                    {
                        BoundField bf = new BoundField();

                        bf = new BoundField();
                        bf.DataField = "RecommendationCompletionDueDate";
                        bf.HeaderText = "Previous Target Date";
                        grdWaiverOnJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "SafetyIncidentReportNo";
                        bf.HeaderText = "Safety Incident Report No";
                        grdWaiverOnJobTask.Columns.Add(bf);

                        bf.DataField = "RecommendationNo";
                        bf.HeaderText = "Recommendation No";
                        grdWaiverOnJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "IncidentDepartment";
                        bf.HeaderText = "Department";
                        grdWaiverOnJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "Section";
                        bf.HeaderText = "Section";
                        grdWaiverOnJobTask.Columns.Add(bf);

                        bf = new BoundField();
                        bf.DataField = "SubmittedBy";
                        bf.HeaderText = "Submitted By";
                        grdWaiverOnJobTask.Columns.Add(bf);


                        HyperLinkField hyperlinkField = new HyperLinkField();
                        hyperlinkField.HeaderText = "View Waiver";
                        hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                        hyperlinkField.Text = "View";
                        grdWaiverOnJobTask.Columns.Add(hyperlinkField);


                        grdWaiverOnJobTask.DataSource = dt;
                        grdWaiverOnJobTask.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void IR01TaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();


                    dt.Columns.Add("IncidentCategory", typeof(string));
                    dt.Columns.Add("InjuryCategory", typeof(string));
                    dt.Columns.Add("DateOfIncident", typeof(string));
                    dt.Columns.Add("TimeOfIncident", typeof(string));
                    dt.Columns.Add("TitleOfIncident", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "IR-1";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='IncidentCategory' />")
                        .Append("<FieldRef Name='InjuryCategory' />")
                        .Append("<FieldRef Name='DateOfIncident' />")
                        .Append("<FieldRef Name='TimeOfIncident' />")
                        .Append("<FieldRef Name='TitleOfIncident' />")
                        .Append("<FieldRef Name='SubmittedBy' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;



                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            if (!String.IsNullOrEmpty(Convert.ToString(item["IncidentCategory"])))
                            {
                                dr["IncidentCategory"] = item["IncidentCategory"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["InjuryCategory"])))
                            {
                                dr["InjuryCategory"] = item["InjuryCategory"];
                            }

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["DateOfIncident"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["DateOfIncident"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["DateOfIncident"] = Convert.ToDateTime(Convert.ToString(item["DateOfIncident"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["DateOfIncident"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TimeOfIncident"])))
                            {
                                dr["TimeOfIncident"] = item["TimeOfIncident"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TitleOfIncident"])))
                            {
                                dr["TitleOfIncident"] = item["TitleOfIncident"];
                            }


                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string recommendationLink = Utility.GetRedirectUrl("IR1FormLink");

                            dr["LinkFileName"] = string.Format("{0}?IRID=" + item["ID"], recommendationLink);

                            dt.Rows.Add(dr);
                        }
                    }


                    //RecommendationNo Column
                    BoundField bf = new BoundField();
                    bf.DataField = "IncidentCategory";
                    bf.HeaderText = "Incident Category";
                    grdIR01Task.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "InjuryCategory";
                    bf.HeaderText = "Injury Category";
                    grdIR01Task.Columns.Add(bf);


                    bf = new BoundField();
                    bf.DataField = "DateOfIncident";
                    bf.HeaderText = "Date Of Incident";
                    grdIR01Task.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TimeOfIncident";
                    bf.HeaderText = "Time Of Incident";
                    grdIR01Task.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TitleOfIncident";
                    bf.HeaderText = "Title Of Incident";
                    grdIR01Task.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "SubmittedBy";
                    bf.HeaderText = "Submitted By";
                    grdIR01Task.Columns.Add(bf);


                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View IR-01";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdIR01Task.Columns.Add(hyperlinkField);


                    grdIR01Task.DataSource = dt;
                    grdIR01Task.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }
        private void FRTaskWorkQueue(SPWeb oSPWeb, SPUser currentUser)
        {
            try
            {
                if (oSPWeb != null)
                {
                    string getName = string.Empty;

                    DataTable dt = new DataTable();



                    dt.Columns.Add("DateOfIncident", typeof(string));
                    dt.Columns.Add("TimeOfIncident", typeof(string));
                    dt.Columns.Add("DescriptionOfIncident", typeof(string));
                    dt.Columns.Add("ActionTaken", typeof(string));
                    dt.Columns.Add("Unit_x002f_Section", typeof(string));
                    dt.Columns.Add("SubmittedBy", typeof(string));
                    dt.Columns.Add("LinkFileName", typeof(string));

                    string listName = "FlashReport";
                    // Fetch the List
                    SPList splistMSARecommendation = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));

                    SPQuery query = new SPQuery();
                    SPListItemCollection spListItems;
                    // Include only the fields you will use.
                    StringBuilder vf = new StringBuilder();
                    vf.Append("<FieldRef Name='ID' />")
                        .Append("<FieldRef Name='DateOfIncident' />")
                        .Append("<FieldRef Name='TimeOfIncident' />")
                        .Append("<FieldRef Name='DescriptionOfIncident' />")
                        .Append("<FieldRef Name='ActionTaken' />")
                        .Append("<FieldRef Name='Unit_x002f_Section' />")
                        .Append("<FieldRef Name='SubmittedBy' />")
                        .Append("<FieldRef Name='IR1ID' />");

                    query.ViewFields = vf.ToString();
                    query.ViewFieldsOnly = true;

                    String currentUserName = Utility.GetUsername(currentUser.LoginName);

                    query.Query = "<Where><And><Neq><FieldRef Name='Status' /><Value Type='Text'>Completed</Value></Neq><Contains><FieldRef Name='Assignee' /><Value Type='Note'>" + currentUserName + "</Value></Contains></And></Where>";
                    //query.Query = "<Where><And><Eq><FieldRef Name='AssigneeEmail' /><Value Type='Text'>" + currentUser.Email + "</Value></Eq><Eq><FieldRef Name='IsSavedAsDraft' /><Value Type='Boolean'>0</Value></Eq></And></Where>";
                    spListItems = splistMSARecommendation.GetItems(query);

                    DataRow dr;

                    if (spListItems != null && spListItems.Count > 0)
                    {
                        foreach (SPListItem item in spListItems)
                        {
                            dr = dt.NewRow();

                            if (!String.IsNullOrEmpty(Convert.ToString(item["DescriptionOfIncident"])))
                            {
                                dr["DescriptionOfIncident"] = item["DescriptionOfIncident"];
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["ActionTaken"])))
                            {
                                dr["ActionTaken"] = item["ActionTaken"];
                            }

                            DateTime date;
                            bool bValid = DateTime.TryParse(Convert.ToString(item["DateOfIncident"]), new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out date);

                            if (bValid)
                            {
                                dr["DateOfIncident"] = date.ToShortDateString();
                            }
                            else
                            {
                                try
                                {
                                    dr["DateOfIncident"] = Convert.ToDateTime(Convert.ToString(item["DateOfIncident"])).ToShortDateString();
                                }
                                catch (Exception ex)
                                {
                                    SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("WQ-IR01:" + Convert.ToString(item["DateOfIncident"]), TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                                }
                            }

                            if (!String.IsNullOrEmpty(Convert.ToString(item["TimeOfIncident"])))
                            {
                                dr["TimeOfIncident"] = item["TimeOfIncident"];
                            }


                            if (!String.IsNullOrEmpty(Convert.ToString(item["Unit_x002f_Section"])))
                            {
                                dr["Unit_x002f_Section"] = item["Unit_x002f_Section"];
                            }


                            string rpUsername = item["SubmittedBy"] != null ? Convert.ToString(item["SubmittedBy"]) : "";


                            if (!String.IsNullOrEmpty(rpUsername))
                            {
                                SPUser SubmittedBy = Utility.GetUser(oSPWeb, rpUsername);

                                if (SubmittedBy != null)
                                {
                                    dr["SubmittedBy"] = SubmittedBy.Name;
                                }
                            }

                            string Link = Utility.GetRedirectUrl("FlashReportFormLink");

                            dr["LinkFileName"] = string.Format("{0}?IRID=" + item["IR1ID"], Link);

                            dt.Rows.Add(dr);
                        }
                    }

                    //RecommendationNo Column
                    BoundField bf = new BoundField();

                    bf = new BoundField();
                    bf.DataField = "DateOfIncident";
                    bf.HeaderText = "Date Of Incident";
                    grdFlashReportTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "TimeOfIncident";
                    bf.HeaderText = "Time Of Incident";
                    grdFlashReportTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "DescriptionOfIncident";
                    bf.HeaderText = "Description Of Incident";
                    grdFlashReportTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "ActionTaken";
                    bf.HeaderText = "Action Taken";
                    grdFlashReportTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "Unit_x002f_Section";
                    bf.HeaderText = "Unit Section";
                    grdFlashReportTask.Columns.Add(bf);

                    bf = new BoundField();
                    bf.DataField = "SubmittedBy";
                    bf.HeaderText = "Submitted By";
                    grdFlashReportTask.Columns.Add(bf);


                    HyperLinkField hyperlinkField = new HyperLinkField();
                    hyperlinkField.HeaderText = "View Flash Report";
                    hyperlinkField.DataNavigateUrlFields = new[] { "LinkFileName" };
                    hyperlinkField.Text = "View";
                    grdFlashReportTask.Columns.Add(hyperlinkField);


                    grdFlashReportTask.DataSource = dt;
                    grdFlashReportTask.DataBind();
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(WorkQueue->FillMSARecommendationWorkQueue)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
                message_div.InnerHtml = "Something went wrong!!!  Please contact the administrator at email address - FFL.HSE@fatima-group.com.";
            }
        }

        //End Work Queue
    }
}
