using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace dotnet
{
    public class SqlProductDal : IProductDal
    {
        private SqlConnection GetSqlConnection(){
            string connectionString=@"Data Source=.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=SSPI;";
            return new SqlConnection(connectionString);
        }
        public int Create(Product p)
        {
            int result=0;
            using (var connection = GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql= "insert into Products(ProductName,UnitPrice,Discontinued) VALUES (@ProductName,@UnitPrice,@Discontinued)";
                    SqlCommand command=new SqlCommand(sql,connection);
                    command.Parameters.AddWithValue("@ProductName",p.Name);
                    command.Parameters.AddWithValue("@UnitPrice",p.Price);
                    command.Parameters.AddWithValue("@Discontinued",1);

                    result=command.ExecuteNonQuery();
                    Console.WriteLine($"{result} eklendi");

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return result;
        }

        public int Delete(int productId)
        {
            int result=0;
            using (var connection = GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql= "delete from products where ProductID=@productid";
                    SqlCommand command=new SqlCommand(sql,connection);
                    command.Parameters.AddWithValue("@productid", productId);

                    result=command.ExecuteNonQuery();
                    

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return result;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products=null;
            using(var connection=GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql="select *from products";
                    SqlCommand command=new SqlCommand(sql,connection);
                    SqlDataReader reader=command.ExecuteReader();

                    products=new List<Product>();
                    while(reader.Read())
                    {
                        products.Add(
                            new Product{
                                ProductId=int.Parse(reader["ProductID"].ToString()),
                                Name=reader["ProductName"].ToString(),
                                Price=double.Parse(reader["UnitPrice"]?.ToString())

                            }
                        );
                    }
                    reader.Close();

                
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }



            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product=null;
            using(var connection=GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql ="select * from Products where ProductID=@id";
                    SqlCommand command=new SqlCommand(sql,connection);
                    command.Parameters.Add("@id",SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    SqlDataReader reader=command.ExecuteReader();
                    reader.Read();
                    if(reader.HasRows)
                    {
                        product=new Product(){
                        ProductId=int.Parse(reader["ProductID"].ToString()),
                        Name=reader["ProductName"].ToString(),
                        Price=double.Parse(reader["UnitPrice"]?.ToString())
                        };
                        
                    }

                    
                    reader.Close();

                
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }



            }
            return product;
        }

        public int Update(Product p)
        {
            int result=0;
            using (var connection = GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql= "update Products set ProductName=@productname,UnitPrice=@unitprice where ProductID=@productid";
                    SqlCommand command=new SqlCommand(sql,connection);
                    command.Parameters.AddWithValue("@productname",p.Name);
                    command.Parameters.AddWithValue("@unitprice",p.Price);
                    command.Parameters.AddWithValue("@productid",p.ProductId);

                    result=command.ExecuteNonQuery();
                    

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return result;
        }

        public List<Product> Find(string ProductName)
        {
            List<Product> products=null;
            using(var connection=GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql ="select * from Products where ProductName LIKE @ProductName";
                    SqlCommand command=new SqlCommand(sql,connection);
                    command.Parameters.Add("@ProductName",SqlDbType.Text);
                    command.Parameters["@ProductName"].Value = "%"+ProductName+"%";

                     SqlDataReader reader=command.ExecuteReader();

                    products=new List<Product>();
                    while(reader.Read())
                    {
                        products.Add(
                            new Product{
                                ProductId=int.Parse(reader["ProductID"].ToString()),
                                Name=reader["ProductName"].ToString(),
                                Price=double.Parse(reader["UnitPrice"]?.ToString())

                            }
                        );
                    }
                    

                    
                    reader.Close();

                
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }



            }
            return products;
        }

        public int Count()
        {
            int count=0;
            using(var connection=GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql ="select count(*) from Products";
                    SqlCommand command=new SqlCommand(sql,connection);
                    object result=command.ExecuteScalar();
                    if (result!=null)
                    {
                        count=Convert.ToInt32(result);
                    }
                                      
                
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }



            }
            return count;
        }
    }
}