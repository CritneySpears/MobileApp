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
            var shift = (ClockInstance)BindingContext;
            shift.Date = DateTime.Today;
            shift.Time = DateTime.Now;
            shift.ClockString = "Shift Started";
            await App.Database.SaveShiftAsync(shift);
            await Navigation.PopAsync();
        }

        async void OnShiftEndButtonClicked(object sender, EventArgs e)
        {
            var shift = (ClockInstance)BindingContext;
            shift.Date = DateTime.Today;
            shift.Time = DateTime.Now;
            shift.ClockString = "Shift Ended";
            await App.Database.SaveShiftAsync(shift);
            await Navigation.PopAsync();
        }
    }
}