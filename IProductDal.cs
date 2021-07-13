using System.Collections.Generic;

namespace dotnet
{
    public interface IProductDal
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);

        int Count();

        List<Product> Find(string ProductName);
        int Create(Product p);
        int Update(Product p);
        int Delete(int productId);

    }
}