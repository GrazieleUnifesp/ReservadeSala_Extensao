using System;

namespace Study_Classes_Booking_System.src.Models
{
    public enum StatusReserva
    {
        PENDENTE,
        CONFIRMADA,
        CANCELADA,
        EXPIRADA
    }

    public class Reserva
    {
        public int Id { get; set; }
        public Sala Sala { get; set; } // Aqui usamos a classe mãe (Polimorfismo)
        public Usuario Usuario { get; set; }
        public DateTime Horario { get; set; }
        public int Duracao { get; set; }
        public StatusReserva Status { get; set; }

        public void Confirmar() => Status = StatusReserva.CONFIRMADA;
        public void Cancelar() => Status = StatusReserva.CANCELADA;
    }
}