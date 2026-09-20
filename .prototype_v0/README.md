# Educa+

Sistema modular de gestão educacional para secretarias municipais, secretarias escolares e professores.

O projeto apresenta uma aplicação web responsiva construída com Next.js, React, TypeScript e Tailwind CSS. A identidade visual utiliza a marca Educa+, a logo externa do con3ktar e a paleta principal `#fcbf6b` e `#afab50`.

## Sumário

- [Visão geral](#visão-geral)
- [Perfis e permissões](#perfis-e-permissões)
- [Fluxo de acesso](#fluxo-de-acesso)
- [Funcionalidades](#funcionalidades)
- [Regras de notas](#regras-de-notas)
- [Importação por CSV](#importação-por-csv)
- [Como executar](#como-executar)
- [Estrutura do projeto](#estrutura-do-projeto)
- [Estado atual e limitações](#estado-atual-e-limitações)
- [Próximas evoluções](#próximas-evoluções)

## Visão geral

O Educa+ centraliza o acompanhamento da rede municipal de ensino em uma única interface. A aplicação possui uma entrada inicial com acesso separado para:

1. Acesso Administrativo, destinado ao Superadmin.
2. Secretaria Municipal.
3. Secretaria da Escola.
4. Professor.

Cada perfil possui seu próprio painel, menu e conjunto de operações. A navegação entre perfis representa a hierarquia de permissões do sistema.

## Perfis e permissões

### Superadmin

O Superadmin é o dono do sistema e possui acesso total às áreas administrativas.

Pode:

- Visualizar o painel geral da plataforma.
- Cadastrar, editar e excluir secretarias municipais.
- Selecionar qualquer município da Bahia.
- Cadastrar ou atualizar o secretário municipal com nome, telefone e e-mail.
- Acompanhar indicadores de secretarias, escolas e usuários.
- Visualizar a atividade do sistema.
- Consultar o fluxo hierárquico de permissões.

### Secretaria Municipal

A Secretaria Municipal administra sua rede municipal e possui acesso às áreas subordinadas.

Pode:

- Visualizar indicadores da rede.
- Cadastrar, editar e excluir escolas.
- Cadastrar, editar e excluir diretores vinculados às escolas.
- Acessar a visão da Secretaria da Escola.
- Acessar a visão do Professor.
- Visualizar gráficos de frequência e desempenho da rede.
- Gerar relatórios em PDF na interface demonstrativa.

### Secretaria da Escola

A Secretaria da Escola administra uma unidade escolar e seus usuários.

Pode:

- Visualizar os indicadores da escola.
- Cadastrar, editar e excluir professores.
- Cadastrar, editar e excluir turmas.
- Cadastrar, editar e excluir alunos por turma.
- Atribuir uma ou mais disciplinas a cada professor.
- Importar cadastros por CSV.
- Consultar relatórios e dados da unidade escolar.
- Acessar a visão do Professor.

### Professor

O Professor possui acesso somente ao próprio painel docente.

Pode:

- Registrar chamadas por turma.
- Planejar aulas, unidades e cursos.
- Inserir planos e conteúdos das aulas.
- Selecionar turma e disciplina.
- Inserir notas dos alunos.
- Consultar o desempenho por aluno e disciplina.
- Visualizar a média final e o resultado de cada aluno.

## Fluxo de acesso

A hierarquia de cadastro é:

```text
Superadmin
  └── cadastra Secretarias Municipais
        └── cadastram Secretarias das Escolas
              └── cadastram Professores
                    └── Professores registram chamadas, conteúdos e notas
```

As permissões esperadas são:

| Perfil | Acessos |
|---|---|
| Superadmin | Todas as seções |
| Secretaria Municipal | Própria tela + Secretaria da Escola + Professor |
| Secretaria da Escola | Própria tela + Professor |
| Professor | Apenas a própria tela |

## Funcionalidades

### Acesso Administrativo

Na tela inicial, o acesso do Superadmin é representado por um pequeno botão quadrado com ícone de cadeado. O botão possui `aria-label` e tooltip com o texto `Acesso Administrativo`.

A autenticação demonstrativa atualmente usa:

```text
E-mail: admin@educamais.com
Senha: Educa@2026
```

Essa autenticação é apenas de demonstração e mantém o estado durante a sessão do preview.

### Cadastros

As páginas de cadastro exibem ações individuais para:

- Editar um registro.
- Excluir um registro com confirmação.

Os cadastros contemplados são:

- Secretarias municipais.
- Secretários municipais.
- Escolas.
- Diretores.
- Professores.
- Turmas.
- Alunos.
- Atribuições de disciplinas.

### Disciplinas

A lista padrão de disciplinas é:

1. Língua Portuguesa
2. Matemática
3. Geografia
4. História
5. Ciências
6. Arte
7. Religião
8. Inglês
9. Educação Física
10. Diversificada

As disciplinas podem ser atribuídas individualmente aos professores da Secretaria da Escola. No perfil docente, a nota fica associada ao aluno e à disciplina selecionada.

### Chamadas

O módulo de chamadas permite selecionar uma turma e registrar a presença ou ausência dos alunos. O painel apresenta os registros e indicadores de frequência da turma.

### Relatórios e gráficos

Os painéis exibem indicadores e visualizações para apoiar o acompanhamento de:

- Frequência mensal.
- Desempenho dos alunos.
- Distribuição por modalidade.
- Quantidade de escolas, professores e alunos.
- Atividade de acessos por perfil.

## Regras de notas

O ano letivo possui três unidades:

- Unidade 1.
- Unidade 2.
- Unidade 3.

Cada unidade possui quatro avaliações. A soma das quatro avaliações representa o resultado da unidade e deve respeitar o limite máximo de 10 pontos.

```text
Total da unidade = avaliação 1 + avaliação 2 + avaliação 3 + avaliação 4
```

Para cada aluno e disciplina, o resultado final é calculado assim:

```text
Soma anual = unidade 1 + unidade 2 + unidade 3
Média final = soma anual / 3
```

Critério de resultado:

- `Média final >= 5,0`: Aprovado.
- `Média final < 5,0`: Conservado.

As notas são exibidas por aluno, disciplina, unidade, total da unidade, soma anual e média final.

## Importação por CSV

As telas de cadastro possuem as ações:

- `Baixar modelo CSV`.
- Importar arquivo CSV.

O modelo baixado contém cabeçalho e linha de exemplo. O separador recomendado é ponto e vírgula (`;`). O arquivo deve ser salvo em UTF-8.

### Modelos disponíveis

#### Escolas

```csv
nome_escola
Escola Municipal Aurora
```

#### Diretores

```csv
nome_diretor;email;escola
Maria Silva;maria@escola.edu.br;Escola Municipal Aurora
```

#### Professores

```csv
nome_professor;email
Ana Paula Mendes;ana@escola.edu.br
```

#### Turmas

```csv
nome_turma
5º Ano A
```

#### Alunos

```csv
nome_aluno;turma
João da Silva;5º Ano A
```

#### Notas

```csv
aluno;disciplina;unidade;avaliacao;nota
João da Silva;Matemática;1;1;7,5
```

### Recomendações para o arquivo

- Use a primeira linha como cabeçalho.
- Não deixe nomes obrigatórios vazios.
- Use uma linha para cada registro.
- Use `;` entre as colunas.
- Use valores numéricos válidos para as notas.
- Não ultrapasse 10 pontos no total de cada unidade.
- Confira se o aluno, a turma e a disciplina já existem antes de importar notas.

> Observação: a interface aceita a seleção de arquivos `.csv` e `.xlsx`, mas o leitor implementado no protótipo interpreta o conteúdo como CSV. Para importar Excel nativo de forma completa, será necessário adicionar um parser de planilhas, como `xlsx`, em uma etapa futura.

## Como executar

### Pré-requisitos

- Node.js compatível com Next.js 16.
- pnpm 12 ou versão compatível.

### Instalação

```bash
pnpm install
```

### Desenvolvimento

```bash
pnpm dev
```

Depois, abra `http://localhost:3000`.

### Build de produção

```bash
pnpm build
pnpm start
```

## Scripts disponíveis

| Comando | Finalidade |
|---|---|
| `pnpm dev` | Inicia o servidor de desenvolvimento |
| `pnpm build` | Gera o build de produção |
| `pnpm start` | Inicia a aplicação compilada |

## Estrutura do projeto

```text
.
├── app/
│   ├── globals.css       # Tokens, cores e estilos globais
│   ├── layout.tsx        # Layout e metadados da aplicação
│   └── page.tsx          # Interface principal e perfis do protótipo
├── public/               # Arquivos públicos do projeto
├── package.json          # Dependências e scripts
├── postcss.config.mjs    # Configuração do PostCSS
├── tsconfig.json         # Configuração do TypeScript
└── README.md             # Esta documentação
```

## Organização interna da aplicação

O arquivo `app/page.tsx` contém o protótipo modular por componentes e estados locais:

- `Landing`: tela inicial e seleção de perfil.
- `AdminLogin`: entrada do Acesso Administrativo.
- `Admin`: painel do Superadmin.
- `Municipal`: painel da Secretaria Municipal.
- `SchoolApp`: painel da Secretaria da Escola.
- `TeacherApp`: painel do Professor.
- `Shell`: estrutura comum dos painéis.
- `CsvUpload`: importação e download de modelos CSV.
- `Brand`: marca Educa+ e logo con3ktar.

## Identidade visual

Cores principais:

```text
Laranja: #fcbf6b
Oliva:   #afab50
```

A logo exibida atualmente é carregada de:

```text
https://con3ktar.nekoweb.org/logo.jpeg
```

Para produção, recomenda-se copiar a imagem para `public/` e referenciá-la localmente, evitando dependência de um domínio externo.

## Estado atual e limitações

Este projeto é um protótipo funcional de interface. Os dados são mantidos em estado React durante a sessão atual do navegador.

Ainda não há:

- Banco de dados persistente.
- Autenticação real com sessões no servidor.
- Controle de acesso validado no backend.
- Multi-tenancy persistente por município e escola.
- Upload real de arquivos Excel `.xlsx`.
- Geração de PDF no servidor.
- API para sincronizar os módulos.
- Auditoria persistente de alterações.
- Criptografia e gestão de credenciais em produção.

Por isso, não use as credenciais demonstrativas nem os dados do protótipo em um ambiente público sem implementar a camada de produção.

## Próximas evoluções

1. Adicionar autenticação real com e-mail, senha, sessões e recuperação de acesso.
2. Conectar banco de dados PostgreSQL.
3. Criar entidades de município, escola, usuário, turma, aluno, disciplina, chamada e nota.
4. Aplicar autorização no servidor conforme o perfil e o tenant.
5. Implementar RLS ou escopo obrigatório por município e escola.
6. Adicionar importação real de CSV e Excel com validação, pré-visualização e relatório de erros.
7. Implementar geração de relatórios PDF.
8. Persistir chamadas, planos, conteúdos e notas.
9. Adicionar trilha de auditoria para editar e excluir registros.
10. Criar testes automatizados para regras de notas e permissões.

## Contribuição

Antes de alterar uma funcionalidade:

1. Identifique o perfil responsável pela operação.
2. Preserve a separação de permissões.
3. Valide entradas obrigatórias.
4. Atualize o modelo CSV correspondente quando alterar um cadastro.
5. Teste o fluxo no preview.
6. Atualize este README quando houver mudança de regra ou arquitetura.

## Licença

Este projeto ainda não define uma licença pública. Consulte o responsável pelo sistema antes de redistribuir ou publicar o código.

## Suporte

Para problemas relacionados ao ambiente do v0 ou ao preview, consulte o suporte da Vercel em `https://vercel.com/help`.

Para regras de negócio, dados e permissões do Educa+, contate o responsável pelo sistema.

---

Educa+ — Gestão educacional conectada.
