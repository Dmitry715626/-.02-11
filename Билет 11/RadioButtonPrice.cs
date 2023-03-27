using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Билет_11
{
    public class RadioButtonPrice
    {
        private double LastPrice;
        public RadioButtonPrice(RadioButton button, double StartPrice) 
        {
            double Cost = 0;

            if(button.Name == "VIP")
            {
                Cost = (StartPrice / 100) * 50;
            }
            else if (button.Name == "Parter")
            {
                Cost = (StartPrice / 100) * 7;
            }
            else if (button.Name == "Balkon")
            {
                Cost = (StartPrice / 100) * 20;
            }

            LastPrice = StartPrice + Cost;
        }
        public double GetPrice()
        {
            return LastPrice;
        }
    }
}
