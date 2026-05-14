using System;
using Study_Classes_Booking_System.src.Factories;
using Study_Classes_Booking_System.src.Repositories;
using Study_Classes_Booking_System.src.Models;
using Study_Classes_Booking_System.src.Observers;
using Study_Classes_Booking_System.src.Services;
using Study_Classes_Booking_System.src.Strategies;
using Study_Classes_Booking_System.src.Proxies;
using System.Linq;

namespace Study_Classes_Booking_System
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = ReservaRepositorySingleton.GetInstance();

            // Usando Factory para criar salas diferentes
            var sala1 = SalaFactory.CriarSala("individual", 1, "Cabine 01");
            var sala2 = SalaFactory.CriarSala("laboratorio", 2, "Lab de Química");

            var usuario = new Usuario { Matricula = 1, Nome = "Nicolas", Email = "nicolas@estudante.com" };

            // Criando uma reserva e guardando no Singleton
            var novaReserva = new Reserva
            {
                Id = "101",
                Sala = sala1,
                Usuario = usuario,
                Horario = DateTime.Now
            };

            repo.Adicionar(novaReserva);

            // Exibindo os dados salvos para provar o funcionamento
            Console.WriteLine("--- Status do Sistema de Reservas ---");
            foreach (var r in repo.ListarTodas())
            {
                Console.WriteLine($"Reserva confirmada: #{r.Id}");
                Console.WriteLine($"Sala: {r.Sala.Nome} | Tipo: {r.Sala.GetType().Name}");
                Console.WriteLine($"Responsável: {r.Usuario.Nome}");
            }

            var validador = new ValidadorReserva(new PrimeiroAChegar());
            var proxy = new ReservaRecorrenteProxy(validador);

            // registra observer de pendentes
            repo.Notificador.Subscribe("ReservaPendente", new NotificacaoPendenteObserver());

            // cria reserva recorrente por 4 semanas
            var reservaBase = new Reserva
            {
                Id = Guid.NewGuid().ToString(),
                Sala = sala2,
                Usuario = usuario,
                Horario = new DateTime(2025, 1, 7, 14, 0, 0),
                Duracao = 2
            };

            proxy.CriarRecorrente(reservaBase, 4);
            // exibe todas as reservas incluindo as recorrentes
            Console.WriteLine("\n--- Reservas Recorrentes Criadas ---");
            foreach (var r in repo.ListarTodas().Where(r => r.Sala.Id == sala2.Id))
            {
                Console.WriteLine($"Reserva #{r.Id} | " +
                                $"Sala: {r.Sala.Nome} | " +
                                $"Horario: {r.Horario:dd/MM/yyyy HH:mm} | " +
                                $"Status: {r.Status}");
            }

            Console.WriteLine("\nTeste concluído com sucesso. Pressione qualquer tecla...");
            Console.ReadKey();
        }
    }
}
