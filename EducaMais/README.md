# Educa+ (Sistema de Gestão Escolar - Desktop/Linux)

## Objetivo do Projeto
O Educa+ é uma plataforma de gestão educacional multi-tenant projetada para atender Secretarias Municipais de Educação na Bahia. O sistema visa informatizar a administração escolar, matrículas, presenças e notas, estruturado hierarquicamente (Superadmin -> Secretaria Municipal -> Escola -> Professor). 
Esta versão é a refatoração e migração do antigo projeto Next.js (web) para uma aplicação **Desktop Nativa** robusta focada em Linux.

## Stack Tecnológico
- **Frontend / UI:** C# .NET 9.0, Avalonia UI, padrão MVVM (CommunityToolkit.Mvvm).
- **Backend / DB:** Appwrite 1.6.0 hospedado localmente (PostgreSQL sob o capô).
- **Paradigma Visual:** "Vibe Coding", focado em Alta Fidelidade (UI Pro Max), Glassmorphism, temas claros limpos e experiência de usuário premium sem delays de navegador.
- **Requisições de Banco:** Uso de HTTP REST Client nativo interagindo diretamente com o servidor Appwrite (bypass de SDK desatualizado).

## Instruções de Configuração e Execução
1. Certifique-se de ter o SDK do .NET 8 ou 9 instalado (`dotnet --version`).
2. Clone o repositório ou descompacte o arquivo.
3. No terminal, navegue até a pasta do projeto (onde está o `EducaMais.csproj`).
4. Execute `dotnet build` para compilar o projeto e restaurar as dependências (Avalonia UI).
5. Execute `dotnet run` para iniciar o aplicativo.

## Histórico de Modificações (Changelog)
- **Fase 1 (Design Base & Landing):**
  - Implementação da tela "Escolha seu Perfil".
  - Implementação da tela de Login Genérica baseada em perfis (Role-based).
- **Fase 2 (Painel Superadmin):**
  - UI de Painel Centralizado (Dashboard).
  - Integração nativa (HttpClient) com Appwrite 1.6.0 enviando strings JSON puras.
  - CRUD Completo de Secretarias: Leitura, Criação de novas cidades na base com ID auto-gerado, Edição rápida em Modal sobreposto e Exclusão imediata.
  - Carregamento assíncrono dos 417 municípios da Bahia pelo IBGE.
  - Suporte a "Acessar" (Impersonificação do Superadmin em Painel da Secretaria).

## Histórico de Modificações (Changelog)

### Versão 1.0 (MVP) - Módulo Professor (Teacher Dashboard) - 18/09/2026
* **Frequência (Chamada):** Implementado batch saving de chamadas com suporte a "Falta Justificada" e "Assunto da Aula".
* **Planejamento de Aulas:** Implementado CRUD para Planos de Aula, Unidade e Curso com suporte visual a anexos (PDF/DOCX).
* **Superplanilha de Notas:** Implementação de 2 modelos de avaliação intercambiáveis (Tradicional 4 Bimestres e Contínua 3 Unidades x 4 Avaliações).
* **Validação em Tempo Real:** Limite matemático de 10.0 pontos por unidade com formatação visual de erro e travamento no salvamento para evitar inconsistências.
* **Resumo Individual:** Quadro de "Resultado final por aluno" dinâmico indicando Aprovação baseada em métrica configurável pelo professor (5.0, 7.0, ou livre).
* **Prevenção de Crash:** Resolvido erro de StackOverflowException durante cálculos de nota e adicionado global crash logger no Program.cs.


### Versão 1.1 (Fase 2) - Alunos e Assistência Social - 19/09/2026
* **Módulo Aluno/Família:** Adicionada tela de consulta de histórico, permitindo login direto para visualização de notas e frequências cadastradas pelo professor.
* **Boletim na Escola:** O módulo da Secretaria da Escola (School Dashboard) ganhou um recurso visual para consolidar notas de múltiplas disciplinas e imprimir um Boletim interativo por aluno.
* **Integração Social (Bolsa Família):** O painel da Secretaria Municipal (Secretary Dashboard) agora consegue extrair um relatório gerencial cruzado. O sistema agrupa Escolas > Turmas > Alunos e localiza a Assiduidade.
* **Exportação PDF Vibe Coding:** Implementada a geração nativa de PDF abrindo relatórios oficiais formatados no navegador nativo para uso e assinatura do Secretário(a).
* **Fix de UI/Responsividade:** Refatoradas `LandingView` e `SecretaryDashboardView` removendo bugs de colunas de grid ocultas e botões duplicados.