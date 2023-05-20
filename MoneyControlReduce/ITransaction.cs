namespace MoneyControlReduce
{
    public interface ITransaction
    {
        string Name { get; }
        void AddTransactionValue(double value);
        void AddTransactionValue(double value, Boolean loadData);
        void AddTransactionValue(float value);
        void AddTransactionValue(int value);
        void AddTransactionValue(string value);
        void AddTransactionValue(string value, Boolean saveData);
    }
}
