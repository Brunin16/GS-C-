# 💡 GS - Energy Monitor 

Este é o projeto da disciplina **Cybersecurity** do Global Solution 2025, desenvolvido em **C# (Console Application)**, com o objetivo de aplicar os conceitos aprendidos de segurança da informação no contexto de um sistema de monitoramento de consumo de energia elétrica residencial.

---

## 📌 Finalidade do sistema

A aplicação simula um sistema completo de gerenciamento de dados de consumo energético, com funcionalidades que permitem:

- Registro e autenticação de usuários
- Cadastro e gerenciamento de pessoas e equipamentos
- Registro de consumo energético por mês
- Cálculo do total de energia consumida
- Adoção de práticas básicas de segurança como autenticação, controle de acesso e simulação de tratamento seguro de dados

O foco principal da disciplina é compreender os **riscos de segurança em aplicações locais** e como garantir a **persistência e integridade dos dados**, especialmente em situações de **falha elétrica**.

---

## 🖥️ Como executar

### ✅ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Oracle Database (pode usar o servidor da FIAP)
- Oracle Managed Data Access (instalado via NuGet)
- Visual Studio (ou outro editor compatível com .NET)

### 🚀 Executando o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/nome-do-repositorio.git
   ```
2. Compile e execute o projeto a partir da `GS_View`:
   ```bash
   dotnet run --project GS_View
   ```

---

## 📁 Estrutura de pastas

```
GS-Solution/
│
├── GS_Model/           # Entidades de domínio (AppUser, Person, Equipament, EnergyConsume)
│
├── GS_Repository/      # Classes de persistência com acesso ao Oracle DB via Dapper
│
├── GS_Service/         # Regras de negócio e lógica de aplicação
│
├── GS_View/            # Aplicação Console com menu interativo
│
└── README.md           # Este arquivo
```

---

## 🛠️ Dependências principais

- `Oracle.ManagedDataAccess`
- `Dapper`
- `.NET 8 SDK`

---

## 🔐 Segurança aplicada

- Login com validação de credenciais
- Armazenamento de dados com acesso seguro (Oracle)
- Estrutura modular e organizada para facilitar evolução com criptografia ou backup em nuvem
- Simulação de proteção de dados em caso de falha elétrica

---

## 👨‍💻 Aluno

- Bruno Eduardo Caputo Paulino - rm55803

