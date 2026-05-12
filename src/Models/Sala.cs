namespace Study_Classes_Booking_System.src.Models
{
    public abstract class Sala
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public string Localizacao { get; set; }

        public abstract string GetNome();
        public abstract int GetCapacidade();
        public virtual double PrecoBase { get; set; } = 0.0;
    }
}