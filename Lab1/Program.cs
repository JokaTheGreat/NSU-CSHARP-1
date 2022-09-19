class Freind
{
    public static int Compare(List<Contender> contenders, int contenderAId, int contenderBId, int currentContenderId)
    {
        if (currentContenderId < contenderAId || currentContenderId < contenderBId)
        {
            //Console.WriteLine("Idk..");
            return 1;
        }

        if (contenders[contenderAId].Goodness > contenders[contenderBId].Goodness)
        {
            //Console.WriteLine($"{contenders[contenderAId].ToString()} is better than {contenders[contenderBId].ToString()}");
            return 0;
        }
        
        //Console.WriteLine($"{contenders[contenderBId].ToString()} is better than {contenders[contenderAId].ToString()}");
        return 2;
    }
}

class Princess
{
    private const int AloneGoodness = 10;
    private const double FirstContendersSkipCount = Program.ContendersCount * 0.37;
    private const double ContenderToStopFactor = 2 * 0.93;
    public static int ChooseContender(List<Contender> contenders)
    {
        for (int i = 0; i < Program.ContendersCount; i++)
        {
            //Console.WriteLine(contenders[i].ToString());
            if (i > FirstContendersSkipCount)
            {
                int compareCount = 0;
                for (int j = 0; j < i; j++)
                {
                    compareCount += Freind.Compare(contenders, j, i, i);
                }

                if (!(i * ContenderToStopFactor <= compareCount)) continue;
                
                Console.WriteLine("---");
                Console.WriteLine($"{2 * i} >= {compareCount}");
                Console.WriteLine($"{contenders[i].Goodness}");
                return contenders[i].Goodness;
            }
        }
        
        Console.WriteLine("---");
        Console.WriteLine($"{AloneGoodness}");
        return AloneGoodness;
    }
}

public struct Contender
{
    public string Name { get; set; }
    public int Goodness { get; set; }

    public override string ToString()
    {
        return $"{Name}, {Goodness}";
    }
}

class Program
{
    public const int ContendersCount = 100;
    private const int MaxGoodness = ContendersCount;
    private const int MinGoodness = 50;
    private static List<Contender> contenders;
    
    private static void Shuffle<T>(IList<T> list)
    {
        var random = new Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n--);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    
    private static void Init()
    {
        var firstNames = new[]{"Никита", "Вадим", "Игорь", "Кирилл", "Матвей", 
            "Владимир", "Михаил", "Александр", "Максим", "Дмитрий"};
        var lastNames = new[]{"Иванов", "Петров", "Лобанов", "Киреев", "Мандрыко", 
            "Чернов", "Латыш", "Баранов", "Копылов", "Куликов"};
        int goodness = MaxGoodness;
        
        contenders = new List<Contender>();

        foreach (string firstName in firstNames)
        {
            foreach (string lastName in lastNames)
            {
                contenders.Add(new Contender { Name = $"{firstName} {lastName}", Goodness = goodness > MinGoodness ? goodness-- : 0 });
            }
        }

        Shuffle(contenders);
    }
    
    public static void Main()
    {
        //Init();
        //Princess.ChooseContender(contenders);
        
        double epochCounter = 1000;
        double averageHappiness = 0;
        for (int i = 0; i < epochCounter; i++)
        {
            Init();
            averageHappiness += Princess.ChooseContender(contenders) / epochCounter;
        }

        Console.WriteLine($"Average happiness is {averageHappiness}");
    }
}
