using GrapeCity.Documents.Html;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class PdfExportBLL
    {
        public void ExportToPDF(string reportName, string content)
        {
            var reportsPath = Session.Settings.ReportsPath;
            if (!Directory.Exists(reportsPath))
                Directory.CreateDirectory(reportsPath);

            using (var re = new GcHtmlRenderer(content))
            {
                var pdfSettings = new PdfSettings()
                {
                    Margins = new Margins(0.2f, 1, 0.2f, 1),
                    IgnoreCSSPageSize = true
                };

                re.RenderToPdf(Path.GetFullPath(reportsPath) + reportName, pdfSettings);
            }
        }
    }
}
