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
using System.Windows.Media.Animation;
using CustomControls;


namespace ITU_NET {
    public partial class MainWindow : CustomWindow {
        public MainWindow() {
            InitializeComponent();

            MainViewModel vm = new MainViewModel();
            DataContext = vm;


            vm.DownloadUserCommand.Execute(new Credentials());
            //vm.DownloadUserCommand.Execute(new Credentials("Te", "t"));

            NavbarLeft.SelectedItem = NavbarHome;
        }

        private void NavBar_Click(object sender, RoutedEventArgs e) {
            ListBoxItem lbi = sender as ListBoxItem;
            string tag = lbi.Tag.ToString();
            if (MainFrame != null) {
                MainFrame.Navigate(new Uri(tag + ".xaml", UriKind.Relative));
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e) {
            var page = (Page)MainFrame.Content;
            page.DataContext = DataContext;
            SetNavbar(e.Uri.ToString());
        }

        private void SetNavbar(string page) {
            switch (page) {
                case "Home.xaml":
                    NavbarLeft.SelectedItem = NavbarHome;
                    NavbarRight.SelectedItem = null;
                    break;
                case "Library.xaml":
                    NavbarLeft.SelectedItem = NavbarLibrary;
                    NavbarRight.SelectedItem = null;
                    break;
                case "You.xaml":
                    NavbarLeft.SelectedItem = NavbarYou;
                    NavbarRight.SelectedItem = null;
                    break;
                case "User.xaml":
                    NavbarLeft.SelectedItem = null;
                    NavbarRight.SelectedItem = NavbarUser;
                    break;
                case "GameDetail.xaml":
                    NavbarLeft.SelectedItem = null;
                    NavbarRight.SelectedItem = null;
                    break;
            }
        }
    }
}
