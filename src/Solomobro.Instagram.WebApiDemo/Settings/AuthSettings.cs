using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.WebApiDemo.Settings
{
    internal static class AuthSettings
    {
        //Instagram Settings
        public static string InstaClientId { get; set; }
        public static string InstaClientSecret { get; set; }
        public static string InstaWebsiteUrl { get; set; } 
        public static string InstaRedirectUrl { get; set; }


        internal static void LoadSettings()
        {
            //Load all fields to settings file
            Type settingsType = typeof(AuthSettings);
            PropertyInfo[] fields = settingsType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            //Load all fields into set
            var fieldSet = new HashSet<string>(fields.Select(m => m.Name));
            var settingsFile = Properties.Settings.Default.InstagramSetingsFilePath;

            using (var sr = new StreamReader(settingsFile))
            {
                try
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Escape character used for comments
                        if (line.StartsWith("#"))
                            continue;

                        var kvp = line.Split('=');

                        if (kvp.Count() == 2 && fieldSet.Contains(kvp[0]))
                        {
                            var propertyInfo = settingsType.GetProperty(kvp[0], BindingFlags.Static | BindingFlags.Public);

                            propertyInfo?.SetValue(null, kvp[1]);
                        }

                    }
                }
                catch 
                {
                    // todo: add some logging here
                }
            }
        }
    }
}
