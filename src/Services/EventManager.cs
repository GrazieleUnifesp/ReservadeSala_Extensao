using System;
using Study_Classes_Booking_System.src.Observers;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Services
{
    public class EventManager
    {
        private readonly Dictionary<string, List<IReservaObserver>> _listeners = new();

        public void Subscribe(string evento, IReservaObserver observer)
        {
            if (!_listeners.ContainsKey(evento))
                _listeners[evento] = new List<IReservaObserver>();
            _listeners[evento].Add(observer);
        }

        public void Notify(string evento, Reserva reserva)
        {
            if (_listeners.TryGetValue(evento, out var lista))
                lista.ForEach(o => o.Update(evento, reserva));
        }
    }
}