using BusinessEntities;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_EditCommonItems.xaml
    /// </summary>
    public partial class UC_EditCommonItems : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        private IStock selectedItem;
        public UC_EditCommonItems(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshBarStockList();
            Model.refreshCommonItemsList();
            Populate_Items();
        }
        private void Refresh()
        {
            StockList.UnselectAll();
            Model.refreshBarStockList();
            Model.refreshCommonItemsList();
            StockList.Items.Clear();
            CommonItems.Items.Clear();
            Populate_Items();
        }
        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }
        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            if (StockList.SelectedIndex >= 0)
            {
                string content = ((ListBoxItem)StockList.SelectedItem).Content.ToString();
                string[] splitcontent = content.Split('-');
                string val = splitcontent[0];

                int stockID = Convert.ToInt32(((ListBoxItem)StockList.SelectedItem).Tag.ToString());

                foreach (IStock item in Model.BarStockList)
                {
                    if (item.StockID.Equals(stockID) && item.NameStock.Equals(val))
                    {
                        selectedItem = item;
                        return;
                    }
                }
            }
            else if (CommonItems.SelectedIndex >=0)
            {
                string content = ((ListBoxItem)CommonItems.SelectedItem).Content.ToString();
                string[] splitcontent = content.Split('-');
                string val = splitcontent[0];

                int stockID = Convert.ToInt32(((ListBoxItem)CommonItems.SelectedItem).Tag.ToString());

                foreach (IStock item in Model.CommonItemsList)
                {
                    if (item.StockID.Equals(stockID) && item.NameStock.Equals(val))
                    {
                        selectedItem = item;
                        return;
                    }
                }
            }
        }

        private void Populate_Items()
        {
            if (Model.BarStockList != null)
            {
                foreach (Stock stock in Model.BarStockList)
                {
                    StockList.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                }
            }
            if (Model.CommonItemsList != null)
            {
                foreach (Stock stock in Model.CommonItemsList)
                {
                    CommonItems.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                }
            }

        }
        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            if (StockList.SelectedIndex >= 0)
            {
                Model.addCommonItemDB(selectedItem);
                CommonItems.Items.Add(new ListBoxItem { Content = string.Concat(selectedItem.NameStock, "-", selectedItem.Size), Tag = selectedItem.StockID });
            }
            else
            {
                MessageBox.Show("You can only add items from the Stock list");
            }
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommonItems.SelectedIndex >= 0)
            {
                Model.removeCommonItemDB(selectedItem);
                CommonItems.Items.Remove(CommonItems.SelectedItem);
            }
            else
            {
                MessageBox.Show("You can only remove items from the Common Items list");
            }
        }

        private void ResetSale_Click(object sender, RoutedEventArgs e)
        {
            CommonItems.Items.Clear();
        }
        private void UpdateSearch()
        {
            StockList.Items.Clear();
            Populate_Items();
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
                    StockList.Items.Clear();

                    foreach (IStock stock in Model.BarStockList)
                    {
                        if (stock.StockID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.NameStock.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.Type.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            StockList.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = StockList.Items;
                    StockList.Items.Clear();

                    foreach (IStock stock in Model.BarStockList)
                    {
                        StockList.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                    }
                }
            }
        }

        private void Searchtxt_TextChanged(object sender, TextChangedEventArgs e)
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
                    StockList.Items.Clear();

                    foreach (IStock stock in Model.BarStockList)
                    {
                        if (stock.StockID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.NameStock.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.Type.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            StockList.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = StockList.Items;
                    StockList.Items.Clear();

                    foreach (IStock stock in Model.BarStockList)
                    {
                        StockList.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                    }
                }
            }
        }

        private void CommonItemsBack_Click(object sender, RoutedEventArgs e)
        {

            UC_RecordBarSale c = new UC_RecordBarSale(parent, Model);
            ContextSwitcher._context.SwitchScreen(c);
        }
    }
}
