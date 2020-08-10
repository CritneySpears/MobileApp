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

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var shift = (ShiftInstance)BindingContext;
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