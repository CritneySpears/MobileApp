﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkHorse.Data;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace WorkHorse
{
    public partial class App : Application
    {
        static ShiftDatabase database;

        public static ShiftDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ShiftDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Shifts.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
