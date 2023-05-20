using System.Net.NetworkInformation;

namespace MoneyControlReduce
{
    public class Display
    {

        public delegate void UpdateDisplay(Object value, EventArgs args);
        public event UpdateDisplay OnUpdateDisplay;
        public int Position { get; private set; }
        private bool Exists { get; set; }

        private ContainerTransactions Incomes { get; set; }
        private ContainerTransactions Outlays { get; set; }
        private Statistics Statistics { get; set; }
        public Display()
        {
            Position = 0;
            Exists = false;
            Incomes = new ContainerTransactions(Transaction.TRANSACTION_INCOME);
            Outlays = new ContainerTransactions(Transaction.TRANSACTION_OUTLAY);
        }

        public void updateContainer(ContainerTransactions conTransaction)
        {
            Statistics = new Statistics();
            switch (conTransaction.KindTransaction)
            {
                case Transaction.TRANSACTION_INCOME:
                    Incomes = conTransaction;
                    break;
                case Transaction.TRANSACTION_OUTLAY:
                    Outlays = conTransaction;
                    break;
            }
            if (Incomes != null && Outlays != null)
            {
                Statistics = Incomes.GetStatistics() + Outlays.GetStatistics();
            }

            if (OnUpdateDisplay != null)
            {
                OnUpdateDisplay(this, new EventArgs());
            }
        }
        public void ShowHeader()
        {
            Console.Clear();
            Console.WriteLine("\t\t\tControl your money!!!\n" +
            "---------------------------------------------------------------------------\n" +
            "\t(b - back, q - quit (in main menu))\n");
        }

        public void ShowMainMenu()
        {
            string MainMenuList = "\tMain menu:\n--------------------------\n" +
                                 $"1. Incomes (Summary: {Incomes.GetStatistics().Sum})\n" +
                                 $"2. Outlay (Summary: {Outlays.GetStatistics().Sum})\n" +
                                 $"3. Show statistics (Balance: {Statistics.Sum})\n";

            Console.WriteLine(MainMenuList);
            Console.WriteLine("------Choose option:");
        }
        public void ShowException(string excepption)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\t\t{excepption}!!!!!!!!!!!!");
            Console.ResetColor();
        }
        public void AlreadyExist(bool exist)
        {
            Exists = exist;
        }

        // Menage display for incomes
        public void ShowIncomesMenu()
        {
            Console.WriteLine("------List of incomes");
            Console.WriteLine(Incomes.ToString());
            Console.WriteLine("------Choose option:");
            Console.WriteLine("11. Add new income.\n12. Add value to income.\n");
        }
        public void ShowValueToIncomeP1()
        {
            Console.WriteLine("------List of incomes");
            Console.WriteLine(Incomes.ToString());
            Console.WriteLine("------Choose income:\n");

        }
        public void ShowValueToIncomeP2()
        {
            Console.WriteLine($"Incomes for {Incomes.GetActiveName()}\n");
            Console.WriteLine("------List of values\n");
            Console.WriteLine(Incomes.GetValuesSelectedTransaction());
            Console.WriteLine($"Add value to {Incomes.GetActiveName()}");
        }
        public void AddNewKindOfIncome()
        {
            Console.WriteLine("------List of incomes");
            Console.WriteLine(Incomes.ToString());
            Console.WriteLine("------New kind of income:");
        }
        // Menage display for outlay
        public void ShowOutlaysMenu()
        {
            Console.WriteLine("------List of outlays");
            Console.WriteLine(Outlays.ToString());
            Console.WriteLine("------Choose option:");
            Console.WriteLine("21. Add new outlay.\n22. Add value to outlay.\n");
        }
        public void ShowValueToOutlayP1()
        {
            Console.WriteLine("------List of outlays");
            Console.WriteLine(Outlays.ToString());
            Console.WriteLine("------Choose outlay:");

        }


