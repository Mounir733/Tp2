using System;
using System.ComponentModel.DataAnnotations;

namespace ClientConvertisseurV1.Models
{
    public class Devise
    {
        private int id;
        private string? nomdevise;
        private double taux;

        public Devise()
        {
        }

        public Devise(int id, string? nomdevise, double taux)
        {
            this.Id = id;
            this.NomDevise = nomdevise;
            this.Taux = taux;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [Required]
        public string? NomDevise
        {
            get
            {
                return nomdevise;
            }

            set
            {
                nomdevise = value;
            }
        }

        public double Taux
        {
            get
            {
                return taux;
            }

            set
            {
                taux = value;
            }
        }

        
    }
}
