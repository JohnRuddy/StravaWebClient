using Strava.Activities;
using Strava.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Strava.WebClient.UserControls
{
    public partial class ActivitySummaryUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ActivityClient activities = new ActivityClient((Strava.Authentication.StaticAuthentication)(Session["strava_auth"]));

            DateTime after = DateTime.Now.AddDays(-30);
            DateTime before = DateTime.Now;

            List<Strava.Activities.ActivitySummary> activityList = activities.GetActivities(after, before);

            Repeater repeater = (Repeater)this.FindControl("ActivityRepeater");
            repeater.DataSource = activityList.ToList<ActivitySummary>();
            repeater.DataBind();
        }
    }
}