using ClientConvertisseurV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ClientConvertisseurV2.Services
{
    public class WSService : IService
    {
        private HttpClient client;

        public WSService(string url)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url); //"http://localhost:7153/"
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Devise>> GetDevisesAsync(string nomControlleur)
        {
            try
            {
                return await client.GetFromJsonAsync<List<Devise>>(nomControlleur);
            }

            catch (Exception)
            {
                return null;
            }
        }
        public double CalculMontant(Devise devise, double montant)
        {
            if (devise == null)
                throw new ArgumentException("Veuillez indiquer un type de cours");
            
            if (montant <= 0)
                throw new ArgumentException("Veuillez indiquer un nombre d'heure > 0");

            return montant * devise.Taux;
        }

    }
}