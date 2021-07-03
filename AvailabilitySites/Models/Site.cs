using System.ComponentModel;

namespace AvailabilitySites.Models
{
    public delegate void PropertyChanged();

    public class Site
    {
        private bool isAvailable1;

        public Site(int id, string name, string url, int interval)
        {
            PrimaryKey = id;
            Name = name;
            Url = url;
            Interval = interval;
        }

        public int PrimaryKey { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Interval { get; set; }

        public bool IsAvailable { 
            get => isAvailable1;
            set
            {
                var temp = isAvailable1;
                isAvailable1 = value;

                if (temp != value)
                {
                    AvailableChanged?.Invoke();
                }
            }
        }

        public event PropertyChanged AvailableChanged;
    }
}
