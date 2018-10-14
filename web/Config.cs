using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace web
{
    public static class Config
    { 
        public static string Ambiente { get { return ConfigurationManager.AppSettings["AMBIENTE"]; } }

        public static string AssemblyVersion = typeof(Config).Assembly.GetName().Version.ToString();

        private static AppSetting _appSettings;
        public static AppSetting AppSettings
        {
            get
            {
                if (_appSettings == null)
                    Load();
                return _appSettings;
            }
            set
            {
                _appSettings = value;
            }
        }

        public static void Load()
        {
            if (Config.Ambiente == null)
                throw new KeyNotFoundException("Parametro AMBIENTE nao informado.");

            var file = string.Format("{0}\\App_Data\\config.{1}.json",
                System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory),
                Config.Ambiente.ToLower());

            using (var streamReader = new StreamReader(file))
            {
                string json = streamReader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Data>(json);

                AppSettings = data.AppSettings;
            }
        }

        public class AppSetting
        {
            public string API_URL { get; set; }
        }

        private class Data
        {
            public AppSetting AppSettings { get; set; }
        }
    }
}