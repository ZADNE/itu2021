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

namespace ITU_NET{
    public partial class Home : Page {
        public Home() {
            InitializeComponent();

            DataContextChanged += Library_DataContextChanged;
        }

        public MainViewModel MVM => (MainViewModel)DataContext;

        private void Library_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            MVM.PropertyChanged += Vm_PropertyChanged;
            MVM.DownloadTrendingGamesCommand.Execute(4);
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            
        }

        private void Trend_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            if (NavigationService != null) {
                MVM.DownloadGameDetailCommand.Execute((string)b.Tag);
                NavigationService.Navigate(new Uri("GameDetail.xaml", UriKind.Relative));
            }
        }
    }
}
