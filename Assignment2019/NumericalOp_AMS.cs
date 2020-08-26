using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2019
{
    public class NumericalOp_AMS
    {
    
        //addition 
        public double sum(IDictionary<string, string> values)
        {
            double total = 0;
            foreach (KeyValuePair<string, string> item in values)
            {
                double value = double.Parse(item.Value);
                total += value;
            }
            return total;
        }

        //multiplication
        public double mul(IDictionary<string, string> values)
        {
            double total = 1;
            foreach (KeyValuePair<string, string> item in values)
            {
                double value = double.Parse(item.Value);
                total *= value;
            }
            return total;
        }

        //subtraction
        public double sub(IDictionary<string, string> values)
        {
            double total = 0;
            foreach (KeyValuePair<string, string> item in values)
            {
                double value = double.Parse(item.Value);
                total -=value;
            }
            return total;
        }

        //division
        public double div(IDictionary<string, string> values)
        {
            double total = 0;
            foreach (KeyValuePair<string, string> item in values)
            {
                double value = double.Parse(item.Value);
                total /= value;
            }
            return total;
        }

        //mean 
        public double Mean(IDictionary<string, string> values)
        {
            double total = 0;
            foreach (KeyValuePair<string, string> item in values)
            {
                double mean = double.Parse(item.Value);
                total += mean / values.Count();
            }
            return total;
        }

        //average
        public double Average(IDictionary<string, string> values)
        {
            double total = 0;
            foreach (KeyValuePair<string, string> item in values)
            {
                double avg = double.Parse(item.Value);
                total += avg / values.Count();
            }
            return total;
        }

        //Median 
        public double Median(List<double> values)
        {
            double total = 0;
            values.Sort();
               if (values.Count()+1%2==0)
               {
                    double midvalue1 = values[((values.Count) + 1)/2];
                    double midvalue2 = values[(values.Count) / 2];
                    total = (midvalue1 + midvalue2) / 2;

               }
               else
               {
                    total = values[(values.Count) / 2];
               }
            return total;
        }

        //Mode
        public double Mode(List<double> values)
        {
            Dictionary<double, double> counter = new Dictionary<double, double>();
            foreach (double x in values)
            {
                if (counter.ContainsKey(x))
                {
                    counter[x] = counter[x] + 1;
                }
                else
                {
                    counter[x] = 1;
                }
            }
            double Result_min = double.MinValue;
            double Result_max = double.MinValue;
            foreach (double key in counter.Keys)
            {
                if (counter[key] > Result_max)
                {
                    Result_max = counter[key];
                    Result_min = key;
                }
            }
            return Result_min;
        }

       
    }
}
