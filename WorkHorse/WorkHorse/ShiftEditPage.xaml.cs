using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHorse.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkHorse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShiftEditPage : ContentPage
    {
        public ShiftEditPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var shift = (ShiftInstance)BindingContext;
            SelectedStartTime.Time = shift.StartTime.TimeOfDay;
            SelectedEndTime.Time = shift.EndTime.TimeOfDay;
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var shift = (ShiftInstance)BindingContext;
            shift.StartTime = SelectedStartDate.Date + SelectedStartTime.Time;
            shift.EndTime = SelectedEndDate.Date + SelectedEndTime.Time;
            await App.Database.SaveShiftAsync(shift);
            await Navigation.PopAsync();
        }
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var shift = (ShiftInstance)BindingContext;
            await App.Database.DeleteShiftAsync(shift);
            await Navigation.PopAsync();
        }
    }
}