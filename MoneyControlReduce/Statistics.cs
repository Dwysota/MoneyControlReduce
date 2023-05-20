namespace MoneyControlReduce
{
    public class Statistics
    {
        public double[] values { get; private set; }

        public double Sum
        {
            get
            {
                if (values == null)
                {
                    return 0;
                }
                return values.Sum();
            }

        }

        public double Min
        {
            get
            {
                if (values == null)
                {
                    return 0;
                }
                return values.Min();
            }
        }
        public double Max
        {
            get
            {
                if (values == null)
                {
                    return 0;
                }
                return values.Max();
            }

        }
        public double Average
        {
            get
            {
                if (values == null)
                {
                    return 0;
                }
                return values.Average();
            }
        }

        public Statistics()
        {
        }
        public Statistics(double[] values)
        {
            if (values == null)
            {
                this.values = new double[0];
            }
            else
            {
                this.values = values;
            }

        }
        public Statistics(List<Transaction> list)
        {
            int size = 0;
            foreach (Transaction transaction in list)
            {
                size += transaction.values.Count;
            }
            values = new double[size];
            int index = 0;
            foreach (Transaction transaction in list)
            {
                foreach (double value in transaction.values)
                {
                    values[index++] = value;
                }
            }
        }
        public Statistics(double[] a, double[] b)
        {
            values = new double[a.Length + b.Length];
            Array.Copy(a, values, a.Length);
            Array.Copy(b, 0, values, a.Length, b.Length);
        }

        public static Statistics operator +(Statistics a, Statistics b)
        {
            return new Statistics(a.values, b.values);
        }


    }
}
