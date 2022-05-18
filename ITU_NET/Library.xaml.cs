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

    public partial class Library : Page {
        public Library() {
			InitializeComponent();

			DataContextChanged += Library_DataContextChanged;
			LVGames.ItemsSource = null;
		}

		public MainViewModel MVM => (MainViewModel)DataContext;

		private void Library_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			MVM.PropertyChanged += Vm_PropertyChanged;
			UpdateList();
		}
		private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "DownloadingGameExtracts") {
				if (MVM.DownloadingGameExtracts == false) {
					LVGames.ItemsSource = MVM.GameExtracts;
					LVLoading.Visibility = Visibility.Hidden;
				}
			}
		}

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
			UpdateList();
		}

        private void LVGames_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (LVGames.SelectedItem != null) {
				if (NavigationService != null) {
					MVM.DownloadGameDetailCommand.Execute(((GameExtractModel)LVGames.SelectedItem).Name);
					NavigationService.Navigate(new Uri("GameDetail.xaml", UriKind.Relative));
				}
			}
		}

		private void UpdateList() {
			MVM.DownloadGameExtractsCommand.Execute(TxtFilter.Text);
			LVLoading.Visibility = Visibility.Visible;
		}
	}
}
