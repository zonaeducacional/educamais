# Arquitetura Multi-Tenant com Appwrite (Educa+)

Para garantir isolamento de dados entre diferentes Secretarias Municipais (Tenants) utilizando uma única instância do Appwrite, utilizaremos a funcionalidade nativa de **Teams** (Equipes) combinada com permissões em nível de documento (Row Level Security).

## 1. Mapeamento
* **Tenant (Secretaria Municipal)** = `Team` no Appwrite. Ex: `team_sme_aurora`.
* **Superadmin** = Usuário normal com uma role ou label específica (ex: label `superadmin`), que permite bypass ou pertencimento global.
* **Secretário Municipal** = Membro do Team `team_sme_aurora` com a role `owner` ou `admin`.
* **Secretário Escolar / Diretor** = Membro do Team `team_sme_aurora` com a role `school_admin`.
* **Professor** = Membro do Team `team_sme_aurora` com a role `teacher`.

## 2. Permissões nas Coleções (Database)
O banco de dados principal (`EducaMaisDB`) conterá as seguintes coleções, e **TODOS** os documentos criados nelas terão suas permissões ajustadas na criação (no backend/appwrite).

* **Escolas (`schools`)**
  * *Read:* `team:team_sme_aurora`
  * *Write:* `team:team_sme_aurora/owner`
* **Turmas (`classes`)**
  * *Read:* `team:team_sme_aurora`
  * *Write:* `team:team_sme_aurora/school_admin`
* **Notas (`grades`)**
  * *Read:* `team:team_sme_aurora/school_admin`, `team:team_sme_aurora/teacher`
  * *Write:* Apenas o professor criador ou `school_admin`.

## 3. Fluxo de Criação (SDK)
Quando o `Superadmin` cadastrar um novo município:
1. O Appwrite cria um novo **Team** (ex: `team_id_xyz`).
2. Cadastra-se o usuário do secretário e convida-o (ou adiciona-o) para o team recém criado.

Quando a `Secretaria` cadastrar uma escola:
1. O aplicativo envia os dados junto com a permissão do Team que a secretaria pertence (obtido na sessão).
2. O Appwrite armazena o documento com `["team:{team_id}"]` como permissão de leitura.

Com isso, o Appwrite garante nativamente que a consulta `client.Databases.ListDocuments(...)` **nunca** retornará escolas ou alunos de outro município, evitando vazamento de dados, e dispensando cláusulas `WHERE municipality_id = X` em todas as queries no cliente!
