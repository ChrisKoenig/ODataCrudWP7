using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using ODataCrudWP7.PeopleService;

namespace ODataCrudWP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                this.DataContext = App.AppViewModel;
            };
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            App.AppViewModel.LoadData();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            App.AppViewModel.ClearData();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            App.AppViewModel.SelectedPerson = new Person();
            ShowDetailsPage();
        }

        private void PeopleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = sender as ListBox;
            var si = lb.SelectedItem;
            if (si == null)
                return;
            else
                ShowDetailsPage();
        }

        private void ShowDetailsPage()
        {
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));
        }
    }
}