using System.Collections.Generic;
using System.Linq;

namespace AvailabilitySites.Data;

public static class DbInitializer
{
    public static void Initialize(AvailabilitySitesDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Sites.Any())
        {
            context.Sites.AddRange(new List<Site>
            {
                new Site
                {
                    Name = "yandex",
                    Url = "http://yandex.ru",
                    Interval = 60
                },
                new Site
                {
                    Name = "vk",
                    Url = "http://vk.ru",
                    Interval = 10
                },
                new Site
                {
                    Name = "wqwq",
                    Url = "http://wqwq.ru",
                    Interval = 15
                },
                new Site
                {
                    Name = "dffgdg",
                    Url = "http://dffgdg.ru",
                    Interval = 20
                },
                new Site
                {
                    Name = "google",
                    Url = "http://google.com",
                    Interval = 25
                }
            });

            context.SaveChanges();
        }
    }
}