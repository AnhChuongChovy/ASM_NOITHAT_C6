using System.Collections.Generic;
using System.Linq;
using Admin.Model;

namespace Admin.Model
{
    public class AdminService
    {
            private List<Product> _products = new List<Product>();

            public List<Product> GetProducts()
            {
                return _products;
            }

            public Product GetProductById(int id)
            {
                return _products.FirstOrDefault(p => p.ID == id);
            }

            public void AddProduct(Product product)
            {
                product.ID = _products.Count + 1;
                _products.Add(product);
            }

            public void UpdateProduct(Product product)
            {
                var existingProduct = GetProductById(product.ID);
                if (existingProduct != null)
                {
                    existingProduct.TenSP = product.TenSP;
                    existingProduct.MoTa = product.MoTa;
                    existingProduct.Gia = product.Gia;
                }
            }

            public void DeleteProduct(int id)
            {
                var product = GetProductById(id);
                if (product != null)
                {
                    _products.Remove(product);
                }
            }

    }
}
