namespace ASP_Library.models.builders
{
    public class BookBuilder
    {
        private readonly Book _book;

        public BookBuilder()
        {
            _book = new Book();
        }

        public BookBuilder WithId (long id)
        {
            _book.Id = id;
            return this;
        }

        public BookBuilder WithTitle (string title)
        {
            _book.Title = title;
            return this;
        }

        public BookBuilder WithAuthor (Author author)
        {
            _book.Author = author;
            return this;
        }

        public BookBuilder WithGenre (Genre genre) 
        { 
            _book.Genre = genre; 
            return this; 
        }

        public BookBuilder WithPublisher (Publisher publisher)
        {
            _book.Publisher = publisher;
            return this;
        }

        public Book Build()
        {
            return _book;
        }
    }
}
