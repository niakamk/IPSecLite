using System;
using System.Collections.Generic;
using System.Text;

namespace adabtek.IPsecLite.Stats
{
    public class Statistics
    {
        double[] doubleVals;
        int cleanDataLength;
        double mean;
        double std;

        public Statistics(double[] Values)
        {
            double sigmaX = 0;
            double dev = 0;
            int n = Values.Length;
            for (int i = 0; i < n; i++)
                sigmaX += Values[i];

            mean = (double)sigmaX / n;

            for (int i = 0; i < n; i++)
                dev += Math.Pow((Values[i] - mean), 2);

            if (n < 30)
                std = Math.Sqrt(dev / (n - 1));
            else
                std = Math.Sqrt(dev / n);

REPEAT:
            doubleVals = new double[Values.Length];
            sigmaX = 0;
            cleanDataLength = 0;

            for (int i = 0; i < n; i++)
                if (Math.Abs((Values[i] - mean) / std) <= 3)
                {
                    doubleVals[cleanDataLength++] = Values[i];
                    sigmaX += Values[i];
                }

            mean = (double)sigmaX / cleanDataLength;
            dev = 0;
            for (int i = 0; i < cleanDataLength; i++)
                dev += Math.Pow((doubleVals[i] - mean), 2);

            if (cleanDataLength < 30)
                std = Math.Sqrt(dev / (cleanDataLength - 1));
            else
                std = Math.Sqrt(dev / cleanDataLength);


            if (doubleVals.Length != cleanDataLength)
            {
                Values = new double[cleanDataLength];
                n = Values.Length;
                for (int z = 0; z < cleanDataLength; z++)
                    Values[z] = doubleVals[z];
                goto REPEAT;
            }
        }
        public int Length
        {
            get { return this.cleanDataLength; }
        }
        public double Mean
        {
            get { return this.mean; }
        }
        public double Std
        {
            get { return this.std; }
        }
    }
}
