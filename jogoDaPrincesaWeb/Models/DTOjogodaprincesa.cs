using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace jogoDaPrincesaWeb.Models
{
    [DataContract, Serializable]
    public class DTOjogodaprincesa
    {
        public DTOjogodaprincesa()
        {
            Narrador = "Olá principe, temos aqui 6 candidatas, porém somente uma é a real princesa, para descobrir qual é faça uma pergunta para cada uma e deduza por si mesmo";
            candidatos = new Candidatos[7];
        }

        [DataMember]
        public string Narrador { get; set; }

        [DataMember]
        public Candidatos[] candidatos { get; set; }
    }
}
