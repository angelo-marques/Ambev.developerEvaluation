# Ambev Developer Evaluation

# Visão geral
Este repositório contém a implementação de uma API de avaliação de desenvolvedor, organizada de acordo com os princípios de Clean Architecture e Domain‑Driven Design (DDD). 
O projeto foi complementado por Angelo Marques de Oliveira Silva – Tech Lead, desenvolvedor e arquiteto de software – como parte de uma avaliação para a Ambev. 

---

## Como executar a API
### Executar localmente (sem Docker)

1. **Pré‑requisitos:**
   * [.NET SDK 8.0](https://dotnet.microsoft.com/download) ou superior instalado na máquina.
   * PostgreSQL configurados localmente (podem ser instalados via Docker ou manualmente). As credenciais padrão utilizadas no estão no `docker-compose.yml`.

2. **Clonar o repositório:**
```bash
git clone https://github.com/angelo-marques/Ambev.developerEvaluation.git
cd Ambev.developerEvaluation/backend
```

3. **Restaurar pacotes e compilar:**
  ```
  dotnet restore
  dotnet build
  ```

4. **Configurar variáveis de ambiente:**
    O projeto utiliza variáveis definidas em appsettings.json e appsettings.Development.json para definir conexões e chaves JWT. Para rodar localmente, garanta que a string de conexão DefaultConnection aponte para seu banco PostgreSQL e que a chave Jwt:SecretKey esteja     definida. Se estiver utilizando banco local, a string típica ficaria assim:
  ````
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=seu;Username=seu;Password=seu"
  }
  ````

5. **Aplicar migrações (opcional):**
    Caso deseje criar o banco a partir das migrações do Entity Framework, execute:
  ````
  dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj --startup-project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
  ````

6. **Executar a API:**
````
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj
````

A API estará disponível em http://localhost:8080 ou definida no arquivos de configuração. 
O Swagger UI será configurado automaticamente, permitindo explorar os endpoints e realizar chamadas para /users, /products e /carts.

### Executar via Docker
O projeto já inclui uma infraestrutura pronta com Docker Compose:

````
cd backend
docker-compose up -d
````

Após a construção das imagens, a API ficará acessível na porta configurada no docker, assim como os demais serviços. 
As credenciais dos serviços estão definidas no arquivo docker-compose.yml e podem ser alteradas conforme a necessidade.
<br>
Para derrubar os containers:

### Estrutura do projeto
O backend segue a filosofia de Clean Architecture, que separa responsabilidades em camadas bem definidas. <br> A organização dos diretórios em backend/src corresponde a essas camadas:

| Diretório       | Descrição resumida                                                                                                                                                                                                                                                                                                    |
| --------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Domain**      | Contém entidades, value objects, serviços de domínio e repositórios (interfaces). É a camada mais interna e independente, onde a lógica de negócio é modelada. Exemplo: entidades `Cart` e `CartItems` implementam regras como limites de quantidade e cálculo de descontos.                                          |
| **Application** | Implementa os casos de uso (handlers) utilizando o padrão **CQRS/Mediator** via MediatR. Cada recurso (Users, Products, Carts) possui commands, handlers, results, perfis de mapeamento e validadores. Exemplo: o `CreateCartHandler` orquestra a criação de um carrinho chamando serviços de domínio e repositórios. |
| **Common**      | Camada de cross‑cutting com utilitários, validações, serviços de segurança (JWT) e comportamentos de pipeline. Inclui comportamento de validação para Mediator, implementações de `IPasswordHasher` e gerador de tokens JWT.                                                                                          |
| **IoC**         | Define o **Inversion of Control**, registrando serviços e repositórios no contêiner de injeção de dependências. As classes `ModuleInitializer` são descobertas refletivamente para registrar serviços específicos (Application, Infrastructure, WebApi).                                                              |
| **ORM**         | Implementa a persistência usando **Entity Framework Core**. Define o `DefaultContext`, mapeamentos (via `Mapping`), migrações e repositórios concretos. As implementações de `IProductRepository`, `IUserRepository` e `ICartRepository` executam operações de CRUD e paginação.                                      |
| **WebApi**      | Camada de apresentação, responsável por expor os endpoints HTTP. Configura controle de versões da API, autenticação via JWT, health checks, Swagger, Serilog para logging e as dependências necessárias. O arquivo `Program.cs` mostra a configuração dos serviços e do middleware.                                   |

--------------------

### Padrões e tecnologias utilizados
  - **Domain‑Driven Design (DDD):** entidades encapsulam lógica de negócio, evitando anêmicos. As regras de negócio, como validação de quantidade de itens e cálculo de descontos em carrinhos, estão implementadas diretamente nas entidades Cart e CartItems.
  - **Clean Architecture:** separação em camadas (Domain, Application, Infrastructure/ORM, WebApi). As dependências sempre apontam de fora para dentro, mantendo a camada de domínio isolada.
  - **CQRS e Mediator:** utilização do MediatR para desacoplar commands/queries dos handlers. Isso facilita testes unitários e organização por casos de uso.
  - **Repository Pattern:** interfaces de repositórios no domínio e implementações na camada de ORM para isolar a persistência.
  - **Specification Pattern:** permite filtrar entidades com critérios reutilizáveis. Embora não destacado nos testes, a pasta Domain/Specifications contém especificações para status de usuário, por exemplo.
  - **AutoMapper:** mapeia entidades para DTOs de saída (Results) e vice‑versa.
  - **FluentValidation:** define validadores para commands e inputs. Um pipeline de validação (ValidationBehavior) garante que qualquer handler receba entradas válidas antes de processá‑las.
  - **Autenticação JWT:** a camada Common/Security implementa gerador de tokens e AuthenticationExtension configura a autenticação e autorização por roles.
  - **Serilog e Health Checks:** para logging estruturado e monitoramento da saúde da API.
  - **Docker Compose:** orquestra a aplicação e os serviços de infraestrutura (PostgreSQL, MongoDB, Redis), facilitando a criação de ambientes consistentes.

### Executando os testes
O projeto inclui testes unitários, de integração e funcionais na pasta backend/tests. Para executá‑los todos de uma vez:

````
cd backend
dotnet test
````

Os testes unitários utilizam xUnit, NSubstitute e FluentAssertions. Os testes de integração pressupõem que os serviços de banco estejam disponíveis (você pode executá‑los com o Docker Compose ativo). <br> 
Há também scripts coverage-report.sh e coverage-report.bat para geração de relatórios de cobertura.

----------------------

### Dados do editor
<p>
Nome: Angelo Marques de Oliveira Silva <br>
Profissão: Tech Lead, desenvolvedor e arquiteto de software <br>
E‑mail: angelomarquesdeoliveira@gmail.com <br>  
</p>

### Licença

Este projeto está licenciado sob a MIT License. <br> Consulte o arquivo LICENSE para mais detalhes.

