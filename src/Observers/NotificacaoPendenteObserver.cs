using System;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Observers
{
    public class NotificacaoPendenteObserver : IReservaObserver
    {
        public void Update(string evento, Reserva reserva)
        {
            if (evento == "ReservaPendente")
                Console.WriteLine(
                    $"[AVISO] Conflito detectado: reserva da sala " +
                    $"'{reserva.Sala.Nome}' em {reserva.Horario:dd/MM/yyyy HH:mm} " +
                    $"ficou PENDENTE para {reserva.Usuario.Nome}."
                );
        }
    }
}