# .NET Architecture Challenges

> Uma jornada prática por diferentes formas de projetar aplicações com .NET — dos fundamentos de uma aplicação MVC até modularização e sistemas distribuídos.

Este repositório representa uma trilha de estudos e desafios práticos criada para explorar **arquitetura de software, padrões de projeto, persistência, organização de código e modelagem de domínio no ecossistema .NET**.

A proposta não é construir várias aplicações iguais utilizando estruturas diferentes apenas por exercício. Cada projeto possui um problema próprio e foi pensado para introduzir uma combinação específica de conceitos, permitindo compreender **quando uma abordagem ajuda, quais custos ela adiciona e em quais cenários sua utilização faz sentido**.

Cada desafio é desenvolvido como um projeto independente, com seu próprio repositório, documentação, testes e histórico de evolução.

---

## Objetivo

O objetivo desta trilha é evoluir progressivamente de uma aplicação simples e bem organizada para arquiteturas com maior separação de responsabilidades e complexidade estrutural.

Ao final, a intenção é conseguir responder perguntas como:

- Por que utilizar uma arquitetura em camadas neste sistema?
- Quando um `Repository` realmente agrega valor?
- Quando utilizar o `DbContext` diretamente é suficiente?
- Qual problema a Clean Architecture tenta resolver?
- Como Onion e Hexagonal diferem na forma de enxergar dependências?
- Quando organizar por funcionalidade é melhor do que organizar por camada?
- Onde DDD realmente ajuda?
- Quando um Modular Monolith é mais apropriado que Microservices?
- Qual é o custo real de distribuir um sistema?

Mais importante do que aplicar um padrão é conseguir **justificar a decisão de utilizá-lo**.

---

## Trilha

| # | Projeto | Modelo | Arquitetura | Principais conceitos |
|---:|---|---|---|---|
| 01 | **EventFlow** | ASP.NET Core MVC | Monólito simples | MVC, Service Layer, ViewModel, EF Core |
| 02 | **ReservaHub** | Razor Pages | Layered Architecture | Layers, Repository, Dependency Inversion |
| 03 | **WorkshopFlow** | Blazor | Onion Architecture | DDD, Repository, Unit of Work |
| 04 | **SupportDesk** | React + ASP.NET Core API | Clean Architecture | DTOs, Use Cases, Dependency Rule |
| 05 | **SprintBoard** | Minimal API | Vertical Slice Architecture | Features, CQRS, EF Core direto |
| 06 | **FreightHub** | ASP.NET Core API | Hexagonal Architecture | Ports, Adapters, Strategy, Dapper |
| 07 | **CampusCore** | ASP.NET Core API | Modular Monolith | Modules, DDD, CQRS, Vertical Slices |
| 08 | **MiniCommerce** | APIs | Microservices | Eventos, bancos independentes, mensageria |

---

# 01 — EventFlow

### Gerenciamento de eventos e inscrições

Primeiro projeto da trilha e ponto de partida para os fundamentos do desenvolvimento web com ASP.NET Core.

Uma empresa responsável por workshops, palestras e treinamentos precisa substituir o controle manual de participantes por uma aplicação capaz de administrar eventos, vagas e inscrições.

### Arquitetura

```text
ASP.NET Core MVC
        ↓
Controller
        ↓
Service
        ↓
EF Core / DbContext
        ↓
SQL Server
```

### Conceitos explorados

- ASP.NET Core MVC
- Controllers
- Razor Views
- Models
- ViewModels
- Service Layer
- Dependency Injection
- Entity Framework Core
- Migrations
- Separation of Concerns
- coesão e acoplamento
- fundamentos de SOLID

### Propósito

Aprender a estruturar corretamente uma aplicação sem introduzir abstrações antes que exista uma necessidade real para elas.

Neste projeto, não há Repository, CQRS ou Clean Architecture propositalmente.

O desafio é provar que uma aplicação simples também pode ser organizada, testável e sustentável.

**Evolução:** Fundamentos

---

# 02 — ReservaHub

### Sistema de reservas de salas de coworking

Um coworking precisa controlar salas, clientes, disponibilidade e reservas, impedindo conflitos de horários e utilização de espaços indisponíveis.

Este projeto introduz a primeira separação física da aplicação em camadas.

### Arquitetura

```text
Web
 ↓
Application
 ↓
Infrastructure
 ↓
Database
```

### Conceitos explorados

- Razor Pages
- Layered Architecture
- Service Layer
- Repository Pattern
- interfaces
- Dependency Injection
- Dependency Inversion
- EF Core
- separação entre apresentação, aplicação e infraestrutura

