using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;

namespace ODataCrudWP7
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public DetailsPage()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                this.DataContext = App.AppViewModel;
            };
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            App.AppViewModel.SaveCurrentItem();
            GoBack();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            App.AppViewModel.DeleteCurrentItem();
            GoBack();
        }

        private void GoBack()
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}