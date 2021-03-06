using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;

public partial class CMSFormControls_Inputs_USZIPCode : CMS.FormControls.FormEngineUserControl
{
    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.txtZIPCode.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtZIPCode.Text;
        }
        set
        {
            txtZIPCode.Text = (string)value;
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        if (!DataHelper.IsEmpty(txtZIPCode.Text.Trim()))
        {
            // US ZIP Code must have 5 digits.
            Validator val = new Validator();
            string result = val.IsRegularExp(txtZIPCode.Text, @"\d{5}", "error").Result;

            if (result != "")
            {
                this.ValidationError = GetString("USZIPcode.ValidationError");
                return false;
            }
        }
        return true;
    }
}
