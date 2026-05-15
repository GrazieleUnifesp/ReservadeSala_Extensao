using System;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Strategies
{
    public class PrimeiroAChegar : IPoliticaReserva
    {
        public bool Validar(Reserva nova, List<Reserva> existentes) =>
            !existentes.Any(e => //Any() percorre a lista e pergunta "existe alguma reserva que satisfaça todas essas condições?"
            // Se sim retorna false
                e.Sala.Id == nova.Sala.Id &&
                e.Horario == nova.Horario &&
                e.Status != StatusReserva.CANCELADA); //Estratégia para validar quem chega primeiro
    }    
}
