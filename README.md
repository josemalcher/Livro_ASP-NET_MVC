# Desenvolvimento web com Livro ASP.NET MVC

## <a name="indice">Índice</a>

- [Sua primeira aplicação](#parte1)
- [Entendendo a estrutura de uma aplicação ASP.NET MVC](#parte2)
- [Projetando a aplicação “Cadê meu médico?”](#parte3)
- [Models: Desenhando os modelos da nossa aplicação](#parte4)
- [Controllers: Adicionando comportamento a nossa aplicação](#parte5)
- [Views: interagindo com o usuário](#parte6)
- [Segurança: Criando sua área administrativa](#parte7)
- [Publicando sua aplicação](#parte8)

---

## <a name="parte1">Sua primeira aplicação</a>

requisitos necessários para a composição do primeiro exemplo:
• Microsoft Visual Studio Express 2012 para web [http://bit.ly/mvc-vsexpress](http://bit.ly/mvc-vsexpress) ;
• NET framework 4.5 (Visual Studio já incorpora esta instalação);
• Você encontrará um tutorial sobre como realizar a instalação destes recursos no apêndice deste livro.

código fote da aplicação que estamos construindo no livro pode ser encontrado no GitHub através desse link curto: [http://bit.ly/mvc-livrocodigofonte](http://bit.ly/mvc-livrocodigofonte)

Utilize sempre que necessário para referência nos estudos e contribua com o código fonte, faça um fork e aguardamos seu pull request. Além do código fonte, temos um grupo de discussão: [http://bit.ly/mvc-livrogrupodiscussao](http://bit.ly/mvc-livrogrupodiscussao)

Ele foi criado para conversarmos sobre ASP.NET MVC e dúvidas referentes ao livro. Aguardamos sua participação

[Voltar ao Índice](#indice)

---

## <a name="parte2">Entendendo a estrutura de uma aplicação ASP.NET MVC</a>

Os diretórios mais importantes de nossa aplicação (justamente por estarem incorporados à convenção) são: Models, Views, Controllers, App_Data e App_Start. Suas respectivas funções são apresentadas a seguir:

- Models: responsável por agrupar os modelos de dados que serão utilizados pela aplicação. Você pode entender como “modelo de dados” tudo que se aplica à expressão, como por exemplo: arquivos EDMX (modelos do Entity Framework, XML’s, webservices, entidades de negócio, DTO’s [(]em português, “Objeto de Transferência de Dados"] etc.);

- Views: agrupa os elementos a serem visualizados pelo usuário final (as famosas views). Nome bem sugestivo, não?!

- Controllers: reúne as classes (e seus respectivos métodos) responsáveis por definir os comportamentos da aplicação a nível de servidor. Se facilitar, você pode entender este diretório como sendo o coração de sua aplicação web;

- App_Data: aqui oMVC entende que você poderá posicionar arquivos a serem consumidos por seu projeto, como imagens, vídeos, áudios etc;

- App_Start: arquivos que implementamcomportamentos específicos e que devem ser inicializados junto com o projeto, devem ser posicionados neste diretório.

- **App_Code** ele não aparece na estrutura de diretórios da aplicação exemplo por um motivo muito simples: ele recebe arquivos Razor [3] provenientes de “Helpers” terceiros e a aplicação exemplo não implementa qualquer helper terceiro.

Convenção sobre Configuração

Controllers -> todo controller deve possuir o sufixo Controller, ou seja, EmailController, ContatoController, ClientesController etc.

As Views também possuem um diretório definido, e oMVC faz uso de uma convenção simples para identificar ou relacionar Views e Controllers. No caso do controller EmailController, uma pasta Email será criada dentro da pasta Views.

Observar no Projeto: Os controllers HomeController, AccountController e as pastas Home e Account para as Views

Navegação baseada em rotas

o ASP.NET MVC consegue entender que as três diferentes chamadas ( http://localhost:1153/ , http://localhost:1153/Home e  http://localhost:1153/Home/Index ) estão na verdade fazendo referência ao mesmo recurso.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _01_PrimeiraAPPWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

```
O primeiro faz menção ao fato de que se pode registrar novas e personalizadas rotas (vide o nome dométodo RegisterRoutes) para uma aplicação. O segundo reside justamente no fato de que o ASP.NET MVC já traz consigo a implementação de uma rota padrão de navegação e é ela que gera, de fato, o
comportamento apresentado.

Note que, para a rota Default ( name: "Default"), temos a atribuição do valor Home para o atributo Controller ( controller ="Home"), do valor Index para o atributo Action ( action = "Index") e do valor UrlParameter.Optional para o atributo id ( id = UrlParameter.Optional).

Se ligarmos os pontos, concluiremos que a rota apresentada por nossa aplicação de exemplo (ver novamente figura 2.2) atende ao padrão definido pelo framework. 

Veja a comparação, padrão implementado:

controller = "Home", action = "Index", id = UrlParameter.Optional

Vale lembrar mais uma vez que Home e Index são valores padrão na rota padrão do ASP.NET MVC. Opcionalmente, seria possível adicionar à URL um valor
chave para identificar algum objeto. Desta forma, poderíamos ter algo como http://localhost:1153/Home/Index/200 por exemplo. Entretanto,muitoembora seja possível realizar tal adição, no caso do exemplo discutido não faz sentido fazê-lo, pois não estamos identificando de forma unitária algum elemento (como http://localhost:1153/Clientes/Perfil/18556 ). Por este motivo, o último parâmetro da rota padrão é definido como opcional ( id = UrlParameter.Optional).

![MVC](https://github.com/josemalcher/Livro_ASP-NET_MVC/blob/master/img/mvc_asp-net.PNG?raw=true)



[Voltar ao Índice](#indice)

---

## <a name="parte3">Projetando a aplicação “Cadê meu médico?”</a>

“Cadê meu médico?” implementa justamente esta ideia: De forma rápida e fácil, os usuários poderão realizar consultas simples aos médicos disponíveis de forma segmentada por especialidade.

Assim, os seguintes recursos serão implementados:

- Área administrativa da aplicação: área de acesso restrito, onde os usuários precisarão realizar o processo de autenticação para ter o devido acesso. Além do mecanismo de login, dentro da área administrativa serão implementados todos os cadastros (médicos, especialidades, cidades e usuários);
- Gerenciamento de médicos: acoplada à área administrativa criaremos uma subárea para CRUD (Create, Read, Update e Delete) de novos médicos;
- Gerenciamento de especialidades médicas: também acoplada à área administrativa, será criada uma subárea para CRUD de especialidades médicas;
- Página pública para consulta de médicos por especialidade: criaremos uma página pública, onde disponibilizaremos os recursos necessários para que a consulta de médicos possa ser realizada;
- Versão do site para dispositivos móveis: conforme mencionado anteriormente, temos a preocupação de que nossa aplicação possua uma experiência adequada para dispositivos móveis. Desta forma, criaremos uma nova página (também pública) de consulta aos médicos mas, desta vez, voltada para dispositivos móveis.

![Cade Meu médico](https://github.com/josemalcher/Livro_ASP-NET_MVC/blob/master/img/cadeMeuMedico_macro.PNG?raw=true)

DER (Diagrama Entidade-Relacionamento) proposto. Para chegarmos a este modelo, estamos considerando algumas regras de negócio para o sistema. São as principais:

- Um médico deve possuir uma especialidade médica;
- Uma especialidade médica pode estar associada a diferentes médicos;
- Um médico poderá estar associado apenas a uma cidade;
- Uma cidade pode possuir diversos médicos;
- O sistema deverá controlar o acesso de usuários ao sistema administrativo.

![Cade Meu médico](https://github.com/josemalcher/Livro_ASP-NET_MVC/blob/master/img/DER.PNG?raw=true)

Para que possamos criar um bom nível de interatividade entre o usuário final e a aplicação Cadê meu médico?, utilizaremos um conhecido framework javascript, a saber, jQuery.

- jQuery: é uma das mais famosas e funcionais bibliotecas baseadas em javascript do mundo. Facilita o trabalho e manipulação dos objetos DOM (Document Object Model), chamadas Ajax, manipulação de eventos e animações;
- Twitter Bootstrap: trata-se de uma biblioteca de CSS (Cascading Style-Sheet) e componentes jQuery que facilita o trabalho de estruturar ou criar novos layouts para aplicações;

Ferramenta introduzida pela Microsoft no Visual Studio 2010 com suporte ao ASP.NET MVC a partir da versão 3, chamada NuGet. A lista a seguir apresenta alguns dos benefícios proporcionados pelo NuGet.

- Repositório de bibliotecas: o NuGet possui um repositório oficial onde qualquer biblioteca pode ser cadastrada e disponibilizada. Toda pesquisa é realizada nesse repositório público;
- Ranking: quando uma pesquisa é realizada, as bibliotecas são ordenadas pelo número de downloads realizados. Logo, os mais baixados aparecem no topo da lista;
- Instalação: as responsabilidades de download e instalação são do NuGet. Como usuários, precisamos informar apenas qual o projeto no qual a biblioteca deve ser instalada;
- Atualização: quando uma biblioteca é atualizada no repositório oficial do NuGet, ela entra na lista de bibliotecas que podem ser atualizadas no seu projeto. Isso é muito útil já que, para a maioria das bibliotecas, as versões com correções e melhorias de desempenho são liberadas frequentemente;
- Dependências: imagine que você precisa da biblioteca A, mas para funcionar ela depende da B—a automatização do download e instalação das dependências também é feita automaticamente pelo NuGet.

Umdosmuitos conceitos interessantes introduzidos pelo ASP.NET foi o deMaster Pages (páginas mestras). Através das famosas master pages, conseguimos reaproveitar muito do layout que é comum ao site.

```csharp

@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Layout</title>
</head>
<body>
    <div> 
    </div>
</body>
</html>

```

A propriedade “Layout” é atribuído o valor “null”. Pois é justamente esta a propriedade que recebe a indicação do layout padrão, ou seja, a master page da qual a página herdará.

Layout.chtml
```csharp

@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Cade meu medico? - @ViewBag.Title</title>
    <link href="@Url.Content("~/Content/bootstrap.min.css")"
          rel="stylesheet" />
    <style>
        body {
            padding-top: 60px;
        }
    </style>
</head>
<body>
<div class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <a class="navbar-brand" href="#">Cade meu medico?</a>
        </div>
        <div class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
                <li class="active"><a href="#">Home</a></li>
                <li id="menuPaises">
                    @Html.ActionLink("Países", "Index", "Paises")
                </li>
            </ul>
        </div>
    </div>
</div>
    
<div class="container">
        @RenderBody()
</div>

<script src="@Url.Content("~/Scripts/jquery-3.1.1.min.js")"></script>
<script src="@Url.Content("~/Scripts/bootstrap.min.js")"></script>

@RenderSection("script", required: false);
</body>
</html>

```

Além de básico, o HTML de nossa estrutura contém a referência às bibliotecas de CSS e JavaScript que utilizaremos na aplicação. Além disso, você deve ter notado que em alguns pontos utilizamos o caractere "@”. Pois saiba que é exatamente nestes pontos que estamos utilizando o ASP.NET Razor.

Outro aspecto a ser notado reside na definição do título das páginas de nossa aplicação. Estamos concatenando o texto “Cadê meu médico?” com "@ViewBag.Title". Como você deve se lembrar, quando personalizamos a mensagem da nossa primeira aplicação, utilizamos a propriedade ViewBag para enviar nossamensagem para a view. Agora estamos utilizando novamente a ViewBag,mas para mostrar nas páginas os valores predefinidos.

O Razor possui uma grande quantidade de métodos que auxiliam os desenvolvedores em grande escala no processo de construção de aplicações. Um deles é o "@Url.Content()”, que converte um diretório virtual relativo para um diretório absoluto na aplicação. Através disso, podemos vincular nossos arquivos .css e .js (por exemplo) sem nos preocuparmos com a estrutura de publicação da aplicação no IIS (Internet Information Services).

Lembre-se que todas as paginas irão utilizar o layout padrão, portanto, não precisamos repetir alguns códigos comuns, mas é preciso definir em nosso layout onde o código das páginas será renderizado em nosso layout padrão. É isso que estamos fazendo ao utilizarmos o método "@RenderBody()” do ASP.NET Razor. Quando uma view for renderizada, seu código será adicionado naquele ponto da página HTML. 

Um outro truque muito interessante do Razor são as Sections. Através delas, podemos definir seções onde determinados códigos serão renderizados. As futuras views de nossa aplicação possuirão código JavaScript próprio, e eles precisamser renderizados após a definição das bibliotecas JavaScript que estamos utilizando. Desta forma, como @RenderSection("script", required:false), criamos uma seção onde os scripts serão renderizados. Como nem toda página possuirá algum
script, essa seção não é obrigatória e por isso, utilizamos o "required:false”.


_ViewStart.chtml

```csharp

@{
    Layout = "~/Views/Layout.cshtml";
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>_ViewStart</title>
</head>
<body>
    <div> 
    </div>
</body>
</html>

```

TesteLayoutController.cs
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedicoAPP.Controllers
{
    public class TesteLayoutController : Controller
    {
        // GET: TesteLayout
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

Adicionar ao final -> "/testelayout"





[Voltar ao Índice](#indice)

---

## <a name="parte4">Models: Desenhando os modelos da nossa aplicação</a>

[Voltar ao Índice](#indice)

---

## <a name="parte5">Controllers: Adicionando comportamento a nossa aplicação</a>

[Voltar ao Índice](#indice)

---

## <a name="parte6">Views: interagindo com o usuário</a>

[Voltar ao Índice](#indice)

---

## <a name="parte7">Segurança: Criando sua área administrativa</a>

[Voltar ao Índice](#indice)

---

## <a name="parte8">Publicando sua aplicação</a>

[Voltar ao Índice](#indice)

---
