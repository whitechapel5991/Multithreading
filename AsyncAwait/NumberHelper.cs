using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait1
{
    class NumberHelper
    {
        public int Number { get; set; } = default;

        event Action OnNumberChange;

        public void TypeNumber()
        {
            Console.WriteLine("Type number:");
            try
            {
                Number = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("It needs type numeric symbol.");
            }
        }
    }
}
