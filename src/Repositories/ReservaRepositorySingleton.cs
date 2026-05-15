using System;
using System.Collections.Generic;
using System.Linq;
using Study_Classes_Booking_System.src.Models;
using Study_Classes_Booking_System.src.Observers;
using Study_Classes_Booking_System.src.Services;

namespace Study_Classes_Booking_System.src.Repositories
{
    public class ReservaRepositorySingleton
    {
        private static ReservaRepositorySingleton _instance;
        private static readonly object _lock = new object();

        // Mantemos a lista de reservas ativas/gerais
        private readonly List<Reserva> _reservas;
        private readonly List<Sala> _salasSistema;

        public EventManager Notificador { get; private set; }

        private ReservaRepositorySingleton()
        {
            _reservas = new List<Reserva>();
            Notificador = new EventManager();

            _salasSistema = new List<Sala>
            {
                new SalaEstudoIndividual { Id = 1, Nome = "Sala Individual A1", PrecoBase = 20.0 },
                new SalaTrabalhoGrupo { Id = 2, Nome = "Sala de Grupo B1", PrecoBase = 50.0 },
                new Laboratorio { Id = 3, Nome = "Laboratório Lab 01", PrecoBase = 100.0 }
            };
        }

        public static ReservaRepositorySingleton GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ReservaRepositorySingleton();
                }
                return _instance;
            }
        }

        public void Adicionar(Reserva reserva)
        {
            lock (_lock)
            {
                _reservas.Add(reserva);
                Notificador.Notify("ReservaCriada", reserva);
            }
        }

        public List<Reserva> ListarTodas()
        {
            return _reservas;
        }

        public List<Sala> BuscarSalasDisponiveis(DateTime inicio, DateTime fim)
        {
            var salasOcupadasIds = _reservas
                .Where(r => r.Status != StatusReserva.CANCELADA &&
                           ((inicio >= r.Horario && inicio < r.Horario.AddHours(2)) ||
                            (fim > r.Horario && fim <= r.Horario.AddHours(2))))
                .Select(r => r.Sala.Id)
                .Distinct()
                .ToList();

            return _salasSistema.Where(s => !salasOcupadasIds.Contains(s.Id)).ToList();
        }

        public List<Sala> ListarTodasAsSalas()
        {
            return _salasSistema;
        }

        public void GerarRelatorioDiario()
        {
            var hoje = DateTime.Now.Date;
            var reservasHoje = _reservas.Where(r => r.Horario.Date == hoje).ToList();

            Console.WriteLine("\n==============================================");
            Console.WriteLine($"   RELATÓRIO DE OCUPAÇÃO - {hoje:dd/MM/yyyy}");
            Console.WriteLine("==============================================");

            if (!reservasHoje.Any())
            {
                Console.WriteLine("Nenhuma ocupação registrada para hoje.");
            }
            else
            {
                foreach (var r in reservasHoje)
                {
                    Console.WriteLine($"- Sala: {r.Sala.Nome.PadRight(20)} | Usuário: {r.Usuario.Nome}");
                }
            }
            Console.WriteLine("==============================================\n");
        }

        // ============================================================
        // HISTÓRICO DE RESERVAS — usado pelo Proxy
        // ============================================================

        /// <summary>
        /// Retorna quantas reservas ativas um usuário já fez.
        /// O Proxy usa isso para decidir prioridade de acesso.
        /// </summary>
        public int ConsultarFrequenciaUsuario(int matricula)
        {
            lock (_lock)
            {
                return _reservas.Count(r =>
                    r.Usuario.Matricula == matricula &&
                    r.Status != StatusReserva.CANCELADA);
            }
        }

        /// <summary>
        /// Retorna o histórico de reservas passadas de um usuário,
        /// ordenado da mais recente para a mais antiga.
        /// </summary>
        public List<Reserva> ListarHistorico(int matricula)
        {
            return _reservas
                .Where(r =>
                    r.Usuario.Matricula == matricula &&
                    r.Horario < DateTime.Now)
                .OrderByDescending(r => r.Horario)
                .ToList();
        }

        /// <summary>
        /// Retorna um dicionário com todos os usuários e suas frequências,
        /// ordenado do que mais reservou para o que menos reservou.
        /// O Proxy pode usar isso para ranquear prioridade de acesso.
        /// </summary>
        public Dictionary<string, int> ListarFrequenciaTodosUsuarios()
        {
            return _reservas
                .Where(r => r.Status != StatusReserva.CANCELADA)
                .GroupBy(r => r.Usuario.Email)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count())
                .OrderByDescending(kv => kv.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        // ============================================================
    }
}
