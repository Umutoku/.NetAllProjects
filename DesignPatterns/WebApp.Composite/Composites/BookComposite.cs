using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace WebApp.Composite.Composites
{
    public class BookComposite : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private List<IComponent> _components; // IComponent türünde bir liste oluşturuldu

        public IReadOnlyCollection<IComponent> Components => _components; // IComponent türündeki elemanlar listelenebilir


        public BookComposite(int id, string name)
        {
            Id = id;
            Name = name;
            _components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            if (_components == null)
            {
                _components = new List<IComponent>();
            }
            _components.Add(component); // IComponent türündeki elemanlar listeye eklendi
        }

        public void RemoveComponent(IComponent component)
        {
            if (_components != null)
            {
                _components.Remove(component); // IComponent türündeki elemanlar listeden silindi
            }
        }



        public int Count()
        {
            return _components.Sum(c => c.Count()); // IComponent türündeki elemanların sayısı döndürüldü
        }

        public string Display()
        {
            var sb = new StringBuilder();
            sb.Append($"<div class='text-primary my-1'><a href='#' class='menu'>   {Name} ({Count()}) </a></div>"); // sayesinde kitaplar listelenebilir

            if (!_components.Any()) return sb.ToString(); // Eğer alt kategoriler yoksa

            sb.Append("<ul class='list-group list-group-flush ms-3'>"); 

            foreach (var item in _components)
            {
                sb.Append(item.Display());
            }

            sb.Append("</ul>");

            return sb.ToString();
        }
        // SelectListItem sayesinde kitaplar listelenebilir 
        public List<SelectListItem> GetSelectListItems(string line)
        {
            var list = new List<SelectListItem> { new($"{line}{Name}", Id.ToString()) }; 

            if (_components.Any(x => x is BookComposite)) // Eğer alt kategoriler varsa
            { line += " - "; } // alt kategorilerin başına - işareti eklenir

            _components.ForEach(x =>
            {
                if (x is BookComposite bookComposite)
                {
                    list.AddRange(bookComposite.GetSelectListItems(line));
                }
            });
            return list;
        }
    }
}
