using BusinessLayer;
using BusinessEntities;
using SoftwareEngineeringTeam1;
using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Reporting.WebForms;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_ManageStock.xaml
    /// </summary>
    public partial class UC_ManageStock : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        private IStock SelectedStock = new Stock();
        public UC_ManageStock(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Refresh();
            Populate_Stock();
        }
        public void Refresh()
        {
            Model.refreshStockList();
        }

        private void UpdateSearch()
        {
            StockListBox.Items.Clear();
            Populate_Stock();
        }

        public void Populate_Stock()
        {
            if (Model.StockList != null)
            {
                foreach (Stock stock in Model.StockList)
                {
                    //StockListBox.Items.Add(FormateStock(stock.NameStock, stock.Size));
                    StockListBox.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });

                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Searchtxt.Text == "")
            {
                UpdateSearch();

            }

            if (Searchtxt.Text != "Search")
            {

                string userInput = Searchtxt.Text;
                if (String.IsNullOrEmpty(Searchtxt.Text.Trim()) == false)
                {
                    StockListBox.Items.Clear();
                    foreach (IStock stock in Model.StockList)
                    {
                        if (stock.NameStock.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.Location.ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {

                            StockListBox.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                        }

                    }



                }
                else if (Searchtxt.Text.Trim() == "")
                {

                    ItemCollection listboxlist = StockListBox.Items;
                    StockListBox.Items.Clear();

                    foreach (IStock stock in Model.StockList)
                    {
                        StockListBox.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });

                    }


                }
            }
        }


        private void StockListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StockListBox.SelectedIndex != -1)
            {
                //string val = StockListBox.SelectedItem.ToString();
                //string joinedName = ((ListBoxItem)StockListBox.SelectedValue).Content.ToString();
                //string[] splitName = joinedName.Split(' ');
                //string name;
                ////  MessageBox.Show(splitName.Count().ToString());
                //if (splitName.Count() == 3)
                //{
                //    name = splitName[0] + ' ' + splitName[1];
                //}
                //else
                //{
                //    name = splitName[0];
                //}
                int stockNo = Convert.ToInt32(((ListBoxItem)StockListBox.SelectedItem).Tag.ToString());

                ArrayList StockList = Model.StockList;

                foreach (Stock stock in StockList)
                {
                    if (stock.StockID.Equals(stockNo))
                    {
                        StID.Content = string.Concat("StockID: ".PadRight(15), stock.StockID);
                        nameS.Content = string.Concat("Name: ".PadRight(14), stock.NameStock);
                        type.Content = string.Concat("Type: ".PadRight(16), stock.Type);
                        amount.Content = string.Concat("Amount: ".PadRight(13), stock.Amount);
                        price.Content = string.Concat("Price: ".PadRight(18), stock.Price);
                        size.Content = string.Concat("Size: ".PadRight(18), stock.Size);
                        location.Content = string.Concat("Location: ".PadRight(15), stock.Location);

                        SelectedStock = stock;

                        return;

                    }

                }
            }
        }

        private void Adjust_Price_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stock_Report_Click(object sender, RoutedEventArgs e)
        {
            ExportToPDF();
        }

        private void ExportToPDF()
        {
            string deviceInfo = "";
            string[] steamIds;
            Microsoft.Reporting.WinForms.Warning[] warnings;

           
            string mimetype = string.Empty;
            string encoding = string.Empty;
            string extention = string.Empty;

            Microsoft.Reporting.WinForms.ReportViewer viewer = new Microsoft.Reporting.WinForms.ReportViewer();
            viewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "StockReport.rdlc";


            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("StockDS", Model.StockList));
            viewer.RefreshReport();
            var bytes = viewer.LocalReport.Render("PDF", deviceInfo,out mimetype, out encoding, out extention, out steamIds, out warnings);

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string QRDirectory = string.Concat(projectDirectory, @"\Reports\");
            string name = DateTime.Now.ToString("dd-MM-yyyy");

            string fileName = string.Concat(QRDirectory,name,".pdf");
            //MessageBox.Show(fileName);
            File.WriteAllBytes(fileName, bytes);
            System.Diagnostics.Process.Start(fileName);
        
        }

      

        private void Searchtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "")
            {
                Refresh();
            }

            if (Searchtxt.Text != "Search")
            {

                string userInput = Searchtxt.Text;

                if (String.IsNullOrEmpty(Searchtxt.Text.Trim()) == false)
                {
                    StockListBox.Items.Clear();

                    foreach (IStock stock in Model.StockList)
                    {
                        if (stock.StockID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.NameStock.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.Type.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            StockListBox.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = StockListBox.Items;
                    StockListBox.Items.Clear();

                    foreach (IStock stock in Model.StockList)
                    {
                        StockListBox.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                    }
                }
            }
        }
    }
}