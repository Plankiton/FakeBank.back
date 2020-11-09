# CidadeAlta Back-End

Uma API **ASP.NET Core** para se comunicar com o [Front-End]() feito em **React.JS**.

Eu não criei suporte a banco de dados para facilitar na execução desse projeto em outras máquinas, logo, todos os dados são gravados em memória.

> Apesar de ser bem simples de adicionar um banco de dados a esse projeto, basta configurar o `Models/ChallengeContext.cs`.

## Build - Como compilar

Para poder compilar o projeto você vai precisar do [sdk  `3.1`](https://dotnet.microsoft.com/download/dotnet-core/3.1) instalado em seu computador, e da ferramenta `dotnet` que já vem junto com o sdk.

Feito isso você tem que rodar os seguintes comandos em seu terminal:

```sh
$ cd /pasta/do/projeto
$ dotnet build
```

## Run - Como rodar a API

Para rodar a API, você vai ter que entrar na pasta `bin/Debug/netcoreapp3.1` em seu terminal:

```sh
$ cd bin/Debug/netcoreapp3.1
```

E é só rodar o comando correspondente ao seu sistema operacional:

> Linux / Mac / Unix

 ```sh
$ ./Challenge
 ```

> Windows

```sh
$ .\Challenge.exe
```

## Documentação da API

Você pode consultar uma versão interativa desta parte através da API entrando em `localhost:5000/swagger` :

