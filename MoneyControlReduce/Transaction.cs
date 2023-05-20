using System.Xml.Linq;

namespace MoneyControlReduce
{
    public class Transaction : TransactionBase
    {
        public const int TRANSACTION_INCOME = 0;
        public const int TRANSACTION_OUTLAY = 1;
        public static string[] FOLDER_TRANSACTION = { "Incomes", "Outlays" };

        private int KindTransaction { get; set; }
        public Transaction(string name, int kind) : base(name)
        {
            KindTransaction = kind;
            SaveName(name);
        }
        public Transaction(string name, int kind, Boolean saveData) : base(name)
        {
            KindTransaction = kind;
            if (saveData)
            {
                SaveName(name);
            }

        }
        protected void SaveName(string name)
        {
            if (!Directory.Exists(FOLDER_TRANSACTION[TRANSACTION_INCOME]))
            {
                Directory.CreateDirectory($"{FOLDER_TRANSACTION[TRANSACTION_INCOME]}/{FOLDER_VALUES}");
            }
            if (!Directory.Exists(FOLDER_TRANSACTION[TRANSACTION_OUTLAY]))
            {
                Directory.CreateDirectory($"{FOLDER_TRANSACTION[TRANSACTION_OUTLAY]}/{FOLDER_VALUES}");
            }
            using (var file = File.AppendText($"{FOLDER_TRANSACTION[KindTransaction]}/{FILE_NAMES_TRANSACTION}"))
            {
                file.WriteLine(name);
            }
        }

        public override void AddTransactionValue(double value)
        {
            if (value > 0)
            {
                using (var file = File.AppendText($"{FOLDER_TRANSACTION[KindTransaction]}/{FOLDER_VALUES}/{Name}.txt"))
                {
                    file.WriteLine(value);
                }

                if (KindTransaction == TRANSACTION_OUTLAY)
                {
                    value *= -1;
                }
                base.AddTransactionValue(value);

            }
            else
            {
                throw new Exception("Value is not valid.");
            }

        }
        public override void AddTransactionValue(double value, Boolean saveData)
        {
            if (value > 0)
            {
                if (saveData)
                {
                    using (var file = File.AppendText($"{FOLDER_TRANSACTION[KindTransaction]}/{FOLDER_VALUES}/{Name}.txt"))
                    {
                        file.WriteLine(value);
                    }
                }

                if (KindTransaction == TRANSACTION_OUTLAY)
                {
                    value *= -1;
                }
                base.AddTransactionValue(value);

            }
            else
            {
                throw new Exception("Value is not valid.");
            }

        }
        public override void AddTransactionValue(string value)
        {
            if (double.TryParse(value, out double tmpValue))
            {
                this.AddTransactionValue(tmpValue);
            }
            else
            {
                throw new Exception($"Value is not valid.{value}");
            }
        }
        public override void AddTransactionValue(string value, Boolean loadData)
        {
            if (double.TryParse(value, out double tmpValue))
            {
                this.AddTransactionValue(tmpValue, loadData);
            }
            else
            {
                throw new Exception($"Value is not valid.{value}");
            }
        }
        public override void AddTransactionValue(float value)
        {
            this.AddTransactionValue((double)value);
        }
        public override void AddTransactionValue(int value)
        {
            this.AddTransactionValue((double)value);
        }
        public Statistics GetStatistics()
        {
            if (values.Count == 0) return new Statistics();
            return new Statistics(values.ToArray());
        }

    }
}
