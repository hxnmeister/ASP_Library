namespace ASP_Library.models.builders
{
    public class GenreBuilder
    {
        private readonly Genre _genre;

        public GenreBuilder()
        {
            _genre = new Genre();
        }

        public GenreBuilder WithId (long id)
        {
            _genre.Id = id;
            return this;
        }

        public GenreBuilder WithName (string name)
        {
            _genre.Name = name;
            return this;
        }

        public Genre Build()
        {
            return _genre;
        }
    }
}
