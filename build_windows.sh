#!/bin/bash
echo "🚀 Iniciando Build para Windows (Standalone)..."

rm -rf EducaMais/bin/Release/net9.0/win-x64/publish/
rm -rf Publish/Windows

# Publica o app C# para Windows
dotnet publish EducaMais/EducaMais.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Organiza a pasta final
mkdir -p Publish/Windows
cp EducaMais/bin/Release/net9.0/win-x64/publish/EducaMais.exe Publish/Windows/
cp EducaMais/bin/Release/net9.0/win-x64/publish/*.db Publish/Windows/ 2>/dev/null || true

cd Publish
zip -r EducaMais_Windows_v1.0.zip Windows/
echo "✅ Build concluído com sucesso! Arquivo em: Publish/EducaMais_Windows_v1.0.zip"
