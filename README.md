# API de Locação de Motos e Entregadores

Aplicação backend em **.NET (C#)** para gerenciar motos e entregadores, seguindo os requisitos do desafio. A API utiliza um banco de dados relacional (**PostgreSQL**) e um sistema de mensageria (**RabbitMQ**). O código adota **ASP.NET Core Web API**, com **Entity Framework Core** para acesso a dados, e inclui documentação automática via **Swagger/OpenAPI**. Imagens de CNH (PNG/BMP) são recebidas por endpoint e armazenadas externamente (por exemplo, em disco local).

---

## Tecnologias utilizadas

- .NET 8 + C# (ASP.NET Core Web API)  
- Entity Framework Core (ORM para Postgres)  
- PostgreSQL (banco de dados relacional)  
- RabbitMQ (broker de mensagens para eventos de domínio)  
- Docker & Docker Compose (para subir serviço e dependências)  
- AutoMapper, FluentValidation, Newtonsoft.Json  
- Swagger / OpenAPI (documentação interativa)

---

## Como executar a aplicação

**Pré-requisitos:** instale o .NET SDK compatível (ex.: .NET 8) e o Docker (opcional).

### 1. Clonar repositório
```bash
git clone https://github.com/GustavoMariano/Desafio-BackEnd.git
cd Desafio-BackEnd
```

### 2. Via Docker (recomendado)
- Ajuste as credenciais no `docker-compose.yml` ou `.env` se necessário (usuário/senha do Postgres, RabbitMQ, etc.).
- Suba os containers:
```bash
docker compose up --build -d
```
Isso criará containers do banco de dados, mensageria e (se configurado) da API.

### 3. Ou sem Docker
- Edite a connection string do banco em `Desafio-BackEnd.Api/appsettings.*.json` apontando para seu PostgreSQL local.
- Instale a ferramenta EF (se ainda não tiver):
```bash
dotnet tool install --global dotnet-ef
```
- Aplique as migrations:
```bash
dotnet ef database update \
  --project Desafio-BackEnd.Infrastructure \
  --startup-project Desafio-BackEnd.Api
```

### 4. Rodar a API
```bash
dotnet run --project ./Desafio-BackEnd.Api
```

### 5. Acessar a API
Por padrão a aplicação ficará disponível em `http://localhost:<porta>/` (verifique a porta no `launchSettings.json` ou no Docker). A UI do Swagger estará em:
```
http://localhost:<porta>/swagger
```

---

## Endpoints disponíveis (resumo)

> Consulte o Swagger (`/swagger`) para a especificação completa de request/response e códigos HTTP.

### Motos
- `POST /api/Motorcycle` — Cadastrar nova moto (id gerado pelo servidor). Ao cadastrar, um evento é publicado.
- `GET /api/Motorcycle` — Listar motos (aceita filtro por `plate` via query string).
- `GET /api/Motorcycle/{id}` — Obter moto por id.
- `PUT /api/Motorcycle/{id}/plate` — Atualizar apenas a placa.
- `DELETE /api/Motorcycle/{id}` — Remover moto (somente se não houver locações vinculadas).

### Entregadores (Couriers)
- `POST /api/Courier` — Cadastrar entregador (campos: id, name, cnpj único, birthDate, cnhNumber único, cnhType: `"A"`, `"B"` ou `"AB"`, cnhImagePath).
- `GET /api/Courier` — Listar entregadores.
- `GET /api/Courier/{id}` — Obter entregador por id.
- `POST /api/Courier/{courierId}/cnh` — Upload/atualização da imagem da CNH (`multipart/form-data`). Aceita apenas `.png` e `.bmp`. A imagem é salva em storage (não no banco).

### Locações (Rentals)
- `GET /api/Rental` — Listar locações.
- `POST /api/Rental` — Criar locação (valida habilitação, plano, datas; gera valores/multas conforme regras).
- `PATCH /api/Rental/{id}/devolution` — Registrar devolução e calcular valores finais (multa/extra conforme regras do plano).

*(Veja o Swagger integrado para detalhes de parâmetros, modelos e códigos HTTP.)*

---

## Observações / regras importantes (resumo)

- A placa da moto é **única**.  
- **CNPJ** e **número da CNH** são únicos por entregador.  
- **CNH** aceita categorias: `"A"`, `"B"`, `"AB"`.  
- Upload de CNH: apenas `.png` e `.bmp`; arquivo salvo em storage (disco local por padrão).  
- Planos de locação e regras financeiras:
  - 7 dias — R$30,00 / dia — multa 20% sobre diárias não efetivadas (se devolver antes).
  - 15 dias — R$28,00 / dia — multa 40% sobre diárias não efetivadas (se devolver antes).
  - 30 dias — R$22,00 / dia.
  - 45 dias — R$20,00 / dia.
  - 50 dias — R$18,00 / dia.
  - Devolução após a data prevista: R$50,00 por diária adicional.
- Somente entregadores habilitados na categoria `A` (ou `AB`, que inclui A) podem efetuar locação.

