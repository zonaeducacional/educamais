# Educa+ (Gestão Escolar Municipal)

## 📌 Objetivo do Projeto
O **Educa+** é um sistema Desktop multi-tenant desenvolvido sob a filosofia *Local-First* voltado para a administração de redes de ensino municipais.
Seu objetivo é resolver a desconexão estrutural, o retrabalho dos professores e a falta de visibilidade da Secretaria de Educação. O Educa+ unifica Secretários, Coordenadores, Professores e Alunos em um único sistema escalável e robusto.

## 🛠 Tech Stack
- **Frontend / UI**: C# e **Avalonia UI** (MVVM - Model-View-ViewModel)
- **Backend / BaaS**: **Appwrite** (PostgreSQL e Auth) rodando remotamente (ou via cloud)
- **Sincronização Offline**: **SQLite** local usando `Microsoft.Data.Sqlite` para o "Cache-First" 
- **Linguagem Principal**: C# (.NET 9.0)

## 🚀 Como Executar e Configurar

### Pré-requisitos
- .NET 9.0 SDK instalado (`dotnet --version`)
- Chaves do Appwrite configuradas no código (ou variáveis de ambiente).

### Rodando o Projeto (Modo de Desenvolvimento)
```bash
cd EducaMais
dotnet clean
dotnet run
```

### Funcionalidades Implementadas (Changelog MVP)
- **Fase 1 (Design UI)**: Construção da arquitetura de navegação de Master-Detail, Modais, e painéis baseados em Glassmorphism e gradientes.
- **Fase 2 (Integração BaaS)**: Implementação do `AppwriteService.cs` e suporte a multi-tenancy usando atributos Custom do banco. Configuração do esquema Document Level Security.
- **Fase 3 (Sincronização Offline)**: Criação do `SyncService.cs`. Agora o sistema faz dump de documentos no SQLite local para permitir navegação quando a rede cai no interior do município.
- **Fase 4 (Motor de Notas)**: Painel do professor com digitação fluida. Funciona off-line e envia um pacote Diff (payload) quando a rede volta, preservando `Conflict-Resolutions`.
- **Fase 5 (Portal do Aluno)**: Sistema inteligente que intercepta credenciais e loga diretamente os alunos sem eles passarem pelo painel administrativo, validando os dados no Appwrite de forma segura usando API Key restrita para regeneração de senhas de alunos (Patch password).
- **Fase 6 (Auditoria e Relatórios)**: Geração de CSV massivo com carimbo de tempo da Secretaria Municipal (`$createdAt`, `$updatedAt`) despejados com `StreamWriter` no Desktop. E geração dinâmica em HTML e impressão para PDF através do navegador (usando contornos `xdg-open` para ambientes Flatpak).

## 📄 Notas de LGPD
Todos os acessos são limitados pelo RBAC do Appwrite (Role-Based Access Control). Membros da escola só conseguem enxergar dados onde tenham a label "school:X". Professores apenas turmas atribuídas.


## 📦 Empacotamento e Distribuição (Fase 7)

O sistema foi configurado para gerar executáveis autônomos (`Self-Contained` e `PublishSingleFile`), o que significa que o computador de destino não precisa ter o .NET instalado.

### Gerando o Instalador para Windows (.exe)
```bash
./build_windows.sh
```
*O arquivo `EducaMais_Windows_v1.0.zip` será gerado na pasta `Publish/`.*

### Gerando o Binário para Linux
```bash
./build_linux.sh
```
*O arquivo `EducaMais_Linux_v1.0.tar.gz` será gerado na pasta `Publish/`.*
