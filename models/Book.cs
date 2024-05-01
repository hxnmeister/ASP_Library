namespace ASP_Library.models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public Publisher Publisher { get; set; }
    }
}
