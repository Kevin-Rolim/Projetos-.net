# PROJETO 1 — EventFlow

## Sistema de gerenciamento de eventos e inscrições

### Nível

Fundamentos.

### Combinação arquitetural

**Modelo**
- ASP.NET Core MVC

**Arquitetura**
- Monólito simples / All-in-One

**Padrões**
- Service Layer
- Dependency Injection
- ViewModel

**Persistência**
- Entity Framework Core diretamente no Service/Data
- SQL Server

**Conceitos estruturais**
- Separation of Concerns
- coesão;
- acoplamento;
- responsabilidade única;
- primeiros princípios SOLID.

Não usar:
- Repository;
- Clean Architecture;
- CQRS;
- MediatR;
- DDD avançado.

A intenção é justamente perceber o quanto conseguimos organizar bem uma aplicação **sem arquitetura exagerada**.

---

## Pedido do cliente

Uma pequena empresa organiza workshops, palestras e treinamentos presenciais.

Atualmente as inscrições são controladas em planilhas.

A empresa deseja um sistema web interno que permita cadastrar eventos, controlar vagas e registrar participantes.

O sistema será utilizado pelos funcionários responsáveis pela organização dos eventos.

---

## Usuários

### Administrador

Pode:

- cadastrar eventos;
- editar eventos;
- cancelar eventos;
- consultar participantes;
- registrar inscrições;
- cancelar inscrições.

Não haverá autenticação nesta primeira versão.

Isso é proposital: autenticação não é o objetivo arquitetural deste projeto.

---

# Funcionalidades

## Eventos

O sistema deverá permitir:

- cadastrar evento;
- editar evento;
- consultar evento;
- listar eventos;
- cancelar evento.

Um evento possui:

- Id;
- título;
- descrição;
- data;
- horário;
- local;
- capacidade máxima;
- situação.

Situações:

- Planejado;
- Aberto;
- Encerrado;
- Cancelado.

---

## Participantes

Um participante possui:

- Id;
- nome;
- e-mail;
- telefone.

O e-mail deve ser único.

---

## Inscrições

O sistema deverá permitir registrar um participante em um evento.

Uma inscrição contém:

- Id;
- participante;
- evento;
- data da inscrição;
- situação.

Situações:

- Confirmada;
- Cancelada.

---

# Regras de negócio

1. Um evento cancelado não aceita novas inscrições.
2. Um evento encerrado não aceita inscrições.
3. Não pode haver mais inscrições confirmadas que a capacidade do evento.
4. Um participante não pode se inscrever duas vezes no mesmo evento.
5. A capacidade deve ser maior que zero.
6. Um evento não pode ser cadastrado com data anterior ao momento atual.
7. Cancelar uma inscrição libera uma vaga.
8. Cancelar um evento cancela logicamente todas as inscrições ativas.
9. Eventos que já ocorreram devem poder ser marcados como encerrados.
10. Exclusão física de eventos não será permitida.

---

# Estrutura esperada

```text
EventFlow/
│
├── Controllers/
├── Models/
├── ViewModels/
├── Services/
├── Data/
├── Views/
├── wwwroot/
└── Program.cs
```

Um único `.csproj`.

---

# Fluxo que você deve compreender

```text
Browser
   ↓
Controller
   ↓
Service
   ↓
DbContext
   ↓
SQL Server
```

Na volta:

```text
SQL Server
   ↓
Service
   ↓
Controller
   ↓
ViewModel
   ↓
Razor View
   ↓
HTML
```

---

# O que deve existir

### Controllers

Exemplos:

- `EventsController`
- `ParticipantsController`
- `RegistrationsController`

### Services

Exemplos:

- `EventService`
- `RegistrationService`

O Service deverá conter regras de negócio que não pertencem ao Controller.

---

# ViewModels

Não envie automaticamente uma Entity inteira para todas as Views.

Por exemplo:

```text
EventDetailsViewModel

Event
TotalRegistrations
AvailableSlots
```

Isso ensinará por que:

```text
Entity != ViewModel
```

---

# Testes obrigatórios

Teste pelo menos:

- inscrição em evento com vagas;
- tentativa de inscrição em evento lotado;
- inscrição duplicada;
- tentativa de inscrição em evento cancelado;
- cancelamento liberando vaga.

---

# Fora do escopo

Não implementar:

- login;
- pagamento;
- envio de e-mail;
- fila de espera;
- API;
- microservices.

---

# O que este projeto deve ensinar

Quando terminar, você deve entender muito bem:

- MVC;
- request/response;
- routing;
- controllers;
- model binding;
- Razor;
- ViewModels;
- DI;
- Services;
- DbContext;
- migrations;
- separação de responsabilidades.

---