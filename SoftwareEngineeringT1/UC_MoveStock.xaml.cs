using BusinessLayer;
using BusinessEntities;
using SoftwareEngineeringTeam1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
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
    /// Interaction logic for UC_MoveStock.xaml
    /// </summary>
    public partial class UC_MoveStock : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        private IMoveStock SelectedStock = new MoveStock();
        public UC_MoveStock(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Refresh();
            Populate_Stock();
        }
        public void Populate_Stock()
        {
            if (Model.MoveStockList != null)
            {
                foreach (MoveStock stock in Model.MoveStockList)
                {
                    ////StockListBox.Items.Add(FormateStock(stock.NameStock, stock.Size));
                    Detailbox.Items.Add(new ListBoxItem { Content = string.Concat(stock.Name + " - " + stock.Type + " - " + stock.Size), Tag = stock.ID });

                }
            }



        }
        public void Refresh()
        {
            Model.refreshMoveStockList();
        }

        public void UpdateSearch()
        {
            Detailbox.Items.Clear();
            Populate_Stock();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Searchtxt_TextChanged(object sender, TextChangedEventArgs e)
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
                    Detailbox.Items.Clear();
                    foreach (IMoveStock stock in Model.MoveStockList)
                    {
                        if (stock.Name.ToLower().Contains(Searchtxt.Text.Trim().ToLower()) || stock.Type.ToLower().Contains(Searchtxt.Text.Trim().ToLower()))
                        {

                            Detailbox.Items.Add(new ListBoxItem { Content = string.Concat(stock.Name + " - " + stock.Type + " - " + stock.Size), Tag = stock.ID });
                        }

                    }



                }
                else if (Searchtxt.Text.Trim() == "")
                {

                    ItemCollection listboxlist = Detailbox.Items;
                    Detailbox.Items.Clear();

                    foreach (IMoveStock stock in Model.MoveStockList)
                    {
                        Detailbox.Items.Add(new ListBoxItem { Content = string.Concat(stock.Name + " - " + stock.Type + " - " + stock.Size), Tag = stock.ID });

                    }


                }

            }
        }

        private void Detailbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Detailbox.SelectedItem != null)
            {
                string val = Detailbox.SelectedItem.ToString();
                string joinedName = ((ListBoxItem)Detailbox.SelectedValue).Content.ToString();
                string[] splitName = joinedName.Split('-');
                string name;
                //  MessageBox.Show(splitName.Count().ToString());

                name = splitName[0];

                int stockNo = Convert.ToInt32(((ListBoxItem)Detailbox.SelectedItem).Tag.ToString());

                ArrayList MoveList = Model.MoveStockList;

                foreach (MoveStock stock in MoveList)
                {
                    if (stock.ID.Equals(stockNo))
                    {
                        barbox.Text = stock.InBar.ToString();
                        storagebox.Text = stock.InStorage.ToString();

                        SelectedStock = stock;

                        return;

                    }
                }
            }
            else
            {
                MessageBox.Show("Must Pick a Stock");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (Detailbox.SelectedItem != null)
            {
                if (String.IsNullOrEmpty(movebox.Text))
                {
                   MessageBox.Show("Must Pick a Put in Move Stock");

                }
                else {
                    int amountM = Convert.ToInt32(movebox.Text.ToString());
                    int Quantity = Convert.ToInt32(storagebox.Text.ToString());
                    int stockNo = Convert.ToInt32(((ListBoxItem)Detailbox.SelectedItem).Tag.ToString());
                    if (amountM <= Quantity)
                    {
                        foreach (MoveStock moveStock in Model.MoveStockList)
                        {

                            if (moveStock.ID.Equals(stockNo))
                            {

                                moveStock.InBar += amountM;
                                moveStock.InStorage -= amountM;
                                //    MessageBox.Show(amountM.ToString() + moveStock.InBar + moveStock.InStorage);
                                SelectedStock = moveStock;
                            }
                        }




                        Model.UpdateMStock(SelectedStock);

                        //MessageBox.Show("Sugsesh");
                        foreach (MoveStock moveStock in Model.MoveStockList)
                        {
                            if (moveStock.ID.Equals(stockNo))
                            {
                                barbox.Text = moveStock.InBar.ToString();
                                storagebox.Text = moveStock.InStorage.ToString();
                                movebox.Clear();
                                Refresh();
                            }

                        }
                    }
                    else
                    {

                        MessageBox.Show("Amount being moved cant be larger then amount in storage");
                    }
                }
            }
            else
            {
                MessageBox.Show("Must Pick a Stock");
            }
        }
    }
}
