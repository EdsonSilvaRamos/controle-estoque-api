# Controle de Estoque

## Aplicação desenvolvida usando os conceitos de padrão de projeto Mediator

Foram utilizadas as tecnologias:
- .Net 8
- MediatR
- Dapper
- Entity Framework
- Pomelo Entity Framework Core (persistência com banco de dados MySql)

## Breves instruções de uso:

- Atualizar a string de conexão no arquivo appsettings
- Restaurar os pacotes
 - No Visual Studio:
   - Clicar com o botão direito na Solução e escolher a opção: **Restaurar Pacotes do Nuget** ou via comando CLI: ``` dotnet restore ```
 - No Visual Studio Code:
   - Rodar comando: ``` dotnet restore ```
- instalar o pacote dotnet-ef > ```dotnet tool install --global dotnet-ef``` (_Geralmente é necessário no VSCode_)
 - Rodar o comando ```update-database``` 
   - Visual Studio Code > ```dotnet ef database update```    
- E em seguida rodar o projeto para subir a api
 - Visual Studio Code > ```dotnet run```
