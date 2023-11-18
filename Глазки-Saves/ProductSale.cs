//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Глазки_Saves
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ProductSale
    {
        public int ID { get; set; }
        public int AgentID { get; set; }
        public int ProductID { get; set; }
        public System.DateTime SaleDate { get; set; }
        public int ProductCount { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
        private static EyesEntities _context;

        public static EyesEntities GetContext()
        {
            if (_context == null)
            {
                _context = new EyesEntities();
            }
            return _context;
        }
        public int SumProd
        {
            get
            {
                int sum = 0;
                sum = GetContext().ProductSale
                .Where(s => s.AgentID == this.AgentID)
                .Sum(s => s.ProductCount);
                return sum;
            }
        }
        public int Discount
        {
            get
            {
                var sumQuery = EyesEntities.GetContext().ProductSale
                .Join(EyesEntities.GetContext().Product,
                sale => sale.ProductID,
                product => product.ID,
                (sale, product) => new { sale, product })
                .Where(joined => joined.sale.AgentID == this.AgentID)
                .GroupBy(joined => joined.sale.AgentID)
                .Select(grouped => new
                {
                    AgentID = grouped.Key,
                    Total = grouped.Sum(x => x.sale.ProductCount * x.product.MinCostForAgent)
                });
                int sum = (int)(sumQuery.FirstOrDefault()?.Total ?? 0);

                int Disc = 0;

                if (sum > 10000 && sum < 50000)
                    Disc = 5;
                else if (sum > 50000 && sum < 150000)
                    Disc = 10;
                else if (sum > 150000 && sum < 500000)
                    Disc = 20;
                else if (sum > 500000)
                    Disc = 25;
                else
                    Disc = 0;
                return Disc;
            }
        }

    }

}
