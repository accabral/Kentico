using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Staging_Tools_Log : CMSStagingPage
{
    #region "Private variables"

    private int serverId = 0;
    private int taskId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check hash
        if (!QueryHelper.ValidateHash("hash"))
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1"));

        }
        else
        {
            string element = QueryHelper.GetString("tasktype", null);

            // Check UI permissions for Staging
            CurrentUserInfo user = CMSContext.CurrentUser;
            if (!user.IsAuthorizedPerUIElement("cms.staging", element))
            {
                RedirectToCMSDeskUIElementAccessDenied("cms.staging", element);
            }

            // Check 'Manage XXX tasks' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "Manage" + element + "Tasks"))
            {
                RedirectToAccessDenied("cms.staging", "Manage" + element + "Tasks");
            }

            // Register modal dialog scripts
            RegisterModalPageScripts();

            serverId = QueryHelper.GetInteger("serverid", 0);
            taskId = QueryHelper.GetInteger("taskid", 0);

            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@TaskID", taskId);
            parameters.Add("@ServerID", serverId);

            gridLog.QueryParameters = parameters;
            gridLog.OnAction += gridLog_OnAction;
            gridLog.ZeroRowsText = GetString("Task.LogNoEvents");
            gridLog.OnBeforeDataReload += new OnBeforeDataReload(gridLog_OnBeforeDataReload);

            CurrentMaster.Title.TitleText = GetString("Task.LogHeader");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Staging_Task/object.png");
            CurrentMaster.DisplayControlsPanel = true;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Get task data
        if (taskId > 0)
        {
            TaskInfo ti = TaskInfoProvider.GetTaskInfo(taskId);
            if (ti != null)
            {
                lblInfo.Text += String.Format(GetString("Task.LogTaskInfo"), HTMLHelper.HTMLEncode(ti.TaskTitle));
            }
        }
        // Get server data
        if (serverId > 0)
        {
            ServerInfo si = ServerInfoProvider.GetServerInfo(serverId);
            if (si != null)
            {
                if (lblInfo.Text != "")
                {
                    lblInfo.Text += "<br /><br />";
                }
                lblInfo.Text += String.Format(GetString("Task.LogServerInfo"), si.ServerDisplayName);
            }
        }
        lblInfo.Visible = (lblInfo.Text != "");
        base.OnPreRender(e);
    }

    #endregion


    #region "Control events"

    protected void gridLog_OnBeforeDataReload()
    {
        if (serverId > 0)
        {
            gridLog.GridView.Columns[3].Visible = false;
        }
        if (taskId > 0)
        {
            gridLog.GridView.Columns[1].Visible = false;
        }
    }


    /// <summary>
    /// UniGrid action event handler.
    /// </summary>
    protected void gridLog_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                int logid = ValidationHelper.GetInteger(actionArgument, 0);
                if (logid > 0)
                {
                    SyncLogInfoProvider.DeleteSyncLogInfo(logid);
                }
                break;
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        SyncLogInfoProvider.DeleteSyncLogInfo(taskId, serverId);
        gridLog.ReloadData();
    }

    #endregion
}