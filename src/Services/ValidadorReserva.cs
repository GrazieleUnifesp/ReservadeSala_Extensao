using System;
using Study_Classes_Booking_System.src.Repositories;
using Study_Classes_Booking_System.src.Strategies;
using Study_Classes_Booking_System.src.Models;

namespace Study_Classes_Booking_System.src.Services
{
    public class ValidadorReserva
    {
        private IPoliticaReserva _politica;

        public ValidadorReserva(IPoliticaReserva politica) => _politica = politica;
        //Injetando Dependência

        public void SetPolitica(IPoliticaReserva politica) => _politica = politica;

        public bool Validar(Reserva nova) =>
            _politica.Validar(nova, ReservaRepositorySingleton.GetInstance().ListarTodas());
            //Usando a função de validação em Strategies/IPoliticaReserva, tipo passado externamente
    }
}