### Propósito

Experimentar conscientemente o Repository Pattern e comparar sua utilização com o acesso direto ao `DbContext` realizado no projeto anterior.

O objetivo não é concluir que Repository é melhor ou pior, mas entender:

- qual problema ele resolve;
- qual abstração adiciona;
- qual custo estrutural surge;
- como influencia testes e manutenção.

**Evolução:** Camadas

---

# 03 — WorkshopFlow

### Gestão de manutenção de equipamentos

Uma empresa de manutenção industrial precisa controlar equipamentos, técnicos, peças, custos e o ciclo completo das ordens de serviço.

Neste projeto, as regras de negócio deixam de ser apenas validações simples e passam a ocupar o centro da arquitetura.

### Arquitetura

```text
Infrastructure
      ↓
Application
      ↓
Domain
```

Interface desenvolvida com Blazor.

### Conceitos explorados

- Onion Architecture
- Blazor
- Domain-Driven Design
- Entity
- Value Object
- Aggregate
- Aggregate Root
- Domain Service
- Domain Event
- Repository
- Unit of Work
- Dependency Inversion
- domínio independente da infraestrutura

### Propósito

Construir um domínio que continue fazendo sentido mesmo sem saber que existem:

- Blazor;
- Entity Framework;
- SQL Server;
- HTTP.

O projeto introduz comportamento dentro do modelo de domínio em vez de utilizar entidades apenas como estruturas de dados.

**Evolução:** Domínio

---

# 04 — SupportDesk

### Plataforma de chamados internos

Sistema para centralizar solicitações de suporte entre funcionários e uma equipe de TI.

É o primeiro projeto da trilha em que frontend e backend são aplicações separadas.

### Stack

```text
React + TypeScript
        ↓ HTTP / JSON
ASP.NET Core Web API
        ↓
Application
        ↓
Domain
```

### Arquitetura

**Clean Architecture**

```text
Presentation
      ↓
Application
      ↓
Domain

Infrastructure
      ↓
Application
```

### Conceitos explorados

- React
- TypeScript
- ASP.NET Core Web API
- Controllers
- Clean Architecture
- Domain
- Application
- Infrastructure
- DTOs
- Use Cases / Services
- Repository quando necessário
- Dependency Rule
- SOLID
- testes unitários e de integração

### Propósito

Entender que Clean Architecture não significa simplesmente separar código em quatro projetos.

O principal aprendizado é controlar **a direção das dependências** e impedir que regras centrais da aplicação sejam determinadas por banco, framework ou interface.

**Evolução:** Independência de dependências

---

# 05 — SprintBoard

### Gerenciador de projetos em Kanban

Uma pequena equipe de desenvolvimento precisa organizar projetos, quadros, colunas e tarefas.

Neste desafio, a estrutura tradicional baseada em `Controllers`, `Services` e `Repositories` é abandonada como organização principal.

### Arquitetura

**Vertical Slice Architecture**

```text
Features/
├── Projects/
├── Boards/
└── Tasks/
```

Cada funcionalidade contém o necessário para executar seu próprio caso de uso.

### Conceitos explorados

- ASP.NET Core Minimal APIs
- Vertical Slice Architecture
- CQRS
- Commands
- Queries
- Handlers
- DTOs
- organização por feature
- EF Core diretamente nos handlers
- PostgreSQL

### Propósito

Comparar duas formas diferentes de enxergar uma aplicação:

```text
Organização por tecnologia
Controllers / Services / Repositories
```

versus:

```text
Organização por funcionalidade
CreateTask / MoveTask / CompleteTask
```

Também é utilizado EF Core diretamente, sem Repository, para comparar novamente os trade-offs dessa abstração.

**Evolução:** Funcionalidades

---

# 06 — FreightHub

### Agregador de cotações de frete

Uma loja precisa consultar diferentes transportadoras através de integrações distintas e apresentar as melhores opções de preço e prazo ao usuário.

Cada transportadora possui sua própria forma de comunicação, mas o núcleo da aplicação não deve depender desses detalhes.

### Arquitetura

**Hexagonal Architecture — Ports & Adapters**

```text
             HTTP
              │
          Adapter In
              │
              ▼
         Application
         /          \
        ▼            ▼
 Freight Port    Persistence Port
      │                 │
      ▼                 ▼
Transportadoras     PostgreSQL
```

### Conceitos explorados

- ASP.NET Core API
- Controllers
- Hexagonal Architecture
- Ports
- Adapters
- Strategy Pattern
- Dependency Injection
- DTOs
- Dapper
- SQL explícito
- tratamento de falhas externas
- correlation IDs

