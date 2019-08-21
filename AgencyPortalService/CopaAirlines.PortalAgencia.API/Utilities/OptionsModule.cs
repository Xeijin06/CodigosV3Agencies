using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Portaldeagencias
{
    public class OptionsModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            string origins = ConfigurationManager.AppSettings.Get("OriginCORS");
            context.BeginRequest += (sender, args) =>
            {
                var app = (HttpApplication)sender;

                if (app.Request.HttpMethod == "OPTIONS")
                {
                    app.Response.StatusCode = 200;
                    app.Response.AddHeader("Access-Control-Allow-Headers", "content-type");
                    app.Response.AddHeader("Access-Control-Allow-Origin", origins);
                    app.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                    app.Response.AddHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS");
                    app.Response.AddHeader("Content-Type", "application/json");
                    app.Response.End();
                }
            };
        }

        public void Dispose()
        {
        }
    }
}