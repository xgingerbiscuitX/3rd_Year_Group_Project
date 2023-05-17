using BusinessEntities;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections;
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
    /// Interaction logic for UC_AddOrder.xaml
    /// </summary>
    public partial class UC_AddOrder : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        private IStock selectedItem;
        public UC_AddOrder(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Model.refreshStockList();
            Populate_List();
            Populate_Popular();
        }

        private void RefreshDetails()
        {
            detailList.Items.Clear();
        }


        private void Populate_Popular()
        {
            Model.StockList.Sort(new PopularSorter());

            int i = 0;
            foreach (IStock item in Model.StockList)
            {
                if (i <= 10)
                {
                    MostPopularItems.Items.Add(new ListBoxItem { Content = item.NameStock, Tag = item.StockID });
                    i++;
                }

                else return;
            }


        }

       

        private void Refresh()
        {
            ItemList.Items.Clear();
            detailList.Items.Clear();
            Model.refreshOrderList();
            Populate_List();
        }

        private void Populate_List()
        {
            foreach(IStock item in Model.StockList)
            {
                ItemList.Items.Add(new ListBoxItem { Content = item.NameStock, Tag = item.StockID});
            }
        }

        private void ItemSelected(object sender, RoutedEventArgs e)
        {
            RefreshDetails();
            if (ItemList.SelectedIndex == -1)
            {
                return;
            }
            string val = ((ListBoxItem)ItemList.SelectedValue).Content.ToString();

            int stockID = Convert.ToInt32(((ListBoxItem)ItemList.SelectedItem).Tag.ToString());

            foreach (IStock item in Model.StockList)
            {
                if(item.StockID.Equals(stockID) && item.NameStock.Equals(val))
                {
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item name: ", ((ListBoxItem)ItemList.SelectedValue).Content.ToString()) });
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item type: ", item.Type )});
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item price: ", item.Price )});
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("In-stock: ", item.Amount )});
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item size: ", item.Size )});
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Location: ", item.Location )});
                    selectedItem = item;
                    return;
                }
            }
        }

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            string quantity = Quantitytxt.Text;
            OrderItems.Items.Add(new ListBoxItem { Content = string.Concat(selectedItem.NameStock , "-", quantity)});
        }
        private void Quantitytxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Quantitytxt.Text == "Quantity")
            {
                Quantitytxt.Clear();
            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            string orderID = Guid.NewGuid().ToString();
            string[] splitID = orderID.Split('-');
            orderID = string.Concat(splitID[0], splitID[1]);
            foreach (ListBoxItem a in OrderItems.Items)
            {
                string Item_ID = "x";
                string content = ((ListBoxItem)a).Content.ToString();
                string[] splitcontent = content.Split('-');
                string Item_Name = splitcontent[0];
                int CQuantity = Convert.ToInt32(splitcontent[1]);

                Model.addNewOrder(orderID, Item_ID, Item_Name, CQuantity);
            }

            OrderItems.Items.Clear();
            MessageBox.Show("Order successfully made and saved to DB");
            Refresh();


        }

        private void MostPopularItems_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RefreshDetails();
            if (ItemList.SelectedIndex == -1)
            {
                return;
            }
            string val = ((ListBoxItem)MostPopularItems.SelectedValue).Content.ToString();

            int stockID = Convert.ToInt32(((ListBoxItem)MostPopularItems.SelectedItem).Tag.ToString());

            foreach (IStock item in Model.StockList)
            {
                if (item.StockID.Equals(stockID) && item.NameStock.Equals(val))
                {
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item name: ", ((ListBoxItem)MostPopularItems.SelectedValue).Content.ToString()) });
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item type: ", item.Type) });
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item price: ", item.Price) });
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("In-stock: ", item.Amount) });
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Item size: ", item.Size) });
                    detailList.Items.Add(new ListBoxItem { Content = string.Concat("Location: ", item.Location) });
                    selectedItem = item;
                    return;
                }
            }
        }

        private void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Searchtxt.Text == "Search")
                Searchtxt.Clear();
        }

        private void Searchbtn_Click(object sender, RoutedEventArgs e)
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
                    ItemList.Items.Clear();

                    foreach (IStock stock in Model.StockList)
                    {
                        if (stock.StockID.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.NameStock.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.Type.ToString().ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {
                            ItemList.Items.Add(new ListBoxItem { Content = stock.NameStock, Tag = stock.StockID });
                        }
                    }
                }

                else if (Searchtxt.Text.Trim() == "")
                {
                    ItemCollection listboxlist = ItemList.Items;
                    ItemList.Items.Clear();

                    foreach (IStock stock in Model.StockList)
                    {
                        ItemList.Items.Add(new ListBoxItem { Content = stock.NameStock, Tag = stock.StockID });
                    }
                }
            }
        }
    }
}
