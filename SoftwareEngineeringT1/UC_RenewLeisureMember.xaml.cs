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
using BusinessEntities;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for UC_RenewLeisureMember.xaml
    /// </summary>
    public partial class UC_RenewLeisureMember : UserControl
    {
        private IAccessHandler Model;
        private Test parent;
        public UC_RenewLeisureMember(Test parent, IAccessHandler Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.parent = parent;
            listViewMemberDetails.Items.Add(new TwoColumnData("ID:",Model.CurrentMember.ID.ToString()));
            listViewMemberDetails.Items.Add(new TwoColumnData("First Name:", Model.CurrentMember.FirstName));
            listViewMemberDetails.Items.Add(new TwoColumnData("Last Name:", Model.CurrentMember.LastName));
            listViewMemberDetails.Items.Add(new TwoColumnData("Member Type:",Model.CurrentMember.AreaType));
            listViewMemberDetails.Items.Add(new TwoColumnData("Age:", Model.CurrentMember.Age.ToString()));
            listViewMemberDetails.Items.Add(new TwoColumnData("Email:", Model.CurrentMember.Email));
            listViewMemberDetails.Items.Add(new TwoColumnData("Phone:", Model.CurrentMember.Phone));
            listViewMemberDetails.Items.Add(new TwoColumnData("Medical Condition:", Model.CurrentMember.MedicalConditions));
            cb_MemberType.SelectedItem = Model.CurrentMember.AreaType;

            monthly.Tag = 1;
            six_months.Tag = 6;
            yearly.Tag = 12;
        }

        private void Renew_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Are you sure you want to renew this member?","",MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int months = (int)((ComboBoxItem)cb_SubscriptionType.SelectedItem).Tag;
                    bool success = Model.renewMember(Model.CurrentMember.ID, cb_MemberType.Text, months);
                    if (success)
                    {
                        MessageBox.Show("Successfully renewed member");
                        UC_ManageMember m = new UC_ManageMember(parent, Model);
                        ContextSwitcher._context.SwitchScreen(m);
                        return;
                    }
                    MessageBox.Show("An error occured while trying to renew member","ERROR");
                    break;
                default:
                    break;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            UC_ManageMember m = new UC_ManageMember(parent,Model);
            ContextSwitcher._context.SwitchScreen(m);
        }
    }
}
