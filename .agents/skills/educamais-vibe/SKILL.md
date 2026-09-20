---
name: educamais-vibe
description: Diretrizes de Vibe Coding para Avalonia UI, Appwrite REST e MVVM do projeto Educa+
---

# Educa+ Vibe Coding Skill

Esta skill define as regras de ouro para o desenvolvimento do sistema Educa+ (Desktop nativo em C# / Avalonia).

## 1. Interface Gráfica (Avalonia UI)
- **Aesthetic:** O sistema DEVE seguir o paradigma "UI Pro Max".
- **Visuals:** Use cores sofisticadas (ex: fundos `#f4f4f4` ou `#ffffff`, toques em dourado `#a98a44` ou azul-acinzentado).
- **Componentes:** Abuse de `Border` com `CornerRadius`, sombras leves (`BoxShadow="0 2 10 0 #05000000"`) e ícones nítidos.
- **Transições:** Quando possível, adicione animações simples de hover ou transições suaves entre telas.

## 2. Padrão Arquitetural (MVVM)
- **Framework:** Utilize o `CommunityToolkit.Mvvm`.
- **ViewModels:** Herde de `ViewModelBase`. Use atributos `[ObservableProperty]` para variáveis e `[RelayCommand]` para comandos.
- **Roteamento:** A navegação ocorre injetando o novo ViewModel na propriedade `CurrentViewModel` da `MainViewModel`. Não abra novas janelas a menos que explicitamente solicitado. Para voltar, emule uma ação de "Logout" ou crie um callback de "OnBack".

## 3. Banco de Dados (Appwrite REST API)
- **ALERTA CRÍTICO DE SDK:** O SDK oficial do Appwrite para C# (v1.9.5+) quebra ao tentar consumir o Appwrite Server v1.6.0 devido à ausência da chave `$sequence`. **NÃO USE** os métodos `Databases.CreateDocument` ou `Databases.ListDocuments` do SDK.
- **A Abordagem:** Use `HttpClient` puro dentro da `AppwriteService` (`RawListDocumentsAsync`, `RawCreateDocumentAsync`, etc).
- **Sintaxe de Queries (Appwrite 1.5+):** Ao enviar consultas GET via REST, o array `queries[]` EXIGE uma string formatada em JSON válido. Exemplo correto: 
  `queries[]={"method":"equal","attribute":"name","values":["Valor"]}`
- **Multi-tenancy:** Os dados são isolados por municípios. Cada município cadastrado recebe um ID que pode ser usado para rotear permissões (Teams e Roles) garantindo que o usuário de uma cidade não veja os dados da outra.
