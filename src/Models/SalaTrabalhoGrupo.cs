namespace Study_Classes_Booking_System.src.Models
{
    public class SalaTrabalhoGrupo : Sala
    {
        public int QtdCadeiras { get; set; }

        public override string GetNome() => "Sala de Trabalho em Grupo";
        public override int GetCapacidade() => Capacidade;
    }
}