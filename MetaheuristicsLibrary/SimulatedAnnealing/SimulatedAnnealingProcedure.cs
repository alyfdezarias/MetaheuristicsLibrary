using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace MetaheuristicsLibrary.SimulatedAnnealing
{
    public class SimulatedAnnealingProcedure<TSolution> : LocalSearchProcedure<TSolution, SimulatedAnnealingParameters> where TSolution : ICloneable
    {
        public SimulatedAnnealingProcedure() : base() { }

        public double AceptanceProbability(double currentCost, double neihgborCost, double tmp)
        {
            return Math.Exp((currentCost - neihgborCost) / tmp);
        }

        public override TSolution Solve(SimulatedAnnealingParameters parameters, TSolution initial, List<Func<TSolution, Exploration, Random, TSolution>> neighborhoods, Exploration expCondition, Func<TSolution, double> cost, Random rdObj)
        {

            ResetProgress();

            TSolution current = (TSolution)initial.Clone();
            TSolution best = (TSolution)initial.Clone();
            TSolution neighbor = default(TSolution);
            double bestCost = cost(best);
            double currentCost = bestCost;
            double neighborCost;

            int repetitions = parameters.Repetitions;
            double temperature = parameters.InitialTemperature;
            int iteration = 0;
            OnIteration(new IterationEventArgs<TSolution>(best));

            List<double> iterationCost = new List<double>();

            timer.Start();

            while (temperature > parameters.FinalTemperature)
            {
                iterationCost.Clear();
                iterationCost.Add(currentCost);
                for (int i = 0; i < repetitions; i++)
                {
                    neighbor = SelectRandomNeigborStrategy(neighborhoods, rdObj)(current, expCondition, rdObj);
                    neighborCost = cost(neighbor);

                    iterationCost.Add(neighborCost);

                    if (neighborCost < bestCost)
                    {
                        best = (TSolution)neighbor.Clone();
                        bestCost = neighborCost;
                    }

                    if (neighborCost < currentCost || rdObj.NextDouble() < AceptanceProbability(currentCost, neighborCost, temperature))
                    {
                        current = (TSolution)neighbor.Clone();
                        currentCost = neighborCost;
                    }
                    iteration++;
                    OnIteration(new IterationEventArgs<TSolution>(best, current, iteration, timer.Elapsed.TotalSeconds));

                }
                temperature = parameters.CoolingProcedure(new double[] { temperature, parameters.CoolingRate, BasicStatisticsTool.StandardDeviation(iterationCost) });
                repetitions = (int)Math.Ceiling(repetitions * parameters.RepetitionFactor);

            }

            timer.Stop();
            return best;
        }
    }
}
