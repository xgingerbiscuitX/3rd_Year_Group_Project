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
    /// Interaction logic for UC_ChangeRoomPricing.xaml
    /// </summary>
    public partial class UC_ChangeRoomPricing : UserControl
    {
        private IAccessHandler Model;
        private IRoomPrice roomPrice = new RoomPrice();
        private Test parent;
        public UC_ChangeRoomPricing(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            LoadRoomTypes();
            Model.refreshRoomTypeList();
            Model.refreshRoomPricingList();
        }

        private void Refresh()
        {
            Model.refreshRoomPricingList();
            Model.refreshRoomTypeList();
        }

        private void LoadRoomTypes()
        {
            foreach (string rt in Model.RoomTypeList)
            {
                CB_roomType.Items.Add(rt);
            }
        }
        private void price_GotFocus(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("Black");

            if (RoomPriceInput.Text == "Insert new price")
                RoomPriceInput.Clear();
            RoomPriceInput.Foreground = brush;
        }
        private void CB_roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            foreach (RoomPrice r in Model.RoomPricingList)
            {
                if (r.RoomName.ToString() == CB_roomType.SelectedItem.ToString())
                {
                    RoomCurrentPrice.Text = r.Pricing.ToString();
                }
                else
                {

                }
            }

        }

        private void Change_room_pricing_Click(object sender, RoutedEventArgs e)
        {

            double newPrice;
            if (double.TryParse(RoomPriceInput.Text, out newPrice))
            {
                roomPrice.RoomName = CB_roomType.SelectedItem.ToString();
                roomPrice.Pricing = Convert.ToDouble(RoomPriceInput.Text);

                Model.changeRoomPricing(roomPrice);

                MessageBox.Show("The price of " + CB_roomType.SelectedItem.ToString() + " has been changed to " + RoomPriceInput.Text);
                Refresh();
            }
            else
            {
                MessageBox.Show("Please enter numbers only in the price change");
            }

            

           
        }
    }
}
