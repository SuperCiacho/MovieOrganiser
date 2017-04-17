// File created by Bartosz Nowak on 04/07/2014 00:00

using System.ComponentModel;

namespace MovieOrganiser.Model
{
    public enum TranslationTechnique
    {
        [Description("Napisy")]
        Subtitles,
        [Description("Lektor")]
        VoiceOver,
        [Description("Dubbing")]
        Dubbing,
        [Description("PL")]
        Polish,
        [Description("Brakujące napisy")]
        MissingSubtitles
    }
}