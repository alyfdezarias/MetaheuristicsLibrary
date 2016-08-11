using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsLibrary
{
    public enum Exploration { TotalRandom, Random, FirstImprovement, BestImprovement }

    public abstract class LocalSearchProcedure<TSolution, TParameters> : IterationNotifier<TSolution> where TSolution : ICloneable
    {
        public event IterationEventHandler<TSolution> IterationInfo;

        protected Stopwatch timer;

        public LocalSearchProcedure()
        {
            timer = new Stopwatch();
        }

        protected void OnIteration(IterationEventArgs<TSolution> args)
        {
            if (IterationInfo != null)
                IterationInfo(this, args);
        }

        protected virtual void ResetProgress()
        {
            timer.Reset();
        }

        public abstract TSolution Solve(TParameters parameters,
                                       TSolution initial,
                                       List<Func<TSolution, Exploration, Random, TSolution>> neighborhoods,
                                       Exploration expCondition,
                                       Func<TSolution, double> cost,
                                       Random rdObj);

        protected Func<TSolution, Exploration, Random, TSolution> SelectRandomNeigborStrategy(List<Func<TSolution, Exploration, Random, TSolution>> neighborhoods, Random rdObj)
        {
            return neighborhoods[rdObj.Next(neighborhoods.Count)];
        }

    }

    public class NullParameters { }

}
