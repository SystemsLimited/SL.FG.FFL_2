using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SL.FG.FFL.Layouts.SL.FG.FFL.Common;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;


namespace SL.FG.FFL.WebParts.IntermediateOffForm
{
    [ToolboxItemAttribute(false)]
    public partial class IntermediateOffForm : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public IntermediateOffForm()
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
                using (SPSite oSPsite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {
                        if (Page.Request.UrlReferrer != null)
                        {
                            string listName = "IR02OffJob";
                            // Fetch the List
                            SPList SPListIR02 = oSPWeb.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oSPWeb.Url, listName));


                            SPQuery query = new SPQuery();

                            //query.ViewFields = "<ViewFields><FieldRef Name='ID' /><FieldRef Name='Total' /><FieldRef Name='Created' /></ViewFields>";
                            //query.ViewFieldsOnly = true;

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<Where><Eq><FieldRef Name='Author' /><Value Type='Integer'><UserID /></Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>");
                            query.Query = sb.ToString();
                            SPListItemCollection spListItems = SPListIR02.GetItems(query);
                            query.RowLimit = 1;



                            if (spListItems != null && spListItems.Count > 0)
                            {
                                foreach (SPListItem item in spListItems)
                                {
                                    AddDummyEntryInIR01ListToAssignTaskTo(oSPWeb, item);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IntermediateOffForm->Page_Load)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }


        protected void AddDummyEntryInIR01ListToAssignTaskTo(SPWeb oWebSite, SPListItem IR02ListItem)
        {

            try
            {
                if (oWebSite != null)
                {
                    String listName = "IR-1-Off";

                    SPList list = oWebSite.GetList(string.Format("{0}/Lists/{1}/AllItems.aspx", oWebSite.Url, listName));

                    if (list != null)
                    {
                        SPListItem IR1ListItem = list.Items.Add();

                        if (IR1ListItem != null)
                        {
                            oWebSite.AllowUnsafeUpdates = true;
                            String UserName = null;
                            String User = oWebSite.CurrentUser.LoginName;
                            String[] Name = User.Split('|');
                            if (Name.Length > 1)
                            {
                                UserName = Utility.GetUsername(Name[1], true);
                            }

                            IR1ListItem["Assignee"] = UserName;
                            IR1ListItem["IsSaveAsDraft"] = true;
                            IR1ListItem["Status"] = "Inprogress";
                            IR1ListItem["AuditedBy"] = UserName;
                            IR1ListItem["SubmittedBy"] = UserName;

                            if (!String.IsNullOrEmpty(Convert.ToString(IR02ListItem["ID"])))
                                IR1ListItem["IR2ID"] = Convert.ToString(IR02ListItem["ID"]);

                            if (!String.IsNullOrEmpty(Convert.ToString(IR02ListItem["Total_ActualScore"]).Split('#')[1].Split('.')[0]))
                                IR1ListItem["IncidentScore"] = Convert.ToString(IR02ListItem["Total_ActualScore"]).Split('#')[1].Split('.')[0];

                            IR1ListItem.Update();

                            oWebSite.AllowUnsafeUpdates = false;

                            string IR01Url = Utility.GetRedirectUrl("IR_1OffFormLink");

                            if (!String.IsNullOrEmpty(IR01Url))
                            {
                                Page.Response.Redirect(IR01Url + "?IRID=" + IR1ListItem["ID"], false);
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SPDiagnosticsService.Local.WriteTrace(0, new SPDiagnosticsCategory("SL.FG.FFL(IntermediateOffForm->AddDummyEntryInIR01ListToAssignTaskTo)", TraceSeverity.Unexpected, EventSeverity.Error), TraceSeverity.Unexpected, ex.Message, ex.StackTrace);
            }
        }
    }
}
