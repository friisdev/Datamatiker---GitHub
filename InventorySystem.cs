using System;
using System.Collections.Generic;
using System.Linq;

namespace GenspilSystem
{
    public class InventorySystem
    {
        private readonly List<BoardGame> games = new List<BoardGame>();
        private readonly List<Request> requests = new List<Request>();
        private readonly DataHandler dataHandler;

        public List<BoardGame> Games => games;
        public List<Request> Requests => requests;

        public InventorySystem()
        {
            dataHandler = new DataHandler("genspil_data.txt");
            dataHandler.LoadAll(games, requests);
        }

        public void AddGame(BoardGame game)
        {
            games.Add(game);
            dataHandler.SaveAll(games, requests);
        }

        public void AddRequest(Request request)
        {
            requests.Add(request);
            dataHandler.SaveAll(games, requests);
        }

        public void ShowAllGames()
        {
            if (games.Count == 0)
            {
                Console.WriteLine("Ingen spil på lager.");
                return;
            }

            foreach (var game in games)
                Console.WriteLine(game);
        }

        public void ShowRequests()
        {
            if (requests.Count == 0)
            {
                Console.WriteLine("Ingen forespørgsler.");
                return;
            }

            foreach (var request in requests)
                Console.WriteLine(request);
        }

        public List<BoardGame> SearchGames(
            string name = null,
            string genre = null,
            int? players = null,
            string condition = null,
            decimal? maxPrice = null)
        {
            var result = games.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                result = result.Where(g => g.Name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrEmpty(genre))
                result = result.Where(g => g.Genre.ToLower() == genre.ToLower());

            if (players.HasValue)
                result = result.Where(g => g.NumberOfPlayers == players.Value);

            if (!string.IsNullOrEmpty(condition))
                result = result.Where(g => g.Condition.ToLower() == condition.ToLower());

            if (maxPrice.HasValue)
                result = result.Where(g => g.Price <= maxPrice.Value);

            return result.ToList();
        }

        public void PrintSortedByName()
        {
            foreach (var game in games.OrderBy(g => g.Name))
                Console.WriteLine(game);
        }

        public void PrintSortedByGenre()
        {
            foreach (var game in games.OrderBy(g => g.Genre))
                Console.WriteLine(game);
        }
    }
}