### Propósito

Aprender a construir um núcleo que depende de contratos, enquanto tecnologias externas funcionam como adapters substituíveis.

Também permite comparar:

```text
EF Core
```

com:

```text
Dapper + SQL explícito
```

**Evolução:** Fronteiras e adapters

---

# 07 — CampusCore

### Plataforma universitária modular

Sistema destinado ao gerenciamento de atividades extracurriculares de uma instituição.

A aplicação possui três áreas de negócio principais:

```text
Events
Rooms
Memberships
```

Apesar de existir apenas um deploy, cada módulo deve possuir limites claramente definidos.

### Arquitetura

**Modular Monolith**

```text
CampusCore
│
├── Events
├── Rooms
└── Memberships
```

Internamente, diferentes técnicas são combinadas:

- DDD
- Vertical Slice
- CQRS
- Domain Events

### Conceitos explorados

- Modular Monolith
- bounded modules
- contratos entre módulos
- isolamento de infraestrutura
- DDD
- Aggregates
- Value Objects
- Domain Events
- CQRS
- Vertical Slices
- testes arquiteturais
- EF Core
- PostgreSQL

### Restrição central

Um módulo não deve acessar diretamente estruturas internas de outro.

```text
Events
  ✕
RoomsDbContext
```

A comunicação deve ocorrer através de contratos definidos.

### Propósito

Aprender que:

> Uma aplicação única não precisa ser um grande bloco de código sem fronteiras.

O projeto também prepara a comparação direta com a arquitetura distribuída utilizada no desafio seguinte.

**Evolução:** Modularização

---

# 08 — MiniCommerce

### Plataforma de comércio baseada em Microservices

Último desafio da trilha.

O domínio é propositalmente pequeno. A complexidade está na distribuição do sistema.

A aplicação é dividida em três serviços:

```text
Catalog Service
Order Service
Notification Service
```

Cada serviço é responsável por seus próprios dados e possui autonomia sobre sua implementação.

### Arquitetura

**Microservices**

```text
Catalog Service
      ↑
      │ HTTP
      │
Order Service
      │
      │ Event
      ▼
Message Broker
      │
      ▼
Notification Service
```

### Conceitos explorados

- Microservices
- comunicação síncrona
- comunicação assíncrona
- banco por serviço
- eventos de integração
- RabbitMQ
- Docker Compose
- timeout
- retry controlado
- idempotência
- observabilidade
- correlation IDs
- falhas distribuídas

### Propósito

Entender na prática que Microservices não representam simplesmente uma versão “mais profissional” de um monólito.

Distribuir uma aplicação oferece vantagens em determinados cenários, mas também introduz problemas que não existiam anteriormente:

- rede;
- disponibilidade;
- consistência;
- mensageria;
- observabilidade;
- deployment;
- resiliência.

O desafio final é comparar este projeto diretamente com o **CampusCore** e identificar em quais situações um Modular Monolith seria uma escolha mais adequada.

**Evolução:** Sistemas distribuídos

---

# Progressão arquitetural

A trilha foi construída para que cada projeto introduza um problema ou uma abordagem que possa ser comparada com aquilo que veio anteriormente.

```text
01  MVC / Monólito
        │
        ▼
02  Layered Architecture
        │
        ▼
03  Onion + DDD
        │
        ▼
04  Clean Architecture
        │
        ▼
05  Vertical Slice + CQRS
        │
        ▼
06  Hexagonal / Ports & Adapters
        │
        ▼
07  Modular Monolith
        │
        ▼
08  Microservices
```

Não existe a intenção de demonstrar que a última arquitetura é a melhor.

A evolução representa **aumento de problemas arquiteturais estudados**, não uma classificação de qualidade.

---

# Conceitos cobertos

## Modelos de aplicação

- ASP.NET Core MVC
- Razor Pages
- Blazor
- SPA + API
- APIs com Controllers
- Minimal APIs

## Arquiteturas

- Monólito simples
- Layered Architecture
- Onion Architecture
- Clean Architecture
- Vertical Slice Architecture
- Hexagonal Architecture
- Modular Monolith
- Microservices

## Padrões

- Service Layer
- Repository
- Unit of Work
- Dependency Injection
- CQRS
- DTO
- ViewModel
- Strategy
- Ports & Adapters

## Domain-Driven Design

- Entity
- Value Object
- Aggregate
- Aggregate Root
- Domain Service
- Domain Event

## Fundamentos estruturais

- Separation of Concerns
- Dependency Inversion
- baixo acoplamento
- alta coesão
- SOLID
- testabilidade
- responsabilidade das camadas
- direção de dependências

