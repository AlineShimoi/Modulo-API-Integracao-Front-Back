# Modulo-API-Integracao-Front-Back
Repositório com o exercício de integração de Front e Back end com implementação de API.

Bootcamp Desenvolvimento Backend com .NET
Tarefa: Integração Front-End com Back-End .NET
Exercício prático individual

Contexto
Nas últimas aulas você construiu a MinhaPrimeiraApi, uma API REST completa com .NET que faz
CRUD de produtos usando SQLite. Ela tem endpoints para listar todos os produtos, buscar por ID,
criar, atualizar e deletar.
Nesta tarefa você vai criar um projeto Angular que se conecta a essa API e demonstrar cada operação
funcionando de ponta a ponta, desde a tela do navegador até o banco de dados no DBeaver.

Objetivo
  ● Criar um projeto Angular do zero que consome a MinhaPrimeiraApi.
  ● Implementar as 5 operações do CRUD na tela: listar todos, buscar por ID, criar, editar e
remover.
  ● Configurar o CORS no back-end para permitir chamadas do Angular.
  ● Validar cada operação no DBeaver, confirmando que o banco foi alterado.

Pré-requisitos
  ● MinhaPrimeiraApi funcionando localmente com dotnet run.
  ● Node.js e Angular CLI instalados (ng --version para verificar).
  ● DBeaver conectado ao arquivo minhaapi.db.

Parte 1 - Configurar o CORS na API
O Angular vai rodar em localhost:4200 e a API em localhost:5027 (ou a porta que aparecer no seu
terminal quando rodar dotnet run). O navegador bloqueia chamadas entre endereços diferentes por
segurança, esse mecanismo se chama CORS. Você precisa configurar a API para aceitar chamadas
do Angular.
Abra o arquivo Program.cs da MinhaPrimeiraApi e adicione:
  1. Antes do builder.Build():
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("PermitirAngular", policy =>
      {
          policy.WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
    });

2. Antes do app.MapControllers():
    app.UseCors("PermitirAngular");

Depois de salvar, pare a API (Ctrl+C) e rode dotnet run novamente.
Commit sugerido:
  git add .
  git commit -m "feat: configura CORS para Angular"

Parte 2 - Criar o projeto Angular
Abra o terminal em uma pasta fora do projeto .NET e execute:
  ng new produtos-front --routing=false --style=css --standalone
  cd produtos-front
  code .

Quando perguntar sobre SSR, responda N.

Commit sugerido:
  git init
  git add .
  git commit -m "init: projeto Angular criado"

Parte 3 - Criar o model e o serviço HTTP

3.1 Interface Produto
Crie o arquivo src/app/models/produto.model.ts:
  export interface Produto {
  id: number;
  nome: string;
  preco: number;
  }

3.2 Configurar HttpClient
No arquivo src/app/app.config.ts, adicione provideHttpClient():
  import { provideHttpClient } from '@angular/common/http';
  export const appConfig: ApplicationConfig = {
    providers: [
    // ... o que já existe ...
    provideHttpClient()
    ]
  };

3.3 Serviço de produtos
Gere o serviço:
  ng generate service services/produtos

Implemente os 5 métodos em src/app/services/produtos.service.ts:
  import { Injectable } from '@angular/core';
  import { HttpClient } from '@angular/common/http';
  import { Observable } from 'rxjs';
  import { Produto } from '../models/produto.model';

  @Injectable({ providedIn: 'root' })
  export class ProdutosService {
    private apiUrl = 'http://localhost:5027/api/Produtos';
    constructor(private http: HttpClient) {}
    listarTodos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.apiUrl);
  }

  buscarPorId(id: number): Observable<Produto> {
    return this.http.get<Produto>(`${this.apiUrl}/${id}`);
    }

  criar(produto: { nome: string; preco: number }):
    Observable<Produto> {
    return this.http.post<Produto>(this.apiUrl, produto);
    }

  atualizar(id: number, produto: { nome: string; preco: number }):
    Observable<Produto> {
    return this.http.put<Produto>(`${this.apiUrl}/${id}`, produto);
    }
  remover(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
  }

Verifique a porta da sua API no terminal onde você rodou dotnet run. Se for diferente de 5027, ajuste a apiUrl.

Parte 4 - Construir a tela
O AppComponent deve ter:
  ● Campo de busca por ID com botão Buscar: chama GET /api/Produtos/{id}
  ● Formulário de novo produto (nome + preço) com botão Adicionar: chama POST
  ● Tabela com todos os produtos : chama GET /api/Produtos
  ● Botão Editar em cada linha, abre formulário inline para atualizar: chama PUT
  ● Botão Remover em cada linha, chama DELETE

Você já viu tudo isso nas aulas de Angular. O desafio aqui é conectar cada ação ao endpoint certo da
sua própria API.

Parte 5 - Validar no DBeaver
Para cada operação realizada no Angular, abra o DBeaver, conecte no arquivo minhaapi.db e
execute:
  SELECT * FROM Produtos;

Confirme que o banco reflete o que você fez na tela:
  ● Criar um produto → linha nova aparece na tabela
  ● Editar um produto → os campos são atualizados na linha existente
  ● Remover um produto → a linha desaparece
  ● Buscar por ID → o produto aparece na tela (não altera o banco)

Entrega
Nenhum arquivo para enviar. A validação acontece na aula, o vídeo de resolução vai mostrar
exatamente como o projeto funciona. Use como referência para comparar com o que você fez.
Se travar em algum ponto, anote onde parou e qual foi o erro.

Dicas
  ● Repositório backend (MinhaPrimeiraApi):
    https://github.com/ThaysaRafaele/MinhaPrimeiraApi
  ● Repositório frontend (produtos-front):
    https://github.com/ThaysaRafaele/produtos-front
  ● Se a lista não carregar, verifique se a API está rodando (dotnet run no terminal do projeto .NET).
  ● Se aparecer erro de CORS no console do navegador (F12), confirme que o Program.cs foi salvo e a API foi reiniciada.
  ● O endpoint no Angular precisa ter o mesmo caminho que aparece no Swagger: /api/Produtos com P maiúsculo.
  ● Use o console do navegador (F12 → Console e Network) para ver o que está acontecendo nas chamadas HTTP.
