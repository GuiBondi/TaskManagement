sk Management API Descrição A Task Management API é uma aplicação desenvolvida em .NET 8 para gerenciar projetos e tarefas. O sistema inclui:

Projetos: Criação, gerenciamento e exclusão com validações específicas. Tarefas: Gerenciamento completo com histórico de alterações e suporte a comentários. Regras de Negócio: Limite de tarefas por projeto. Proibição de exclusão de projetos com tarefas pendentes. Registro automático de histórico de alterações. Controle de prioridades imutáveis após criação. Relatórios de desempenho acessíveis apenas por gerentes. Tecnologias Utilizadas .NET 8 Entity Framework Core (com SQLite e In-Memory para testes) Swagger (documentação da API) Docker (deploy containerizado) xUnit (testes unitários) Moq (mocks para testes) Microsoft.EntityFrameworkCore.InMemory (banco em memória para testes) C# 12 (aproveitando recursos mais recentes do .NET 8) Instalação e Execução Pré-requisitos .NET SDK 8.0+ Docker (opcional, para execução containerizada)

Clonar o Repositório git clone https://github.com/usuario/taskmanagement-api.git cd taskmanagement-api

Configurar o Ambiente Banco de Dados SQLite A API está configurada para usar SQLite como banco de dados. O arquivo taskmanagement.db será gerado automaticamente na primeira execução.

Executar o Projeto Localmente Execute o seguinte comando no diretório raiz do projeto:

dotnet run --project TaskManagement.Web

A API estará disponível em: http://localhost:5000

Documentação Swagger Acesse: http://localhost:5000/swagger

Testes Executar Testes Unitários Para rodar os testes unitários, use:

dotnet test

Cobertura de Testes Para verificar a cobertura de testes (usando Coverlet):

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov

Uso com Docker

Construir a Imagem Docker Execute o seguinte comando no diretório raiz:
docker build -t taskmanagementapi .

Executar o Contêiner Inicie o contêiner mapeando a porta 80 do contêiner para a porta local 5000:
docker run -d -p 5000:80 --name taskmanagementapi_container taskmanagementapi

A API estará disponível em: http://localhost:5000

Comandos do Entity Framework Core

Criar Migration Se precisar criar uma nova migration:
dotnet ef migrations add NomeDaMigration --project TaskManagement.Core --startup-project TaskManagement.Web

Atualizar Banco de Dados Para aplicar as migrations:
dotnet ef database update --project TaskManagement.Core --startup-project TaskManagement.Web

Estrutura do Projeto TaskManagementAPI/ ├── TaskManagement.Core/ # Lógica de domínio e repositórios ├── TaskManagement.Web/ # API (endpoints e configuração) ├── TaskManagement.Tests/ # Testes unitários ├── Dockerfile # Dockerfile para execução containerizada ├── README.md # Documentação do projeto └── taskmanagement.db # Banco de dados SQLite (gerado automaticamente)

Endpoints Disponíveis

Projetos Método Endpoint Descrição GET /projects Lista todos os projetos POST /projects Cria um novo projeto DELETE /projects/{id} Remove um projeto (com validações)
Tarefas Método Endpoint Descrição GET /projects/{id}/tasks Lista as tarefas de um projeto POST /projects/{id}/tasks Adiciona uma nova tarefa a um projeto PUT /tasks/{id} Atualiza uma tarefa (exceto prioridade) DELETE /tasks/{id} Remove uma tarefa POST /tasks/{id}/comments Adiciona comentário a uma tarefa GET /tasks/{id}/history Visualiza o histórico de uma tarefa Regras de Negócio Implementadas Prioridades de Tarefas:
Cada tarefa deve ter uma prioridade atribuída (baixa, média, alta). Não é permitido alterar a prioridade de uma tarefa após sua criação. Restrições de Remoção de Projetos:

Um projeto não pode ser removido se houver tarefas pendentes associadas a ele. A API retorna um erro e sugere a conclusão ou remoção das tarefas pendentes. Histórico de Atualizações:

Cada vez que uma tarefa é atualizada, um registro é adicionado ao histórico. O histórico inclui detalhes da modificação, data e usuário responsável. Limite de Tarefas por Projeto:

Cada projeto pode ter no máximo 20 tarefas. Tentativas de adicionar mais tarefas resultam em erro. Relatórios de Desempenho:

Endpoints para gerar relatórios, acessíveis apenas por usuários com função de "gerente". Comentários nas Tarefas:

Usuários podem adicionar comentários às tarefas. Comentários são registrados no histórico da tarefa.

Refinamento - Pergunta ao Product Owner (PO) Quais são as funcionalidades mais importantes a serem priorizadas nas próximas iterações?

Melhorias para o Projeto Autenticação e Autorização: Implementaria um sistema de autenticação para controlar o acesso aos recursos da API. Poderíamos utilizar JWT (JSON Web Token) para autenticação e autorização, garantindo maior segurança ao sistema.

Utilização de Azure como Cloud: Subiria a API para a Microsoft Azure, utilizando os serviços de containerização e orquestração disponíveis, como Azure Kubernetes Service (AKS) ou Azure App Service.

Docker para Implantação na Cloud: Configuraria a aplicação para rodar em um container do Docker na Azure, permitindo fácil escalabilidade, portabilidade e gerenciamento.

Essas melhorias garantiriam uma arquitetura mais robusta, segura e escalável, alinhada com práticas modernas de desenvolvimento e implantação.
