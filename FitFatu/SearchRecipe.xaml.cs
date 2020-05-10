using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitFatu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchRecipt : ContentPage
    {
        public SearchRecipt()
        {
            InitializeComponent();
        }

        private async void PowrotMenuGosc_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ZalogowanyGosc());
        }
    }
}