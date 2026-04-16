using System;

namespace GenspilSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            InventorySystem system = new InventorySystem();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("===== GENSPIL LAGERSTYRING =====");
                Console.WriteLine("1. Tilføj nyt spil");
                Console.WriteLine("2. Vis alle spil");
                Console.WriteLine("3. Søg efter spil");
                Console.WriteLine("4. Tilføj forespørgsel");
                Console.WriteLine("5. Vis forespørgsler");
                Console.WriteLine("6. Udskriv lagerliste sorteret efter navn");
                Console.WriteLine("7. Udskriv lagerliste sorteret efter genre");
                Console.WriteLine("0. Afslut");
                Console.Write("Vælg en mulighed: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGameMenu(system);
                        break;

                    case "2":
                        Console.Clear();
                        system.ShowAllGames();
                        Pause();
                        break;

                    case "3":
                        SearchMenu(system);
                        break;

                    case "4":
                        AddRequestMenu(system);
                        break;

                    case "5":
                        Console.Clear();
                        system.ShowRequests();
                        Pause();
                        break;

                    case "6":
                        Console.Clear();
                        system.PrintSortedByName();
                        Pause();
                        break;

                    case "7":
                        Console.Clear();
                        system.PrintSortedByGenre();
                        Pause();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Pause();
                        break;
                }
            }
        }

        static void AddGameMenu(InventorySystem system)
        {
            Console.Clear();
            Console.WriteLine("===== TILFØJ SPIL =====");

            Console.Write("Navn: ");
            string name = Console.ReadLine();

            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            Console.Write("Antal spillere: ");
            int players = int.Parse(Console.ReadLine());

            Console.Write("Stand: ");
            string condition = Console.ReadLine();

            Console.Write("Pris: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Er på lager? (ja/nej): ");
            bool inStock = Console.ReadLine().ToLower() == "ja";

            BoardGame game = new BoardGame
            {
                Id = new Random().Next(1000, 9999),
                Name = name,
                Genre = genre,
                NumberOfPlayers = players,
                Condition = condition,
                Price = price,
                InStock = inStock
            };

            system.AddGame(game);

            Console.WriteLine("Spillet er tilføjet.");
            Pause();
        }

        static void SearchMenu(InventorySystem system)
        {
            Console.Clear();
            Console.WriteLine("===== SØG SPIL =====");

            Console.Write("Navn: ");
            string name = Console.ReadLine();

            Console.Write("Genre: ");
            string genre = Console.ReadLine();

            Console.Write("Antal spillere: ");
            string playersInput = Console.ReadLine();

            Console.Write("Stand: ");
            string condition = Console.ReadLine();

            Console.Write("Maks pris: ");
            string priceInput = Console.ReadLine();

            int? players = string.IsNullOrWhiteSpace(playersInput)
                ? null
                : int.Parse(playersInput);

            decimal? maxPrice = string.IsNullOrWhiteSpace(priceInput)
                ? null
                : decimal.Parse(priceInput);

            var result = system.SearchGames(name, genre, players, condition, maxPrice);

            Console.WriteLine("\n===== RESULTATER =====");

            if (result.Count == 0)
                Console.WriteLine("Ingen spil fundet.");
            else
                foreach (var game in result)
                    Console.WriteLine(game);

            Pause();
        }

        static void AddRequestMenu(InventorySystem system)
        {
            Console.Clear();
            Console.WriteLine("===== NY FORESPØRGSEL =====");

            Console.Write("Kundenavn: ");
            string customerName = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            string phone = Console.ReadLine();

            Console.Write("Ønsket spil: ");
            string gameName = Console.ReadLine();

            Request request = new Request
            {
                CustomerName = customerName,
                CustomerPhone = phone,
                GameName = gameName
            };

            system.AddRequest(request);

            Console.WriteLine("Forespørgslen er gemt.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nTryk Enter for at fortsætte...");
            Console.ReadLine();
        }
    }
}
