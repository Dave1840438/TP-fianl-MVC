using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDImage.Models
{
    public class Dal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }


        public void AjouterUtilisateur(string nomUtilisateur, string motDePasse, string nomComplet, string email)
        {
            bdd.Utilisateurs.Add(new Utilisateur { NomUtilisateur = nomUtilisateur, MotDePasse = motDePasse, NomComplet = nomComplet, Email = email });            bdd.SaveChanges();
        }

        public List<Utilisateur> ListerUtilisateur()
        {
            return bdd.Utilisateurs.ToList();
        }

        public Utilisateur Authentifier(string username, string password)
        {
            return bdd.Utilisateurs.FirstOrDefault(u => u.NomUtilisateur == username && u.MotDePasse == password);
        }

        public void Dispose()
        {
            
            bdd.Dispose();
        }
    }
}