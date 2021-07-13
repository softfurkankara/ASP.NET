using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
          var productManager=new ProductManager(new SqlProductDal());
          var products=productManager.GetProductById(4);
          Console.WriteLine($"{products.Name}");
          Console.WriteLine("-----------");
          
          var productDal=new ProductManager(new SqlProductDal());
          var prod=productDal.Find("Sir");
          foreach(var item in prod)
          {
              Console.WriteLine($"{item.ProductId}, {item.Name}");
          }
          Console.WriteLine("--------");
          int Count=productDal.Count();
          Console.WriteLine($"Total Products:{Count}");

          var p=new Product(){
            ProductId=71,
            Name="Samnsung s10",
            Price=10000
          };
          int cou=productDal.Update(p);

          int ress=productDal.Delete(78);
          Console.WriteLine($"{ress} adet  Kayıt Silindii");
          


        }


        static SqlConnection GetSqlConnection(){
            string connectionString=@"Data Source=.\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=SSPI;";
            return new SqlConnection(connectionString);           
        }
    
    }

    
}
