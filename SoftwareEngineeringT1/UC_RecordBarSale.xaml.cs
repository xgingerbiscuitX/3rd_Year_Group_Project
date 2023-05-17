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
    /// Interaction logic for UC_RecordBarSale.xaml
    /// </summary>
    public partial class UC_RecordBarSale : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        private IStock selectedItem;
        public UC_RecordBarSale(Test parent, IAccessHandler Model)
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
            commonList.Items.Clear();
            CurrentSale.Items.Clear();
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
                    commonList.Items.Add(new ListBoxItem { Content = string.Concat(stock.NameStock, "-", stock.Size), Tag = stock.StockID });
                }
            }
            
        }
        private void AddToSale_Click(object sender, RoutedEventArgs e)
        {
            string quantity = Quantitytxt.Text;
            var isNumeric = int.TryParse(quantity, out int n);
            if (isNumeric == false)
            {
                MessageBox.Show("Your quantity contains non-numeric values");
                Quantitytxt.Clear();
                Quantitytxt.Text = "Quantity";
            }
            if(isNumeric == true && Convert.ToInt32(quantity) > selectedItem.Amount)
            {
                MessageBox.Show("Your quantity exceeds amount of stock available.\nQuantity of " + selectedItem.NameStock + " in stock is " + selectedItem.Amount);
            }
            else if(isNumeric == true && Convert.ToInt32(quantity) <= selectedItem.Amount)
            {
                CurrentSale.Items.Add(new ListBoxItem { Content = string.Concat("(", selectedItem.StockID, ")", selectedItem.NameStock, "-", quantity) });
            }
        }
        private void Quantitytxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Quantitytxt.Text == "Quantity")
            {
                Quantitytxt.Clear();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentSale.Items.Remove(CurrentSale.SelectedItem);
        }

        private void ResetSale_Click(object sender, RoutedEventArgs e)
        {
            CurrentSale.Items.Clear();
        }

        private void RecordSale_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSale.Items.Count == 0)
            {
                MessageBox.Show("There is currently no items in the current sale");
            }
            else
            {
                string orderID = Guid.NewGuid().ToString();
                string[] splitID = orderID.Split('-');
                orderID = string.Concat(splitID[0], splitID[1]);
                foreach (ListBoxItem a in CurrentSale.Items)
                {
                    string content = ((ListBoxItem)a).Content.ToString();
                    string[] splitcontent = content.Split(')', '-');
                    string Item_ID = splitcontent[0];
                    Item_ID = Item_ID.Replace("(", "");
                    //splitcontent = content.Split('-');
                    string Item_Name = splitcontent[1];
                    int CQuantity = Convert.ToInt32(splitcontent[2]);

                    Model.addNewBarSale(orderID, Item_ID, Item_Name, CQuantity);

                }
                StockList.UnselectAll();
                Refresh();
                CurrentSale.Items.Clear();
                MessageBox.Show("Bar Sale successful and saved to DB");

            }
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

        private void CommonItemsEdit_Click(object sender, RoutedEventArgs e)
        {
            UC_EditCommonItems c = new UC_EditCommonItems(parent, Model);
            ContextSwitcher._context.SwitchScreen(c);
        }

        private void commonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commonList.SelectedIndex >= 0)
            {
                string content = ((ListBoxItem)commonList.SelectedItem).Content.ToString();
                string[] splitcontent = content.Split('-');
                string val = splitcontent[0];

                int stockID = Convert.ToInt32(((ListBoxItem)commonList.SelectedItem).Tag.ToString());

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
    }
}
