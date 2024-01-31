using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WSSeries.Models.EntityFramework
{
    [Table("serie")]
    public partial class Serie
    {
        [Key]
        [Column("serieid")]
        public int Serieid { get; set; }
        [Column("titre")]
        [StringLength(100)]
        public string Titre { get; set; } = null!;
        [Column("resume")]
        public string? Resume { get; set; }
        [Column("nbsaisons")]
        public int? Nbsaisons { get; set; }
        [Column("nbepisodes")]
        public int? Nbepisodes { get; set; }
        [Column("anneecreation")]
        public int? Anneecreation { get; set; }
        [Column("network")]
        [StringLength(50)]
        public string? Network { get; set; }

        public Serie(int _serieId, string _titre, string _resume, int _nbsaisons, int nbepisodes, int anneecreation, string _network)
        {
            this.Serieid = _serieId;
            this.Titre = _titre;
            this.Resume = _resume;
            this.Nbsaisons = _nbsaisons;
            this.Nbepisodes = nbepisodes;
            this.Anneecreation = anneecreation;
            this.Network = _network;
        }

        public Serie():this(0, "", "", 0, 0, 0, "") {}

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
    }
}