---

# Organização local

Cada desafio é tratado como um projeto independente.

```text
dotnet/
│
├── 00-trilha-dotnet/
│
├── 01-eventflow-mvc/
│
├── 02-reservahub-layered/
│
├── 03-workshopflow-onion/
│
├── 04-supportdesk-clean/
│
├── 05-sprintboard-vertical-slice/
│
├── 06-freighthub-hexagonal/
│
├── 07-campuscore-modular-monolith/
│
└── 08-minicommerce-microservices/
```

Cada projeto possui seu próprio:

```text
Git repository
README
Solution
src/
tests/
docs/
```

Dessa forma, nenhum desafio depende fisicamente dos demais.

---

# Padrão dos projetos

Apesar das arquiteturas internas variarem, os repositórios seguem uma estrutura externa previsível:

```text
project/
│
├── src/
├── tests/
├── docs/
├── .github/
├── .gitignore
├── .editorconfig
├── Project.slnx
└── README.md
```

A estrutura dentro de `src/` muda de acordo com a arquitetura estudada.

Isso é proposital.

O objetivo não é fazer todas as aplicações parecerem iguais, mas manter consistência no repositório sem esconder as diferenças arquiteturais.

---

# Metodologia

Cada projeto segue algumas regras.

### Código funcional

O projeto deve:

- compilar;
- executar;
- possuir persistência funcional;
- validar suas principais regras de negócio;
- possuir tratamento de erros adequado.

### Testes

Os principais comportamentos e regras de negócio devem possuir testes automatizados.

### Git

O histórico deve representar a evolução real do projeto.

Exemplo:

```text
chore: initialize solution
feat: add event domain model
feat: implement event creation
test: cover registration capacity rules
refactor: extract registration service
docs: document architecture decisions
```

### Documentação

Cada repositório deve explicar:

1. o problema;
2. a solução;
3. as tecnologias;
4. a arquitetura;
5. por que ela foi escolhida;
6. como o código está organizado;
7. regras de negócio;
8. como executar;
9. como testar;
10. decisões arquiteturais;
11. trade-offs;
12. aprendizados.

---

# Princípios da trilha

### Não utilizar padrões por hábito

Uma abstração só deve existir quando houver uma razão compreensível para ela.

### Evitar overengineering

Mais projetos, interfaces ou camadas não significam automaticamente uma arquitetura melhor.

### Comparar abordagens

Repository será utilizado e também propositalmente removido em outros projetos.

EF Core será comparado com Dapper.

Organização em camadas será comparada com Vertical Slice.

Modular Monolith será comparado com Microservices.

### Arquitetura é contexto

Não existe uma estrutura universal adequada para qualquer aplicação.

Uma decisão arquitetural deve ser consequência do problema que está sendo resolvido.

---

# Status

- [ ] **01 — EventFlow** · MVC / Monólito
- [ ] **02 — ReservaHub** · Layered Architecture
- [ ] **03 — WorkshopFlow** · Onion Architecture / DDD
- [ ] **04 — SupportDesk** · Clean Architecture
- [ ] **05 — SprintBoard** · Vertical Slice / CQRS
- [ ] **06 — FreightHub** · Hexagonal Architecture
- [ ] **07 — CampusCore** · Modular Monolith
- [ ] **08 — MiniCommerce** · Microservices

---

# Ambiente principal

A trilha utiliza principalmente o ecossistema moderno do .NET:

```text
.NET 10
C#
ASP.NET Core
Entity Framework Core
SQL Server / PostgreSQL
Git
GitHub
Linux
```

Outras tecnologias são introduzidas apenas quando possuem um propósito específico dentro do desafio, como:

- React + TypeScript;
- Blazor;
- Dapper;
- RabbitMQ;
- Docker.

---

# Resultado esperado

Ao concluir a trilha, o objetivo não é apenas possuir oito aplicações publicadas.

O resultado esperado é conseguir analisar um problema e escolher conscientemente entre alternativas como:

```text
MVC ou SPA?
Controllers ou Minimal APIs?
DbContext ou Repository?
Layered ou Vertical Slice?
Clean ou Hexagonal?
DDD ou modelo simples?
Monólito ou Modular Monolith?
Modular Monolith ou Microservices?
```

O foco deste portfólio é demonstrar **processo de aprendizagem, capacidade de comparação e tomada consciente de decisões arquiteturais**.

---

## Autor

**Kevin Rolim**

Estudante de Análise e Desenvolvimento de Sistemas, utilizando esta trilha como laboratório prático de desenvolvimento backend, arquitetura de software e engenharia de aplicações com .NET.