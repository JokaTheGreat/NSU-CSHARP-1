class Freind
{
    public static int Compare(List<Contender> contenders, int contenderAId, int contenderBId, int currentContenderId)
    {
        if (currentContenderId < contenderAId || currentContenderId < contenderBId)
        {
            Console.WriteLine("Idk..");
            return 0;
        }

        if (contenders[contenderAId].Goodness > contenders[contenderBId].Goodness)
        {
            Console.WriteLine($"{contenders[contenderAId].ToString()} is better than {contenders[contenderBId].ToString()}");
            return 1;
        }
        
        Console.WriteLine($"{contenders[contenderBId].ToString()} is better than {contenders[contenderAId].ToString()}");
        return 2;
    }
}

class Princess
{
    public static int ChooseContender(List<Contender> contenders)
    {
        for (int i = 0; i < 100; i++)
        {
            //Console.WriteLine(contenders[i].ToString());
            if (i == 5)
            {
                Console.WriteLine("---");
                Console.WriteLine($"{contenders[i].Goodness}");
                return contenders[i].Goodness;
            }
        }

        return 0;
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
        var firstNames = new string[]{"Никита", "Вадим", "Игорь", "Кирилл", "Матвей", 
            "Владимир", "Михаил", "Александр", "Максим", "Дмитрий"};
        var lastNames = new string[]{"Иванов", "Петров", "Лобанов", "Киреев", "Мандрыко", 
            "Чернов", "Латыш", "Баранов", "Копылов", "Куликов"};
        int goodness = 100;
        
        contenders = new List<Contender>();

        foreach (string firstName in firstNames)
        {
            foreach (string lastName in lastNames)
            {
                contenders.Add(new Contender { Name = $"{firstName} {lastName}", Goodness = goodness > 50 ? goodness-- : 0 });
            }
        }

        Shuffle(contenders);
    }
    
    public static void Main()
    {
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
