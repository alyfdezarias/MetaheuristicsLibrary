using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools
{
    public enum SearchSpace { Strong, Weak, Complete };
    public enum Order { Increasing, Decreasing, Random };

    public class TesterTools
    {
        public static Dictionary<string, string> LoadParametersData(string[] args)
        {
            Regex parametersExpression = new Regex(@"(?<option>\w+)=(?<value>\d+(\.\d+)?|\.\d+|\w+)");
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var item in args)
            {
                Match m = parametersExpression.Match(item);
                parameters.Add(m.Groups["option"].Value, m.Groups["value"].Value);
            }
            return parameters;
        }
    }

    public class LSStatistics
    {
        public double TravelCost { get; set; }
        public double StrongOverload { get; set; }
        public int Routes { get; set; }
        public double Time { get; set; }
        public LSStatistics(double travelCost, double strongOverload, int routes, double time)
        {
            TravelCost = travelCost;
            StrongOverload = strongOverload;
            Routes = routes;
            Time = time;
        }

        public override string ToString()
        {
            return $"TC={TravelCost}, O={StrongOverload}, R={Routes}, T={Time}";
        }
    }
}
