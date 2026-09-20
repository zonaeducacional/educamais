# Product Requirements Document (PRD) - Educa+

## 1. Visão Geral (O que estamos construindo e por quê?)
O **Educa+** é um sistema modular de gestão educacional voltado para secretarias municipais, secretarias escolares e professores.
**Por quê:** Centralizar o acompanhamento da rede municipal de ensino em uma única interface, abandonando controles descentralizados e facilitando a gestão e o acesso à informação em tempo real por todas as esferas administrativas.
**Diretriz de Engenharia:** Foco máximo em estabilidade, confiabilidade a longo prazo, tipagem forte e manutenção previsível ("Tanque de Guerra").

## 2. Perfis e Hierarquia de Acesso (Multi-tenant)
O sistema respeitará a seguinte hierarquia:

1. **Superadmin (Proprietário):**
   - Acesso total à plataforma.
   - Cadastra e gerencia Secretarias Municipais.
   - Visualiza métricas gerais e logs de sistema.

2. **Secretaria Municipal:**
   - Visualiza apenas a sua rede (escolas do seu município).
   - Cadastra Escolas e Diretores.
   - Acompanha estatísticas agregadas (frequência, rendimento).

3. **Secretaria da Escola (Diretor/Coordenador):**
   - Visualiza apenas a sua escola.
   - Cadastra Professores, Turmas e Alunos.
   - Vincula disciplinas a professores.
   - Pode importar dados via CSV.

4. **Professor:**
   - Visualiza apenas suas turmas e disciplinas.
   - Registra chamadas (frequência) e notas (3 unidades).
   - Acompanha o rendimento da sua turma.

## 3. Funcionalidades Principais (MVP)
- **Autenticação e Autorização Real:** Login por e-mail/senha com controle de acesso rigoroso por perfil utilizando a lógica do Appwrite.
- **Gestão de Entidades (CRUDs):** Secretarias, Escolas, Usuários, Turmas, Alunos, Disciplinas.
- **Módulo de Importação:** Lógica em C# para leitura de arquivos CSV (cadastro em massa).
- **Módulo de Diário de Classe:** Lançamento de frequência e notas por unidade.
  - *Regra de Notas:* 3 Unidades no ano. Cada unidade soma até 10 pontos. Média Final = Soma das 3 unidades / 3. Aprovação se >= 5.0.
- **Dashboards:** Exibição de gráficos e indicadores estatísticos de gestão baseados nos dados.

## 4. Arquitetura Técnica Proposta
- **Front-end / Cliente:** Avalonia UI (C# / .NET).
  - Padrão arquitetural: MVVM (Model-View-ViewModel).
  - Interface baseada no protótipo original gerado, adaptada para os controles e recursos visuais do ecossistema .NET.
- **Back-end & Banco de Dados (BaaS):** Appwrite.
  - Hospedagem local (Self-hosted) no servidor físico Dell N4050.
  - Banco de Dados (Collections), Autenticação e Storage.
- **Rede / Infraestrutura:** Cloudflare.
  - Domínio estabelecido: `educamais.vibelab.app.br` (apontando para a instância do Appwrite).

## 5. Limitações e Escopo (O que NÃO faremos no V1)
- Estética 100% idêntica ao web moderno via Tailwind (o design será focado no pragmatismo do Avalonia, mantendo as cores e hierarquia visual).
- Integração com sistemas estaduais/federais externos.
- Módulos financeiros.

## 6. Próximos Passos
1. Validação final deste documento com o usuário.
2. Iniciar o comando `/speckit.plan` para desenhar o plano de implementação em C# / Avalonia e o schema do banco de dados no Appwrite.
