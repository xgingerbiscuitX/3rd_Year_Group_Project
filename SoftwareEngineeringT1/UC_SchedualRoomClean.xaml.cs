using BusinessEntities;
using BusinessLayer;
using SoftwareEngineeringTeam1;
using System;
using System.Collections.Generic;
using System.Collections;
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
    /// Interaction logic for UC_SchedualRoomClean.xaml
    /// </summary>
    public partial class UC_SchedualRoomClean : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        private ICleaningRooms RoomsClean = new CleaningRoom();
        private IRoom Room = new Room();
        public UC_SchedualRoomClean(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Refresh();
            LoadDateTypes();
        }
        private void LoadDateTypes() {
            string dats = DateTime.Now.ToString("d");
            Datebox.Text = dats;
        
        
        }

        private void Refresh()
        {
            Model.refreshToBeCleanedList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

           int roomNo= Convert.ToInt32( roomnumber.Text.ToString());
           DateTime set = Convert.ToDateTime(Datebox.Text.ToString());
         
           Model.insertClean(roomNo,set);
            Refresh();
            roomnumber.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UC_ViewCleaningSchedule rn = new UC_ViewCleaningSchedule(parent, Model);
            ContextSwitcher._context.SwitchScreen(rn);
        }
    }
}
