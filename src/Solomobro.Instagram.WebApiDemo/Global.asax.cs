using Solomobro.Instagram.WebApiDemo.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Solomobro.Instagram.WebApiDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // set environment settings key
            EnvironmentManager.SetSettingsKey(Properties.Settings.Default);

            // load secret app authentication settings
            using (var file = File.OpenRead(Properties.Settings.Default.InstagramSetingsFilePath))
            {
                AuthSettings.LoadSettings(file);
            }
        }
    }
}
