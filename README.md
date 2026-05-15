# Study Classes Booking System — Extensão

Extensão do sistema de reserva de salas, desenvolvida como continuação do projeto original.
O foco foi adicionar suporte a **reservas recorrentes semanais** usando o padrão **Proxy**.

## Autores

| Nome | Contribuição                                                |
|------|-------------------------------------------------------------|
| João | Implementação do Proxy e Observer de pendente               |
| Nicolas | Histórico de reservas no Singleton                          |
| Graziele | Documentação (funcionalidade adicional e diagrama) e README |

## O que foi adicionado

A nova funcionalidade permite reservar a mesma sala toda semana, no mesmo horário, por
quantas semanas o usuário quiser. O Proxy cuida de verificar cada semana individualmente —
se tiver conflito, salva como pendente e notifica, sem cancelar as outras semanas.

Também foram adicionados métodos de histórico no Singleton, para consultar quantas reservas
um usuário já fez e listar as reservas passadas por matrícula.

## Padrão utilizado

**Proxy** — implementado em `ReservaRecorrenteProxy`. Funciona como intermediário entre
o pedido de reserva recorrente e o repositório, adicionando a lógica de recorrência e
validação sem modificar as classes existentes.

## Como rodar

```bash
git clone https://github.com/GrazieleUnifesp/ReservadeSala_Extensao.git
cd ReservadeSala_Extensao
dotnet run
```

## Documentação

Mais detalhes sobre a funcionalidade e o padrão aplicado em [docs/funcionalidade_adicional.md](docs/funcionalidade_adicional.md).