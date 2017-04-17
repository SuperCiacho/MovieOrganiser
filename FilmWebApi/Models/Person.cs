// File created by Bartosz Nowak on 04/10/2014 14:18

using System;

namespace Yorgi.FilmWebApi.Models
{
    public class Person
    {
        /// <summary>
        /// ID w serwisie Filmweb
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rola pełniona w filmie
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Dodatkowe informacje, np. "Niewymieniony w czołówce", "głos" etc.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Imię i nazwisko
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Adres zdjęcia
        /// </summary>
        public Uri PhotoUrl { get; set; }
    }
}