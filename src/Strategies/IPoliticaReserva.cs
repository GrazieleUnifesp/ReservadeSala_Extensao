using System;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Strategies
{
    public interface IPoliticaReserva //Interface da Estratégia
    {
        bool Validar(Reserva nova, List<Reserva> existentes); // Forma de validar diferentes tipos de reserva
    }
}