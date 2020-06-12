
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FitFatu.LoggedUserMenu;

namespace FitFatu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedPage : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public LoggedPage()
        {
            InitializeComponent();

            MenuTitle.Text = "Witaj, wybierz opcję :";
        }

        private async void AddRecipt_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddReciptPage());
        }

        private void SearchProduct_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchProductUser());
        }

        private void SearchRecipe_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchRecipeUser());
        }

        private void YourProducts_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new YourProducts());
        }

        private void YourRecipt_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new YourRecipts());
        }

        private void CalculateBMI_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CalculateBMIUser());
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("O autorze : ", "Dymnicki Hubert, 2020 © ", "OK");
        }

        

        private void LogoutUser_Clicked(object sender, EventArgs e)
        {
            LoggedUserMenu.identity = 0;
            DisplayAlert("Informacja", "Zostałeś wylogowany", "OK");
            Navigation.PushModalAsync(new MainPage());
        }

        

        private void RecipeLotteryUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RecipeLotteryUser());
        }
    }
}