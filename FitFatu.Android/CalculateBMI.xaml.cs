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
    public partial class CalculateBMI : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        public CalculateBMI()
        {
            InitializeComponent();

        }

        private async void GoBack_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ZalogowanyGosc());
        }

        

        public void Weight_Completed(object sender, EventArgs e)
        {
            Weight_BMI.Text = ((Entry)sender).Text;
            float Weight_Calculate = (float)Convert.ToDouble(Weight_BMI.Text);
        }

        public void Height_Completed(object sender, EventArgs e)
        {
            Height_BMI.Text = ((Entry)sender).Text;
            float Height_Calculate = (float)Convert.ToDouble(Height_BMI.Text);
        }

        public void Calculate_BMI_Clicked(object sender, EventArgs e)
        {
            float Height_Calculate = (float)Convert.ToDouble(Height_BMI.Text);
            float Weight_Calculate = (float)Convert.ToDouble(Weight_BMI.Text);

            float Result = Weight_Calculate / (Height_Calculate * Height_Calculate);
            Your_BMI.Text = "Twoje BMI wynosi :" + Result;

            if(Result < 16)
            {
                Description_BMI.Text = "Diagnoza : Wygłodzenie ";

            }
            else if(Result >= 16 && Result < 18.49 )
            {
                Description_BMI.Text = "Diagnoza : Wychudzenie ";
            }
            else if (Result >= 17 && Result < 18.49)
            {
                Description_BMI.Text = "Diagnoza : Niedowaga ";
            }
            else if (Result >= 18.5 && Result < 24.99)
            {
                Description_BMI.Text = "Diagnoza : Wartość prawidłowa ";
            }
            else if (Result >= 25 && Result < 29.99)
            {
                Description_BMI.Text = "Diagnoza : Nadwaga ";
            }
            else if (Result >= 30 && Result < 34.99)
            {
                Description_BMI.Text = "Diagnoza : I stopień otyłości ";
            }
            else if (Result >= 35 && Result < 39.99)
            {
                Description_BMI.Text = "Diagnoza : II stopień otyłości ";
            }
            else
            {
                Description_BMI.Text = "Diagnoza : Otyłość skrajna ";
            }
        }
    }
}