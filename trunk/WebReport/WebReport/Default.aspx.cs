using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Dynamic;
using FastReport;
using FastReport.Web;

namespace Web
{
    public partial class _Default : System.Web.UI.Page
    {
        public string id
        {
            get { if (!string.IsNullOrEmpty(Request["id"]))return Request["id"]; return string.Empty; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string reportFile = "~/Report/Master_Detail.frx";
                if (id != string.Empty) reportFile = "~/report/" + id + ".frx";
                WebReport1.ReportFile = reportFile;
                WebReport1.Prepare();
            }
        }

        protected void WebReport1_StartReport(object sender, EventArgs e)
        {
            Report frport = (sender as WebReport).Report;
            switch (id)
            {
                case "Master_Detail":
                    GetCategorys(frport);
                    GetProducts(frport);
                    break;
                case "Groups":
                    GetProducts(frport);
                    break;
                case "Complex":
                    GetCustomers(frport);
                    GetProducts(frport);
                    GetOrders(frport);
                    GetOrderDetails(frport);
                    break;
                case "Simple_List":
                case "Wrapped":
                    GetEmployees(frport);
                    break;
                case "Labels":case "Mail_Merge":
                    GetCustomers(frport);
                    break;
                case "Subreports":case "Side_by_Side":
                    GetProducts(frport);
                    GetSuppliers(frport);
                    break;
                case "Matrix":
                case "Two_Matrices":
                case "Matrix_Rows_1":
                case "Matrix_Columns_1":
                case "Two_Column_Dimensions":
                case "Two_Row_Dimensions":
                case "Two_Cell_Dimensions":
                case "Two_Cell_Dimensions_Side_by_Side":
                case "Print_Month_Names":
                case "Objects_Inside_The_Matrix":
                case "Microsoft_Chart_Sample":
                    GetMatrixDemo(frport);
                    break;
                default:
                    GetCategorys(frport);
                    GetProducts(frport);
                    GetSuppliers(frport);
                    GetOrders(frport);
                    GetOrderDetails(frport);
                    GetCustomers(frport);
                    GetEmployees(frport);
                    GetShippers(frport);
                    break;
            }
        }
        /// <summary>
        /// 用list读取XML
        /// </summary>
        /// <param name="frport"></param>
        public void GetMatrixDemo1(Report frport)
        {
            #region xml data
            string xml = @"<root>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>1999</Year>
                                <Month>2</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>1999</Year>
                                <Month>11</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>1999</Year>
                                <Month>12</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1200</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2000</Year>
                                <Month>1</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1300</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2000</Year>
                                <Month>2</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1400</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2001</Year>
                                <Month>2</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1500</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2001</Year>
                                <Month>3</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1600</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2002</Year>
                                <Month>1</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1700</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>2002</Year>
                                <Month>1</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1800</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>1999</Year>
                                <Month>10</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1900</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>1999</Year>
                                <Month>11</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>2000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>2000</Year>
                                <Month>2</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>2100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Janet Leverling</Name>
                                <Year>1999</Year>
                                <Month>10</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>3000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Janet Leverling</Name>
                                <Year>1999</Year>
                                <Month>11</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>3100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Janet Leverling</Name>
                                <Year>2000</Year>
                                <Month>3</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>3200</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Steven Buchanan</Name>
                                <Year>2001</Year>
                                <Month>1</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>4000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Steven Buchanan</Name>
                                <Year>2001</Year>
                                <Month>2</Month>
                                <ItemsSold>4</ItemsSold>
                                <Revenue>4100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Steven Buchanan</Name>
                                <Year>2000</Year>
                                <Month>1</Month>
                                <ItemsSold>4</ItemsSold>
                                <Revenue>3999</Revenue>
                              </MatrixDemo>
                            </root>";
            #endregion
            dynamic dx = new DynamicXml(xml);
            List<object> dataTable = new List<object>();
            foreach (var item in dx.MatrixDemo)
            {
                dataTable.Add(new
                {
                    Name = item.Name.Value,
                    Year = item.Year.Value,
                    Month = item.Month.Value,
                    ItemsSold = item.ItemsSold.Value,
                    Revenue = decimal.Parse(item.Revenue.Value)
                });
            }
            frport.RegisterData(dataTable, "MatrixDemo");
        }
        /// <summary>
        /// linq to xml
        /// </summary>
        /// <param name="frport"></param>
        public void GetMatrixDemo(Report frport)
        {
            #region xml data
            XElement root = XElement.Parse(@"<root>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>1999</Year>
                                <Month>2</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>1999</Year>
                                <Month>11</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>1999</Year>
                                <Month>12</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1200</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2000</Year>
                                <Month>1</Month>
                                <ItemsSold>1</ItemsSold>
                                <Revenue>1300</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2000</Year>
                                <Month>2</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1400</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2001</Year>
                                <Month>2</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1500</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2001</Year>
                                <Month>3</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1600</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Nancy Davolio</Name>
                                <Year>2002</Year>
                                <Month>1</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1700</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>2002</Year>
                                <Month>1</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1800</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>1999</Year>
                                <Month>10</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>1900</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>1999</Year>
                                <Month>11</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>2000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Andrew Fuller</Name>
                                <Year>2000</Year>
                                <Month>2</Month>
                                <ItemsSold>2</ItemsSold>
                                <Revenue>2100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Janet Leverling</Name>
                                <Year>1999</Year>
                                <Month>10</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>3000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Janet Leverling</Name>
                                <Year>1999</Year>
                                <Month>11</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>3100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Janet Leverling</Name>
                                <Year>2000</Year>
                                <Month>3</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>3200</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Steven Buchanan</Name>
                                <Year>2001</Year>
                                <Month>1</Month>
                                <ItemsSold>3</ItemsSold>
                                <Revenue>4000</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Steven Buchanan</Name>
                                <Year>2001</Year>
                                <Month>2</Month>
                                <ItemsSold>4</ItemsSold>
                                <Revenue>4100</Revenue>
                              </MatrixDemo>
                              <MatrixDemo>
                                <Name>Steven Buchanan</Name>
                                <Year>2000</Year>
                                <Month>1</Month>
                                <ItemsSold>4</ItemsSold>
                                <Revenue>3999</Revenue>
                              </MatrixDemo>
                            </root>");
            #endregion
            var query = (from item in root.Descendants("MatrixDemo")
                         select new
                         {
                             Name = item.Element("Name").Value,
                             Year = int.Parse(item.Element("Year").Value),
                             Month = int.Parse(item.Element("Month").Value),
                             ItemsSold = int.Parse(item.Element("ItemsSold").Value),
                             Revenue = decimal.Parse(item.Element("Revenue").Value)
                         }).OrderBy(p => p.Year).ThenBy(p => p.Month); 
            frport.RegisterData(query.ToList(), "MatrixDemo");
        }
        private void GetOrders(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from [Orders]").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Orders");
        }
        private void GetOrderDetails(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from [Order Details]").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Order Details");
        }
        private void GetShippers(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from [Shippers]").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Shippers");
        }
        private void GetSuppliers(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from Suppliers").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Suppliers");
        }
        private void GetCustomers(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from Customers").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Customers");
        }
        private void GetEmployees(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from Employees").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Employees");
        }
        private void GetCategorys(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from Categories").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Categories");
        }
        private void GetProducts(Report frport)
        {
            var dataTable = DALHelper.DBHelper.ExecuteQuery(CommandType.Text, @"select * from Products").Tables[0];
            frport.RegisterData(dataTable, "NorthWind.Products");
        }
    }
}
