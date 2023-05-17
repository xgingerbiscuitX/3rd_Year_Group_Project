using BusinessLayer;
using BusinessEntities;
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
using System.Collections;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_LeisureCleanView.xaml
    /// </summary>
    public partial class UC_LeisureCleanView : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        public UC_LeisureCleanView(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            Refresh();
        }
        public void Refresh()
        {
            Model.refreshLeisureCleanList();
            CleaningList.Items.Clear();
            CleaningList.Items.Add(new ListBoxItem { Content = "Employee" + "\t" + " Area " + "\t\t\t" + " Comment " });
        }
        private void CleaningCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
            ArrayList getList = Model.Leisure_CleanList;
            if (getList != null)
            {
                foreach (LeisureCleaning clean in getList)
                {
                    if (clean.dateDone == CleaningCalendar.SelectedDate)
                    {
                        if (clean.AreaCleaned.Length >= 9)
                        {
                            CleaningList.Items.Add(new ListBoxItem { Content = string.Concat(clean.Employee_Name, "\t\t", clean.AreaCleaned, "\t\t", clean.Comment, "  "), Tag = clean.Reference_No });
                        }
                        else {
                            CleaningList.Items.Add(new ListBoxItem { Content = string.Concat(clean.Employee_Name, "\t\t", clean.AreaCleaned, "\t\t\t", clean.Comment, "  "), Tag = clean.Reference_No });

                        }
                    }

                }
            }

        }
    }
}
