namespace Study_Classes_Booking_System.src.Models
{
    public class SalaEstudoIndividual : Sala
    {
        public bool PossuiLocker { get; set; }

        public override string GetNome() => "Sala de Estudo Individual";
        public override int GetCapacidade() => Capacidade;
    }
}