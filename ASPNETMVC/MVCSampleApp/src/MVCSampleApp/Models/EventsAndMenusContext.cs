using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.Models
{
    public class EventsAndMenusContext
    {
        private IEnumerable<Event> events = null;
        public IEnumerable<Event> Events =>
            events ?? (events = new List<Event>()
            {
                new Event
                {
                    Id =1,
                    Text ="Formula 1 G.P. Australia, Melbourne",
                    Day=new DateTime(2016, 4, 3)
                },
                new Event
                {
                    Id =2,
                    Text ="Formula 1 G.P. China, Shanghai",
                    Day = new DateTime(2016, 4, 10)
                },
                new Event
                {
                    Id =3,
                    Text ="Formula 1 G.P. Bahrain, Sakhir",
                    Day = new DateTime(2016, 4, 24)
                },
                new Event
                {
                    Id = 4,
                    Text ="Formula 1 G.P. Russia, Socchi",
                    Day = new DateTime(2016, 5, 1)
                }
            });

        private List<Menu> menus = null;
        public IEnumerable<Menu> Menus =>
            menus ?? (menus = new List<Menu>()
            {
                    new Menu
                    {
                        Id =1,
                        Text ="Baby Back Barbecue Ribs",
                        Price =16.9,
                        Category="Main"
                    },
                    new Menu
                    {
                        Id =2,
                        Text ="Chicken and Brown Rice Piaf",
                        Price =12.9,
                        Category="Main"
                    },
                    new Menu
                    {
                        Id =3,
                        Text ="Chicken Miso Soup with Shiitake Mushrooms",
                        Price=6.9,
                        Category ="Soup"
                    }
            });
    }

}
