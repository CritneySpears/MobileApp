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
    public partial class ShiftViewPage : ContentPage
    {
        public ShiftViewPage()
        {
            InitializeComponent();
        }

        protected double hoursDone;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetShiftsAsync();
            hoursDone = 0;
            CalculateTotalHoursDone();
            listView.Header = hoursDone.ToString();
        }

        async void OnClearClicked(Object sender, EventArgs e)
        {
            await App.Database.WipeDatabase();
            var vUpdatedPage = new ShiftViewPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();
        }

        async void CalculateTotalHoursDone()
        {
            List<DateTime> startTimes = await App.Database.GetShiftStartTimes();
            List<DateTime> endTimes = await App.Database.GetShiftEndTimes();


            for (int i = 0; i < endTimes.Count(); i++)
            {
                TimeSpan span = endTimes[i].Subtract(startTimes[i]);
                hoursDone += span.TotalHours;
            }
        }

        //async void OnExportClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ExportPage
        //    {
        //    
        //    });
        //}


        // WILL USE THIS TO EDIT INFORMATION WHEN SELECTED.
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {/*
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ShiftEditPage
                {
                    BindingContext = e.SelectedItem as ClockInstance
                });
            }*/
        }

        // ItemSelected="OnListViewItemSelected"> This is required in the layout file within the list to enabled functionality
    }
}