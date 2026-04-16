using System;
using System.Collections.Generic;
using System.IO;

namespace GenspilSystem
{
    public class DataHandler
    {
        public string FilePath { get; set; }

        public DataHandler(string filePath)
        {
            FilePath = filePath;
        }

        public void SaveAll(List<BoardGame> games, List<Request> requests)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                foreach (var game in games)
                    sw.WriteLine(game.ToFileString());

                foreach (var request in requests)
                    sw.WriteLine(request.ToFileString());
            }

            Console.WriteLine($"Data gemt i {FilePath}");
        }

        public void LoadAll(List<BoardGame> games, List<Request> requests)
        {
            games.Clear();
            requests.Clear();

            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Ingen fil fundet — starter med tomme lister.");
                return;
            }

            using (StreamReader sr = new StreamReader(FilePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("GAME;"))
                        games.Add(BoardGame.FromFileString(line));

                    else if (line.StartsWith("REQUEST;"))
                        requests.Add(Request.FromFileString(line));
                }
            }

            Console.WriteLine($"Indlæst: {games.Count} spil, {requests.Count} forespørgsler.");
        }
    }
}
