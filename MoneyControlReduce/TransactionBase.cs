namespace MoneyControlReduce
{
    public abstract class TransactionBase : ITransaction
    {
        public static string FILE_NAMES_TRANSACTION = "Names.txt";
        public static string FOLDER_VALUES = "Values";

        public string Name { get; private set; }
        public List<double> values = new List<double>();

        protected TransactionBase(string name)
        {
            if (name == "")
            {
                throw new ArgumentNullException("name");
            }
            this.Name = name;
        }



        public virtual void AddTransactionValue(double value)
        {
            values.Add(value);
        }
        public virtual void AddTransactionValue(double value, Boolean saveData)
        {
            values.Add(value);
        }
        public abstract void AddTransactionValue(float value);
        public abstract void AddTransactionValue(int value);
        public abstract void AddTransactionValue(string value);
        public abstract void AddTransactionValue(string value, Boolean saveData);
        public override string ToString()
        {
            string valuesString = "";
            int i = 0;
            foreach (var value in values)
            {
                i++;
                valuesString += $"| {i}. {value}\n";
            }
            return valuesString;
        }
    }


}
