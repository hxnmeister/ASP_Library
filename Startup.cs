using ASP_Library.models;
using ASP_Library.route_constraits;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ASP_Library
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        private static readonly StringBuilder httpResponseBuilder = new StringBuilder();

        public Startup(IHostingEnvironment env)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath + "//config");
            builder.AddJsonFile("authors.json");
            builder.AddJsonFile("books.json");
            builder.AddJsonFile("genres.json");
            builder.AddJsonFile("publishers.json");
            builder.AddJsonFile("styles.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        private async Task MainPageDelegate(HttpContext context)
        {
            await context.Response.WriteAsync("<h1 style='text-align: center'>Hello, you are on main page!</h1>");
        }

        private async Task BooksPageDelegate(HttpContext context)
        {
            httpResponseBuilder.Clear();
            List<Book> books = Configuration.GetSection("books").Get<List<Book>>();

            httpResponseBuilder.AppendLine($"<div {Configuration["gridSettings"]}>");
            foreach (Book book in books)
            {
                httpResponseBuilder.AppendLine($"<div {Configuration["cardSettings"]}>");
                httpResponseBuilder.AppendLine($"<p>Title: {book.Title}</p>");
                httpResponseBuilder.AppendLine($"<p>Author: {book.Author.FirstName} {book.Author.LastName}</p>");
                httpResponseBuilder.AppendLine($"<p>Genre: {book.Genre.Name}</p>");
                httpResponseBuilder.AppendLine($"<p>Publisher: {book.Publisher.Name} ({book.Publisher.Country}, {book.Publisher.City})</p>");
                httpResponseBuilder.AppendLine("</div>");
            }
            httpResponseBuilder.AppendLine("</div>");

            await context.Response.WriteAsync(httpResponseBuilder.ToString());
        }

        private async Task GenresPageDelegate(HttpContext context)
        {
            httpResponseBuilder.Clear();

            List<Genre> genres = Configuration.GetSection("genres").Get<List<Genre>>();

            httpResponseBuilder.AppendLine($"<div style='max-width: 200px; margin: auto'>");
            httpResponseBuilder.AppendLine($"<p>Genres:</p>");
            foreach (Genre genre in genres)
            {
                httpResponseBuilder.AppendLine($"<p>{genre.Name};</p>");
            }
            httpResponseBuilder.AppendLine("</div>");

            await context.Response.WriteAsync(httpResponseBuilder.ToString());
        }

        private async Task AuthorsPageDelegate(HttpContext context)
        {
            httpResponseBuilder.Clear();

            var id = context.GetRouteValue("id")?.ToString();

            if(id == null)
            {
                List<Author> authors = Configuration.GetSection("authors").Get<List<Author>>();

                httpResponseBuilder.AppendLine("<h1 style='text-align: center'>Authors List</h1>");
                httpResponseBuilder.AppendLine($"<div {Configuration["gridSettings"]}>");
                foreach (Author author in authors)
                {
                    httpResponseBuilder.AppendLine($"<div {Configuration["cardSettings"]}>");
                    httpResponseBuilder.AppendLine($"<p>Name: <a href='/library/authors/{author.Id}'>{author.FirstName} {author.LastName}</a></p>");
                    httpResponseBuilder.AppendLine("</div>");
                }
                httpResponseBuilder.AppendLine("</div>");
            }
            else
            {
                int authorId = int.Parse(id);
                Author author = Configuration.GetSection("authors").Get<List<Author>>()[authorId - 1];

                httpResponseBuilder.AppendLine($"<h1>Author with id: {id}</h1>");
                httpResponseBuilder.AppendLine($"<p>Name: {author.FirstName} {author.LastName}</p>");
            }
            

            await context.Response.WriteAsync(httpResponseBuilder.ToString());
        }

        private async Task PublishersPageDelegate(HttpContext context)
        {
            httpResponseBuilder.Clear();

            List<Publisher> publishers = Configuration.GetSection("publishers").Get<List<Publisher>>();

            httpResponseBuilder.AppendLine("<h1 style='text-align: center'>Publishers:</h1>");
            httpResponseBuilder.AppendLine($"<div {Configuration["gridSettings"]}>");
            foreach (Publisher publisher in publishers)
            {
                httpResponseBuilder.AppendLine($"<div {Configuration["cardSettings"]}>");
                httpResponseBuilder.AppendLine($"<p>{publisher.Name};</p>");
                httpResponseBuilder.AppendLine($"<p>{publisher.Country};</p>");
                httpResponseBuilder.AppendLine($"<p>{publisher.City};</p>");
                httpResponseBuilder.AppendLine("</div>");
            }
            httpResponseBuilder.AppendLine("</div>");

            await context.Response.WriteAsync(httpResponseBuilder.ToString());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RouteHandler routeHandler = new RouteHandler(AuthorsPageDelegate);
            RouteBuilder routeBuilder = new RouteBuilder(app, routeHandler);

            routeBuilder.MapGet("library", MainPageDelegate);
            routeBuilder.MapGet("library/books", BooksPageDelegate);
            routeBuilder.MapGet("library/genres", GenresPageDelegate);
            routeBuilder.MapGet("library/publishers", PublishersPageDelegate);
            routeBuilder.MapRoute("authors", "library/authors/{id?}", null, new { id = new IdRangeConstraint(0, 9) });

            app.UseRouter(routeBuilder.Build());

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Page not found");
            });
        }
    }
}
