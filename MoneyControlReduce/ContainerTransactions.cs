using MoneyControlReduce;
namespace MoneyControlReduce
{
    public class ContainerTransactions
    {

        public int KindTransaction { get; private set; }
        private List<Transaction> transactions = new List<Transaction> { };
        private string FileNamesTransactions { get; set; }
        public int PositionSelected { get; protected set; }
        public int CountItems
        {
            get
            {
                return transactions.Count;
            }
        }
        public ContainerTransactions(int kindTransaction, bool loadData)
        {
            PositionSelected = -1;
            KindTransaction = kindTransaction;
            FileNamesTransactions = $"{Transaction.FOLDER_TRANSACTION[KindTransaction]}/{Transaction.FILE_NAMES_TRANSACTION}";
            if (loadData)
            {
                LoadData();
            }
            
        }
        public ContainerTransactions(int kindTransaction)
        {
            PositionSelected = -1;
            KindTransaction = kindTransaction;
            FileNamesTransactions = $"{Transaction.FOLDER_TRANSACTION[KindTransaction]}/{Transaction.FILE_NAMES_TRANSACTION}";
            LoadData();
        }
        protected void LoadData()
        {
            if (File.Exists(FileNamesTransactions))
            {
                using (var file = File.OpenText(FileNamesTransactions))
                {
                    var line = file.ReadLine();
                    while (line != null)
                    {
                        transactions.Add(new Transaction(line, KindTransaction, false));
                        string FilesWithValues = $"{Transaction.FOLDER_TRANSACTION[KindTransaction]}/{Transaction.FOLDER_VALUES}/{line}.txt";
                        if (File.Exists(FilesWithValues))
                        {
                            using (var fileValues = File.OpenText(FilesWithValues))
                            {
                                var lineValue = fileValues.ReadLine();
                                while (lineValue != null)
                                {
                                    transactions[transactions.Count - 1].AddTransactionValue(lineValue, false);
                                    lineValue = fileValues.ReadLine();
                                }

                            }
                        }
                        line = file.ReadLine();
                    }

                }
            }
        }
        public void AddNewTransaction(string name)
        {
            foreach (var transaction in transactions)
            {
                if (transaction.Name == name) throw new Exception("Transaction exist!!");
            }
            transactions.Add(new Transaction(name, KindTransaction));
        }
        public void AddNewTransaction(string name, bool saveData)
        {
            foreach (var transaction in transactions)
            {
                if (transaction.Name == name) throw new Exception("Transaction exist!!");
            }
            transactions.Add(new Transaction(name, KindTransaction, saveData));
        }
        public void AddValueToTransaction(string value)
        {
            if (PositionSelected < 0 || PositionSelected >= transactions.Count)
            {
                throw new Exception("Wrong value index!!!");
            }
            transactions[PositionSelected].AddTransactionValue(value);
        }
        public void AddValueToTransaction(string value, bool saveData)
        {
            if (PositionSelected < 0 || PositionSelected >= transactions.Count)
            {
                throw new Exception("Wrong value index!!!");
            }
            transactions[PositionSelected].AddTransactionValue(value, saveData);
        }
        public void SetPosition(int position)
        {
            if (position >= -1 || PositionSelected >= transactions.Count)
            {
                this.PositionSelected = position;
            }
            else
            {
                throw new Exception("Wrong value index!!!");
            }
        }
        public void SetPosition(string position)
        {
            if (int.TryParse(position, out int pos))
            {
                this.SetPosition(pos - 1);
            }
            else
            {
                throw new Exception("Wrong value index!!!");
            }
        }
        public Statistics GetStatistics()
        {
            return new Statistics(transactions);
        }
        public override string ToString()
        {
            string dataToString = "";
            int i = 0;
            foreach (var transaction in transactions)
            {
                i++;
                dataToString += $"| {i}. {transaction.Name} \t" +
                    $"(Summary: {transaction.GetStatistics().Sum}\t" +
                    $"Average: {transaction.GetStatistics().Average}\t" +
                    $"Min: {transaction.GetStatistics().Min}\t" +
                    $"Max: {transaction.GetStatistics().Max})\n";
            }
            return dataToString;
        }
        public string GetActiveName()
        {
            return transactions[PositionSelected].Name;
        }
        public string GetValuesSelectedTransaction()
        {
            return transactions[PositionSelected].ToString();
        }
        private List<Transaction> SortTransactionsViaSumValue()
        {
            return (List<Transaction>)transactions.OrderBy(o => o.GetStatistics().Sum);
        }
        public Transaction getHigestSumTransaction()
        {
            transactions.Sort((x, y) => x.GetStatistics().Sum.CompareTo(y.GetStatistics().Sum));
            return transactions[transactions.Count-1];
        }
        public Transaction getSmallestSumTransaction()
        {
            transactions.Sort((x, y) => x.GetStatistics().Sum.CompareTo(y.GetStatistics().Sum)); 
            return transactions[0];
        }
    }
}
