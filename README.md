# 🎬 VideoManager-API

Esta é uma API de alto desempenho desenvolvida em **.NET 10 (C#)** projetada para o consumo, gerenciamento e postagem de vídeos, servindo perfeitamente clientes web (HTML) e mobile (**VideoManager-Android**).

---

## 🏗️ Arquitetura e Design de Software

O ecossistema foi estruturado seguindo as melhores práticas de mercado e os princípios do **SOLID**, garantindo que a aplicação seja altamente escalável, testável e de fácil manutenção:

* **Camada de Apresentação (Controllers):** Controladores limpos (*Skinny Controllers*) focados exclusivamente em receber as requisições HTTP e direcionar os fluxos de entrada e saída.
* **Camada de Negócio (Services):** Centraliza todas as regras de negócio, validações de arquivos, lógica de permissões e processamento de dados.
* **Camada de Acesso a Dados (Repositories):** Abstrai e encapsula a persistência e a comunicação direta com o banco de dados.
* **Inversão de Controle e Dependência (IoC/DI):** Uso desacoplado de interfaces para injeção nativa de serviços em todo o sistema.

---

## 🔒 Segurança e Infraestrutura

* **Autenticação JWT (JSON Web Token):** Controle de acesso seguro e assinado para todos os endpoints privados do ecossistema.
* **Provedor de Contexto Automatizado:** Implementação do `UsuarioLogadoService` via `IHttpContextAccessor`. O `UsuarioID` é extraído de forma imutável direto do Token JWT do cabeçalho da requisição. Isso removeu a necessidade de enviar o ID manualmente nos DTOs (`VideosCriacaoDto`), blindando a API contra fraudes de identidade.
* **Gerenciamento Físico de Arquivos:** Armazenamento dos vídeos em diretórios isolados por ID de usuário (`wwwroot/videos/{usuarioId}/`), utilizando hashes únicos (`Guid`) para evitar sobreposição de arquivos no servidor.

---

## ✅ Funcionalidades Concluídas

- [x] Cadastro e autenticação de conta (JWT Token)
- [x] Edição e exclusão de conta de usuário
- [x] Postagem e upload de vídeos físicos no servidor com vínculo automático
- [x] Listagem dinâmica de vídeos salvos
- [x] Edição e exclusão de metadados dos vídeos

---

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C# (.NET 10)
* **Framework:** ASP.NET Core Web API
* **Banco de Dados:** Microsoft SQL Server
* **Ferramentas:** Visual Studio 2026, SSMS 22, Git/GitHub, Scalar/Swagger

---

## 🚀 Como Utilizar o Projeto

### Pré-requisitos
* .NET 10 SDK instalado na máquina.
* Instância ativa do Microsoft SQL Server.

### Passo a Passo para Execução

1. **Clonar o Repositório:**
   ```bash
   git clone [https://github.com/Matheus-A-Guedes936/VideoManager-API.git](https://github.com/Matheus-A-Guedes936/VideoManager-API.git)
   ```

2. **Restaurar e Compilar o Projeto:**
   ```bash
   dotnet build
   ```

3. **Executar a API:**
   ```bash
   dotnet run
   ```

4. **Testar os Endpoints:**
   
   Abra a URL gerada pelo console no seu navegador para acessar a interface do Scalar/Swagger (ex: https://localhost:7xxx/scalar/v1).
   
   Realize a autenticação no endpoint de login para obter o Token JWT.

   Insira o token no campo de autorização global (Bearer Token) para liberar os testes dos endpoints de vídeos.