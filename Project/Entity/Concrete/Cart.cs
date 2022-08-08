
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    
        public class Cart
        {
            private List<CartLine> cartLines = new List<CartLine>();

            public List<CartLine> CartLines
            {
                get { return cartLines; }
            }

            public void AddProduct(Product product, int quantity)
            {
                var line = cartLines.FirstOrDefault(i => i.product.Id== product.Id);
                if (line == null)
                {
                    cartLines.Add(new CartLine() { product = product, Quantity = quantity });
                }
                else
                {
                    line.Quantity += quantity;
                }
            }

            public void DeleteProduct(Product product)
            {
                cartLines.RemoveAll(i => i.product.Id == product.Id);
            }

            public double Total()
            {
                return cartLines.Sum(i => i.product.UnitPrice * i.Quantity);
            }

            public void Clear()
            {
                cartLines.Clear();
            }
        }

        public class CartLine
        {
            public Product product { get; set; }
            public int Quantity { get; set; }
        }

    }

