using Study_Classes_Booking_System.src.Repositories;
using Study_Classes_Booking_System.src.Models;


namespace Study_Classes_Booking_System.src.Proxies
{
    public class ReservaRecorrenteProxy
    {
        private readonly ReservaRepositorySingleton _repo;
        private readonly ValidadorReserva _validador;

        public ReservaRecorrenteProxy(ValidadorReserva validador)
        {
            _repo = ReservaRepositorySingleton.GetInstance();
            _validador = validador;
        }

        public void CriarRecorrente(Reserva reservaOrigem, int semanas)
        {
            for (int i = 0; i < semanas; i++)
            {
                var nova = new Reserva
                {
                    Id = Guid.NewGuid().ToString(),
                    Sala = reservaOrigem.Sala,
                    Usuario = reservaOrigem.Usuario,
                    Horario = reservaOrigem.Horario.AddDays(7 * i),
                    Duracao = reservaOrigem.Duracao,
                    Status = StatusReserva.PENDENTE
                };

                if (_validador.Validar(nova))
                {
                    nova.Confirmar();
                    _repo.Adicionar(nova); // Observer dispara "ReservaCriada"
                }
                else
                {
                    // conflito — salva como PENDENTE e notifica
                    _repo.Adicionar(nova);
                    _repo.Notificador.Notify("ReservaPendente", nova);
                }
            }
        }
    }
}

    