using MoneyControlReduce;
using System.Runtime.CompilerServices;

ContainerTransactions Incomes = new ContainerTransactions(Transaction.TRANSACTION_INCOME);
ContainerTransactions Outlays = new ContainerTransactions(Transaction.TRANSACTION_OUTLAY);

Display display = new Display();
display.OnUpdateDisplay += updateDisplay;
display.updateContainer(Outlays);
display.updateContainer(Incomes);
void updateDisplay(Object sender, EventArgs args)
{
    display.Show();
}

while (true)
{
    try
    {
        var menu = Console.ReadLine();
        if (menu == "b")
        {
            display.SetPosition("0");
            continue;
        }
        if (menu == "q")
        {
            break;
        }
        display.SetPosition(menu);
        switch (display.Position)
        {
            case 11:
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input == "b")
                    {
                        display.SetPosition("1");
                        Incomes.SetPosition("0");
                        break;
                    }
                    Incomes.AddNewTransaction(input);
                    display.updateContainer(Incomes);
                }
                break;
            case 12:
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input == "b")
                    {
                        display.SetPosition("1");
                        break;
                    }
                    Incomes.SetPosition(input);
                    display.updateContainer(Incomes);


                    while (true)
                    {
                        var value = Console.ReadLine();
                        if (value == "b")
                        {
                            display.SetPosition("12");
                            Incomes.SetPosition("0");
                            display.updateContainer(Incomes);
                            break;
                        }
                        Incomes.AddValueToTransaction(value);
                        display.updateContainer(Incomes);
                    }
                }
                break;
            case 21:
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input == "b")
                    {
                        display.SetPosition("2");
                        break;
                    }
                    Outlays.AddNewTransaction(input);
                    display.updateContainer(Outlays);
                }
                break;
            case 22:
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input == "b")
                    {
                        display.SetPosition("2");
                        Outlays.SetPosition("0");
                        break;
                    }
                    Outlays.SetPosition(input);
                    display.updateContainer(Outlays);
                    while (true)
                    {
                        var value = Console.ReadLine();
                        if (value == "b")
                        {
                            display.SetPosition("22");
                            Outlays.SetPosition("0");
                            display.updateContainer(Outlays);
                            break;
                        }
                        Outlays.AddValueToTransaction(value);
                        display.updateContainer(Outlays);
                    }

                }
                throw new Exception("Invalid option!!");
        }
    }
    catch (Exception e)
    {
        display.SetPosition("0");
        Incomes.SetPosition("0");
        display.ShowException(e.Message);
    }

}




