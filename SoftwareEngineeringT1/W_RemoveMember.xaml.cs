using BusinessEntities;
using BusinessLayer;
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
using System.Windows.Shapes;

namespace SoftwareEngineeringT1
{
    /// <summary>
    /// Interaction logic for W_RemoveMember.xaml
    /// </summary>
    public partial class W_RemoveMember : Window
    {

        private IAccessHandler Model;
        private ILeisureMember LMember = new LeisureMember();
        private UC_ManageMember parent;
        public W_RemoveMember(ILeisureMember member,UC_ManageMember parent, IAccessHandler model)
        {
            InitializeComponent();
            LMember = member;
            this.Model = model;
            this.parent = parent;
            Populate_Details();
        }
        public void Populate_Details(){

            MembNo.Text = Model.CurrentMember.ID.ToString();
            fname.Text = Model.CurrentMember.FirstName.ToString();
            lname.Text = Model.CurrentMember.LastName.ToString();
            Age.Text = Model.CurrentMember.Age.ToString();
            email.Text = Model.CurrentMember.Email.ToString();
            Phone.Text = Model.CurrentMember.Phone.ToString();
            MedCon.Text = Model.CurrentMember.MedicalConditions.ToString();
            MemType.Text = Model.CurrentMember.AreaType.ToString();
            ExpireDate.Text = Model.CurrentMember.ExpireDate.ToString();
        }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
