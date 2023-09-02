using System.Collections.Generic;
using System.Linq;

namespace PerfumeStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Perfume perfume, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Perfume.PerfumeId == perfume.PerfumeId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Perfume = perfume,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        
        public void RemoveLines(Perfume perfume)
        {
            lineCollection.RemoveAll(l => l.Perfume.PerfumeId == perfume.PerfumeId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Perfume.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Perfume Perfume { get; set; }
        public int Quantity { get; set; }
    }
}
