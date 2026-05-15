namespace Study_Classes_Booking_System.src.Models
{
    public enum TipoUsuario
    {
        ALUNO,
        PROFESSOR,
        TECNICO
    }

    public class Usuario
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}