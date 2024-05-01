namespace ASP_Library.models.builders
{
    public class PublisherBuilder
    {
        private readonly Publisher _publisher;

        public PublisherBuilder()
        {
            _publisher = new Publisher();
        }

        public PublisherBuilder WithId (long id)
        {
            _publisher.Id = id;
            return this;
        }

        public PublisherBuilder WithName (string name)
        {
            _publisher.Name = name;
            return this;
        }

        public PublisherBuilder WithCountry (string country)
        {
            _publisher.Country = country;
            return this;
        }

        public PublisherBuilder WithCity (string city)
        {
            _publisher.City = city;
            return this;
        }

        public Publisher Build ()
        {
            return _publisher;
        }
    }
}
