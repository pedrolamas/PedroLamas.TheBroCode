using System;
using System.Windows.Controls;
using Cimbalino.Phone.Toolkit.Extensions;
using Microsoft.Phone.Controls;
using PedroLamas.TheBroCode.ViewModel;

namespace PedroLamas.TheBroCode.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            this.ResetLanguageWithCurrentCulture();

            InitializeComponent();

            ShareTypeListPicker.ItemsSource = new[] { string.Empty, "Email Message", "SMS Message", "Social Networks" };
        }

        private void IndexApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            QuotesListPicker.Open();
        }

        private void ShareApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            ShareTypeListPicker.Open();
        }

        private void ShareTypeListPicker_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;

            switch (ShareTypeListPicker.SelectedIndex)
            {
                case 1:
                    vm.ShareByEmailCommand.Execute(null);
                    break;
                case 2:
                    vm.ShareBySmsCommand.Execute(null);
                    break;
                case 3:
                    vm.ShareOnSocialNetworkCommand.Execute(null);
                    break;
            }

            ShareTypeListPicker.SelectedIndex = 0;
        }
    }
}