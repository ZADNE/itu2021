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

namespace ITU_NET {
    public partial class You : Page {

        public You() {
            InitializeComponent();

            DataContextChanged += You_DataContextChanged;
        }

        public MainViewModel MVM  => (MainViewModel)DataContext;

        private void You_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            ((MainViewModel)DataContext).PropertyChanged += Vm_PropertyChanged;
            UpdateData();
        }

        private void DescTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (MVM != null) {
                if (DescTextBox.Text != MVM.User.Desc) {
                    AcceptDescChange.Visibility = Visibility.Visible;
                }
            }
        }

        private void AcceptDescChange_Click(object sender, RoutedEventArgs e) {
            MVM.UpdateDescCommand.Execute(DescTextBox.Text);
            AcceptDescChange.Visibility = Visibility.Hidden;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "User") {
                UpdateData();
            }
        }
        private void UpdateData() {
            DescTextBox.Text = ((MainViewModel)DataContext).User.Desc;
        }

        private void List_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            MVM.DownloadGameDetailCommand.Execute(b.Content);
            NavigationService.Navigate(new Uri("GameDetail.xaml", UriKind.Relative));
        }
    }
}
