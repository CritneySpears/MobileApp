using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WorkHorse.Models;

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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetShiftsAsync();
        }

        async void OnShiftAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShiftEntryPage
            {
                BindingContext = new ClockInstance()
            });
        }

        async void OnClearClicked(Object sender, EventArgs e)
        {
            await App.Database.WipeDatabase();
            var vUpdatedPage = new MainPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();
        }

        //async void OnExportClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ExportPage
        //    {
        //    
        //    });
        //}


        // WILL USE THIS TO EDIT INFORMATION WHEN SELECTED.
        //async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    if (e.SelectedItem != null)
        //    {
        //        await Navigation.PushAsync(new ShiftEntryPage
        //        {
        //            BindingContext = e.SelectedItem as ClockInstance
        //        });
        //    }
        //}

        // ItemSelected="OnListViewItemSelected"> This is required in the layout file within the list to enabled functionality
    }
}
