using LaundryManagement.Domain.DTOs;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class JsonExportBLL
    {
        public JsonExportBLL()
        {
            var reportsPath = Session.Settings.ReportsPath;
            if (!Directory.Exists(reportsPath))
                Directory.CreateDirectory(reportsPath);
        }

        public void Export(object items, string reportName)
        {
            var result = JsonSerializer.Serialize(items, options: new JsonSerializerOptions()
            {
                WriteIndented = true,
            });
            File.WriteAllText(Session.Settings.ReportsPath + $"/{reportName}.json", result);
        }
    }
}
