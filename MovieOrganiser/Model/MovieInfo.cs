using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public struct MovieInfo : IEquatable<MovieInfo>
    {
        public string FilePath {get ; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public MovieType Type { get; set; }
        public string HD { get; set; }
        public TranslationTechnique TranslationTechinque { get; set; }

        public bool Equals(MovieInfo other)
        {
            return string.Equals(FilePath, other.FilePath) && string.Equals(Title, other.Title) && Year == other.Year && Type == other.Type && string.Equals(HD, other.HD) && TranslationTechinque == other.TranslationTechinque;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is MovieInfo && Equals((MovieInfo)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (FilePath != null ? FilePath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Type;
                hashCode = (hashCode * 397) ^ (HD != null ? HD.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)TranslationTechinque;
                return hashCode;
            }
        }
        public static bool operator ==(MovieInfo x, MovieInfo y)
        {
            return x.FilePath == y.FilePath && 
                x.Title == y.Title &&
                x.Year == y.Year &&
                x.Type == y.Type &&
                x.HD == y.HD &&
                x.TranslationTechinque == y.TranslationTechinque;
        }
        public static bool operator !=(MovieInfo x, MovieInfo y)
        {
            return !(x == y);
        }
    }
}
