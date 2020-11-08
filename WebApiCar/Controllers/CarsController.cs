using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        /// Method for get all the cars from the database
        /// </summary>
        /// <returns>List of cars</returns>
        // GET: api/Cars
        [HttpGet(Name = "GetAll")]
        public IEnumerable<Car> GetAll()
        {
            string selectall = "select id, vendor, model, price from Car";
            
            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectall, databaseConnection))
                {


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

        /// <summary>
        /// Method for get the car with the specific id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Car</returns>
        // GET: api/Cars/5
        [HttpGet("{id}", Name = "GetbyId")]
        public Car Get(int id)
        {
            string selectById = "select vendor, model, price from Car where id = @id";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectById, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);

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

        /// <summary>
        /// Method for get all the cars by a specific Vendor
        /// </summary>
        /// <param name="vendorGet"></param>
        /// <returns>List of cars</returns>
        // GET: api/Cars/Vendor/Tesla
        [HttpGet("byVendor/{vendorGet}", Name = "GetByVendor")]
        public IEnumerable<Car> GetByVendor(string vendorGet)
        {
            var carList = new List<Car>();
            string selectVendor = "select id, vendor, model, price from Car where vendor = @vendor";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectVendor, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@vendor", vendorGet);

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
        /// Method for get all the cars by a specific model
        /// </summary>
        /// <param name="modelGet"></param>
        /// <returns>List of cars</returns>
        // GET: api/Cars/Model/X1
        [HttpGet("byModel/{modelGet}", Name = "GetByModel")]
        public IEnumerable<Car> GetByModel(string modelGet)
        {
            var carList = new List<Car>();
            string selectModel = "select id, vendor, model, price from Car where model = @model";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectModel, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@model", modelGet);

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
        /// Method for get all the cars by a specific price
        /// </summary>
        /// <param name="priceGet"></param>
        /// <returns>List of cars</returns>
        // GET: api/Cars/Price/5000
        [HttpGet("byPrice/{priceGet}", Name = "GetByPrice")]
        public IEnumerable<Car> GetByPrice(string priceGet)
        {
            var carList = new List<Car>();
            string selectPrice = "select id, vendor, model, price from Car where price = @price";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectPrice, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@price", priceGet);

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
        /// Method for get all the cars by a specific price ordered by Vendor
        /// </summary>
        /// <param name="priceGet"></param>
        /// <param name="ascending"></param>
        /// <returns>List of cars</returns>
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
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectPrice, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@price", priceGet);
                    selectCommand.Parameters.AddWithValue("@asc", ascending);

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
        /// Method for get all the cars by a specific Vendor and Price
        /// </summary>
        /// <param name="vendorGet"></param>
        /// <param name="priceGet"></param>
        /// <returns>List of cars</returns>
        // GET: api/Cars/byVendor/Tesla/10000
        [HttpGet("byVendor/{vendorGet}/{priceGet}", Name = "GetByVendorAndPrice")]
        public IEnumerable<Car> GetByVendorAndPrice(string vendorGet, string priceGet)
        {
            var carList = new List<Car>();
            string selectVendorAndPrice = "select id, vendor, model, price from Car where vendor = @vendor and price = @price";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectVendorAndPrice, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@vendor", vendorGet);
                    selectCommand.Parameters.AddWithValue("@price", priceGet);

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
        public string Post([FromBody] Car value)
        {
            string insertString = "";
            var carList = new List<Car>();
            string insertCar = "insert into car (id,model,vendor,price) values (@id,@model,@vendor,@price)";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertCar, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", GetId());
                    insertCommand.Parameters.AddWithValue("@model",value.Model);
                    insertCommand.Parameters.AddWithValue("@vendor",value.Vendor);
                    insertCommand.Parameters.AddWithValue("@price",value.Price);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    insertString = $"Row(s) affected: {rowsAffected}.\nCar with id {GetId()} inserted.";
                }
                return insertString;
            }
            
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Car value)
        {
            string updateString = "";
            string updateCar = "update car set model = @model, vendor = @vendor, price = @price where id = @id";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(updateCar, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    insertCommand.Parameters.AddWithValue("@model", value.Model);
                    insertCommand.Parameters.AddWithValue("@vendor", value.Vendor);
                    insertCommand.Parameters.AddWithValue("@price", value.Price);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    updateString = $"Row(s) affected: {rowsAffected}.\nCar with id {id} updated.";
                }
                return updateString;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string deleteString = "";
            string deleteCar = "delete from car where id = @id";

            using (SqlConnection databaseConnection = new SqlConnection(conn))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(deleteCar, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = selectCommand.ExecuteNonQuery();
                    deleteString = $"Row(s) affected: {rowsAffected}.\nCar with id {id} deleted.";
                }
                return deleteString;
            }
        }

       int GetId()
        {
            int max = GetAll().Max(x => x.Id);
            return max+1;
        }

    }
}
