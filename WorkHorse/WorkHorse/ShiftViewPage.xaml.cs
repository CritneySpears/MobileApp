﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHorse.Models;
using System.Globalization;

namespace WorkHorse
{
    public partial class ShiftViewPage : ContentPage
    {
        public ShiftViewPage()
        {
            InitializeComponent();
        }

        protected string hoursDone;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetShiftsAsync();
            CalculateTotalHoursDone();
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
            List<ShiftInstance> shifts = await App.Database.GetShiftsAsync();
            double totalHours = 0;
            foreach(ShiftInstance shift in shifts)
            {
                if (shift.EndTime >= shift.StartTime)
                {
                    double dateDiff = (shift.EndTime - shift.StartTime).TotalHours;
                    totalHours += dateDiff;
                }
            }
            hoursDone = totalHours.ToString("F");
            listView.Header = "Hours Done: " + hoursDone;
        }

        //async void OnExportClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ExportPage
        //    {
        //    
        //    });
        //}


        // WILL USE THIS TO EDIT INFORMATION WHEN SELECTED.
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ShiftEditPage
                {
                    BindingContext = e.SelectedItem as ShiftInstance
                });
            }
        }

        // ItemSelected="OnListViewItemSelected"> This is required in the layout file within the list to enabled functionality
    }
}