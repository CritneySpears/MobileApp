using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WorkHorse.Models;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;

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

            StartShiftButton.IsEnabled = false;
            EndShiftButton.IsEnabled = true;
        }

        async void OnShiftEndButtonClicked(object sender, EventArgs e)
        {
            var shift = await App.Database.GetLastShiftAsync();
            shift.EndTime = DateTime.Now;

            await App.Database.SaveShiftAsync(shift);
            await Navigation.PopAsync();

            StartShiftButton.IsEnabled = true;
            EndShiftButton.IsEnabled = false;
        }
    }
}
