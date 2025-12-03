## 💈 BarberBoss

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)

**Sistema de gestão financeira para barbearias** 💼✂️

[Funcionalidades](#-funcionalidades) • [Tecnologias](#-tecnologias) • [Arquitetura](#-arquitetura) • [Instalação](#-instalação) • [API](#-api-endpoints) • [Testes](#-testes)

</div>

---

## 📋 Sobre o Projeto

**BarberBoss** é uma API REST desenvolvida em **.NET 8** para gerenciar o faturamento de barbearias, permitindo registrar, consultar, atualizar e deletar cobranças, além de gerar relatórios detalhados em **PDF** e **Excel**.

O projeto segue os princípios de **Clean Architecture**, **SOLID**, e utiliza **Entity Framework Core** com **MySQL** para persistência de dados.

---

## ✨ Funcionalidades

### 📊 Gestão de Cobranças (Billings)

- ✅ **Registrar** nova cobrança
- ✅ **Listar** todas as cobranças
- ✅ **Buscar** cobrança por ID
- ✅ **Atualizar** dados de uma cobrança
- ✅ **Deletar** cobrança

### 📈 Relatórios

- 📄 **Relatório em PDF** com design customizado (MigraDoc/PDFsharp)
- 📊 **Relatório em Excel** com formatação profissional (ClosedXML)
- 🔍 **Filtro por mês** para gerar relatórios específicos

### 🌐 Recursos Adicionais

- 🌍 **Internacionalização (i18n)** - Suporte para **Português (pt-BR)** e **Inglês (en)**
- ✅ **Validações robustas** com FluentValidation
- 🔄 **AutoMapper** para mapeamento de objetos
- 🛡️ **Tratamento global de exceções**
- 📝 **Documentação Swagger/OpenAPI**

---

## 🛠️ Tecnologias

### Backend
- **[.NET 8](https://dotnet.microsoft.com/)** - Framework principal
- **[ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)** - Web API
- **[Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)** - ORM
- **[MySQL](https://www.mysql.com/)** - Banco de dados
- **[Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)** - Provedor MySQL para EF Core

### Bibliotecas e Ferramentas
- **[AutoMapper](https://automapper.org/)** - Mapeamento de objetos
- **[FluentValidation](https://fluentvalidation.net/)** - Validação de dados
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentação da API
- **[ClosedXML](https://github.com/ClosedXML/ClosedXML)** - Geração de arquivos Excel
- **[MigraDoc](http://www.pdfsharp.net/wiki/MigraDoc.Main.ashx)** / **[PDFsharp](http://www.pdfsharp.net/)** - Geração de arquivos PDF

### Testes
- **[xUnit](https://xunit.net/)** - Framework de testes
- **[FluentAssertions](https://fluentassertions.com/)** - Assertions fluentes
- **[Bogus](https://github.com/bchavez/Bogus)** - Geração de dados fake para testes

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture** e está organizado em camadas:

```
BarberBoss/
├── src/
│   ├── BarberBoss.API/      # Camada de apresentação (Controllers, Middleware)
│ ├── BarberBoss.Application/      # Casos de uso, validações, AutoMapper
│   ├── BarberBoss.Communication/    # DTOs, Requests, Responses, Enums
│   ├── BarberBoss.Domain/# Entidades, Interfaces de Repositório
│   ├── BarberBoss.Infrastructure/   # Implementação de repositórios, DbContext
│   └── BarberBoss.Exception/        # Exceções customizadas, recursos de erro
└── tests/
    ├── Validators.Test/         # Testes de validação
    └── CommonTestUtilities/         # Builders e utilitários para testes
```

### Camadas

| Camada | Responsabilidade |
|--------|------------------|
| **API** | Controllers, Filtros, Middleware, Configuração |
| **Application** | Casos de uso (Use Cases), Validadores, Profiles do AutoMapper |
| **Communication** | Requests, Responses, Enums |
| **Domain** | Entidades, Interfaces de Repositório, Extensões |
| **Infrastructure** | Repositórios, DbContext, Configuração do EF Core |
| **Exception** | Exceções customizadas, Recursos de mensagens de erro |

---

## 🚀 Instalação

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8.0+](https://dev.mysql.com/downloads/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Passo a Passo

1. **Clone o repositório**
```bash
git clone https://github.com/sannathan/BarberBoss.git
cd BarberBoss
```

2. **Configure a connection string**

Edite o arquivo `appsettings.json` em `src/BarberBoss.Api/`:

```json
{
  "ConnectionStrings": {
    "Connection": "Server=localhost;Database=BarberBossDb;Uid=root;Pwd=yourpassword;"
  }
}
```

3. **Execute as migrations**
```bash
cd src/BarberBoss.Api
dotnet ef database update
```

4. **Execute a aplicação**
```bash
dotnet run
```

5. **Acesse o Swagger**
```
https://localhost:7297/swagger
```

---

## 📡 API Endpoints

### 💰 Billings (Cobranças)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/billings` | Registrar nova cobrança |
| `GET` | `/api/billings` | Listar todas as cobranças |
| `GET` | `/api/billings/{id}` | Buscar cobrança por ID |
| `PATCH` | `/api/billings/{id}` | Atualizar cobrança |
| `DELETE` | `/api/billings/{id}` | Deletar cobrança |

### 📊 Reports (Relatórios)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/report/excel?month=2024-01-01` | Gerar relatório em Excel |
| `GET` | `/report/pdf?month=2024-01-01` | Gerar relatório em PDF |

---

## 📝 Modelos de Dados

### Billing (Cobrança)

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "date": "2024-01-15",
  "barberName": "João Silva",
  "clientName": "Carlos Santos",
  "serviceName": "Corte + Barba",
  "amount": 50.00,
  "paymentMethod": 0,
  "status": 1,
  "notes": "Cliente preferencial",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### Enums

#### PaymentMethod
```csharp
0 = Pix
1 = Money (Dinheiro)
2 = CreditCard (Cartão de Crédito)
3 = DebitCard (Cartão de Débito)
```

#### Status
```csharp
0 = Canceled (Cancelado)
1 = Paid (Pago)
```

---

## ✅ Validações

### Regras de Negócio

| Campo | Regra |
|-------|-------|
| **Date** | Não pode ser futura |
| **BarberName** | Min: 2 caracteres, Max: 80 caracteres, Obrigatório |
| **ClientName** | Min: 2 caracteres, Max: 120 caracteres, Obrigatório |
| **ServiceName** | Min: 2 caracteres, Max: 120 caracteres, Obrigatório |
| **Amount** | Maior que zero |
| **PaymentMethod** | Deve ser um enum válido |
| **Status** | Deve ser um enum válido |
| **Notes** | Max: 500 caracteres (Opcional) |

---

## 🧪 Testes

### Executar testes

```bash
cd tests/Validators.Test
dotnet test
```

### Cobertura de Testes

- ✅ **Validação de campos obrigatórios**
- ✅ **Validação de tamanhos mínimos e máximos**
- ✅ **Validação de valores numéricos**
- ✅ **Validação de enums**
- ✅ **Validação de datas**

### Exemplo de Teste

```csharp
[Fact]
public void Success()
{
  // Arrange
    var validator = new BillingValidator();
    var request = RequestRegisterBillingJsonBuilder.Build();

    // Act
    var result = validator.Validate(request);

// Assert
    result.IsValid.Should().BeTrue();
}
```

---

## 🌍 Internacionalização

O projeto suporta múltiplos idiomas através de arquivos `.resx`:

- **Português (pt-BR)**: `ResourceErrorMessages.pt-BR.resx`
- **Inglês (en)**: `ResourceErrorMessages.resx` (padrão)

### Configuração

A cultura é definida automaticamente através do header `Accept-Language` nas requisições HTTP.

---

## 🎨 Relatórios

### Relatório em PDF
- 📄 Design customizado com **MigraDoc**
- 🎨 Cores e fontes personalizadas
- 📊 Tabelas formatadas com informações detalhadas
- 💰 Total de faturamento destacado

### Relatório em Excel
- 📊 Formatação profissional com **ClosedXML**
- 🎨 Células coloridas e estilizadas
- 📈 Dados organizados por data e valor
- 💾 Formato `.xlsx`

---

## 🔒 Tratamento de Erros

### Exceções Customizadas

| Exceção | Status HTTP | Descrição |
|---------|-------------|-----------|
| `ErrorOnValidationException` | 400 Bad Request | Erro de validação |
| `NotFoundException` | 404 Not Found | Recurso não encontrado |

### Exemplo de Resposta de Erro

```json
{
  "errorMessages": [
    "O nome do serviço é obrigatório",
    "O valor deve ser maior que zero"
  ]
}
```

---

## 📂 Estrutura de Pastas

```
BarberBoss/
├── src/
│ ├── BarberBoss.API/
│   │   ├── Controllers/
│   │   ├── Filters/
│   │   ├── Middleware/
│   │   └── Program.cs
│   │
│   ├── BarberBoss.Application/
│   │   ├── AutoMapper/
│   │   ├── UseCases/
│   │   │   ├── Billings/
│   │   │   │├── Register/
│   │   │   │   ├── GetAll/
│   │   │   │   ├── GetById/
│   │   │   │ ├── Update/
│   │   │   │   ├── Delete/
│   │   │   │   └── Reports/
│   │   │ │       ├── Excel/
│   │   │   │       └── Pdf/
│   │   └── DependencyInjectionExtension.cs
│   │
│   ├── BarberBoss.Communication/
│   │   ├── Requests/
│   │   ├── Responses/
│   │   └── Enums/
│   │
│ ├── BarberBoss.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Extensions/
│   │   └── Repositories/
│   │
│   ├── BarberBoss.Infrastructure/
│   │   ├── DataAccess/
│   │   │   ├── Repositories/
│   │   │   └── BarberBossDbContext.cs
│ │   └── DependencyInjectionExtension.cs
│   │
│   └── BarberBoss.Exception/
│       ├── ExceptionsB/
│    └── ResourceErrorMessages.resx
│
└── tests/
 ├── Validators.Test/
    │   └── Billings/
    │       └── Register/
 └── CommonTestUtilities/
 └── Requests/
```

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Siga os passos:

1. Faça um **fork** do projeto
2. Crie uma **branch** para sua feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Faça o **push** para a branch (`git push origin feature/AmazingFeature`)
5. Abra um **Pull Request**

---

## 📄 Licença

Este projeto está sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👨‍💻 Autor

**Nathan Barbosa**

- GitHub: [@sannathan](https://github.com/sannathan)
- LinkedIn: [Nathan Barbosa](https://www.linkedin.com/in/nathan-barbosa-dos-santos-08b398273/)

---

## 📞 Contato

Tem alguma dúvida ou sugestão? Entre em contato!

- 📧 Email: nbs3@cin.ufpe.br

---

<div align="center">

⭐ **Se este projeto te ajudou, considere dar uma estrela!** ⭐

Made with ❤️ by [Nathan Barbosa](https://github.com/sannathan)

</div>