        public void ShowValueToOutlayP2()
        {
            Console.WriteLine($"Outlays for {Outlays.GetActiveName()}\n");
            Console.WriteLine("------List of values\n");
            Console.WriteLine(Outlays.GetValuesSelectedTransaction());
            Console.WriteLine($"Add value to {Outlays.GetActiveName()}");
        }
        public void AddNewKindOfOutlay()
        {
            Console.WriteLine(Outlays.ToString());
            Console.WriteLine("New kind of outlay:");
        }
        public void SetPosition(string position)
        {

            if (int.TryParse(position, out int pos))
            {
                this.Position = pos;
                if (OnUpdateDisplay != null)
                {
                    OnUpdateDisplay(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid value.");
            }

        }
        private string DrawProcent(int procent)
        {
            int i = 0;
            string line = "\t";
            while (i < procent)
            {
                line += "|";
                i++;
            }
            return line;
        }
        private void ShowStatistics()
        {

            Console.WriteLine("------Statistics------");
            Console.Write("|");
            if (Incomes.GetStatistics().Sum > Outlays.GetStatistics().Sum * -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (Incomes.GetStatistics().Sum < Outlays.GetStatistics().Sum * -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"\tBalance: {Statistics.Sum}{DrawProcent(100)}");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tIncomes: {Incomes.GetStatistics().Sum}{DrawProcent((int)(Incomes.GetStatistics().Sum / (Incomes.GetStatistics().Sum + (Outlays.GetStatistics().Sum * -1)) * 100))}");
            Console.ResetColor();
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\tOutlays: {Outlays.GetStatistics().Sum * -1}{DrawProcent((int)(Outlays.GetStatistics().Sum * (-1) / (Incomes.GetStatistics().Sum + (Outlays.GetStatistics().Sum * -1)) * 100))}");
            Console.ResetColor();
            Console.WriteLine("------Incomes");
            Console.WriteLine($"|\tMax incomes: {Incomes.GetStatistics().Max}");
            Console.WriteLine($"|\tMin incomes: {Incomes.GetStatistics().Min}");
            Console.WriteLine($"|\tAverage incomes: {Incomes.GetStatistics().Average:N2}");
            Console.WriteLine($"|\tHighest value of the total income categories: {Incomes.getHigestSumTransaction().Name}, Summary: {Incomes.getHigestSumTransaction().GetStatistics().Sum}");
            Console.WriteLine($"|\tLowest value of the total income categories: {Incomes.getSmallestSumTransaction().Name}, Summary: {Incomes.getSmallestSumTransaction().GetStatistics().Sum}\n");

            Console.WriteLine("------Outlays");
            Console.WriteLine($"|\tMax outlays: {Outlays.GetStatistics().Max}");
            Console.WriteLine($"|\tMin outlays: {Outlays.GetStatistics().Min}");
            Console.WriteLine($"|\tAverage outlays: {Outlays.GetStatistics().Average:N2}");
            Console.WriteLine($"|\tHighest value of the total outlay categories: {Outlays.getSmallestSumTransaction().Name}, Summary: {Outlays.getSmallestSumTransaction().GetStatistics().Sum}");
            Console.WriteLine($"|\tLowest value of the total outlay categories: {Outlays.getHigestSumTransaction().Name}, Summary: {Outlays.getHigestSumTransaction().GetStatistics().Sum}\n");

        }
        public void Show()
        {
            this.ShowHeader();
            switch (Position)
            {
                case 0:
                    this.ShowMainMenu();
                    break;
                case 1:
                    this.ShowIncomesMenu();
                    break;
                case 11:
                    this.AddNewKindOfIncome();
                    break;
                case 12:
                    if (Incomes.PositionSelected != -1)
                    {
                        this.ShowValueToIncomeP2();
                    }
                    else
                    {
                        this.ShowValueToIncomeP1();
                    }
                    break;
                case 2:
                    this.ShowOutlaysMenu();
                    break;
                case 21:
                    this.AddNewKindOfOutlay();
                    break;
                case 22:
                    if (Outlays.PositionSelected != -1)
                    {
                        ShowValueToOutlayP2();
                    }
                    else
                    {
                        this.ShowValueToOutlayP1();
                    }
                    break;
                case 3:
                    this.ShowStatistics();
                    break;
                default:
                    throw new Exception("Ivalid option!!");
            }
            if (Exists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t\t\tAlready exists!!!!!!!!!!!!");
                Console.ResetColor();
                Exists = false;
            }
        }
    }
}
