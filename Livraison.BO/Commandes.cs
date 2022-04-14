using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraison.BO
{
    public class Commandes
    {
        public string Reference { get; set; }
        public string Date_Livraison { get; set; }
        public double Lieu_Livraison { get; set; }


        public Commandes()
        {

        }

        public Commandes(string reference, string name, double price, string nom_client)
        {
            Reference = reference;
            Date_Livraison = name;
            Lieu_Livraison = price;
        }

        public Commandes(Commandes product) : this(product?.Reference, product?.Date_Livraison, product?.Lieu_Livraison ?? 0)
        {

        }

        public override bool Equals(object obj)
        {
            return obj is Commandes product &&
                   Reference == product.Reference;
        }

        public override int GetHashCode()
        {
            return -1304721846 + EqualityComparer<string>.Default.GetHashCode(Reference);
        }
    }
}

