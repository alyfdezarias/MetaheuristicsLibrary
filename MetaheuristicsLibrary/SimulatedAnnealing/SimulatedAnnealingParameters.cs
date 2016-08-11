using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaheuristicsLibrary.SimulatedAnnealing
{
    public class SimulatedAnnealingParameters
    {
        public double InitialTemperature { get; set; }
        public double FinalTemperature { get; set; }
        public int Repetitions { get; set; }
        public double RepetitionFactor { get; set; }
        public double CoolingRate { get; set; }
        public Func<double[], double> CoolingProcedure { get; set; }

        public SimulatedAnnealingParameters(double initialTmp, double finalTmp,
                                            int repetitions, double repetitionFactor,
                                            double coolingRate, Func<double[], double> coolingProcedure)
        {
            InitialTemperature = initialTmp;
            FinalTemperature = finalTmp;
            Repetitions = repetitions;
            RepetitionFactor = repetitionFactor;
            CoolingRate = coolingRate;
            CoolingProcedure = coolingProcedure;
        }

        public SimulatedAnnealingParameters(double initialTmp,
                                            double finalTmp,
                                            int repetitions,
                                            double repetitionFactor,
                                            double coolingRate)
            : this(initialTmp, finalTmp, repetitions, repetitionFactor, coolingRate, GeometricCooling)
        { }


        public SimulatedAnnealingParameters(double initialCost, double probability, double worstSolution,
                                            int repetitions, double repetitionFactor,
                                            double coolingRate, int coolerAmount)
        {
            InitialTemperature = ComputeInitialTemperature(initialCost, probability, worstSolution);
            FinalTemperature = ComputeFinalTemperature(InitialTemperature, coolingRate, coolerAmount);
            Repetitions = repetitions;
            RepetitionFactor = repetitionFactor;
            CoolingRate = coolingRate;
            CoolingProcedure = GeometricCooling;
        }

        public static double GeometricCooling(double[] args)
        {
            //coolingFactor [0.8, 0.99]
            //args[0] = tmp
            //args[1] = coolingFactor
            return args[0] * args[1];
        }

        public static double LogaritmicCooling(double[] args)
        {
            //coolingFactor [0.01, 0.2]
            //args[0] = tmp
            //args[1] = coolingFactor
            //args[2] = standart desviation
            return args[0] / (1 + (Math.Log(1 + args[1]) * args[0]) / (3 * args[2]));
        }

        public static double ExponentialCooling(double[] args)
        {
            //coolingFactor[1.0, +oo]
            //args[0] = tmp
            //args[1] = coolingFactor
            //args[2] = standart desviation
            return args[0] / Math.Exp((args[1] * args[0]) / args[2]);
        }

        public static double ComputeInitialTemperature(double initialCost, double probability, double worstSolution)
        {
            return (-1 * worstSolution * initialCost) / Math.Log(probability);
        }

        public static double ComputeFinalTemperature(double initialTemperature, double coolingRate, int coolerAmount)
        {
            return initialTemperature * Math.Pow(coolingRate, coolerAmount);
        }
    }
}
