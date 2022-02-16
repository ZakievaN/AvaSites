namespace AvailabilitySites.Data
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Interval { get; set; }

        public bool IsAvailable { get; set; }
    }
}