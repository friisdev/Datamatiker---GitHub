namespace GenspilSystem
{
    public class Request
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string GameName { get; set; }

        public override string ToString()
        {
            return $"{GameName} ønsket af {CustomerName} ({CustomerPhone})";
        }

        public string ToFileString()
        {
            return $"REQUEST;{CustomerName};{CustomerPhone};{GameName}";
        }

        public static Request FromFileString(string line)
        {
            var parts = line.Split(';');
            return new Request
            {
                CustomerName = parts[1],
                CustomerPhone = parts[2],
                GameName = parts[3]
            };
        }
    }
}
