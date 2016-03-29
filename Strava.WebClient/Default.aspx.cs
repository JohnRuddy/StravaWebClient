using System;
using Strava;
using Strava.Authentication;
using Strava.Clients;
using Strava.Common;
using Strava.Athletes;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Strava.WebClient
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["strava_token"] == null && HttpContext.Current.Request.QueryString["code"] == null)
                {
                    Response.Redirect(@"https://www.strava.com/oauth/authorize?client_id=826&response_type=code&redirect_uri=http://localhost:25210&approval_prompt=force&scope=view_private");
                }
                if (HttpContext.Current.Request.QueryString["code"] != null)
                {
                    var code = HttpContext.Current.Request.QueryString["code"];
                    var token = FetchTokenFromRequest(code);
                    
                    //////////////////////////////////////////////////////////////
                    Session["strava_token"] = token;
                    Strava.Authentication.StaticAuthentication auth = new StaticAuthentication(token);
                    Session["strava_auth"] = auth;
                    StravaClient client = new StravaClient(auth);

                    //AthleteClient athleteclient = new AthleteClient(auth);
                    //var a = athleteclient.GetAthlete();
                    //HttpContext.Current.Response.Write(a.FirstName + " " + a.LastName); 
                }
            } 
        }

        private string FetchTokenFromRequest(string code)
        {
            //todo: Add the client code to the config file from web.config
            
            var request = (HttpWebRequest)WebRequest.Create(@"https://www.strava.com/oauth/token");
            var postData = "client_id=826";
            postData += "&client_secret=bc2300a4801270f3e35ae85dfec5af3ff00d3796";
            postData += "&code=" + code;

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            JToken token = JObject.Parse(responseString);

            string access_token = (string)token.SelectToken("access_token");

            return access_token;
        }


        // convert datetime to unix epoch seconds
        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }
    }
}