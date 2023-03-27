using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Билет_11
{
    public class Discount
    {
        private int DiscountSize;
        public Discount(int Count)
        {
            if (Count >= 30)
            {
                DiscountSize = 25;
            }
            else if (Count >= 20)
            {
                DiscountSize = 10;
            }
            else if (Count >= 15)
            {
                DiscountSize = 7;
            }
            else if (Count >= 10)
            {
                DiscountSize = 5;
            }
        }
        public int GetDiscdountSize()
        {
            return DiscountSize;
        }
    }
}
