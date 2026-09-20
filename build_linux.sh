#!/bin/bash
echo "🚀 Iniciando Build para Linux (Standalone)..."

# Limpa builds anteriores
rm -rf EducaMais/bin/Release/net9.0/linux-x64/publish/
rm -rf Publish/Linux

# Publica o app C# de forma auto-contida (sem precisar instalar o .NET na escola)
dotnet publish EducaMais/EducaMais.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true

# Organiza a pasta final
mkdir -p Publish/Linux
cp EducaMais/bin/Release/net9.0/linux-x64/publish/EducaMais Publish/Linux/
cp EducaMais/bin/Release/net9.0/linux-x64/publish/*.db Publish/Linux/ 2>/dev/null || true

# Cria um arquivo Desktop (Atalho) padrão
cat << 'APP' > Publish/Linux/EducaMais.desktop
[Desktop Entry]
Version=1.0
Type=Application
Name=Educa+
Comment=Sistema de Gestão Escolar
Exec=./EducaMais
Icon=utilities-terminal
Terminal=false
Categories=Education;
APP

# Compacta tudo para o usuário
cd Publish
tar -czvf EducaMais_Linux_v1.0.tar.gz Linux/
echo "✅ Build concluído com sucesso! Arquivo em: Publish/EducaMais_Linux_v1.0.tar.gz"
