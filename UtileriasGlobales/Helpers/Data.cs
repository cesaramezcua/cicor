using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using UtileriasGlobales.Constants;
using System.Reflection;
using System.Web;

namespace UtileriasGlobales.Helpers
{
    /// <summary>
    /// Clase de Manejo de Datos
    /// </summary>
    /// <remarks>
    /// 15/09/2011, CAmezcua: Creación.
    /// </remarks>
    public class Data
    {
        /// <summary>
        /// Filtra y/u Ordena DataTable con base a los parametros.
        /// </summary>
        /// <param name=”dt”>DataTable a filtrar.</param>
        /// <param name=”filtro”>Criterio de filtro.</param>
        /// <param name=”orden”>Orden</param>
        /// <returns></returns>
        public static DataTable FiltrarDataTable(DataTable dt, string filtro = null, string orden = null)
        {
            DataRow[] rows;
            DataTable dtNew;
            try
            {
                dtNew = dt.Clone();
                rows = dt.Select(filtro, orden);
                foreach (DataRow dr in rows)
                    dtNew.ImportRow(dr);
                return dtNew;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error Inesperado, Disculpe las molestias que esto le ocasione. Error- " + ex.Message);
            }
        }

         /// <summary>
        /// Obtener listado de tipo ListItem de meses en formato de número.
        /// </summary>
        /// <returns>Listado de meses</returns>
        public static List<ListItem> ObtenerMeses(){
            List<ListItem> meses = new List<ListItem>();
            string mes = string.Empty;
            for (Int32 i = 1; (i <= 12); i++)
            {
                mes = i < 10 ? Common.VALOR_CERO + i.ToString() : i.ToString();
                meses.Add(new ListItem(mes, mes));
            }
            return meses;
        }

         /// <summary>
        /// Obtener listado de tipo ListItem de años.
        /// </summary>
        /// <returns>Listado de años.</returns>
        public static List<ListItem> ObtenerAnios(){
            List<ListItem> anios = new List<ListItem>();
            string anio = string.Empty;
            Int32 currentYear = DateTime.Now.Year;

            for (Int32 i = 1; (i <= 12); i++)
            {
                anio = (currentYear + (i-1)).ToString();
                anios.Add(new ListItem(anio, anio));
            }
            return anios;
        }

