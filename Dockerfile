FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN apt-get update && apt-get install -y python3 && ln -sf /usr/bin/python3 /usr/bin/python
RUN dotnet workload restore EducaMais.Browser/EducaMais.Browser.csproj
RUN dotnet publish EducaMais.Browser/EducaMais.Browser.csproj -c Release -o /app/publish

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
