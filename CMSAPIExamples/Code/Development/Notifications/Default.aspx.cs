﻿using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Notifications;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

[Title(Text = "Notifications", ImageUrl = "CMSModules/CMS_Notifications/module.png")]
public partial class CMSAPIExamples_Code_Development_Notifications_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Notifications);

        // Notification gateway
        this.apiCreateNotificationGateway.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateNotificationGateway);
        this.apiGetAndUpdateNotificationGateway.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateNotificationGateway);
        this.apiGetAndBulkUpdateNotificationGateways.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateNotificationGateways);
        this.apiDeleteNotificationGateway.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteNotificationGateway);

        // Notification template
        this.apiCreateNotificationTemplate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateNotificationTemplate);
        this.apiGetAndUpdateNotificationTemplate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateNotificationTemplate);
        this.apiGetAndBulkUpdateNotificationTemplates.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateNotificationTemplates);
        this.apiDeleteNotificationTemplate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteNotificationTemplate);

        // Notification template text
        this.apiCreateNotificationTemplateText.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateNotificationTemplateText);
        this.apiGetAndUpdateNotificationTemplateText.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateNotificationTemplateText);
        this.apiGetAndBulkUpdateNotificationTemplateTexts.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateNotificationTemplateTexts);
        this.apiDeleteNotificationTemplateText.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteNotificationTemplateText);

        // Notification subscription
        this.apiCreateNotificationSubscription.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateNotificationSubscription);
        this.apiGetAndUpdateNotificationSubscription.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateNotificationSubscription);
        this.apiGetAndBulkUpdateNotificationSubscriptions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateNotificationSubscriptions);
        this.apiDeleteNotificationSubscription.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteNotificationSubscription);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Notification gateway
        this.apiCreateNotificationGateway.Run();
        this.apiGetAndUpdateNotificationGateway.Run();
        this.apiGetAndBulkUpdateNotificationGateways.Run();

        // Notification template
        this.apiCreateNotificationTemplate.Run();
        this.apiGetAndUpdateNotificationTemplate.Run();
        this.apiGetAndBulkUpdateNotificationTemplates.Run();

        // Notification template text
        this.apiCreateNotificationTemplateText.Run();
        this.apiGetAndUpdateNotificationTemplateText.Run();
        this.apiGetAndBulkUpdateNotificationTemplateTexts.Run();

        // Notification subscription
        this.apiCreateNotificationSubscription.Run();
        this.apiGetAndUpdateNotificationSubscription.Run();
        this.apiGetAndBulkUpdateNotificationSubscriptions.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Notification subscription
        this.apiDeleteNotificationSubscription.Run();

        // Notification template text
        this.apiDeleteNotificationTemplateText.Run();

        // Notification template
        this.apiDeleteNotificationTemplate.Run();

        // Notification gateway
        this.apiDeleteNotificationGateway.Run();
    }

    #endregion


    #region "API examples - Notification gateway"

    /// <summary>
    /// Creates notification gateway. Called when the "Create gateway" button is pressed.
    /// </summary>
    private bool CreateNotificationGateway()
    {
        // Create new notification gateway object
        NotificationGatewayInfo newGateway = new NotificationGatewayInfo();

        // Set the properties
        newGateway.GatewayDisplayName = "My new gateway";
        newGateway.GatewayName = "MyNewGateway";
        newGateway.GatewayAssemblyName = "MyNewGateway";
        newGateway.GatewayClassName = "MyNewGateway";
        newGateway.GatewayDescription = "MyNewGateway";
        newGateway.GatewaySupportsEmail = true;
        newGateway.GatewaySupportsPlainText = true;
        newGateway.GatewaySupportsHTMLText = true;
        newGateway.GatewayEnabled = true;

        // Create the notification gateway
        NotificationGatewayInfoProvider.SetNotificationGatewayInfo(newGateway);

        return true;
    }


    /// <summary>
    /// Gets and updates notification gateway. Called when the "Get and update gateway" button is pressed.
    /// Expects the CreateNotificationGateway method to be run first.
    /// </summary>
    private bool GetAndUpdateNotificationGateway()
    {
        // Get the notification gateway
        NotificationGatewayInfo updateGateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");
        if (updateGateway != null)
        {
            // Update the property
            updateGateway.GatewayDisplayName = updateGateway.GatewayDisplayName.ToLower();

            // Update the notification gateway
            NotificationGatewayInfoProvider.SetNotificationGatewayInfo(updateGateway);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates notification gateways. Called when the "Get and bulk update gateways" button is pressed.
    /// Expects the CreateNotificationGateway method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateNotificationGateways()
    {
        // Prepare the parameters
        string where = "GatewayName LIKE N'MyNewGateway%'";

        // Get the data
        DataSet gateways = NotificationGatewayInfoProvider.GetGateways(where, null);
        if (!DataHelper.DataSourceIsEmpty(gateways))
        {
            // Loop through the individual items
            foreach (DataRow gatewayDr in gateways.Tables[0].Rows)
            {
                // Create object from DataRow
                NotificationGatewayInfo modifyGateway = new NotificationGatewayInfo(gatewayDr);

                // Update the property
                modifyGateway.GatewayDisplayName = modifyGateway.GatewayDisplayName.ToUpper();

                // Update the notification gateway
                NotificationGatewayInfoProvider.SetNotificationGatewayInfo(modifyGateway);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes notification gateway. Called when the "Delete gateway" button is pressed.
    /// Expects the CreateNotificationGateway method to be run first.
    /// </summary>
    private bool DeleteNotificationGateway()
    {
        // Get the notification gateway
        NotificationGatewayInfo deleteGateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        // Delete the notification gateway
        NotificationGatewayInfoProvider.DeleteNotificationGatewayInfo(deleteGateway);

        return (deleteGateway != null);
    }

    #endregion


    #region "API examples - Notification template"

    /// <summary>
    /// Creates notification template. Called when the "Create template" button is pressed.
    /// </summary>
    private bool CreateNotificationTemplate()
    {
        // Create new notification template object
        NotificationTemplateInfo newTemplate = new NotificationTemplateInfo();

        // Set the properties
        newTemplate.TemplateDisplayName = "My new template";
        newTemplate.TemplateName = "MyNewTemplate";
        newTemplate.TemplateSiteID = CMSContext.CurrentSiteID;

        // Create the notification template
        NotificationTemplateInfoProvider.SetNotificationTemplateInfo(newTemplate);

        return true;
    }


    /// <summary>
    /// Gets and updates notification template. Called when the "Get and update template" button is pressed.
    /// Expects the CreateNotificationTemplate method to be run first.
    /// </summary>
    private bool GetAndUpdateNotificationTemplate()
    {
        // Get the notification template
        NotificationTemplateInfo updateTemplate = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);
        if (updateTemplate != null)
        {
            // Update the property
            updateTemplate.TemplateDisplayName = updateTemplate.TemplateDisplayName.ToLower();

            // Update the notification template
            NotificationTemplateInfoProvider.SetNotificationTemplateInfo(updateTemplate);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates notification templates. Called when the "Get and bulk update templates" button is pressed.
    /// Expects the CreateNotificationTemplate method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateNotificationTemplates()
    {
        // Prepare the parameters
        string where = "TemplateName LIKE N'MyNewTemplate%'";
        where = SqlHelperClass.AddWhereCondition(where, "TemplateSiteID = " + CMSContext.CurrentSiteID, "AND");

        // Get the data
        DataSet templates = NotificationTemplateInfoProvider.GetTemplates(where, null);
        if (!DataHelper.DataSourceIsEmpty(templates))
        {
            // Loop through the individual items
            foreach (DataRow templateDr in templates.Tables[0].Rows)
            {
                // Create object from DataRow
                NotificationTemplateInfo modifyTemplate = new NotificationTemplateInfo(templateDr);

                // Update the property
                modifyTemplate.TemplateDisplayName = modifyTemplate.TemplateDisplayName.ToUpper();

                // Update the notification template
                NotificationTemplateInfoProvider.SetNotificationTemplateInfo(modifyTemplate);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes notification template. Called when the "Delete template" button is pressed.
    /// Expects the CreateNotificationTemplate method to be run first.
    /// </summary>
    private bool DeleteNotificationTemplate()
    {
        // Get the notification template
        NotificationTemplateInfo deleteTemplate = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        // Delete the notification template
        NotificationTemplateInfoProvider.DeleteNotificationTemplateInfo(deleteTemplate);

        return (deleteTemplate != null);
    }

    #endregion


    #region "API examples - Notification template text"

    /// <summary>
    /// Creates notification template text. Called when the "Create text" button is pressed.
    /// </summary>
    private bool CreateNotificationTemplateText()
    {
        // Get the template
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        // Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        if ((template != null) && (gateway != null))
        {
            // Create new notification template text object
            NotificationTemplateTextInfo newText = new NotificationTemplateTextInfo();

            // Set the properties
            newText.TemplateSubject = "My new text";
            newText.TemplateID = template.TemplateID;
            newText.GatewayID = gateway.GatewayID;
            newText.TemplateHTMLText = "";
            newText.TemplatePlainText = "";
            newText.TempalateTextLastModified = DateTime.Now;

            // Create the notification template text
            NotificationTemplateTextInfoProvider.SetNotificationTemplateTextInfo(newText);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates notification template text. Called when the "Get and update text" button is pressed.
    /// Expects the CreateNotificationTemplateText method to be run first.
    /// </summary>
    private bool GetAndUpdateNotificationTemplateText()
    {
        // Get the template
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        //Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        if ((template != null) && (gateway != null))
        {
            // Get the notification template text
            NotificationTemplateTextInfo updateText = NotificationTemplateTextInfoProvider.GetNotificationTemplateTextInfo(gateway.GatewayID, template.TemplateID);
            if (updateText != null)
            {
                // Update the property
                updateText.TemplateSubject = updateText.TemplateSubject.ToLower();

                // Update the notification template text
                NotificationTemplateTextInfoProvider.SetNotificationTemplateTextInfo(updateText);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates notification template texts. Called when the "Get and bulk update texts" button is pressed.
    /// Expects the CreateNotificationTemplateText method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateNotificationTemplateTexts()
    {
        // Get the template
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        //Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        if ((template != null) && (gateway != null))
        {
            // Prepare the parameters
            string where = "";
            where = SqlHelperClass.AddWhereCondition(where, "TemplateID = " + template.TemplateID, "AND");
            where = SqlHelperClass.AddWhereCondition(where, "GatewayID = " + gateway.GatewayID, "AND");

            // Get the data
            DataSet texts = NotificationTemplateTextInfoProvider.GetNotificationTemplateTexts(where, null);
            if (!DataHelper.DataSourceIsEmpty(texts))
            {
                // Loop through the individual items
                foreach (DataRow textDr in texts.Tables[0].Rows)
                {
                    // Create object from DataRow
                    NotificationTemplateTextInfo modifyText = new NotificationTemplateTextInfo(textDr);

                    // Update the property
                    modifyText.TemplateSubject = modifyText.TemplateSubject.ToUpper();

                    // Update the notification template text
                    NotificationTemplateTextInfoProvider.SetNotificationTemplateTextInfo(modifyText);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes notification template text. Called when the "Delete text" button is pressed.
    /// Expects the CreateNotificationTemplateText method to be run first.
    /// </summary>
    private bool DeleteNotificationTemplateText()
    {
        // Get the template
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        //Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        if ((template != null) && (gateway != null))
        {
            // Get the notification template text
            NotificationTemplateTextInfo deleteText = NotificationTemplateTextInfoProvider.GetNotificationTemplateTextInfo(gateway.GatewayID, template.TemplateID);

            // Delete the notification template text
            NotificationTemplateTextInfoProvider.DeleteNotificationTemplateTextInfo(deleteText);

            return (deleteText != null);
        }

        return false;
    }

    #endregion


    #region "API examples - Notification subscription"

    /// <summary>
    /// Creates notification subscription. Called when the "Create subscription" button is pressed.
    /// </summary>
    private bool CreateNotificationSubscription()
    {
        // Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        // Get the tamplate
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        if ((gateway != null) && (template != null))
        {
            // Create new notification subscription object
            NotificationSubscriptionInfo newSubscription = new NotificationSubscriptionInfo();

            // Set the properties
            newSubscription.SubscriptionEventDisplayName = "My new subscription";
            newSubscription.SubscriptionGatewayID = gateway.GatewayID;
            newSubscription.SubscriptionTemplateID = template.TemplateID;
            newSubscription.SubscriptionTime = DateTime.Now;
            newSubscription.SubscriptionUserID = CMSContext.CurrentUser.UserID;
            newSubscription.SubscriptionTarget = "";
            newSubscription.SubscriptionLastModified = DateTime.Now;
            newSubscription.SubscriptionSiteID = CMSContext.CurrentSiteID;

            // Create the notification subscription
            NotificationSubscriptionInfoProvider.SetNotificationSubscriptionInfo(newSubscription);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates notification subscription. Called when the "Get and update subscription" button is pressed.
    /// Expects the CreateNotificationSubscription method to be run first.
    /// </summary>
    private bool GetAndUpdateNotificationSubscription()
    {
        // Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        // Get the tamplate
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        if ((gateway != null) && (template != null))
        {
            // Prepare the parameters
            string where = "SubscriptionGatewayID = " + gateway.GatewayID + " AND SubscriptionTemplateID = " + template.TemplateID;

            // Get the notification subscription
            DataSet subscriptions = NotificationSubscriptionInfoProvider.GetSubscriptions(where, null);
            if (!DataHelper.DataSourceIsEmpty(subscriptions))
            {
                // Create object from DataRow
                NotificationSubscriptionInfo updateSubscription = new NotificationSubscriptionInfo(subscriptions.Tables[0].Rows[0]);

                // Update the property
                updateSubscription.SubscriptionEventDisplayName = updateSubscription.SubscriptionEventDisplayName.ToLower();

                // Update the notification subscription
                NotificationSubscriptionInfoProvider.SetNotificationSubscriptionInfo(updateSubscription);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates notification subscriptions. Called when the "Get and bulk update subscriptions" button is pressed.
    /// Expects the CreateNotificationSubscription method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateNotificationSubscriptions()
    {
        // Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        // Get the tamplate
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        if ((gateway != null) && (template != null))
        {
            // Prepare the parameters
            string where = "SubscriptionGatewayID = " + gateway.GatewayID + " AND SubscriptionTemplateID = " + template.TemplateID;

            // Get the notification subscription
            DataSet subscriptions = NotificationSubscriptionInfoProvider.GetSubscriptions(where, null);
            if (!DataHelper.DataSourceIsEmpty(subscriptions))
            {
                foreach (DataRow subscriptionDr in subscriptions.Tables[0].Rows)
                {
                    // Create object from DataRow
                    NotificationSubscriptionInfo updateSubscription = new NotificationSubscriptionInfo(subscriptions.Tables[0].Rows[0]);

                    // Update the property
                    updateSubscription.SubscriptionEventDisplayName = updateSubscription.SubscriptionEventDisplayName.ToUpper();

                    // Update the notification subscription
                    NotificationSubscriptionInfoProvider.SetNotificationSubscriptionInfo(updateSubscription);

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes notification subscription. Called when the "Delete subscription" button is pressed.
    /// Expects the CreateNotificationSubscription method to be run first.
    /// </summary>
    private bool DeleteNotificationSubscription()
    {
        // Get the gateway
        NotificationGatewayInfo gateway = NotificationGatewayInfoProvider.GetNotificationGatewayInfo("MyNewGateway");

        // Get the template
        NotificationTemplateInfo template = NotificationTemplateInfoProvider.GetNotificationTemplateInfo("MyNewTemplate", CMSContext.CurrentSiteID);

        if ((gateway != null) && (template != null))
        {
            // Prepare the parameters
            string where = "SubscriptionGatewayID = " + gateway.GatewayID + " AND SubscriptionTemplateID = " + template.TemplateID;

            // Get the notification subscription
            DataSet subscriptions = NotificationSubscriptionInfoProvider.GetSubscriptions(where, null);
            if (!DataHelper.DataSourceIsEmpty(subscriptions))
            {
                // Create object from DataRow
                NotificationSubscriptionInfo deleteSubscription = new NotificationSubscriptionInfo(subscriptions.Tables[0].Rows[0]);

                // Delete the notification subscription
                NotificationSubscriptionInfoProvider.DeleteNotificationSubscriptionInfo(deleteSubscription);

                return (deleteSubscription != null);
            }
        }

        return false;
    }

    #endregion
}
