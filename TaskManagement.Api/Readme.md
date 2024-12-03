Task Management API
Descrição
A Task Management API é uma aplicação desenvolvida em .NET 8 para gerenciar projetos e tarefas. O sistema inclui:

Projetos: Criação, gerenciamento e exclusão com validações específicas.
Tarefas: Gerenciamento completo com histórico de alterações e suporte a comentários.
Regras de Negócio:
Limite de tarefas por projeto.
Proibição de exclusão de projetos com tarefas pendentes.
Registro automático de histórico de alterações.
Controle de prioridades imutáveis após criação.
Relatórios de desempenho acessíveis apenas por gerentes.
Tecnologias Utilizadas
.NET 8
Entity Framework Core (com SQLite e In-Memory para testes)
Swagger (documentação da API)
Docker (deploy containerizado)
xUnit (testes unitários)
Moq (mocks para testes)
Microsoft.EntityFrameworkCore.InMemory (banco em memória para testes)
C# 12 (aproveitando recursos mais recentes do .NET 8)
Instalação e Execução
Pré-requisitos
.NET SDK 8.0+
Docker (opcional, para execução containerizada)
1. Clonar o Repositório
   git clone https://github.com/usuario/taskmanagement-api.git
   cd taskmanagement-api

2. Configurar o Ambiente
   Banco de Dados SQLite
   A API está configurada para usar SQLite como banco de dados. O arquivo taskmanagement.db será gerado automaticamente na primeira execução.

3. Executar o Projeto
   Localmente
   Execute o seguinte comando no diretório raiz do projeto:

dotnet run --project TaskManagement.Web

A API estará disponível em: http://localhost:5000

Documentação Swagger
Acesse: http://localhost:5000/swagger

Testes
Executar Testes Unitários
Para rodar os testes unitários, use:

dotnet test

Cobertura de Testes
Para verificar a cobertura de testes (usando Coverlet):

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov

Uso com Docker
1. Construir a Imagem Docker
   Execute o seguinte comando no diretório raiz:

docker build -t taskmanagementapi .

2. Executar o Contêiner
   Inicie o contêiner mapeando a porta 80 do contêiner para a porta local 5000:

docker run -d -p 5000:80 --name taskmanagementapi_container taskmanagementapi

A API estará disponível em: http://localhost:5000

Comandos do Entity Framework Core
1. Criar Migration
   Se precisar criar uma nova migration:

dotnet ef migrations add NomeDaMigration --project TaskManagement.Core --startup-project TaskManagement.Web

2. Atualizar Banco de Dados
   Para aplicar as migrations:

dotnet ef database update --project TaskManagement.Core --startup-project TaskManagement.Web

Estrutura do Projeto
TaskManagementAPI/
├── TaskManagement.Core/ # Lógica de domínio e repositórios
├── TaskManagement.Web/ # API (endpoints e configuração)
├── TaskManagement.Tests/ # Testes unitários
├── Dockerfile # Dockerfile para execução containerizada
├── README.md # Documentação do projeto
└── taskmanagement.db # Banco de dados SQLite (gerado automaticamente)

Endpoints Disponíveis
1. Projetos
   Método	Endpoint	Descrição
   GET	/projects	Lista todos os projetos
   POST	/projects	Cria um novo projeto
   DELETE	/projects/{id}	Remove um projeto (com validações)
2. Tarefas
   Método	Endpoint	Descrição
   GET	/projects/{id}/tasks	Lista as tarefas de um projeto
   POST	/projects/{id}/tasks	Adiciona uma nova tarefa a um projeto
   PUT	/tasks/{id}	Atualiza uma tarefa (exceto prioridade)
   DELETE	/tasks/{id}	Remove uma tarefa
   POST	/tasks/{id}/comments	Adiciona comentário a uma tarefa
   GET	/tasks/{id}/history	Visualiza o histórico de uma tarefa
   Regras de Negócio Implementadas
   Prioridades de Tarefas:

Cada tarefa deve ter uma prioridade atribuída (baixa, média, alta).
Não é permitido alterar a prioridade de uma tarefa após sua criação.
Restrições de Remoção de Projetos:

Um projeto não pode ser removido se houver tarefas pendentes associadas a ele.
A API retorna um erro e sugere a conclusão ou remoção das tarefas pendentes.
Histórico de Atualizações:

Cada vez que uma tarefa é atualizada, um registro é adicionado ao histórico.
O histórico inclui detalhes da modificação, data e usuário responsável.
Limite de Tarefas por Projeto:

Cada projeto pode ter no máximo 20 tarefas.
Tentativas de adicionar mais tarefas resultam em erro.
Relatórios de Desempenho:

Endpoints para gerar relatórios, acessíveis apenas por usuários com função de "gerente".
Comentários nas Tarefas:

Usuários podem adicionar comentários às tarefas.
Comentários são registrados no histórico da tarefa.
Licença
Este projeto está licenciado sob a licença MIT. Consulte o arquivo LICENSE para mais detalhes.

Contato
Autor: Guilherme
E-mail: guilherme@example.com
GitHub: github.com/usuario