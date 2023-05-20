using MoneyControlReduce;

namespace MoneyControlReduce.Tests
{
    public class Transactions
    {

        [Test]
        public void WhenWeAddedNewIncomeWithValues_ShouldReturnNameSumMaxMin()
        {
            // arrange
            Transaction transactionIncome = new Transaction("Wyplata", Transaction.TRANSACTION_INCOME);
            Transaction transactionOutlay = new Transaction("Czynsz", Transaction.TRANSACTION_OUTLAY);

            // act
            transactionIncome.AddTransactionValue(500.00, false);
            transactionIncome.AddTransactionValue(1000, false);
            transactionIncome.AddTransactionValue("1500", false);
            transactionOutlay.AddTransactionValue(500, false);
            transactionOutlay.AddTransactionValue(1000, false);
            transactionOutlay.AddTransactionValue("1500", false);
            // assert
            Assert.AreEqual("Wyplata", transactionIncome.Name);
            Assert.AreEqual(3000.00, transactionIncome.GetStatistics().Sum);
            Assert.AreEqual(1500.00, transactionIncome.GetStatistics().Max);
            Assert.AreEqual(500.00, transactionIncome.GetStatistics().Min);

            Assert.AreEqual("Czynsz", transactionOutlay.Name);
            Assert.AreEqual(-3000.00, transactionOutlay.GetStatistics().Sum);
            Assert.AreEqual(-500.00, transactionOutlay.GetStatistics().Max);
            Assert.AreEqual(-1500.00, transactionOutlay.GetStatistics().Min);
        }

        [Test]
        public void WhenWeAddedTwoTransaction_SchuldReturnSumMinMax()
        {
            // arrange
            ContainerTransactions containerIncome = new ContainerTransactions(Transaction.TRANSACTION_INCOME, false);
            // act
            if (containerIncome.CountItems == 0)
            {
                containerIncome.AddNewTransaction("Wyplata");
                containerIncome.SetPosition(0);
                containerIncome.AddValueToTransaction("1000");
                containerIncome.AddValueToTransaction("2000");
                containerIncome.AddNewTransaction("Premia");
                containerIncome.SetPosition(1);
                containerIncome.AddValueToTransaction("500");
                containerIncome.AddValueToTransaction("1000");

            }

            // assert
            Assert.AreEqual(4500, containerIncome.GetStatistics().Sum);
            Assert.AreEqual(500, containerIncome.GetStatistics().Min);
            Assert.AreEqual(2000, containerIncome.GetStatistics().Max);

        }
        [Test]
        public void WhenWeAddedTwoContainerTransactions_SchuldReurnSumMinMax()
        {
            // arrange
            ContainerTransactions containerIncomes = new ContainerTransactions(Transaction.TRANSACTION_INCOME, false);
            ContainerTransactions containerOutlays = new ContainerTransactions(Transaction.TRANSACTION_OUTLAY, false);
            // act
            if (containerIncomes.CountItems == 0)
            {
                containerIncomes.AddNewTransaction("Wyplata");
                containerIncomes.SetPosition(0);
                containerIncomes.AddValueToTransaction("1000");
                containerIncomes.AddValueToTransaction("2000");
                containerIncomes.AddNewTransaction("Premia");
                containerIncomes.SetPosition(1);
                containerIncomes.AddValueToTransaction("500");
                containerIncomes.AddValueToTransaction("1000");
            }
            if (containerOutlays.CountItems == 0)
            {
                containerOutlays.AddNewTransaction("Czynsz");
                containerOutlays.SetPosition(0);
                containerOutlays.AddValueToTransaction("1000");
                containerOutlays.AddValueToTransaction("2000");
                containerOutlays.AddNewTransaction("Energia");
                containerOutlays.SetPosition(1);
                containerOutlays.AddValueToTransaction("500");
                containerOutlays.AddValueToTransaction("1000");
            }
            Statistics statistics;
            statistics = containerIncomes.GetStatistics() + containerOutlays.GetStatistics();
            
            // assert
            Assert.AreEqual(0, statistics.Sum);
            Assert.AreEqual(-2000, statistics.Min);
            Assert.AreEqual(2000, statistics.Max);
        }
    }
}