using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CarListApp.Maui.Views;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";

        //private readonly CarService carService;
        public ObservableCollection<Car> Cars { get; private set; } = new();

        //CarService carService // Removed injection here, as we have added in App.xaml.cs
        public CarListViewModel()
        {
            Title = "Car List";
            AddEditButtonText = createButtonText;
            GetCarList().Wait();
            //this.carService = carService;
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int carId;

        [RelayCommand]
        async Task GetCarList()
        {
            if(IsLoading) return;
            try
            {
                IsLoading = true;
                if(Cars.Any()) Cars.Clear();

                //var cars = carService.GetCars();
                var cars = App.CarService.GetCars();
                foreach (var car in cars) Cars.Add(car);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list if cars", "Ok");
            }
            finally
            {
                IsLoading = false;
                isRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}",true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
            }
            var car = new Car { 
                Make = Make,
                Model = Model,
                Vin = Vin
            };
            if (CarId != 0 )
            {
                car.Id = CarId;
                App.CarService.UpdateCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            }
            else
            {
                App.CarService.AddCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "Ok");
            }
            
            await GetCarList();
            await ClearForm();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            var result = App.CarService.DeleteCar(id);
            if (result == 0) await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
            else { 
                await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
                await GetCarList();
            }
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;

            var car = App.CarService.GetCar(id);
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }

        //[RelayCommand]
        //async Task GetCarDetails(Car car)
        //{
        //    if (car == null) return;

        //    await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
        //    {
        //        { nameof(Car), car }
        //    });
        //}
    }
}