        /// <summary>
        /// Exportar listado a Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ExportToExcel<T>(List<T> list, string nombreArchivo)
        {
            int columnCount = 0;

            DateTime StartTime = DateTime.Now;

            StringBuilder rowData = new StringBuilder();

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            rowData.Append("<Row ss:StyleID=\"s62\">");
            foreach (PropertyInfo p in properties)
            {
                if (p.PropertyType.Name != "EntityCollection`1" && p.PropertyType.Name != "EntityReference`1" && p.PropertyType.Name != p.Name)
                {
                    columnCount++;
                    rowData.Append("<Cell><Data ss:Type=\"String\">" + p.Name + "</Data></Cell>");
                }
                else
                    break;

            }
            rowData.Append("</Row>");

            foreach (T item in list)
            {
                rowData.Append("<Row>");
                for (int x = 0; x < columnCount; x++) //each (PropertyInfo p in properties)
                {
                    object o = properties[x].GetValue(item, null);
                    string value = o == null ? "" : o.ToString();

                    Type tipo = properties[x].PropertyType;
                    string tipoDato = "String";
                    if (tipo == typeof(int) || tipo == typeof(decimal))
                        tipoDato = "Number";
                    rowData.Append("<Cell><Data ss:Type=\""+ tipoDato + "\">" + value + "</Data></Cell>");

                }
                rowData.Append("</Row>");
            }

            var sheet = @"<?xml version=""1.0""?>
                    <?mso-application progid=""Excel.Sheet""?>
                    <Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
                        xmlns:o=""urn:schemas-microsoft-com:office:office""
                        xmlns:x=""urn:schemas-microsoft-com:office:excel""
                        xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""
                        xmlns:html=""http://www.w3.org/TR/REC-html40"">
                        <DocumentProperties xmlns=""urn:schemas-microsoft-com:office:office"">
                            <Author>KBill</Author>
                            <LastAuthor>KBill</LastAuthor>
                            <Created>2014-06-01T23:40:11Z</Created>
                            <Company>Microsoft</Company>
                            <Version>12.00</Version>
                        </DocumentProperties>
                        <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">
                            <WindowHeight>6600</WindowHeight>
                            <WindowWidth>12255</WindowWidth>
                            <WindowTopX>0</WindowTopX>
                            <WindowTopY>60</WindowTopY>
                            <ProtectStructure>False</ProtectStructure>
                            <ProtectWindows>False</ProtectWindows>
                        </ExcelWorkbook>
                        <Styles>
                            <Style ss:ID=""Default"" ss:Name=""Normal"">
                                <Alignment ss:Vertical=""Bottom""/>
                                <Borders/>
                                <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>
                                <Interior/>
                                <NumberFormat/>
                                <Protection/>
                            </Style>
                            <Style ss:ID=""s62"">
                                <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""
                                    ss:Bold=""1""/>
                            </Style>
                        </Styles>
                        <Worksheet ss:Name=""Sheet1"">
                            <Table ss:ExpandedColumnCount=""" + (properties.Count() + 1) + @""" ss:ExpandedRowCount=""" + (list.Count() + 1) + @""" x:FullColumns=""1""
                                x:FullRows=""1"" ss:DefaultRowHeight=""15"">
                                " + rowData.ToString() + @"
                            </Table>
                            <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">
                                <PageSetup>
                                    <Header x:Margin=""0.3""/>
                                    <Footer x:Margin=""0.3""/>
                                    <PageMargins x:Bottom=""0.75"" x:Left=""0.7"" x:Right=""0.7"" x:Top=""0.75""/>
                                </PageSetup>
                                <Print>
                                    <ValidPrinterInfo/>
                                    <HorizontalResolution>300</HorizontalResolution>
                                    <VerticalResolution>300</VerticalResolution>
                                </Print>
                                <Selected/>
                                <Panes>
                                    <Pane>
                                        <Number>3</Number>
                                        <ActiveCol>2</ActiveCol>
                                    </Pane>
                                </Panes>
                                <ProtectObjects>False</ProtectObjects>
                                <ProtectScenarios>False</ProtectScenarios>
                            </WorksheetOptions>
                        </Worksheet>
                        <Worksheet ss:Name=""Sheet2"">
                            <Table ss:ExpandedColumnCount=""1"" ss:ExpandedRowCount=""1"" x:FullColumns=""1""
                                x:FullRows=""1"" ss:DefaultRowHeight=""15"">
                            </Table>
                            <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">
                                <PageSetup>
                                    <Header x:Margin=""0.3""/>
                                    <Footer x:Margin=""0.3""/>
                                    <PageMargins x:Bottom=""0.75"" x:Left=""0.7"" x:Right=""0.7"" x:Top=""0.75""/>
                                </PageSetup>
                                <ProtectObjects>False</ProtectObjects>
                                <ProtectScenarios>False</ProtectScenarios>
                            </WorksheetOptions>
                        </Worksheet>
                        <Worksheet ss:Name=""Sheet3"">
                            <Table ss:ExpandedColumnCount=""1"" ss:ExpandedRowCount=""1"" x:FullColumns=""1""
                                x:FullRows=""1"" ss:DefaultRowHeight=""15"">
                            </Table>
                            <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">
                                <PageSetup>
                                    <Header x:Margin=""0.3""/>
                                    <Footer x:Margin=""0.3""/>
                                    <PageMargins x:Bottom=""0.75"" x:Left=""0.7"" x:Right=""0.7"" x:Top=""0.75""/>
                                </PageSetup>
                                <ProtectObjects>False</ProtectObjects>
                                <ProtectScenarios>False</ProtectScenarios>
                            </WorksheetOptions>
                        </Worksheet>
                    </Workbook>";

            System.Diagnostics.Debug.Print(StartTime.ToString() + " - " + DateTime.Now);
            System.Diagnostics.Debug.Print((DateTime.Now - StartTime).ToString());

            string attachment = "attachment; filename=" + nombreArchivo + ".xls";
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.Write(sheet);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.End();

        }
    }
}
