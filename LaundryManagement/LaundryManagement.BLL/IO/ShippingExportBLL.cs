using GrapeCity.Documents.Html;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Extensions;
using LaundryManagement.Services;
using Stubble.Core.Builders;
using System;
using System.IO;
using System.Reflection;

namespace LaundryManagement.BLL
{
    public class ShippingExportBLL
    {
        private readonly PdfExportBLL _pdfExportBLL;

        public ShippingExportBLL()
        {
            this._pdfExportBLL = new PdfExportBLL();
        }

        public void Export(ShippingDTO shipping)
        {
            string templatePath = Path.Combine(Session.Settings.ReportTemplatesPath, "ShippingReport.html");
            string template = File.ReadAllText(templatePath.GetRelativePath());

            var builder = new StubbleBuilder();
            var boundTemplate = builder.Build().Render(template, new { Query = shipping, Detail = shipping.ShippingDetail });

            _pdfExportBLL.ExportToPDF($"Shipping{shipping.Id}.pdf", boundTemplate);
        }
    }
}
