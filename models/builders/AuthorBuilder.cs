namespace ASP_Library.models.builders
{
    public class AuthorBuilder
    {
        private readonly Author _author;

        public AuthorBuilder()
        {
            this._author = new Author();
        }

        public AuthorBuilder WithId(long id)
        {
            this._author.Id = id;
            return this;
        }

        public AuthorBuilder WithFirstName(string firstName)
        {
            this._author.FirstName = firstName;
            return this;
        }

        public AuthorBuilder WithLastName(string lastName)
        {
            this._author.LastName = lastName;
            return this;
        }

        public Author Build()
        {
            return this._author;
        }
    }
}
