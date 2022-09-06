using jogoDaPrincesaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using System.Configuration;

namespace jogoDaPrincesaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DTOjogodaprincesa jogo = new DTOjogodaprincesa();
            jogo.Narrador = "Olá principe, temos aqui 6 candidatas, porém somente uma é a real princesa, para descobrir qual é faça uma pergunta para cada uma e deduza por si mesmo";
            
            return View("Index", jogo);
        }

        public string ObterTexto()
        {
            return "Olá principe, temos aqui 6 candidatas, porém somente uma é a real princesa, para descobrir qual é faça uma pergunta para cada uma e deduza por si mesmo";
        }

        public int[] PreencherTipos()
        {
            Random rnd = new Random();
            int sorteado = 0;
            int[] verificador = new int[7];
            int[] resultado = new int[7];

            for (int i = 0; i < 7; i++)
            {
            inicio:
                sorteado = rnd.Next(1, 8);
                for (int j = 0; j < 7; j++)
                {
                    if (verificador[j] == sorteado)
                    {
                        goto inicio;
                    }
                }

                resultado[i] = sorteado;
                verificador[i] = sorteado;

            }
            return resultado;
        }

        public Candidatos[] PreencherCandidatos()
        {
            Random random = new Random();
            int sorteado = 0;
            int[] verificador1 = new int[7];
            int[] tipos = new int[7];

            for (int i = 0; i < 7; i++)
            {
            inicio:
                sorteado = random.Next(1, 8);
                for (int j = 0; j < 7; j++)
                {
                    if (verificador1[j] == sorteado)
                    {
                        goto inicio;
                    }
                }

                tipos[i] = sorteado;
                verificador1[i] = sorteado;

            }

            candidato = new Candidatos() { };
            candidato1 = new Candidatos() { };
            candidato2 = new Candidatos() { };
            candidato3 = new Candidatos() { };
            candidato4 = new Candidatos() { };
            candidato5 = new Candidatos() { };
            candidato6 = new Candidatos() { };
            candidatos = new Candidatos[7] { candidato, candidato1, candidato2, candidato3, candidato4, candidato5, candidato6 };

            for (int i = 0; i < 7; i++)
            {
                candidatos[i].numero = i + 1;
                candidatos[i].nome = "Candidata " + (i + 1);
                candidatos[i].tipo = tipos[i];
                if (candidatos[i].tipo == 1 || candidatos[i].tipo == 2)
                {
                    candidatos[i].classe = "Verde";
                }
                else if (candidatos[i].tipo == 3 || candidatos[i].tipo == 4)
                {
                    candidatos[i].classe = "Azul";
                }
                else
                {
                    candidatos[i].classe = "Vermelho";
                }

                if (candidatos[i].classe == "Verde")
                {
                    candidatos[i].decisao = true;
                }
                else if (candidatos[i].classe == "Vermelho")
                {
                    candidatos[i].decisao = false;
                }
                else
                {
                    Random rnd = new Random();
                    int verificador = rnd.Next(1, 6);
                    if (verificador % 2 != 0)
                    {
                        candidatos[i].decisao = false;
                    }
                    else
                    {
                        candidatos[i].decisao = true;
                    }

                }

                switch (candidatos[i].tipo)
                {
                    case 1:
                        candidatos[i].nomeReal = "Larissa a Princesa";
                        break;

                    case 2:
                        candidatos[i].nomeReal = "a Fada madrinha";
                        break;

                    case 3:
                        candidatos[i].nomeReal = "Julio o Espião";
                        break;

                    case 4:
                        candidatos[i].nomeReal = "a Camponesa";
                        break;

                    case 5:
                        candidatos[i].nomeReal = "a rainha má";
                        break;
                    case 6:
                        candidatos[i].nomeReal = "Felix o gato";
                        break;
                    default:
                        candidatos[i].nomeReal = "Serjão o guarda";
                        break;
                }
                candidatos[i].pergunta1 = false;
                candidatos[i].pergunta2 = false;
            }
            return candidatos;
        }

        public Candidatos[] candidatos { get; set; }
        public Candidatos candidato { get; set; }
        public Candidatos candidato1 { get; set; }
        public Candidatos candidato2 { get; set; }
        public Candidatos candidato3 { get; set; }
        public Candidatos candidato4 { get; set; }
        public Candidatos candidato5 { get; set; }
        public Candidatos candidato6 { get; set; }


        public string pergunta1(string candidatoEscolhido, string listaCandidatos)
        {
            string resposta = "";
            var candidato = JsonConvert.DeserializeObject<Candidatos>(candidatoEscolhido);
            var candidatos = JsonConvert.DeserializeObject<Candidatos[]>(listaCandidatos);
            if(candidato != null && candidatos != null) 
            {
                var princesa = candidatos.Where(a => a.tipo == 1).First();
                var fada = candidatos.Where(a => a.tipo == 2).First();

                if (candidato.decisao)
                {
                    resposta = "Eu sou " + candidato.nomeReal;
                }
                else
                {
                Ini:
                    Random rnd = new Random();
                    int verificador = rnd.Next(1, 8);
                    if (verificador != candidato.numero)
                    {

                        if (candidato.tipo == 5)
                        {
                            int random = rnd.Next(1, 6);
                            if (random == 5)
                            {
                                foreach (var i in candidatos)
                                {
                                    if (verificador == i.numero)
                                    {
                                        resposta = "Eu sou " + i.nomeReal;
                                    }
                                }
                            }
                            else
                            {
                                resposta = "Eu sou " + princesa.nomeReal;
                            }
                        }
                        else if(candidato.classe == "Vermelho")
                        {
                            int random = rnd.Next(1, 6);
                            if (random == 5)
                            {
                                foreach (var i in candidatos)
                                {
                                    if (verificador == i.numero)
                                    {
                                        resposta = "Eu sou " + i.nomeReal;
                                    }
                                }
                            }
                            else
                            {
                                resposta = "Eu sou " + fada.nomeReal;
                            }
                        }
                        else
                        {
                            foreach (var i in candidatos)
                            {
                                if (verificador == i.numero)
                                {
                                    resposta = "Eu sou " + i.nomeReal;
                                }
                            }
                        }
                    }
                    else
                    {
                        goto Ini;
                    }
                }
            }
            
            return resposta;
        }

        public string pergunta2(string candidatoEscolhido, string listaCandidatos, string candidatoPerguntado)
        {
            string resposta = "";
            var candidato = JsonConvert.DeserializeObject<Candidatos>(candidatoEscolhido);
            var candidatos = JsonConvert.DeserializeObject<Candidatos[]>(listaCandidatos);
            var candidatoP = JsonConvert.DeserializeObject<string>(candidatoPerguntado);

            if (candidato != null && candidatos != null)
            {
                var fada = candidatos.Where(a => a.tipo == 2).First();
                var guarda = candidatos.Where(a => a.tipo == 7).First();
                var princesa = candidatos.Where(a => a.tipo == 1).First();
                int escolha = candidatos.Where(a => a.nome == candidatoP).First().numero;

                if (candidato.decisao)
                {
                    resposta = "A " + candidatos[escolha - 1].nome + " é " + candidatos[escolha - 1].nomeReal;
                }
                else
                {
                Ini:
                    Random rnd = new Random();
                    int verificador = rnd.Next(1, 8);
                    if (verificador != candidatos[escolha - 1].numero)
                    {
                        foreach (var i in candidatos)
                        {
                            if (verificador == i.numero)
                            {
                                resposta = "A " + candidatos[escolha - 1].nome + " é " + i.nomeReal;
                            }
                        }

                        if (candidato.tipo == 5)
                        {
                            int random = rnd.Next(1, 6);
                            if (random == 5)
                            {
                                foreach (var i in candidatos)
                                {
                                    if (verificador == i.numero)
                                    {
                                        resposta = "A " + candidatos[escolha - 1].nome + " é " + i.nomeReal;
                                    }
                                }
                            }
                            else
                            {
                                if (candidatos[escolha - 1].classe == "Vermelho")
                                {
                                    resposta = "A " + candidatos[escolha - 1].nome + " é " + fada.nomeReal;
                                }
                                else
                                {
                                    resposta = "A " + candidatos[escolha - 1].nome + " é " + guarda.nomeReal;
                                }
                            }
                        }
                        else if (candidato.classe == "Vermelho")
                        {
                            int random = rnd.Next(1, 6);
                            if (random == 5)
                            {
                                foreach (var i in candidatos)
                                {
                                    if (verificador == i.numero)
                                    {
                                        resposta = "A " + candidatos[escolha - 1].nome + " é " + i.nomeReal;
                                    }
                                }
                            }
                            else
                            {
                                if (candidatos[escolha - 1].tipo == 5)
                                {
                                    resposta = "A " + candidatos[escolha - 1].nome + " é " + princesa.nomeReal;
                                }
                                else if (candidatos[escolha - 1].classe == "Vermelho")
                                {
                                    resposta = "A " + candidatos[escolha - 1].nome + " é " + fada.nomeReal;
                                }
                                else
                                {
                                    resposta = "A " + candidatos[escolha - 1].nome + " é " + guarda.nomeReal;
                                }
                            }
                        }
                        else
                        {
                            foreach (var i in candidatos)
                            {
                                if (verificador == i.numero)
                                {
                                    resposta = "A " + candidatos[escolha - 1].nome + " é " + i.nomeReal;
                                }
                            }
                        }
                    }
                    else
                    {
                        goto Ini;
                    }
                }
            }
            return resposta;
        }

        public string pergunta3(string candidatoEscolhido, string listaCandidatos)
        {
            string resposta = "";
            var candidato = JsonConvert.DeserializeObject<Candidatos>(candidatoEscolhido);
            var candidatos = JsonConvert.DeserializeObject<Candidatos[]>(listaCandidatos);
            if (candidato != null && candidatos != null)
            {
                var rainha = candidatos.Where(a => a.tipo == 5).First();
                Candidatos princesa = new Candidatos();
                foreach (var i in candidatos)
                {
                    if (i.tipo == 1)
                    {
                        princesa = i;
                    }
                }
                if (candidato.decisao)
                {
                    resposta = "A princesa é a " + princesa.nome;
                }
                else
                {
                Ini:
                    Random rnd = new Random();
                    int verificador = rnd.Next(1, 8);
                    if (verificador != princesa.numero)
                    {
                        if (candidato.tipo == 5)
                        {
                            int random = rnd.Next(1, 6);
                            if (random == 5)
                            {
                                foreach (var i in candidatos)
                                {
                                    if (verificador == i.numero)
                                    {
                                        resposta = "A princesa é a " + i.nome;
                                    }
                                }
                            }
                            else
                            {
                                resposta = "A princesa é a " + candidato.nome;
                            }
                        }
                        else if (candidato.classe == "Vermelho")
                        {
                            int random = rnd.Next(1, 6);
                            if (random == 5)
                            {
                                foreach (var i in candidatos)
                                {
                                    if (verificador == i.numero)
                                    {
                                        resposta = "A princesa é a " + i.nome;
                                    }
                                }
                            }
                            else
                            {
                                resposta = "A princesa é a " + rainha.nome;
                            }
                        }
                        else
                        {
                            foreach (var i in candidatos)
                            {
                                if (verificador == i.numero)
                                {
                                    resposta = "A princesa é a " + i.nome;
                                }
                            }
                        }
                    }
                    else
                    {
                        goto Ini;
                    }
                }
            }
            return resposta;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}