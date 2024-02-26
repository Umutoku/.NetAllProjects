namespace WebApp.Composite.Models
{
    public class Category
    {
        //id    name    userid  books referenceid
        // 1  "Fiction"  "user1"  null  0 
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
        public ICollection<Book> Books { get; set; }
        public int ReferenceId { get; set; }


    }
}
