using System;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Strategies
{
    public class PrioridadeDocente : IPoliticaReserva
    {
        public bool Validar(Reserva nova, List<Reserva> existentes)
        {
            var conflito = existentes.FirstOrDefault(e => // FirsOrDeafault() percorre a lista de salas e retorna
            // O primeiro elemento com as características em ()
                e.Sala.Id == nova.Sala.Id && e.Horario == nova.Horario);

            if (conflito == null) return true;
            // Professor pode sobrescrever reserva de aluno
            return nova.Usuario.Tipo == TipoUsuario.PROFESSOR
                && conflito.Usuario.Tipo == TipoUsuario.ALUNO;
        }
    }    
}
