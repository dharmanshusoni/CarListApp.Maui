using CarListApp.Maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarService
    {
        private SQLiteConnection conn;
        string _dbpath;
        public string StatusMessage;
        int result = 0;
        public CarService(string dbpath)
        {
            _dbpath = dbpath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbpath);
            conn.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                Init();
                return conn.Table<Car>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrive data.";
            }

            return new List<Car>();

            //return new List<Car>()
            //{
            //    new Car
            //    {
            //        Id = 1,Make="Honda",Model="Fit",Vin="123"
            //    },
            //    new Car
            //    {
            //        Id = 2,Make="Toyota",Model="Prado",Vin="123"
            //    },
            //    new Car
            //    {
            //        Id = 3,Make="Honda",Model="Civic",Vin="123"
            //    },
            //    new Car
            //    {
            //        Id = 4,Make="Audi",Model="A5",Vin="123"
            //    },
            //    new Car
            //    {
            //        Id = 5,Make="BMW",Model="M3",Vin="123"
            //    },
            //    new Car
            //    {
            //        Id = 6,Make="Nissan",Model="Note",Vin="123"
            //    },
            //    new Car
            //    {
            //        Id = 1,Make="Ferrari",Model="Spider",Vin="123"
            //    },
            //};
        }

        public Car GetCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrive data.";
            }

            return null;
        }

        public void AddCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Invalid VarRecord");

                result = conn.Insert(car);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Insert data.";
            }
        }

        public int DeleteCar(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().Delete(q => q.Id == id);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data.";
            }
            return 0;
        }

        public void UpdateCar(Car car)
        {
            try
            {
                Init();

                if (car == null)
                    throw new Exception("Invalid Car Record");

                result = conn.Update(car);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to Update data.";
            }
        }
    }
}
