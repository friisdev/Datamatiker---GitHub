namespace GenspilSystem
{
    public class BoardGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int NumberOfPlayers { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        public override string ToString()
        {
            return $"{Name} | Genre: {Genre} | Spillere: {NumberOfPlayers} | Stand: {Condition} | Pris: {Price} kr | Lager: {(InStock ? "Ja" : "Nej")}";
        }

        public string ToFileString()
        {
            return $"GAME;{Id};{Name};{Genre};{NumberOfPlayers};{Condition};{Price};{InStock}";
        }

        public static BoardGame FromFileString(string line)
        {
            var parts = line.Split(';');
            return new BoardGame
            {
                Id = int.Parse(parts[1]),
                Name = parts[2],
                Genre = parts[3],
                NumberOfPlayers = int.Parse(parts[4]),
                Condition = parts[5],
                Price = decimal.Parse(parts[6]),
                InStock = bool.Parse(parts[7])
            };
        }
    }
}
