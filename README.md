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

Você pode consultar uma versão interativa desta parte através da API entrando em [`localhost:5000/swagger`](htttp://localhost:5000/swagger):

### `Get /api/Client`

Esta é uma rota puramente criada para debug, ela não será suportada na versão final do Front-End, já que ela lista todos os clientes cadastrados na API com todos os dados a mostra.

> Se você rodar ela e não tiverem dados cadastrados ainda, ela irá criar alguns clientes fake

Exemplo de Resposta:

```json
[
  {
    "id": 1,
    "name": "string",
    "password": "string",
    "balance": 0
  }
]
```

### `Get /api/Client/{senha}/{id}`

Forma de buscar dados de um cliente específico, para que ela não retorne erro, é preciso colocar o campo `senha` e o `id` do cliente na url.

Exemplo de Resposta:

```json
{
  "id": 1,
  "name": "string",
  "balance": 0
}
```



### `Post /api/client`

Rota criada para criar clientes na API, para que retorne uma saída válida, temos que enviar o seguinte esquema de dados:

```json
{
  "name": "string",
  "password": "string"
}
```

Exemplo de Resposta:

```json
{
  "id": 1,
  "client": "Id do cliente",
  "date": "timestamp",
  "type": "CreateClient",
  "value": null,
  "receiver": null
}
```

