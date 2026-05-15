# Funcionalidade Adicional — Reservas Recorrentes com Proxy

## O que foi implementado

A ideia foi permitir que um usuário reserve a mesma sala toda semana, no mesmo dia e horário,
sem precisar criar cada reserva manualmente. O usuário informa a sala, o horário e quantas
semanas quer reservar, e o sistema cuida do resto.

Se em alguma semana a sala já estiver ocupada, o sistema não cancela as outras — ele salva
aquela semana como pendente e avisa o usuário, para que ele possa resolver o conflito.

## Padrão utilizado — Proxy

Escolhemos o **Proxy** porque ele funciona como uma camada intermediária entre o pedido do
usuário e o repositório de reservas. Em vez de criar todas as reservas diretamente, o Proxy
processa cada semana, verifica se há conflito usando a Strategy já existente no sistema, e
só então decide se confirma ou marca como pendente.

Isso mantém o código organizado e não exigiu mudanças no repositório ou nas classes de reserva.

A integração com o **Observer** acontece naturalmente: quando o Proxy confirma uma reserva,
o evento `ReservaCriada` já é disparado normalmente. Quando detecta conflito, ele dispara
`ReservaPendente`, que é capturado pelo `NotificacaoPendenteObserver` e exibe o aviso no console.

```
Usuário
   │
   ▼
ReservaRecorrenteProxy
   ├── semana OK      → confirma → Observer: "ReservaCriada"
   └── semana conflito → pendente → Observer: "ReservaPendente" → aviso no console
```

## Arquivos criados ou modificados

- `src/Proxies/ReservaRecorrenteProxy.cs` — lógica principal da recorrência
- `src/Observers/NotificacaoPendenteObserver.cs` — notificação de conflito
- `src/Repositories/ReservaRepositorySingleton.cs` — adicionados métodos de histórico e frequência por usuário

## Como testar

1. Ter o .NET 9.0 SDK instalado
2. Rodar `dotnet run` na raiz do projeto
3. O console vai mostrar as reservas sendo criadas semana a semana, com aviso nas que tiverem conflito

## Autores

| Nome | Contribuição |
|------|-------------|
| João | Implementação do Proxy e do Observer de pendente |
| Nicolas | Histórico de reservas no Singleton |
| Graziele | Documentação |