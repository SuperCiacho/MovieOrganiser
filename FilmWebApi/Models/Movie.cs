// File created by Bartosz Nowak on 04/10/2014 14:18

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yorgi.FilmWebApi.Models
{
    public class Movie
    {
        public Movie(IApiHelper apiHelper)
        {
            this.padlock = new object();
            this.apiHelper = apiHelper;
        }

        #region Fields

        private readonly object padlock;
        private readonly IApiHelper apiHelper;

        ///<summary>
        /// Czy zostały pobrane informacje o filmie z użyciem metody zdalnej
        ///</summary> 
        private bool isFilmDataChecked;

        private string description;
        private List<Person> directors;
        private List<Person> screenWriters;
        private List<Person> music;
        private List<Person> picture;
        private List<Person> basedOn;
        private List<Person> actors;
        private List<Person> producers;
        private List<Person> montage;
        private List<Person> costumes;

        #endregion

        #region Properties

        /// <summary>
        /// ID w serwisie Filmweb
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tytuł oryginalny
        /// </summary>
        public string Title { get; set; }

        ///<summary>
        /// Tytuł polski
        ///</summary>
        public string PolishTitle { get; set; }

        ///<summary>
        /// Rok produkcji
        ///</summary>
        public int Year { get; set; }

        ///<summary>
        /// Adres okładki (pliku graficznego)
        ///</summary>
        public Uri CoverUrl { get; set; }

        ///<summary>
        /// Adres strony filmu
        ///</summary>
        public Uri FilmUrl { get; set; }

        ///<summary>
        /// Opis filmu
        ///</summary>
        public string Description
        {
            get { return this.description ?? (this.description = this.apiHelper.GetFilmDescription(this.Id).Result); }
            set { this.description = value; }
        }

        ///<summary>
        /// Średnia ocen filmu
        ///</summary>
        public double Rate { get; set; }

        ///<summary>
        /// Liczba głosów
        ///</summary>
        public long Votes { get; set; }

        ///<summary>
        /// Gatunek
        ///</summary>
        public string Genre { get; set; }

        ///<summary>
        /// Czas trwania
        ///</summary>
        public int Duration { get; set; }

        ///<summary>
        /// Kraj produkcji
        ///</summary>
        [JsonProperty]
        public string Countries { get; set; }

        ///<summary>
        /// Zarys fabuły
        ///</summary>
        public string Plot { get; set; }

        ///<summary> Lista reżyserów</summary>
        public List<Person> Directors => this.directors ?? (this.directors = this.GetFilmPersons(Profession.Director));

        ///<summary>
        /// Lista scenarzystów
        ///</summary>
        public List<Person> ScreenWriters => this.screenWriters ?? (this.screenWriters = this.GetFilmPersons(Profession.ScreenWriter));

        ///<summary>
        /// Lista osób odpowiedzialnych za muzykę
        ///</summary>
        public List<Person> Music => this.music ?? (this.music = this.GetFilmPersons(Profession.Music));

        ///<summary>
        /// Lista osób odpowiedzialnych za zdjęcia
        ///</summary>
        public List<Person> Picture => this.picture ?? (this.picture = this.GetFilmPersons(Profession.Cinematographer));

        ///<summary>
        /// Na podstawie
        ///</summary>
        public List<Person> BasedOn
            => this.basedOn ?? (this.basedOn = this.GetFilmPersons(Profession.OriginalMaterials));

        ///<summary>
        /// Lista aktorów
        ///</summary>
        public List<Person> Actors => this.actors ?? (this.actors = this.GetFilmPersons(Profession.Actor));

        ///<summary>
        /// Producenci
        ///</summary>
        public List<Person> Producers => this.producers ?? (this.producers = this.GetFilmPersons(Profession.Producer));

        ///<summary>
        /// Montaż
        ///</summary>
        public List<Person> Montage => this.montage ?? (this.montage = this.GetFilmPersons(Profession.Montage));

        ///<summary>
        /// Kostiumy
        ///</summary>
        public List<Person> Costumes
            => this.costumes ?? (this.costumes = this.GetFilmPersons(Profession.CostumeDesigner));

        #endregion

        #region Methods

        private List<Person> GetFilmPersons(Profession profession)
        {
            return this.apiHelper.GetFilmPersons(this.Id, profession).Result; // TODO: to trzeba ulepszyć
        }

        #endregion
    }
}