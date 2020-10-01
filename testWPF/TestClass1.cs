using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Internal;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows;

namespace testWPF
{
    public class TestClass1
    {
        public TestClass1()
        {
            int IamNaN = Convert.ToInt32("NaN");
            int Iam1 = 1;

            // first example  
            Double f1 = 1.0 / 0.0;

            bool res = Double.IsNaN(f1);

            // printing the output  
            if (res)
                Console.WriteLine(f1 + " is NaN");
            else
                Console.WriteLine(f1 + " is not NaN");

            // second example  
            double f2 = 0.0 / 0.0;

            bool res1 = Double.IsNaN(f2);

            // printing the output  
            if (res1)
                Console.WriteLine(f2 + " is NaN");
            else
                Console.WriteLine(f2 + " is not NaN");


            if (IamNaN == Iam1)
            {

            }

            Thickness t1 = new Thickness(Convert.ToDouble("NaN"));
        }
    }
}
