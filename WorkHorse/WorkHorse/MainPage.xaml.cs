using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WorkHorse.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Essentials;

namespace WorkHorse
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ShiftInstance();
        }
        async void OnShiftViewClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShiftViewPage());
        }

        async void OnShiftStartButtonClicked(object sender, EventArgs e)
        {
            var shift = (ShiftInstance)BindingContext;
            shift.Date = DateTime.Today;
            shift.StartTime = DateTime.Now;

            await App.Database.SaveShiftAsync(shift);
            await Navigation.PopAsync();

            ClockedIn();
        }

        async void OnShiftEndButtonClicked(object sender, EventArgs e)
        {
            EndShiftButton.IsEnabled = false;
            var shift = await App.Database.GetLastShiftAsync();
            shift.EndTime = DateTime.Now;

            await App.Database.SaveShiftAsync(shift);
            await Navigation.PopAsync();

            var vUpdatedPage = new MainPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();
        }

        public void ClockedOut()
        {
            EndShiftButton.IsEnabled = false;
            EndShiftButton.BackgroundColor = Color.FromHex("#83d1fb");
            StartShiftButton.IsEnabled = true;
            StartShiftButton.BackgroundColor = Color.FromHex("#0289d1");
        }

        public void ClockedIn()
        {
            StartShiftButton.IsEnabled = false;
            StartShiftButton.BackgroundColor = Color.FromHex("#83d1fb");
            EndShiftButton.IsEnabled = true;
            EndShiftButton.BackgroundColor = Color.FromHex("#0289d1");
        }
    }
}
