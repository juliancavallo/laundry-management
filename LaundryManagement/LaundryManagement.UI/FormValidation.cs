using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaundryManagement.UI
{
    public static class FormValidation
    {
        public static void ValidateTextBoxCompleted(List<TextBox> list)
        {
            if (list.Any(x => string.IsNullOrWhiteSpace(x.Text)))
                throw new Exception("Must complete all textboxes");
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
