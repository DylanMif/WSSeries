using Microsoft.EntityFrameworkCore;

namespace WSSeries.Models.EntityFramework
{
    public partial class Serie
    {
        public override bool Equals(object? obj)
        {
            return obj is Serie serie &&
                   Serieid == serie.Serieid &&
                   Titre == serie.Titre &&
                   Resume == serie.Resume &&
                   Nbsaisons == serie.Nbsaisons &&
                   Nbepisodes == serie.Nbepisodes &&
                   Anneecreation == serie.Anneecreation &&
                   Network == serie.Network;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Serieid, Titre, Resume, Nbsaisons, Nbepisodes, Anneecreation, Network);
        }

        public Serie(int _serieid, string _titre, string _resume, int nbSaison, int nbEpisode, int anneecreation, string network)
        {
            Serieid = _serieid;
            Titre = _titre;
            Resume = _resume;
            Nbsaisons = nbSaison;
            Nbepisodes = nbEpisode;
            Anneecreation = anneecreation;
            Network = network;
        }

        public Serie():this(1, "", "", 0, 0, 0, "") {}
    }
}
