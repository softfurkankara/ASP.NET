using System.Collections.Generic;

namespace dotnet
{
    public class ProductManager : IProductDal
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal=productDal;
        }

        public int Count()
        {
            return _productDal.Count();
        }

        public int Create(Product p)
        {
            return _productDal.Create(p);
        }

        public int Delete(int productId)
        {
            return _productDal.Delete(productId);
        }

        public List<Product> Find(string ProductName)
        {
            return _productDal.Find(ProductName);
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productDal.GetProductById(id);
        }

        public int Update(Product p)
        {
            return _productDal.Update(p);
        }
    }
}