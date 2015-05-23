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

        [Required(ErrorMessage = "Ne peut pas être vide"), Display(Name = "Nom d'utilisateur: "), StringLength(20)]
        public string NomUtilisateur { get; set; }

        [Required(ErrorMessage = "Ne peut pas être vide"), Display(Name = "Mot de passe :"), StringLength(20)]
        public string MotDePasse { get; set; }

        [Display(Name="Nom complet :"), StringLength(50)]
        public string NomComplet { get; set; }

        [Display(Name="Email :"), StringLength(100), RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage="L'email est syntaxiquement incorrect")]
        public string Email { get; set; }
    }
}