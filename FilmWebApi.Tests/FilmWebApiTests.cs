using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Yorgi.FilmWebApi.Models;

namespace FilmWebApi.Tests
{
    [TestFixture]
    public class FilmWebApiTests
    {
        private Yorgi.FilmWebApi.FilmWebApi api;

        [SetUp]
        public void SetUp()
        {
            this.api = Yorgi.FilmWebApi.FilmWebApi.CreateInstance();
        }

        [Test]
        public void Test1()
        {
            var a = this.api.GetMovieList("test", null);
        }

        [Test]
        public async Task GetDescription()
        {
            const string url = @"https://ssl.filmweb.pl/api?methods=getFilmDescription%20[664]\n&signature=1.0,1b800b9a0716e482236c550b17eb2e56&version=1.0&appId=android";

            var movies = await this.api.GetMovieList("Jerry Maguire ", 1996);
            var movie = movies[0];

            movie.Description = null;
            var desc = movie.Description;

            using (var httpClient = new HttpClient())
            {
                var a = httpClient.GetStringAsync(new Uri(url)).ConfigureAwait(false).GetAwaiter().GetResult();
                var a1 = httpClient.GetStringAsync(new Uri(url)).Result;

            }
        }
    }
}