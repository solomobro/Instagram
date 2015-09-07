using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.WebApiDemo.Settings
{
    class EnvironmentManager
    {
        private const string InstagramEnv = "INSTAGRAM_ENV";

        public string GetEnvironmentName()
        {
            var env = Environment.GetEnvironmentVariable(InstagramEnv);
            if (env == InstagramEnv)
            {
                return string.Empty;
            }

            return env;
        }

        public void SetSettingsKey(ApplicationSettingsBase settings)
        {
            settings.SettingsKey = GetEnvironmentName();
        }
    }
}
