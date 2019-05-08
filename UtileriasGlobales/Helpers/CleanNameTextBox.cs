using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UtileriasGlobales.Helpers
{
    public class CleanNamesTextBox : TextBox
    {

        private class CleanNamesHtmlTextWriter : HtmlTextWriter
        {

            public CleanNamesHtmlTextWriter(TextWriter writer)
                : base(writer)
            {
            }

            public override void AddAttribute(System.Web.UI.HtmlTextWriterAttribute key, string value)
            {
                value = value.Split('$')[value.Split('$').Length - 1];
                base.AddAttribute(key, value);
            }

        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            CleanNamesHtmlTextWriter noNamesWriter = new CleanNamesHtmlTextWriter(writer);
            base.Render(noNamesWriter);
        }

        public CleanNamesTextBox(string id, string text, string cssClass, ClientIDMode clientIDMode)
            : base()
        {
            this.ID = id;
            this.CssClass = cssClass;
            this.ClientIDMode = clientIDMode;
            this.Text = text;
        }

    }
}
