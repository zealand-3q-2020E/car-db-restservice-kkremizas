using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCar.Model;

namespace WebApiCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        static string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        List<Car> carList = new List<Car>();

        //public static List<Car> carList = new List<Car>()
        //{
        //    new Car(){Id = 1,Model="x3",Vendor="Tesla", Price=400000},
        //    new Car(){Id = 2,Model="x2",Vendor="Tesla", Price=600000},
        //    new Car(){Id = 3,Model="x1",Vendor="Tesla", Price=800000},
        //    new Car(){Id = 4,Model="x0",Vendor="Tesla", Price=1400000},
        //};

        /// <summary>
        /// Method for get all the cars from the static list
        /// </summary>
        /// <returns>List of cars</returns>
        // GET: api/Cars
        [HttpGet(Name = "GetAll")]
        public IEnumerable<Car> GetAll()
        {
            string selectall = "select id, vendor, model, price from Car";
            
            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectall, databaseConnection))
                {
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string vendor = reader.GetString(1);
                            string model = reader.GetString(2);
                            int price = reader.GetInt32(3);

                            carList.Add(new Car(id,vendor,model,price));

                        }
                    }
                }
            }
            return carList;
        }
        

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "GetbyId")]
        public Car Get(int id)
        {
            string selectById = "select vendor, model, price from Car where id = @id";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectById, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string vendor = reader.GetString(0);
                            string model = reader.GetString(1);
                            int price = reader.GetInt32(2);

                            return new Car(id, vendor, model, price);

                        }
                    }
                }
            }
            return null;
        }

        // GET: api/Cars/Vendor/Tesla
        [HttpGet("byVendor/{vendorGet}", Name = "GetByVendor")]
        public IEnumerable<Car> GetByVendor(string vendorGet)
        {
            var carList = new List<Car>();
            string selectVendor = "select id, vendor, model, price from Car where vendor = @vendor";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectVendor, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@vendor", vendorGet);
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string vendor = reader.GetString(1);
                            string model = reader.GetString(2);
                            int price = reader.GetInt32(3);

                            carList.Add(new Car(id, vendor, model, price));

                        }
                    }
                }
            }
            return carList;
        }

        // GET: api/Cars/Model/X1
        [HttpGet("byModel/{modelGet}", Name = "GetByModel")]
        public IEnumerable<Car> GetByModel(string modelGet)
        {
            var carList = new List<Car>();
            string selectModel = "select id, vendor, model, price from Car where model = @model";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectModel, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@model", modelGet);
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string vendor = reader.GetString(1);
                            string model = reader.GetString(2);
                            int price = reader.GetInt32(3);

                            carList.Add(new Car(id, vendor, model, price));

                        }
                    }
                }
            }
            return carList;
        }

        // GET: api/Cars/Price/5000
        [HttpGet("byPrice/{priceGet}", Name = "GetByPrice")]
        public IEnumerable<Car> GetByPrice(string priceGet)
        {
            var carList = new List<Car>();
            string selectPrice = "select id, vendor, model, price from Car where price = @price";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectPrice, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@price", priceGet);
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string vendor = reader.GetString(1);
                            string model = reader.GetString(2);
                            int price = reader.GetInt32(3);

                            carList.Add(new Car(id, vendor, model, price));

                        }
                    }
                }
            }
            return carList;
        }

        // GET: api/Cars/Price/Ascending/true
        [HttpGet("byPrice/{priceGet}/Ascending/{ascending}", Name = "GetByPriceAscending")]
        public IEnumerable<Car> GetByPriceAscending(string priceGet, bool ascending)
        {
            var carList = new List<Car>();
            string selectPrice = "select id, vendor, model, price from Car where price = @price order by vendor ";
            if (ascending)
                selectPrice += "ASC";
            else
                selectPrice += "DESC";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectPrice, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@price", priceGet);
                    selectCommand.Parameters.AddWithValue("@asc", ascending);
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string vendor = reader.GetString(1);
                            string model = reader.GetString(2);
                            int price = reader.GetInt32(3);

                            carList.Add(new Car(id, vendor, model, price));

                        }
                    }
                }
            }
            return carList;
        }

        
        // GET: api/Cars/byVendor/Tesla/10000
        [HttpGet("byVendor/{vendorGet}/{priceGet}", Name = "GetByVendorAndPrice")]
        public IEnumerable<Car> GetByVendorAndPrice(string vendorGet, string priceGet)
        {
            var carList = new List<Car>();
            string selectVendorAndPrice = "select id, vendor, model, price from Car where vendor = @vendor and price = @price";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectVendorAndPrice, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@vendor", vendorGet);
                    selectCommand.Parameters.AddWithValue("@price", priceGet);
                    databaseConnection.Open();

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string vendor = reader.GetString(1);
                            string model = reader.GetString(2);
                            int price = reader.GetInt32(3);

                            carList.Add(new Car(id, vendor, model, price));

                        }
                    }
                }
            }
            return carList;
        }


        /// <summary>
        /// Post a new car to the static list
        /// </summary>
        /// <param name="value"></param>
        // POST: api/Cars
        [HttpPost]
        public void Post([FromBody] Car value)
        {
            Car newcar = new Car() { Id = GetId(), Model = value.Model, Vendor = value.Vendor, Price = value.Price };
            carList.Add(newcar);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Car value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            carList.Remove(Get(id));
        }

       int GetId()
        {
            int max = carList.Max(x => x.Id);
            return max+1;
        }

    }
}
