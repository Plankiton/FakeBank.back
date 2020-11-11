# CidadeAlta Back-End

Uma API **ASP.NET Core** para se comunicar com o [Front-End]() feito em **React.JS**.

Eu não criei suporte a banco de dados para facilitar na execução desse projeto em outras máquinas, logo, todos os dados são gravados em memória. Além disso não foi inserido nenhum método avançado de autenticação já que o foco é ser apenas um protótipo simples.

> Apesar de ser bem simples de adicionar um banco de dados a esse projeto, basta configurar o `Models/ChallengeContext.cs`.

## Run - Como rodar a API

Para poder compilar o projeto você vai precisar do [sdk  `3.1`](https://dotnet.microsoft.com/download/dotnet-core/3.1) instalado em seu computador, e da ferramenta `dotnet` que já vem junto com o sdk.

Feito isso você tem que rodar os seguintes comandos em seu terminal:

```sh
$ cd /pasta/do/projeto
$ dotnet run
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
    "name": "string",
    "balance": 0
}
```

### `Delete /api/Client/{senha}/{id}`

Rota criada para remover clientes na API.

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

### `Get /api/Operation`

Esta é uma rota puramente criada para debug, ela não será suportada na versão final do Front-End, já que ela lista o **extrato** todos os clientes cadastrados na API com todos os dados a mostra.

> Se você rodar ela e não tiverem dados cadastrados ainda, ela irá criar alguns clientes fake

Exemplo de Resposta:

```json
[
  {
    "id": 1,
    "client": "1",
    "date": "timestamp",
    "type": "GetClient",
    "value": null,
    "receiver": null
  },
  {
    "id": 2,
    "client": "3",
    "date": "timestamp",
    "type": "TakeIn",
    "value": 46.80,
    "receiver": null
  }
]
```

### `Get /api/Operations/{senha}/{id}`

Forma de buscar o **extrato** de um cliente específico, para que ela não retorne erro, é preciso colocar o campo `senha` e o `id` do cliente na url.

Exemplo de Resposta:

```json
[
  {
    "id": 1,
    "client": "1",
    "date": "timestamp",
    "type": "GetClient",
    "value": null,
    "receiver": null
  },
  {
    "id": 2,
    "client": "1",
    "date": "timestamp",
    "type": "Trade",
    "value": 46.80,
    "receiver": "3"
  }
]
```

### `Post /api/TakeIn`

Rota usada para **depositar** dinheiro na conta de algum cliente, o formato de dados que tem que ser enviados para ele é:

```json
{
    "clientId": 1,
    "value": 0,
    "password": "string"
}
```

Exemplo de Resposta:

```json
{
  "id": 1,
  "client": "string",
  "date": "timestamp",
  "type": "string",
  "value": "string",
  "receiver": "string"
}
```



### `Post /api/TakeOut`

Rota usada para **sacar** dinheiro na conta de algum cliente, o formato de dados que tem que ser enviados para ele é:

```json
{
    "clientId": 1,
    "value": 0,
    "password": "string"
}
```

Exemplo de Resposta:

```json
{
  "id": 1,
  "client": "string",
  "date": "timestamp",
  "type": "string",
  "value": "string",
  "receiver": "string"
}
```

### `Post /api/Trade`

Rota usada para **transferir** dinheiro de uma conta para outra, o formato de dados que devem ser enviados é:

```json
{
  "senderId": 0,
  "receiverId": 0,
  "value": 0,
  "password": "string"
}
```

Exemplo de Resposta:

```json
{
  "id": 0,
  "client": "string",
  "date": "timestamp",
  "type": "Trade",
  "value": "valor",
  "receiver": "string"
}
```

