using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Task.Common
{
    public class CommonFunctions
    {
        public object HandleNull(object field)
        {
            if (DBNull.Value == field) { return null; }
            else { return field; }
        }
        public string DatePattern()
        {
            return "dd-MMM-yyyy";
        }
        public string DatePatternGap()
        {
            return "dd MMM yyyy";
        }
        public string DateTimePattern()
        {
            return "dd-MMM-yyyy HH:mm tt";
        }
        public string TimePattern()
        {
            return "HH:mm:ss";
        }
        public string TimePatternAMPM()
        {
            return "HH:mm tt";
        }
        public string DatePattern(string pattern)
        {
            return pattern;
        }
        public string IsEmpty(string field)
        {
            if (field == string.Empty) { return ""; }
            else return field.ToString();
        }
        public string convertQuotes(string str)
        {
            //str = str.Replace("’", "");
            str = str.Replace("’", "'");
            return str.Replace("'", "''");
            //return str.Replace("’", "''");

        }


        //for header color changed
        public static void WriteHtmlTable<T>(IEnumerable<T> data, TextWriter output)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    //  Create a form to contain the List
                    Table table = new Table();
                    TableRow row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.Name;
                        hcell.BackColor = System.Drawing.Color.CadetBlue;
                        row.Cells.Add(hcell);
                    }

                    table.Rows.Add(row);

                    //  add each of the data item to the table
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    output.Write(sw.ToString());
                }
            }

        }


    }
}