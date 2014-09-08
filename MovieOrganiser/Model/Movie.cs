using MovieOrganiser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieOrganiser.Model
{
    public class Movie
    {
        #region Fields

        /// <summary>
        /// Referencja do FilWebApi
        /// </summary>
        protected ApiHelper ApiHelper;

        ///<summary>
        /// Czy zostały pobrane informacje o filmie z użyciem metody zdalnej
        ///</summary>
        protected bool IsFilmDataChecked;

        private string title;
        private string polishTitle;
        private int year;
        private Uri coverUri;
        private string description;
        private double rate;
        private long votes;
        private string genre;
        private int duration;
        private string countries;
        private string plot;
        private List<Person> directors;
        private List<Person> screenWriters;
        private List<Person> music;
        private List<Person> picture;
        private List<Person> basedOn;
        private List<Person> actors;
        private List<Person> producers;
        private List<Person> montage;
        private List<Person> costumes;
        protected bool isFilmDataChecked = false;
        #endregion

        #region Constructors

        public Movie(FilmWebApi api)
        {
            this.ApiHelper = api.ApiHelper;
        }

        public Movie() { }

        #endregion

        #region Properties

        /// <summary>
        /// ID w serwisie Filmweb
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tytuł oryginalny
        /// </summary>
        [JsonProperty]
        public string Title
        {
            get
            {
                SetFilmData();
                return title;
            }
            set { title = value; }

        }

        ///<summary>
        /// Tytuł polski
        ///</summary>
        public string PolishTitle
        {
            get
            {
                SetFilmData();
                return polishTitle;
            }
            set { polishTitle = value; }
        }

        ///<summary>
        /// Rok produkcji
        ///</summary>
        public int Year
        {
            get
            {
                SetFilmData();
                return year;
            }
            set { year = value; }
        }

        ///<summary>
        /// Adres okładki (pliku graficznego)
        ///</summary>
        public Uri CoverUrl
        {
            get
            {
                SetFilmData();
                return coverUri;
            }
            set { coverUri = value; }
        }

        ///<summary>
        /// Adres strony filmu
        ///</summary>
        public Uri FilmUrl { get; set; }

        ///<summary>
        /// Opis filmu
        ///</summary>
        public string Description
        {
            get { return description ?? (description = ApiHelper.GetFilmDescription(this.Id)); }
            private set { description = value; }
        }

        ///<summary>
        /// Średnia ocen filmu
        ///</summary>
        public double Rate
        {
            get
            {
                SetFilmData();
                return rate;
            }
            set { rate = value; }
        }

        ///<summary>
        /// Liczba głosów
        ///</summary>
        public long Votes
        {
            get
            {
                SetFilmData();
                return votes;
            }
            set { votes = value; }
        }

        ///<summary>
        /// Gatunek
        ///</summary>
        public string Genre
        {
            get
            {
                SetFilmData();
                return genre;
            }
            set { genre = value; }
        }

        ///<summary>
        /// Czas trwania
        ///</summary>
        public int Duration
        {
            get
            {
                SetFilmData();
                return duration;
            }
            set { duration = value; }
        }

        ///<summary>
        /// Kraj produkcji
        ///</summary>
        [JsonProperty]
        public string Countries
        {
            get
            {
                SetFilmData();
                return countries;
            }
            set { countries = value; }
        }

        ///<summary>
        /// Zarys fabuły
        ///</summary>
        public string Plot
        {
            get
            {
                SetFilmData();
                return plot;
            }
            set { plot = value; }
        }

        ///<summary> Lista reżyserów</summary>
        public List<Person> Directors
        {
            get
            {
                return directors ?? (directors = ApiHelper.GetFilmPersons(this.Id, Profession.Director));
            }
            set { directors = value; }
        }

        ///<summary>
        /// Lista scenarzystów
        ///</summary>
        public List<Person> ScreenWriters
        {
            get
            {
                return screenWriters ?? (screenWriters = ApiHelper.GetFilmPersons(this.Id, Profession.ScreenWriter));
            }
            set { screenWriters = value; }

        }

        ///<summary>
        /// Lista osób odpowiedzialnych za muzykę
        ///</summary>
        public List<Person> Music
        {
            get { return music ?? (music = ApiHelper.GetFilmPersons(this.Id, Profession.Music)); }
        }

        ///<summary>
        /// Lista osób odpowiedzialnych za zdjęcia
        ///</summary>
        public List<Person> Picture
        {
            get
            {
                return picture ?? (picture = ApiHelper.GetFilmPersons(this.Id, Profession.Cinematographer));
            }
        }

        ///<summary>
        /// Na podstawie
        ///</summary>
        public List<Person> BasedOn
        {
            get
            {
                return basedOn ?? (basedOn = ApiHelper.GetFilmPersons(this.Id, Profession.OriginalMaterials));
            }
        }

        ///<summary>
        /// Lista aktorów
        ///</summary>
        public List<Person> Actors
        {
            get
            {
                return actors ?? (actors = ApiHelper.GetFilmPersons(this.Id, Profession.Actor));
            }
        }

        ///<summary>
        /// Producenci
        ///</summary>
        public List<Person> Producers
        {
            get
            {
                return producers ?? (producers = ApiHelper.GetFilmPersons(this.Id, Profession.Producer));
            }
        }

        ///<summary>
        /// Montaż
        ///</summary>
        public List<Person> Montage
        {
            get
            {
                return montage ?? (montage = ApiHelper.GetFilmPersons(this.Id, Profession.Montage));
            }
        }

        ///<summary>
        /// Kostiumy
        ///</summary>
        public List<Person> Costumes
        {
            get
            {
                return costumes ?? (costumes = ApiHelper.GetFilmPersons(this.Id, Profession.CostumeDesigner));
            }
        }

        #endregion

        #region Methods

        protected void SetFilmData()
        {
            if (ApiHelper != null && !IsFilmDataChecked)
            {
                ApiHelper.GetMovieInfo(this);
                IsFilmDataChecked = true;
            }
        }

        #endregion

    }
}
