using Study_Classes_Booking_System.src.Models;
namespace Study_Classes_Booking_System.src.Decorators
{
    public abstract class SalaDecorator : Sala
    {
        protected Sala _sala;

        protected SalaDecorator(Sala sala)
        {
            _sala = sala;
            this.Id = sala.Id;
            this.Nome = sala.Nome;
        }

        public override double PrecoBase => _sala.PrecoBase;
    }
}