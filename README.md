# 🚀 Nexora — Plataforma Web de Soluções em Tecnologia

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/Entity_Framework-68217A?style=for-the-badge&logo=dotnet)](https://docs.microsoft.com/ef/core/)
[![REST API](https://img.shields.io/badge/API-RESTful-009639?style=for-the-badge)](https://swagger.io/)

O **Nexora** é uma plataforma web completa desenvolvida para apresentar serviços de tecnologia, portfólio de projetos e artigos técnicos, além de captar potenciais clientes através de solicitações de orçamentos online. 

O sistema conta com uma **Landing Page Pública** dinâmica e um **Painel Administrativo (Dashboard)** seguro para gestão total de conteúdos.

---

## 🛠️ Arquitetura e Engenharia de Software

O projeto foi construído seguindo princípios de **Clean Architecture** e **DDD (Domain-Driven Design)**, garantindo desacoplamento, facilidade de manutenção e alta testabilidade:

- **`Nexora.API`**: Camada de apresentação RESTful com controllers, middleware de autenticação (JWT) e validação de requisições.
- **`Nexora.Application`**: Camada de serviços de negócio, DTOs e regras de orquestração.
- **`Nexora.Domain`**: Entidades centrais do sistema e interfaces dos repositórios.
- **`Nexora.Infrastructure`**: Acesso a dados com **Entity Framework Core**, mapeamentos do banco e migrações.
- **`Nexora.Tests`**: Testes unitários e de integração para validação de fluxos críticos.

---

## ✨ Principais Funcionalidades

### 🌐 Área Pública (Landing Page)
- **Apresentação Institucional**: Exibição dos serviços prestados pela empresa/profissional.
- **Portfólio de Projetos**: Vitrine dinâmica de projetos desenvolvidos, com filtro de destaque.
- **Blog / Artigos**: Leitura interativa de artigos técnicos com modal de expansão em tela cheia.
- **Solicitação de Orçamento**: Formulário direto integrado ao banco de dados para captação de leads.

### 🔐 Painel Administrativo (Dashboard)
- **Gestão de Orçamentos**: Visualização em tempo real das solicitações enviadas pelos clientes, incluindo status e dados de contato.
- **Métricas do Sistema**: Contador dinâmico de solicitações e resumo de métricas.
- **CRUD Completo**: Cadastro, edição e remoção de Projetos, Serviços e Artigos.
- **Autenticação Segura**: Controle de acesso por token JWT.

---

## 💻 Tecnologias Utilizadas

### **Back-end**
- **Linguagem/Framework:** C# / .NET 10.0
- **Persistência de Dados:** Entity Framework Core
- **Segurança:** Authentication & Authorization via JWT (JSON Web Tokens)
- **Documentação da API:** Swagger / OpenAPI

### **Front-end**
- **Interface:** HTML5, CSS3 Moderno (Flexbox & Grid Responsivo)
- **Lógica e Consumo de API:** JavaScript ES6+ (Fetch API / Asynchronous JS)

---

## 🚀 Como Executar o Projeto Localmente

### **Pré-requisitos**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.
- Banco de dados SQL Server ou SQLite configurado.

### **Passos**

1. **Clonar o repositório:**
   ```bash
   git clone [https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git](https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git)
   cd SEU-REPOSITORIO


2. **Ajuste a string de conexão no arquivo Nexora.API/appsettings.Development.json:**
  ```bash
  JSON
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NexoraDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
```
3. **Executar as Migrations do Banco de Dados:**

```bash
dotnet ef database update --project Nexora.Infrastructure --startup-project Nexora.API
```
Rodar a Aplicação:
```bash
dotnet run --project Nexora.API
```
4. **Acessar no Navegador:**

```bash

Landing Page: https://localhost:XXXX/index.html

Painel Admin: https://localhost:XXXX/pages/login.html

Documentação Swagger: https://localhost:XXXX/swagger

```



📬 Contato para Projetos Freelance

Gostou do sistema e precisa de uma solução sob medida para o seu negócio? Vamos conversar!

Email: larapablo748@gmail.com
