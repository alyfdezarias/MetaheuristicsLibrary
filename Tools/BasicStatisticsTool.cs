using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class BasicStatisticsTool
    {
        private static double SumOfSquares(List<double> data)
        {
            return data.Sum(x => Math.Pow(x, 2));
        }

        private static double SquareSum(List<double> data)
        {
            return Math.Pow(data.Sum(x => x), 2);
        }

        public static double Variance(List<double> data)
        {
            return (SumOfSquares(data) - SquareSum(data) / data.Count) / (data.Count - 1);
        }

        public static double StandardDeviation(List<double> data)
        {
            return Math.Sqrt(Variance(data));
        }

        public static Tuple<double, double> TrustInterval(List<double> data, double percentil)
        {
            double media = data.Average(x => x);
            double interval = percentil * StandardDeviation(data) / Math.Sqrt(data.Count);
            return new Tuple<double, double>(media - interval, media + interval);
        }

        public Tuple<double, double> TrustInterval95percent(List<double> data)
        {
            return TrustInterval(data, 1.96);
        }

        public Tuple<double, double> TrustInterval99percent(List<double> data)
        {
            return TrustInterval(data, 2.576);
        }
    }

}
