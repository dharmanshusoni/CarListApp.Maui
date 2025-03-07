﻿using CarListApp.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarListApp.Maui.ViewModels
{
    //[QueryProperty(nameof(Car),"Car")]
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Car car;

        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string,object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
            Car = App.CarService.GetCar(Id); 
        }

        //public CarDetailsViewModel()
        //{
        //    Title = $"Car Details - {car.Make} {car.Model}";
        //}
    }
}
