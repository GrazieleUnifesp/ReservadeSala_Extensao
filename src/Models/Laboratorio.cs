using System.Collections.Generic;

namespace Study_Classes_Booking_System.src.Models
{
    public class Laboratorio : Sala
    {
        public List<string> SoftwaresInstalados { get; set; } = new List<string>();

        public override string GetNome() => "Laboratório de Informática";
        public override int GetCapacidade() => Capacidade;
    }
}