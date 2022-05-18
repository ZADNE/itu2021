using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITU_NET
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Page {

        public User() {
            InitializeComponent();

            LoginRegister.SelectedItem = Login;

            DataContextChanged += User_DataContextChanged;
        }
        public MainViewModel MVM => (MainViewModel)DataContext;

        private void User_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            MVM.PropertyChanged += Vm_PropertyChanged;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e) {
            if (LoginRegister.SelectedItem == Login) {//Log in
                AcceptButton.Content = "Logging In...";
                MVM.DownloadUserCommand.Execute(new Credentials(NicknameTextBox.Text, PasswordTextBox.Password));
            } else {//Register
                AcceptButton.Content = "Registering...";
                MVM.AddNewUserCommand.Execute(new Credentials(NicknameTextBox.Text, PasswordTextBox.Password));
            }
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DownloadingUser" && !MVM.DownloadingUser) {
                if (!MVM.IsUserInfoInvalid) {
                    if (NavigationService != null) {
                        if (MVM.User.Nickname != "") {//Logged in
                            NavigationService.GoBack();
                        } else {//Logged off
                            NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
                        }
                    }
                } else {
                    AcceptButton.Content = "Log In";
                    NicknameTextBox.BorderBrush = Brushes.Red;
                    PasswordTextBox.BorderBrush = Brushes.Red;
                }
            } else if (e.PropertyName == "UploadingUser" && !MVM.UploadingUser) {
                if (!MVM.IsUserInfoInvalid) {
                    AcceptButton.Content = "Logging In...";
                } else {
                    AcceptButton.Content = "Register & Log In";
                    NicknameTextBox.BorderBrush = Brushes.Red;
                    PasswordTextBox.BorderBrush = Brushes.Red;
                }
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e) {
            LogOutButton.Content = "Logging Out...";
            MVM.DownloadUserCommand.Execute(new Credentials());
        }

        private void LogReg_Selected(object sender, RoutedEventArgs e) {
            ListBoxItem lbi = sender as ListBoxItem;
            if (lbi == Login) {//Log in
                AcceptButton.Content = "Log In";
                NicknameTextBox.BorderBrush = Brushes.Black;
                PasswordTextBox.BorderBrush = Brushes.Black;
            } else {//Register
                AcceptButton.Content = "Register & Log In";
                NicknameTextBox.BorderBrush = Brushes.Black;
                PasswordTextBox.BorderBrush = Brushes.Black;
            }
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e){
            PasswordBox pwb = sender as PasswordBox;
            if (pwb.Password.Length == 0) {
                PasswordHint.Visibility = Visibility.Visible;
            } else {
                PasswordHint.Visibility = Visibility.Hidden;
            }
        }
    }
}
