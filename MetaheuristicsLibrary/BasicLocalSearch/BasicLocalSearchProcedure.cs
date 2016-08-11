using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsLibrary.BasicLocalSearch
{
    public class BasicLocalSearchProcedure<TSolution>: LocalSearchProcedure<TSolution, NullParameters> where TSolution: ICloneable
    {
        public BasicLocalSearchProcedure(): base() { }

        public override TSolution Solve(NullParameters parameters, TSolution initial, List<Func<TSolution, Exploration, Random, TSolution>> neighborhoods, Exploration expCondition, Func<TSolution, double> cost, Random rdObj)
        {
            ResetProgress();

            TSolution current = (TSolution)initial.Clone();
            double currentCost = cost(current);
            int iteration = 0;
            OnIteration(new IterationEventArgs<TSolution>(current));

            timer.Start();
            while (true)
            {
                TSolution neighborhoodSolution = SelectRandomNeigborStrategy(neighborhoods, rdObj)(current, expCondition, rdObj);
                double neighborhoodSolutionCost = cost(neighborhoodSolution);
                if (neighborhoodSolutionCost < currentCost)
                {
                    currentCost = neighborhoodSolutionCost;
                    current = (TSolution)neighborhoodSolution.Clone();
                    iteration++;
                    OnIteration(new IterationEventArgs<TSolution>(current, iteration, timer.Elapsed.TotalSeconds));
                }
                else
                    break;
            }
            timer.Stop();
            return current;
        }
    }
}
