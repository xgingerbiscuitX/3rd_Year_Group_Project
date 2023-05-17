using System.Windows;
using System.Windows.Media;
using BusinessLayer;
using DataAccessLayer;
using Software_Engineering_Project;
using SoftwareEngineeringT1;

namespace SoftwareEngineeringTeam1
{

    public partial class MainWindow : Window
    {
        IDataLayer _Datalayer;
        IAccessHandler _Model;
       

        public MainWindow()
        {
           _Datalayer = IDatalayer.GetInstance();
           _Model = AccessHandler.GetInstance(_Datalayer);
            InitializeComponent();
            SetDBType();
        }

        private void SetDBType()
        {
            Connect_lbl.Content = _Datalayer.GetDBType();
        }

        private void LOGIN_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password.ToString();


            if(_Model.Login(username, password))
            {
                
                Test test = new Test(this, _Model);
                Application.Current.Windows[0].Close();
                test.ShowDialog();
            }

            else
            {
                Error_lbl.Foreground = Brushes.Red;
                Error_lbl.Content = "Incorrect login";
                Error_lbl.Visibility = Visibility.Visible;
            }
        }



        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Username.Text == "Username")
            Username.Clear();
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password.ToString() == "********")
                Password.Clear();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }







    }
}
