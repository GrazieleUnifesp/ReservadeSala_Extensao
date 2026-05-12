using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Factories
{
    public class SalaFactory
    {
        public static Sala CriarSala(string tipo, int id, string nome)
        {
            switch (tipo.ToLower())
            {
                case "individual":
                    return new SalaEstudoIndividual { Id = id, Nome = nome };
                case "grupo":
                    return new SalaTrabalhoGrupo { Id = id, Nome = nome };
                case "laboratorio":
                    return new Laboratorio { Id = id, Nome = nome };
                default:
                    return null;
            }
        }
    }
}