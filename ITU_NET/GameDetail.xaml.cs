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

namespace ITU_NET {
    public partial class GameDetail : Page {
        public GameDetail() {
            InitializeComponent();

            DataContextChanged += GameDetail_DataContextChanged;
        }

        public MainViewModel MVM => (MainViewModel)DataContext;

        private void GameDetail_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            MVM.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "GameDetail") {
                UpdateData();
            }
        }
        private void UpdateData() {

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e) {
            if (NavigationService != null && NavigationService.CanGoBack) {
                NavigationService.GoBack();
            }
        }

        private void LogInPlease_Click(object sender, RoutedEventArgs e) {
            if (NavigationService != null) {
                NavigationService.Navigate(new Uri("User.xaml", UriKind.Relative));
            }
        }

        private void MarkFinished_Click(object sender, RoutedEventArgs e) {
            MVM.MarkGameCommand.Execute(new MarkInfo(GameName.Text, MarkList.Finished));
        }
        private void MarkFavourite_Click(object sender, RoutedEventArgs e) {
            MVM.MarkGameCommand.Execute(new MarkInfo(GameName.Text, MarkList.Favourite));
        }

        private void UnmarkFinished_Click(object sender, RoutedEventArgs e) {
            MVM.MarkGameCommand.Execute(new MarkInfo(GameName.Text, MarkList.Finished, true));
        }
        private void UnmarkFavourite_Click(object sender, RoutedEventArgs e) {
            MVM.MarkGameCommand.Execute(new MarkInfo(GameName.Text, MarkList.Favourite, true));
        }
    }
}
