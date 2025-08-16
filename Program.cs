using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory_Activity_29
{
    internal class Program
    {
        class SensorStream
        {
            private List<double> readings;
            private int window;
            private double spikeThreshold;

            public SensorStream(List<double> readings, int window, double spikeThreshold)
            {
                if (window > readings.Count || window <= 0)
                {
                    throw new ArgumentException("Invalid window size!");
                }

                this.readings = readings;
                this.window = window;
                this.spikeThreshold = spikeThreshold;
            }

            public List<double> MovingAverage()
            {
                List<double> smooth = new List<double>();
                for (int i = 0; i <= readings.Count - window; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < window; j++)
                    {
                        sum += readings[i + j];
                    }
                    smooth.Add(sum / window);
                }
                return smooth;
            }

            public List<int> DetectSpikes()
            {
                List<int> spikes = new List<int>();
                for (int i = 0; i < readings.Count; i++)
                {
                    if (readings[i] > spikeThreshold)
                    {
                        spikes.Add(i);
                    }
                }
                return spikes;
            }
        }
        static void Main()
        {
            List<double> data = new List<double> { 1.0, 2.0, 5.0, 2.5, 10.0, 2.2, 3.0 };
            int window = 3;
            double threshold = 6.0;

            SensorStream stream = new SensorStream(data, window, threshold);

            Console.WriteLine("Smoothed values (moving average):");
            foreach (var avg in stream.MovingAverage())
            {
                Console.WriteLine(avg);
            }

            Console.WriteLine("\nDetected spikes at indices:");
            foreach (var idx in stream.DetectSpikes())
            {
                Console.WriteLine($"Spike at index {idx} (value = {data[idx]})");
            }
        }
    }
}
