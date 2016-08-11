using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsLibrary
{
    public class IterationEventArgs<TSolution>  where TSolution : ICloneable
    {
        public TSolution Best { get; private set; }
        public TSolution Current { get; private set; }
        public int IterationNumber { get; private set; }
        public double ElapsedTime { get; private set; }

        public IterationEventArgs(TSolution best, TSolution current, int iteration, double elapsedTime)
        {
            Best = (TSolution)best.Clone();
            Current = (TSolution)current.Clone();
            IterationNumber = iteration;
            ElapsedTime = elapsedTime;
        }

        public IterationEventArgs(TSolution best, int iteration, double elapsedTime) : this(best, best, iteration, elapsedTime) { }

        public IterationEventArgs(TSolution best) : this(best,0,0) { }
    }

    public delegate void IterationEventHandler<TSolution>(object sender, IterationEventArgs<TSolution> args) where TSolution : ICloneable;

    public interface IterationNotifier<TSolution> where TSolution : ICloneable
    {
        event IterationEventHandler<TSolution> IterationInfo;
    }
}
