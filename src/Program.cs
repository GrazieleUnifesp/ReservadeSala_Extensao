// See https://aka.ms/new-console-template for more information

using System;
using Study_Classes_Booking_System.src.Factories;
using Study_Classes_Booking_System.src.Repositories;
using Study_Classes_Booking_System.src.Models;

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
                Id = 101,
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

            Console.WriteLine("\nTeste concluído com sucesso. Pressione qualquer tecla...");
            Console.ReadKey();
        }
    }
}
