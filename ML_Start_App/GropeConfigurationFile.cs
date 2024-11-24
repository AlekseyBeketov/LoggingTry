using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Serilog;

namespace ML_Start_App
{
    public class GropeConfigurationFile
    {
        internal static string Name { get; set; }
        internal static string Female { get; set; }
        internal static int N { get; set; }
        internal static int L { get; set; }
        public static int Delay { get; set; }

        public static void CreateConfigureFile(string filePath)
        {
            N = Name.Count();
            L = Female.Count();
            Delay = 1000;

            var customConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            customConfig.AppSettings.Settings.Clear(); // Очищаем старые настройки

            customConfig.AppSettings.Settings.Add("N", N.ToString());
            customConfig.AppSettings.Settings.Add("L", L.ToString());
            customConfig.AppSettings.Settings.Add("Delay", Delay.ToString());
            customConfig.SaveAs(filePath, ConfigurationSaveMode.Modified);
        }

        internal static void LoadSettings(string configFilePath)
        {
            var map = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };
            var customConfig = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            // Читаем настройки из файла конфигурации
            var configData = customConfig.AppSettings.Settings;

            if (configData != null)
            {
                N = Convert.ToInt32(configData["N"].Value);
                Log.Information("Происходит процесс конвертирования из строки в Int для извлечения N");
                L = Convert.ToInt32(configData["L"].Value);
                Log.Information("Происходит процесс конвертирования из строки в Int для извлечения L");
                Delay = Convert.ToInt32(configData["Delay"].Value);
                Log.Information("Происходит процесс конвертирования из строки в Int для извлечения Delay");

                Log.Information($"Значение N и L успешно извлечены из конфигурационного файла! N = {N}, L = {L}, Задержка = {Delay/1000} с. ");
            }
        }
    }
}
