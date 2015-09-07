using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Solomobro.Instagram.WebApiDemo.Settings
{
    public class SettingsLoader
    {
        public static void LoadSettings()
        {
            //Load all fields to settings file
            Type settingsType = typeof(Settings);
            PropertyInfo[] fields = settingsType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            //Load all fields into set
            var fieldSet = new HashSet<string>(fields.Select(m => m.Name));
            var settingsFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            settingsFile += "/Settings.txt";
            settingsFile = settingsFile.Remove(0, 6);

            using (var sr = new StreamReader(settingsFile))
            {
                try
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        //Escape character used for comments
                        if (line.StartsWith("#"))
                            continue;

                        string[] kvp = line.Split('=');

                        if (kvp != null && kvp.Count() == 2 && fieldSet.Contains(kvp[0]))
                        {
                            var propertyInfo = settingsType.GetProperty(kvp[0], BindingFlags.Static | BindingFlags.Public);

                            if (propertyInfo != null)
                                propertyInfo.SetValue(null, kvp[1]);
                        }

                    }
                }
                catch(Exception ex)
                {
                    
                }
            }
        }
    }
}