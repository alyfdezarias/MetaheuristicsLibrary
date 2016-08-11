using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsLibrary.VariableNeighborhoodSearch
{
    public class RandomVariableNeighborhoodDescentProcedure<TSolution> : LocalSearchProcedure<TSolution, NullParameters> where TSolution : ICloneable
    {
        public RandomVariableNeighborhoodDescentProcedure() : base() { }

        public override TSolution Solve(NullParameters parameters, TSolution initial, List<Func<TSolution, Exploration, Random, TSolution>> neighborhoods, Exploration expCondition, Func<TSolution, double> cost, Random rdObj)
        {
            ResetProgress();
            timer.Start();

            TSolution current = (TSolution)initial.Clone();
            double currentCost = cost(current);
            OnIteration(new IterationEventArgs<TSolution>(current));

            int iteration = 0;

            List<Func<TSolution, Exploration, Random, TSolution>> availables = new List<Func<TSolution, Exploration, Random, TSolution>>();
            availables.AddRange(neighborhoods);

            while (availables.Count > 0)
            {
                int neighborIndex = rdObj.Next(availables.Count);
                TSolution neighborhoodSolution = availables[neighborIndex](current, expCondition, rdObj);
                double neighborhoodCost = cost(neighborhoodSolution);
                if (neighborhoodCost < currentCost)
                {
                    current = (TSolution)neighborhoodSolution.Clone();
                    currentCost = neighborhoodCost;
                    availables.Clear();
                    availables.AddRange(neighborhoods);
                }
                else
                    availables.RemoveAt(neighborIndex);
                iteration++;
                OnIteration(new IterationEventArgs<TSolution>(current, iteration, timer.Elapsed.TotalSeconds));
            }

            timer.Stop();
            return current;
        }

    }
}
