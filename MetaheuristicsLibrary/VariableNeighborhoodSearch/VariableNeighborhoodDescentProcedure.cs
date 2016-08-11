using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsLibrary.VariableNeighborhoodSearch
{
    public class VariableNeighborhoodDescentProcedure<TSolution>: LocalSearchProcedure<TSolution, NullParameters> where TSolution:ICloneable
    {
        public VariableNeighborhoodDescentProcedure() : base() { }

        public override TSolution Solve(NullParameters parameters, TSolution initial, List<Func<TSolution, Exploration, Random, TSolution>> neighborhoods, Exploration expCondition, Func<TSolution, double> cost, Random rdObj)
        {
            ResetProgress();
            timer.Start();

            TSolution current = (TSolution)initial.Clone();
            double currentCost = cost(current);

            int iteration = 0;
            OnIteration(new IterationEventArgs<TSolution>(current));

            for (int i = 0; i < neighborhoods.Count;) 
            {
                TSolution neighborhoodSolution = neighborhoods[i](current, expCondition, rdObj);
                double neighborhoodCost = cost(neighborhoodSolution);

                if (neighborhoodCost < currentCost)
                {
                    current = (TSolution)neighborhoodSolution.Clone();
                    currentCost = neighborhoodCost;
                    i = 0;
                }
                else
                    i++;
                iteration++;
                OnIteration(new IterationEventArgs<TSolution>(current, iteration, timer.Elapsed.TotalSeconds));

            }

            timer.Stop();
            return current;
        }

    }
}
