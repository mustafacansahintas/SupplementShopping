using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order:BaseEntities
    {
     
        public double Total { get; set; }
        public Guid OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
     

        public string Username { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
   
        public virtual List<OrderLine> Orderlines { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int _UserId { get; set; }
        public virtual _User user { get; set; }
    }
    
}
