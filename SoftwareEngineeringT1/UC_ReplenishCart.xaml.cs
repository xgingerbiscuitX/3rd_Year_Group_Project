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
    /// Interaction logic for UC_ReplenishCart.xaml
    /// </summary>
    public partial class UC_ReplenishCart : UserControl
    {
        IAccessHandler Model;
        private Test parent;
        public UC_ReplenishCart(Test parent, IAccessHandler Model)
        {
            this.parent = parent;
            this.Model = Model;
            InitializeComponent();
            FillTable();
        }

        public void FillTable() 
        {
            foreach (IStock stock in Model.StockList)
            {
                if (stock.Location == "CleaningStock")
                {
                    LB_Stock.Items.Add(new ListBoxItem() {Content=stock.NameStock, Tag=stock});
                }
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (LB_Stock.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item from stock");
                return;
            }
            int amount = 1;
            Int32.TryParse(TBX_Ammount.Text, out amount);
            if (amount == 0)
            {
                amount = 1;
            }
            if (amount > ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Amount)
            {
                MessageBox.Show("You cannot take out more than there is in stock\nThere are " + ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Amount + " items of " + ((ListBoxItem)LB_Stock.SelectedItem).Content + " in stock ");
                return;
            }
            foreach (ListBoxItem item in LB_Cart.Items)
            {
                if (item.Content.ToString().Contains(((ListBoxItem)LB_Stock.SelectedItem).Content.ToString()))
                {
                    MessageBox.Show("That item is already in the cart");
                    return;
                }

            }
            IStock theStock = StockFactory.GetStock(((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).StockID, ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).NameStock, ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Type, ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Amount - amount/*here we change amount based on amount removed*/, ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Price, ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Size, ((IStock)((ListBoxItem)LB_Stock.SelectedItem).Tag).Location);
            String name = ((ListBoxItem)LB_Stock.SelectedItem).Content.ToString();
            LB_Cart.Items.Add(new ListBoxItem() { Content = String.Concat(name + "-" + amount), Tag = theStock });
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            if (LB_Cart.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item from the Cart");
                return;
            }
            LB_Cart.Items.Remove(LB_Cart.SelectedItem);
        }

        private void Button_Click_Replenish(object sender, RoutedEventArgs e)
        {
            List<IStock> replenishList = new List<IStock>();
            foreach (ListBoxItem item in LB_Cart.Items)
            {
                replenishList.Add(((IStock)item.Tag));
            }
            bool success = Model.replenishCart(replenishList);
            LB_Cart.Items.Clear();
            TBX_Ammount.Text = "";
            if (success)
            {
                MessageBox.Show("Added items to cart");
                return;
            }
            MessageBox.Show("An error occured while trying to add items to the cart");
        }
    }
}
