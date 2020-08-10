using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHorse.Models;

namespace WorkHorse
{
    public partial class ShiftEntryPage : ContentPage
    {
        public ShiftEntryPage()
        {
            InitializeComponent();
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