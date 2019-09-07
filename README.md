# Previsão do Tempo

Aplicação Web que disponibiliza o cadastro de cidades e a consulta da previsão do tempo para 5 dias/3 horas. Os dados meteorológicos são obtidos da API OpenWeatherMap.

### Arquitetura
No desenvolvimento foi seguida a modelagem DDD (Domain Driven Design), que significa Projeto Orientado a Domínio. Essa modelagem tem o objetivo de facilitar a compreensão das regras de negócio da aplicação. 

Estrutura/camadas do projeto:
1. **Aplicação**
    - Responsável pela comunicação entre a interface de usuário (Apresentação/UI) e a camada de regras de negócio (Domínio). Nessa camada também é realizada a comunicação com a API externa
2. **Apresentação**
    - Interface de usuário (UI)
3. **Domínio**
    - Camada onde ficam as negras de negócio
4. **Infra**
    - **CrossCutting**
        - Injeção de dependência (IoC)
    - **Data**
        - Persistência com o banco de dados
5. **Testes**
    - Testes de serviços e controller da aplicação
    
### Tecnologias
  - C#, .NET 4.7.2
  - ASP.NET MVC
  - EntityFramework (CodeFirst)
  - SQL Server
  - SimpleInjector
  - AngularJS
  - Newtonsoft.Json
  - Moq
  - xunit
    
### Design Patterns
 - MVC
 - DDD
 - Repository
 - IoC/DI
 
### Executando o Projeto
É necessário configurar a string de conexão (connectionString) no projeto PrevisaoTempo, após configurada assim que o projeto for iniciado será criada a base de dados.
