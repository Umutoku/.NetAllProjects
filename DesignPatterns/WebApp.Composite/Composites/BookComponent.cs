﻿namespace WebApp.Composite.Composites
{
    public class BookComponent : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BookComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Count()
        {
            return 1; // En alttaki eleman olduğu için 1 döndürüldü
        }

        public string Display()
        {
            return $"<li class='list-group-item'>{Name}</li>"; // sayesinde kitaplar listelenebilir
        }
    }
}
