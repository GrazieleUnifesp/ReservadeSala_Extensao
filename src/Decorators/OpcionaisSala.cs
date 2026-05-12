using System;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Decorators
{
    public class ArCondicionadoDecorator : SalaDecorator
    {
        public ArCondicionadoDecorator(Sala sala) : base(sala)
        {
            this.Nome = $"{sala.Nome} + Ar Condicionado";
        }

        public override double PrecoBase => _sala.PrecoBase + 15.0;
        public override string GetNome() => _sala.GetNome();
        public override int GetCapacidade() => _sala.GetCapacidade();
    }

    public class BebedouroDecorator : SalaDecorator
    {
        public BebedouroDecorator(Sala sala) : base(sala)
        {
            this.Nome = $"{sala.Nome} + Bebedouro";
        }

        public override double PrecoBase => _sala.PrecoBase + 10.0;

        public override string GetNome() => _sala.GetNome();
        public override int GetCapacidade() => _sala.GetCapacidade();
    }
}