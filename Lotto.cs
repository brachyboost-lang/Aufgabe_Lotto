class Lotto
{
    private int[] lottoNumbers = new int[6];
    private int[] userNumbers = new int[6];
    private Random random = new Random();

    public void Start()
    {
        Console.WriteLine("Willkommen zum Lotto-Spiel!\n");
        while (true)
        {

        GetUserNumbers();     
        GenerateNumbers();    
        CheckResults();

        Console.WriteLine("\nMöchten Sie nochmal spielen? (j/n)");
        string playAgain = Console.ReadLine().ToLower();

            if (playAgain != "j")
            {
                Console.WriteLine("Danke fürs Spielen! Auf Wiedersehen!");
                Console.ReadKey();
                break;
            }       
        }
    }

    private void GetUserNumbers()
    {
        while (true)
        {
            Console.WriteLine("Bitte geben Sie 6 verschiedene Zahlen (1-49) ein:");

            string input = Console.ReadLine();

            // split durch Leerzeichen durch String.Split(), spart die Umwandlung von 
            // String in Array und die manuelle Überprüfung der Anzahl der Zahlen
            string[] parts = input.Split(' ');

            if (parts.Length != 6)
            {
                Console.WriteLine("Sie müssen genau 6 Zahlen eingeben!\n");
                continue;
            }

            bool valid = true;

            for (int i = 0; i < 6; i++)
            {
                if (int.TryParse(parts[i], out int number) && number >= 1 && number <= 49)
                {
                    if (userNumbers.Contains(number))
                    // contains statt for-Schleife, da es übersichtlicher ist und code erspart
                    {
                        Console.WriteLine("Doppelte Zahlen sind nicht erlaubt!\n");
                        valid = false;
                        break;
                    }

                    userNumbers[i] = number;
                }
                else
                {
                    Console.WriteLine($"Ungültige Eingabe: {parts[i]}");
                    valid = false;
                    break;
                }
            }

            if (valid)
                break;
        }
    }

    private void GenerateNumbers()
    {
        for (int i = 0; i < lottoNumbers.Length; i++)
        {
            int number;

            do
            {
                number = random.Next(1, 50);
            }

            while (lottoNumbers.Contains(number));

            lottoNumbers[i] = number;
        }

        Console.WriteLine("\nDie Lottozahlen wurden gezogen!");
    }

    private void CheckResults()
    {
        int hits = userNumbers.Intersect(lottoNumbers).Count();

            // string.Join erspart lottoNumbers[0], lottoNumbers[1], ... und userNumbers[0], userNumbers[1], ...
            // trennt die Zahlen mit ", " und gibt sie als String aus
        Console.WriteLine("\nIhre Zahlen:  " + string.Join(", ", userNumbers));
        Console.WriteLine("Gezogene Zahlen: " + string.Join(", ", lottoNumbers));
        Console.WriteLine($"Treffer: {hits}");

        // Array.Clear() setzt alle Elemente des Arrays auf den Standardwert (0 für int) zurück,
        // damit die alten Zahlen nicht mehr angezeigt werden, wenn der Spieler nochmal spielt
        Array.Clear(userNumbers, 0, userNumbers.Length);
        Array.Clear(lottoNumbers, 0, lottoNumbers.Length);
    }
}