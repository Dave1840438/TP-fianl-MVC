using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumDImage.Models
{
    [Table("Utilisateurs")]
    public class Utilisateur
    {
        public int Id { get; set; }

        [Required, Display(Name="Nom d'utilisateur: "), StringLength(20)]
        public string NomUtilisateur { get; set; }

        [Required, Display(Name="Mot de passe :"), StringLength(20)]
        public string MotDePasse { get; set; }

        [Required, Display(Name="Nom complet :"), StringLength(50)]
        public string NomComplet { get; set; }

        [Required, Display(Name="Email :"), StringLength(100)]
        public string Email { get; set; }
    }
}