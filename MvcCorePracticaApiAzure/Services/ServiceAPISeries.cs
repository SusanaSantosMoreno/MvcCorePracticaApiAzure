using ApiPracticaAzure.Models;
using MvcCore.Models;
using MvcCorePracticaApiAzure.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcCorePracticaApiAzure.Services {
    public class ServiceAPISeries {

        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceAPISeries(String url) {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<String> GetToken(String userName, String password) {
            using(HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                UsuariosAzure usuario = new UsuariosAzure(0, "", "", userName, password);
                String json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                String request = "Auth/Login";
                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode) {
                    String data = await response.Content.ReadAsStringAsync();
                    JObject jobject = JObject.Parse(data);
                    String token = jobject.GetValue("response").ToString();
                    return token;
                } else {
                    return null;
                }
            }
        }

        private async Task<T> CallApi<T> (String request) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode) {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                } else {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApi<T> (String request, String token) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode) {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                } else {
                    return default(T);
                }
            }
        }

        public async Task<UsuariosAzure> GetUsuario (String token) {
            String request = "Auth/PerfilUsuario";
            UsuariosAzure usuario = await this.CallApi<UsuariosAzure>(request, token);
            return usuario;
        }

        public async Task<List<Serie>> GetSeriesAsync () {
            String request = "api/getSeries";
            List<Serie> series = await this.CallApi<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesAsync () {
            String request = "api/getPersonajes";
            List<Personaje> personajes = await this.CallApi<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Serie> GetSerieAsync (int idSerie) {
            String request = "api/getSerie/" + idSerie;
            Serie serie = await this.CallApi<Serie>(request);
            return serie;
        }

        public async Task<Personaje> GetPersonajeAsync (int idPersonaje) {
            String request = "api/getPersonaje/" + idPersonaje;
            Personaje personaje = await this.CallApi<Personaje>(request);
            return personaje;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync (int idSerie) {
            String request = "api/getPersonajesSerie/" + idSerie;
            List<Personaje> personajes = await this.CallApi<List<Personaje>>(request);
            return personajes;
        }

        public async Task InsertarPersonajeAsync (int idPersonaje, String nombre,
            String imagen, int idSerie) {
            using(HttpClient client = new HttpClient()) {
                String request = "api/insertarPersonaje";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje(idPersonaje, nombre, imagen, idSerie);
                String json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(request, content);
            }
        }

        
        public async Task CambiarPersonajeSerieAsync (int idPersonaje, int idSerie) {
            using (HttpClient client = new HttpClient()) {
                String request = "api/CambiarPersonajeSerie/" + idPersonaje + "/" + idSerie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                await client.PutAsync(request, null);
            }
        }
    }
}
