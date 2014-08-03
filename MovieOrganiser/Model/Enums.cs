using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public enum MovieInfoLevel
    {
        PolishTitle,//= 0,
        OriginalTitle,//= 1,
        Rate,//= 2,
        Votes,//= 3,
        Genre,//= 4,
        Year,//= 5,
        Duration,//= 6,
        NumberOfComments,//= 7,
        ForumAddress,//= 8,
        WhethesHasPlotOutline,//= 9,
        WhetherHasMovieDescription,//= 10,
        CoverUrl,//= 11,
        Trailer,//= 12,
        WorldReleaseDate,//= 13,
        PolandReleaseDate,//= 14,
        SOMETHING_SERIES_RELATED,//= 15,
        NumberOfSeasons,//= 16,
        NumberOfEpisodes,//= 17,
        Country,//= 18,
        Plot                         //= 19
    }

    public enum Profession
    {
        Director = 1,
        ScreenWriter = 2,
        Music = 3,
        Cinematographer = 4,
        OriginalMaterials = 5,
        Actor = 6,
        Producer = 9,
        Montage = 10,
        CostumeDesigner = 11
    }

    public enum MovieType
    {
        Both,
        Movie,
        Series
    }

    public enum TranslationTechnique
    {
        Subtitles,
        VoiceOver,
        Dubbing,
        Polish, 
        MissingSubtitles
    }

}
