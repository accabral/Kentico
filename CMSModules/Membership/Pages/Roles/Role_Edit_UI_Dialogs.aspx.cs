﻿using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_UI_Dialogs : CMSRolesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.UIPersonalization", "Read"))
        {
            RedirectToAccessDenied("CMS.UIPersonalization", "Read");
        }

        int siteID = 0;

        if (SelectedSiteID != 0)
        {
            siteID = SelectedSiteID;
        }
        else if (SiteID != 0)
        {
            siteID = SiteID;
        }

        this.editElem.SiteID = siteID;
        this.editElem.RoleID = QueryHelper.GetInteger("roleid", 0);
        this.editElem.IsLiveSite = false;
        this.editElem.HideSiteSelector = true;
    }
}
