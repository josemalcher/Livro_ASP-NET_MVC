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

“conexão combancos de dados” (que é o que faremos de fato em nossa aplicação exemplo) em uma aplicação ASP.NET MVC, ainda assim, estaremos falando de conexão através de models, entretanto, é de fundamental importância entender que para esta abordagem, existem diferentes formatos para se realizar tal procedimento. Os três mais difundidos no mercado são:  

- Através da utilização direta das classes ADO.NET;
ADOé o acrônimo para ActiveX Data Object. Trata-se de um conjunto de classes e recursos pertencentes ao conjunto de namespaces da plataforma .NET (daí o nome ADO.NET), disponibilizado para possibilitar o acesso às estruturas de bancos de
dados por parte das aplicações. Através dela, é possível realizar diferentes tipos de acessos.

![ADO.NET](https://github.com/josemalcher/Livro_ASP-NET_MVC/blob/master/img/ADO.PNG?raw=true)

- Através de ORM’s (no modelo database first);
- Através de ORM’s (no modelo code-first);
- Através de ORM’s (no modelo model-first).

ORM’s são recursos (ferramentas) criados para facilitar aos desenvolvedores o processo de conexão por parte de suas aplicações, às fontes de dados. Em linhas gerais, o que estas ferramentas fazem é criar uma camada de abstração em relação ao banco de dados, disponibilizando portanto um modelo em nível mais alto (programático) para acessar e manipular os dados.

- Simplicidade: talvez a principal característica proporcionada pelos ORM’s é a simplificação do modelo de acesso aos dados. Graças a esta simplificação, desenvolvedores podem trabalhar com as aplicações sem necessariamente possuírem
conhecimentos avançados de SQL e bancos de dados;
- Produtividade: como o desenvolvedor acessa e manipula os dados através de um modelo programático simplificado, desenvolvedores tendem a ser mais produtivos, uma vez que o modelo de desenvolvimento lhe é familiar;
- Redução de código: como o acesso aos dados é feito em um nível mais alto, automaticamente o número de linhas de código escritas para acessar os dados reduz drasticamente. Você poderá constatar este fato na medida em que avançamos com a criação da aplicação Cadê meu médico;
- Facilita amanutenção: omodelo disponibilizado pelosORM’s facilita emuito amanutenção da aplicação, pois amanipulação das operações se dá através de código já conhecido (objetos e seus respectivos métodos);
- Código elegante: o fato de o modelo mesclado de sentenças SQL e chamadas de métodos de objetos dar lugar a um código mais legível e semântico torna o código fonte da aplicação mais elegante.

Exemplo ADO:
```csharp
string stringDeConexao = "string para conexão com o banco de dados aqui";
SqlConnection objetoDeConexao = new SqlConnection(stringDeConexao);
SqlCommand objetoDeComando = objetoDeConexao.CreateCommand();
string sql = "INSERT INTO Clientes (NomeCompleto, Email)
values (@NomeCompleto, @Email)";
objetoDeComando.CommandText = sql;
objetoDeComando.CommandType = CommandType.Text;
objetoDeComando.Parameters.Add("@NomeCompleto", NomeCompleto.Text);
objetoDeComando.Parameters.Add("@Email", Email.Text);
objetoDeComando.ExecuteNonQuery();
```

Exemplo ORM:
```scharp
Clientes.Add();
Clientes.NomeCompleto = NomeCompleto.Text;
Clientes.Email = Email.Text;
Clientes.Save();
```

Entity Framework é um framework do tipo ORM que permite tratar e manipular dados como classes e objetos de domínio. Por ser desenvolvido, mantido e disponibilizado pela Microsoft, ele se integra de forma otimizada às tecnologias disponíveis na plataforma .NET comperformance, segurança e robustez. Por ser esta integração nativa, desenvolvedores podem utilizar também de forma natural dois grandes recursos da .NET framework: LINQ e expressões Lambda para recuperar e manipular os dados necessários à aplicação.

uma das características funcionais do Entity Framework e é conhecido como code first (código primeiro, em português). Para que esta ideia torne-se mais clara, vamos à prática. Criaremos um projeto paralelo, em nada relacionado ao Cadê
meu médico. Faremos desta forma porque para Cadê meu médico utilizaremos outra estratégia.

Quando falamos em code first, automaticamente falamos em classes POCO. Isso porque não é possível dissociar uma coisa da outra.

POCO é o acrônimo para Plain Old CLR Object. O termo é uma derivação do termo POJO (Plain Old Java Object), originalmente utilizado pela comunidade Java, como você pode imaginar. A ideia deste tipo especial de classe é ser um agente independente de frameworks e/ou componentes dentro da plataforma .NET. Assim, classes podem herdar seus comportamentos, interfaces podem ser implementadas e ainda, atributos podem ser persistidos.

Classe POCO para ’Categorias’:
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacaoComCodeFirst.Controllers
{
    public class Categorias
    {
        [Key]
        public int CategoriaID { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }

    
}
```
- Atributo [Key]: indica ao Entity Framework que a propriedade imediatamente na sequência (no caso, CategoriaID) deverá ser considerada chave primária no momento da geração do banco de dados no servidor de destino. É possível parametrizar este atributo conforme a necessidade para emplacar comportamentos específicos (como a adição ou não da cláusula identity). Para a criação do nosso exemplo,manteremos o padrão utilizado pelo EF para esta geração;

- public virtual ICollection<Posts> Posts { get; set; }: em uma ferramenta tradicional para composição de banco de dados relacionais, conseguimos explicitar os relacionamentos de 1 para muitos ou muitos para muitos de forma bem simples. Para expressar esse tipo de relacionamento através de code first, utilizamos uma coleção de objetos tipada; justamente o que estamos fazendo com a linha em destaque neste tópico. Estamos dizendo ao EF que uma Categoria deverá suportar múltiplos
Posts.

Classe POCO que representa ’Posts’:

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacaoComCodeFirst.Controllers
{
    public class Posts
    {
        [Key]
        public long PostID { get; set; }
        public string TituloPost { get; set; }
        public string ResumoPost { get; set; }
        public string ConteudoPost { get; set; }
        public DateTime DataPostagem { get; set; }
        public int CategoriaID { get; set; }
        [ForeignKey()("CategoriaID")]
        public virtual Categorias Categorias
        {
            get; set;
        }
}

```
A observação válida em relação à listagem 5 fica por conta da última propriedade. Diferentemente da classe Categorias onde possuímos uma coleção de objetos (sim, uma Categoria pode possuir vários Posts), aqui, possuímos uma instância de Categoria, já que, pela definição da regra de negócio, um Post pode pertencer a apenas uma Categoria. O atributo [ForeignKey("CategoriaID")], como você já deve estar imaginando, informa ao EF que a propriedade imediatamente a
seguir assumirá o comportamento de chave estrangeira e que, portanto, será o elo com outra tabela—em nosso caso, a tabela Posts.

Para que a engine do Entity Framework possa ser capaz de gerar a estrutura de banco de dados com base nas classes POCO, é preciso que exista uma classe de amarração, na qual são informados parâmetros cruciais no processo de geração da estrutura final. Esta classe foi convencionada pelo EF como classe de contexto. Nela, implementamos algumas ações que são fundamentais para que tudo funcione de forma adequada.

Classe de contexto para o Entity Framework:

```csharp
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AplicacaoComCodeFirst.Controllers;

namespace AplicacaoComCodeFirst.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
    }
}

```

A classe de contexto a qual nos referimos foi criada nos mesmos moldes das demais apresentadas até aqui. Em se tratando de modelos de dados, evidentemente está dentro do diretório Models.

Esta é a estrutura mais simplificada possível para a classe de contexto. Aqui estamos “dizendo” ao EF, através das diretivas DbSet, que as classes Posts e Categorias deverão ser criadas no banco de dados de destino.

Classe de contexto adaptada para utilizar um servidor específico:

```csharp
public class BlogContext : DbContext
{
public BlogContext() : base("name=BlogContext")
{
Database.Connection.ConnectionString =
@"data source=FABRCIOSANC36FC\SQLEXPRESS;
initial catalog=BlogBDLivro; Integrated Security=SSPI";
}
public DbSet<Categorias> Categorias { get; set; }
public DbSet<Posts> Posts { get; set; }
}
```

Scaffold é o recurso do ASP.NET MVC que permite criar operações de criação, leitura, atualização e remoção de registros no banco de dados de forma automatizada, utilizando boas práticas de desenvolvimento e a view-engine padrão (a saber, Razor). Com base na estrutura de banco de dadosmapeada, oASP.NETMVCé capaz de gerar as actions e respectivas views para possibilitar a realização das operações de CRUD.

- Model First with Entity Framework — MSDN: http://bit.ly/mvc-ef-modelfirst
- Entity Framework Tutorial: http://bit.ly/mvc-ef-tutorial

Scrypt do BD: https://gist.github.com/marcioalthmann/6276617

Code first: vantagens e desvantagens

- Isolamento do código fonte da aplicação emrelação ao banco de dados: esta é, sem dúvida, a grande vantagem do EF code first. Como o modelo é todo baseado em classes e seus respectivos objetos, é possível que desenvolvedores consigam criar aplicações com estruturas de bancos de dados complexas sem escrever uma linha sequer de SQL (Structured Query Language);
- Produtividade: como omodelo de desenvolvimento proporcionado pelo code first é bem familiar, de forma geral, é possível obter boa produtividade por parte dos desenvolvedores;
- Código limpo: a criação das classes POCO ajudam a manter o código limpo, uma vez que os desenvolvedores podem seguir os padrões de desenvolvimento de seus projetos;
- Controle completo de código: como as classes POCO são criadas e mantidas pelo próprio desenvolvedor, o controle sobre aquilo que é gerado no banco de dados é bem mais fácil de ser realizado. Classes geradas automaticamente tendem a ser de difícil manutenção;
- Simplicidade: de forma geral, o trabalho com code first tende a ser mais simples. Isso porque não existe a necessidade de se manter um arquivo .edmx. Qualquer problema gerado neste arquivo implicará necessariamente em problemas com a engine de acesso a dados.

Mas nem tudo são flores. Code first possui um “problema” intrínseco, proveniente do seu esquema de trabalho que limita sua utilização em boa parte dos projetos: de forma geral, ele pode ser aplicado apenas a novos projetos, já que, dependendo da complexidade do banco de dados de aplicações já existentes, fica inviável gerar classes POCO manualmente para obter a equivalência no banco de dados físico.

Dica: Se um novo projeto for seu objeto de estudo, considere utilizar o modelo code first. Os resultados deste tipo de abordagem para esta natureza de projeto costumam render excelentes frutos.

Database first: vantagens e desvantagens

- Isolamento completo das atividades: se o sistema já possui um banco de dados criado e/ou mantido por DBA’s, utilizar o modelo database first permitirá separar aindamais as responsabilidades do projeto, uma vez que a atualização do modelo baseado nas alterações realizadas diretamente no banco de dados funcionam muito bem;
- Reduz drasticamente o tempo de mapeamento de bancos pré-existentes: database first reduz de forma drástica o tempo dispensado ao mapeamento de bancos de dados preexistentes;
- Personalização de classes POCO: muito embora as classes POCO sejam geradas de forma automática pelo Entity Framework, é possível personalizá-las através de classes parciais ou até mesmo através do template de classes disponibilizado pelo EF;
- Possibilidade demúltiplos modelos no mesmo projeto: sim, é possível possuir diferentes mapeamentos, de diferentes bancos de dados, na mesma aplicação.

O principal problema relacionado ao modelo disponibilizado por database first reside na concentração de tudo dentro de um único arquivo (o edmx). Conforme o banco de dados de origem cresce em tamanho e complexidade, o arquivo também o
faz, o que dificulta a manutenção. Em função disso, na grande maioria das vezes, é preciso que as máquinas que manipulamestes arquivos possuam boas configurações de memória.

Dica: De forma geral, o modelo database first tende a ser melhor aplicável em situações em que existem aplicações comseus bancos de dados já em funcionamento (migração, reengenharia etc.), mas, diferentemente do modelo code first que pode ser aplicado na maioria esmagadora das vezes apenas em novas aplicações, se você deseja possuir controle total das ocorrências no banco de dados, database first pode ser aplicável também para novos projetos. Assim, no final das contas, este modelo acaba se tornando mais flexível.

Adicionando atributos de validação nos modelos

A framework .NET disponibiliza através do namespace System.ComponentModel.DataAnnotations vários atributos de validação que podem ser em modelos de classes do Entity Framework e Linq To SQL.

Adicionando esses atributos de validação nos models (modelos) da nossa aplicação, o MVC fará essa validação de forma transparente, sem necessidade de codificação adicional por parte do programador para a validação básicas sobre o model.

Anteriormente, utilizamos omodelo database first para gerar os models da nossa aplicação, ou seja, os models são automaticamente gerados, baseado na estrutura do banco de dados . Uma boa prática é não colocar os atributos de validação diretamente nas classes geradas automaticamente pelo Visual Studio. Isso porque sempre que o modelo do Entity Framework for atualizado com base no banco de dados, as classes serão recriadas, e com isso qualquer modificação feita anteriormente será perdida.

Seguindo essa boa prática, não iremos adicionar diretamente na classe gerada pelo Visual Studio os atributos de validação. Criaremos uma classe de metadados contendo as validações e, utilizando o recurso Partial Class ou Classes Parciais, vamos vincular a classe de metadado com a classe criada automaticamente.

Classes Parciais

Dentre vários recursos disponibilizados pela plataforma .NET, encontramos as Partial Class ou Classes Parciais. Graças às classes parciais podemos dividir a definição de uma classe em vários arquivos físicos. A keyword partial deverá ser utilizada na definição da classe para “dizer” que a classe é parcial.

Todas as classes geradas automaticamente utilizando Entity Framework recebem a keyword partial na sua definição. Devido a esse recurso conseguimos estender essas classes sem alterar o arquivo físico que é passível de modificação pelo Entity Framework.

MedicoMetadado.cs
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadeMeuMedicoAPP.Models
{
    [MetadataType(typeof(MedicosMetadado))]
    public partial class Medicos
    {
    }
    public class MedicosMetadado
    {
        [Required(ErrorMessage = "Obrigatório informar o CRM")]
        [StringLength(30, ErrorMessage = "O CRM deve possuir no máximo 30 caracteres")]
        public string CRM { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Nome")]
        [StringLength(80, ErrorMessage = "O Nome deve possuir no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Endereço")]
        [StringLength(100, ErrorMessage = "O Endereço deve possuir no máximo 100 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Bairro")]
        [StringLength(60, ErrorMessage = "O Bairro deve possuir no máximo 60 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o E-mail")]
        [StringLength(100, ErrorMessage = "O E-mail deve possuir no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório informar se Atende por Convênio")]
        public bool AtendePorConvenio { get; set; }

        [Required(ErrorMessage = "Obrigatório informar se Tem Clínica")]
        public bool TemClinica { get; set; }

        [StringLength(80, ErrorMessage = "O Website deve possuir no máximo 80 caracteres")]
        public string WebsiteBlog { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Cidade")]
        public int IDCidade { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a Especialidade")]
        public int IDEspecialidade { get; set; }
    }
}
```


[Voltar ao Índice](#indice)

---

## <a name="parte5">Controllers: Adicionando comportamento a nossa aplicação</a>

Os controllers serão responsáveis por receber e transformar as requisições enviadas pelas views em informações que serão utilizadas pelas regras de negócio da aplicação, ou seja, os models. O contrário também é verdadeiro, os controllers têm a responsabilidade de selecionar a view correta para apresentação de informações ao usuário. 

Ao receber uma requisição, os controllers devem efetuar alguma ação para que a requisição seja processada, essas ações são as actions. As actions são métodos definidos no controller, e o controller pode ter várias actions.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedicoAPP.Controllers
{
    public class MensagensController : Controller
    {
        public ActionResult BomDia()
        {
            return Content("Bom dia... hoje você acordou cedo!");
        }
        public ActionResult BoaTarde()
        {
            return Content("Boa tarde... não durma na mesa do trabalho!");
        }
    }
}

// /mensagens/boatarde
```

ActionResult é o tipo de retorno mais genérico existente dentro do framework ASP.NET MVC. Ele suporta qualquer tipo de resultado que possa ser retornado, sendo osmais comuns: JsonResult, ContentResult, EmptyResult, FileResult, JavaScriptResult, RedirectResult. Cada um dos mencionados nada mais é do que a extensão (através de herança) de ActionResult. Sua implementação é extremamente simples, contendo apenas um método para ser implementado na classe que a herda, haja vista que ActionResult é uma classe abstrata.

Desta forma, ContentResult trata-se de uma implementação extensiva da classe ActionResult, que permite formatar qualquer tipo de conteúdo que se deseja retornar, especificando inclusive, seu ContentType. Textos simples (como nosso exemplo) até sentenças mais complexas (como nodos de XML, por exemplo) podem ser retornados através de Content.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedicoAPP.Models;

namespace CadeMeuMedicoAPP.Controllers
{
    public class MedicosController : Controller
    {
        private EntidadesCadeMeuMedicoBDEntities db = new EntidadesCadeMeuMedicoBDEntities();

        public ActionResult Index()
        {
            //var medicos = db.Medicos.Include(m => m.Cidade).Include(m => m.Especialidade).ToList();
            var medicos = db.Medicos.Include("Cidades").Include("Especialidades").ToList();
            return View(medicos);
        }
    }
}
```
O aspecto a ser ressaltado aqui é o retorno da action Index. Como você pode perceber, estamos retornando uma lista ( .ToList()) de todos os médicos ( .Medicos) disponíveis no contexto (db).

A utilização dos métodos .Include(). A utilizar o método .Include, informamos ao Entity Framework que além do modelo (tabela) que estamos carregando, queremos obter seus relacionamentos, ou seja, nesse caso estamos listando os médicos e obtendo sua respectiva “Especialidade” e “Cidade”.

```csharp
@model IEnumerable<CadeMeuMedicoAPP.Models.Medicos>

@{
    ViewBag.Title = "Index";
}

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.Nome)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.IDCidade)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.IDEspecialidade)
        </th>
        
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Nome)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Cidades.Nome)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Especialidades.Nome)
        </td>
        
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.IDMedico }) |
            @Html.ActionLink("Details", "Details", new { id=item.IDMedico }) |
            @Html.ActionLink("Delete", "Delete", new { id=item.IDMedico })
        </td>
    </tr>
}

</table>


```

### Adicionar Médico

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedicoAPP.Models;

namespace CadeMeuMedicoAPP.Controllers
{
    public class MedicosController : Controller
    {
        private EntidadesCadeMeuMedicoBDEntities db = new EntidadesCadeMeuMedicoBDEntities();

        public ActionResult Index()
        {
            var medicos = db.Medicos.Include("Cidades").Include("Especialidades").ToList();
            return View(medicos);
        }

        public ActionResult Adicionar()
        {
            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome");
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades,"IDEspecialidade", "Nome");
            return View();
        }
    }
}
```

Podemos utilizar o ViewBag para transferir dados doController para aView. Ele é uma propriedade do tipo dynamic, por isso podemos criar propriedades dinamicamente. Em nossa action criamos duas propriedades, IDCidade e IDEspecialidade,
cada uma coma lista de Cidades e Especialidades quemais adiante será apresentada ao usuário.

Nas propriedades dinâmicas da ViewBag retornarmos já o elemento que será apresentado na View. Para isso utilizamos o helper SelectList—você verá mais sobre os Helpers no próximo capítulo sobre Views.

```csharp
@model CadeMeuMedicoAPP.Models.Medicos

@{
    ViewBag.Title = "Adicionar";
}

<h2>Adicionar</h2>

@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Medicos</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.CRM, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.CRM, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.CRM, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Nome, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Nome, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Nome, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Endereco, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Endereco, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Endereco, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Bairro, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Bairro, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Bairro, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Email, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Email, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Email, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.AtendePorConvenio, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(model => model.AtendePorConvenio)
                    @Html.ValidationMessageFor(model => model.AtendePorConvenio, "", new { @class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.TemClinica, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(model => model.TemClinica)
                    @Html.ValidationMessageFor(model => model.TemClinica, "", new { @class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.WebsiteBlog, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.WebsiteBlog, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.WebsiteBlog, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.IDCidade, "IDCidade", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("IDCidade", null, htmlAttributes: new { @class = "form-control" })
                //@Html.DropDownList("IDCidade", String.Empty)
                @Html.ValidationMessageFor(model => model.IDCidade, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.IDEspecialidade, "IDEspecialidade", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("IDEspecialidade", null, htmlAttributes: new { @class = "form-control" })
                //@Html.DropDownList("IDEspecialidade", String.Empty)
                @Html.ValidationMessageFor(model => model.IDEspecialidade, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

```

Agora que possuímos o fomulário pronto e funcionando, precisamos gravar os dados provenientes dele no banco de dados. Para isso, é preciso criar a action que receberá ummodel já comos dados que o usuário preencheu utilizando o formulário da view. Primeiro, é necessário compreender os verbos GET e POST do do protocolo HTTP. Apesar demais verbos estarem disponíveis no HTTP (DELETE, OPTIONS, entre outros), para a nossa aplicação de exemplo utilizaremos apenasGET
e POST.

Em linhas gerais, o verbo GET é utilizado para obter um recurso do servidor, enquanto o verbo POST serve para adicionar um novo recurso. Por recurso, entenda páginas, imagens, estilos, scripts, dados etc.

O código da action que receberá, em um parâmetro, o model preenchido pelo usuário na view. Além de validar o modelo e adicionar no banco de dados se nenhuma inconsistência for encontrada, note que a action também possui o nome Adicionar, entretanto, encontra-se decorada com o atributo HttpPost. Desta forma, pelo atributo decorativo da Action, o ASP.NET MVC consegue diferenciar os métodos com mesmo nome.

```csharp
 public ActionResult Adicionar()
        {
            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome");
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades,"IDEspecialidade", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Medicos medico)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome", "IDCidade");
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade", "Nome", "IDEspecialidade");
            return View(medico);
        }
```
O atributo HttpPost na action diz ao framework MVC qual action ele deve executar quando a requisição HTTP possuir o verbo POST. Isso é necessário porque a URL /Medicos/Adicionar é utilizada paramostrar o formulário e também para enviar os dados preenchidos. O que muda nas requisições são os verbos executados e os valores enviados para o servidor.

Também possui a action que será executada quando a requisição possuir o verbo GET e é ela quem retorna a página com o formulário. Apesar de o atributo HttpGet existir, ele não precisa ser adicionado porque actions sem atributo, por convenção, são consideradas HttpGet.

### Edição

```csharp
public ActionResult Editar(long id)
        {
            Medicos medico = db.Medicos.Find(id);

            ViewBag.IDCidade = new SelectList(db.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade","Nome", medico.IDEspecialidade);

            return View(medico);
        }

        [HttpPost]
        public ActionResult Editar(Medicos medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCidade = new SelectList(db.Cidades,"IDCidade","Nome",medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(db.Especialidades, "IDEspecialidade","Nome",medico.IDEspecialidade);

            return View(medico);
        }
```

```csharp
@model CadeMeuMedicoAPP.Models.Medicos

@{
    ViewBag.Title = "Editar";
}

<h2>Editar</h2>

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Medicos</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        @Html.HiddenFor(model => model.IDMedico)

        <div class="form-group">
            @Html.LabelFor(model => model.CRM, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.CRM, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.CRM, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Nome, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Nome, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Nome, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Endereco, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Endereco, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Endereco, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Bairro, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Bairro, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Bairro, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Email, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Email, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Email, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.AtendePorConvenio, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(model => model.AtendePorConvenio)
                    @Html.ValidationMessageFor(model => model.AtendePorConvenio, "", new { @class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.TemClinica, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(model => model.TemClinica)
                    @Html.ValidationMessageFor(model => model.TemClinica, "", new { @class = "text-danger" })
                </div>
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.WebsiteBlog, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.WebsiteBlog, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.WebsiteBlog, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.IDCidade, "IDCidade", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("IDCidade", null, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.IDCidade, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.IDEspecialidade, "IDEspecialidade", htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownList("IDEspecialidade", null, htmlAttributes: new { @class = "form-control" })
                @Html.ValidationMessageFor(model => model.IDEspecialidade, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

```

### Excluir

```csharp
[HttpPost]
        public String Excluir(long id)
        {
            try
            {
                Medicos medico = db.Medicos.Find(id);
                db.Medicos.Remove(medico);
                db.SaveChanges();
                return Boolean.TrueString;
            }
            catch
            {
                return Boolean.FalseString;
            }
        } // Ação implementada por Ajax no prox. cap.
```


[Voltar ao Índice](#indice)

---

## <a name="parte6">Views: interagindo com o usuário</a>

Visões (ou como a grandemaioria dos desenvolvedores prefere chamar—views) são os elementos que constituem a “ponta externa do iceberg” nas aplicações ASP.NET MVC.

O fluxo de operação do framework MVC até a view. Fonte: http://www.arrangeactassert.com/

![View](https://github.com/josemalcher/Livro_ASP-NET_MVC/blob/master/img/view.PNG?raw=true)

As famosas view engines ou simplesmente “engenhos de renderização”, são mecanismos presentes em todas as versões framework ASP.NET. Aqui chamamos de view engine omecanismo interno da plataforma (em nosso caso, ASP.NET) que possibilita a transformação de diretivas exclusivamente de servidor em código HTML, legível pelo navegador, ainda no container web para posterior exibição ao usuário final.

A ideia fundamental neste caso é a de proporcionar ao desenvolvedor o alto poder programático oferecido pelas linguagens de servidor — como as tags dos componentes de servidor do ASP.NET, por exemplo — e passar a responsabilidade de geração do código objeto no browser para o container web.

### ASPX ou ASP.NET Razor

O fato de ASP.NET se tratar de uma plataforma e não uma linguagem de programação. Justamente por este motivo, todas as tecnologias agrupadas na plataforma recebem o prefixo “ASP.NET”, como ASP.NET MVC.

Falando especificamente de view engines, atualmente encontram-se disponíveis na plataforma ASP.NET duas opções delas: ASP.NET (dos famosos arquivos com extensão *.ASPX) e ASP.NET Razor (esta última, mais recente). Assim, ao criar
um novo projeto web utilizando qualquer das tecnologias disponíveis na plataforma ASP.NET, o desenvolvedor poderá optar pela utilização daquela view engine quemelhor atende às suas necessidades.

ASP.NET Razor não é uma linguagem de programação. É, sim, uma nova forma de estruturar views que precisam de alguma porção de código processada no servidor de aplicação.

Costuma-se dizer que Razor é um modelo de escrita de código por não possuir uma linguagem predefinida. É possível utilizar o modelo de programação oferecido pelo Razor utilizando as duas principais linguagens da .NET framework: C# ou VB. No caso, arquivos Razor estruturados com base em C# possuem a extensão “*.cshtml”, enquanto arquivos estruturados com base em VB possuem a extensão “*.vbhtml”.

Repetindo itens em uma lista com ASPX:

```csharp
<% foreach(var item in itens) { %>
<span>
<%: item.ValorUnitario %>
</span>
<% } %>
```

Repetindo itens em uma lista com Razor:

```csharp
@foreach(var item in itens) {
<span>
@item.ValorUnitario
</span>
}
```
Estes trechos de códigos são decisivos para apresentarmos algumas conceitos iniciais importantes acerca da view engine Razor. A primeira observação pertinente em relação às abordagens é a diferença que demarca os blocos de código. Enquanto views que utilizamASPX utilizamdelimitadores “<% %>”, Razor utiliza apenas amarcação de “@”. Outra observação (não menos importante) é que o motor de renderização do Razor é “inteligente”. Perceba que, enquanto ASPX tem uma separação explícita entre suas tags e as HTML, Razor não a possui. Isso ocorre porque omotor de renderização do Razor separa de forma automática HTML de código C# (ou Visual Basic que também é suportado, é importante que se diga).

poderíamos utilizar uma abordagem conhecida como “Bloco de múltiplas linhas”. Caso uma linha seja suficiente para expressar um comportamento através do Razor, o método conhecido como “inline” pode ser utilizado. A listagem 3 apresenta a diferença entre as abordagens.

```csharp
@{
//Multi-linhas
ViewBag.Title = "Teste de layout";
var Data = DateTime.Now.DayOfWeek;
string StringConcatenada = "Hoje é '" + Data.ToString()
+ "'. Seja bem vindo(a)!";
}
<!-- Inline -->
<h2>@StringConcatenada</h2>
```

Como você pôde perceber, com a utilização do Razor é possível declarar variáveis e instanciar objetos, e muito mais. É possível também, criar estruturas de repetição que permitem ao desenvolvedor navegar
entre estruturas de dados simples (arrays, por exemplo) ou complexas (dicionários de dados, listasmulti-valoradas etc.). Se toda a estrutura C#/VB é suportada na view que utiliza Razor, estruturas de tomadas de decisão (falamos if/else, switch/case e suas variações) também as são — Tudo isso utilizando não apenas o modelo de programação orientado a objetos, já familiar para quem trabalha com .NET, mas principalmente, utilizando a mesma sintaxe.

Verificando valor de entrada na view com if/else:

```csharp
@{
ViewBag.Title = "Teste de layout";
var Data = DateTime.Now.DayOfWeek;
string StringConcatenada = Data.ToString();
}

<!-- Verificando o dia retornado -->
@if (StringConcatenada == "Friday") {
<h2>Sexta-feira</h2>
}
else
{
<h2>Outro dia qualquer da semana.</h2>
}
```

### Helpers

A criação de helpers é um recurso que se encontra disponível no contexto do ASP.NET Razor desde a primeira versão. Inicialmente, Razor permitia que os helpers apenas retornassem strings para as views. Felizmente, esta realidade mudou e, hoje, podemos criar helpers que retornamporções e HTML comobjetos atrelados. Isso dá ao desenvolvedor maior poder e, consequentemente, maior robustez às views.

é importante saber que existem dois modelos para a criação de helpers. A saber, na própria view onde ele será utilizado
ou de forma que ele possa ser reaproveitado ao longo de múltiplas views. Como o objetivo principal dos helpers é servir comomecanismo de refactoring de códigos na view,

O diretório “App_Code” é outra das convenções (CoC) implementadas pelo frameworkASP.NET MVC para diretórios. Ao adicionar uma classe ou arquivo Razor, o projetoMVC entenderá que o referido arquivo trata-se de um script que poderá ser reutilizado em outras partes da aplicação como um código agregado de função específica (como é o caso dos helpers).

### Prática

```sql
CREATE TABLE BannersPublicitarios
(
IDBanner BIGINT IDENTITY NOT NULL,
TituloCampanha VARCHAR(60) NOT NULL,
BannerCampanha VARCHAR(200) NOT NULL,
LinkBanner VARCHAR(200) NULL,
PRIMARY KEY(IDBanner)
);
INSERT INTO BannersPublicitarios
(TituloCampanha, BannerCampanha, LinkBanner) VALUES
('Campanha Conio', 'logo-conio-cademeumedico.png','http://conio.com.br')
INSERT INTO BannersPublicitarios
(TituloCampanha, BannerCampanha, LinkBanner) VALUES
('Campanha Casa do Código', 'banner-cdc-cademeumedico.png',
'http://casadocodigo.com.br')
```

BannersPublicitarios.cshtml

```csharp
@using CadeMeuMedicoAPP.Models;

@helper RetornaDoisBannersMaisRecentes() {
    var bd = new CadeMeuMedicoAPP.Models.EntidadesCadeMeuMedicoBDEntities();
    var banners = bd.BannersPublicitarios.OrderByDescending(b => b.IDBanner).Take(2);

    <div style="width: 100%; text-align: left; border: 1px solid #efefef; padding: 10px; display: inline-block;">
        
        @foreach (var b in banners)
        {
            <div style="width: 125px; height: 125px; float: left; margin-right: 10px;">
                <a href="@b.LinkBanner">
                    <img src="~/Uploads/Banners/@b.BannerCampanha" title="@b.TituloCampanha"/>
                </a>
            </div>
        }
    </div>
}
```

Alguns pontos importantes relacionados ao código apresentado pela listagem 6:

- Utilizamos a diretiva using direto da view. Lembre-se, aqui é programação C# da forma como você já conhece. O que Razor faz é possibilitar tal mecanismo em uma view. Nunca é demais lembrar;
- Através da diretiva “@helper” informamos ao framework que o trecho de código a seguir deverá se comportar como um helper;
- Na sequência, crio uma instância do modelo de dados, busco os dois banners mais recentes e exibo já na estrutura HTML com Razor.

Invocando o helper de banners publicitários:

```csharp
    @BannersPublicitarios.RetornaDoisBannersMaisRecentes();
```

Note que a chamada é simples de ser realizada. Basta informar o nome do arquivo helper (no caso “BannersPublicitarios”) e, na sequência, invocar o método “RetornaDoisBannersMaisRecentes()”. Isso automaticamente nos diz que um helper pode implementar vários métodos. Em nosso exemplo, poderíamos ter tranquilamente, um segundo método chamado “RetornaSeisBannersMaisRecentes()” etc.

Vale observar aqui que o modelo ideal seria retornar os dados através de um controller e, aí sim, com os dados já disponíveis, navegar entre eles através da view utilizando Razor.

### Helpers nativos

Ainda sobre os helpers, é importante notar o seguinte: existem aqueles personalizados (criados sob demanda, a exemplo do que fizemos no tópico anterior) e existem os helpers nativos do framework, que podem ser utilizados para otimizar algumas operações com views.

@Html é um helper nativo que encapsula a criação de componentes HTML através de código com Razor. O que queremos dizer com isso é que, quando você escreve a linha @Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }), por exemplo, você está dizendo ao motor de renderização do Razor que ele deverá realizar um render para a tag <a> do HTML em tempo de execução.

- Quando mudanças estruturais ocorrerem: para muitos tipos de projetos, o modelo de rotas padrão não endereça as demandas. Assim, ao adicionar uma rota personalizada para sua aplicação, por exemplo, no caso de a view estar estruturada com helpers como o @Html, de forma automática o framework é capaz de ajustar os links das aplicações.
- Linguagem mais familiar para desenvolvedores escreverem views: HTML é, de forma geral, mais natural para designers do que para desenvolvedores. Como a nomenclatura para a utilização dos helpers é mais próxima do C#, o processo de escrita de views para desenvolvedores acaba sendo mais suave.

De forma geral, o preço a se pagar pelo uso excessivo de helpers nas views é a singela perda de desempenho, uma vez que, ao fazê-lo, colocamos uma camada a mais de processamento sobre o processo de renderização (lembre-se, Razor é processado no servidor).

Vale mencionar, ainda, que existe uma grande quantidade de helpers já prontos, que realizam diferentes tarefas, disponíveis de forma gratuita no repositório público do NuGet. Dentre os mais baixados, podemos citar: integração com Twitter (a.k.a., Twitter.Goodies), Facebook, PayPal e Amazon. O processo de instalação destes helpers já é conhecido por você, uma vez que segue o mesmo padrão imposto pelo “Library PackageManager” (NuGet) do Visual Studio.


### Recursos para explicitar caminhos (path’s)

A Microsoft propôs um modelo extremamente funcional e elegante para realizar esta tarefa em suas view engines. Para que uma referência a determinado arquivo possa ser realizada, basta adicionar o símbolo “~” (til) no início do caminho absoluto. Desta forma, paraumcaminho absoluto “/Uploads/Imagens/” teríamos uma nova especificação deste mesmo caminho da seguinte forma: “~/Uploads/Imagens”. Ao assim fazer, estamos “dizendo” ao IIS que, independente do diretório atual, ele deve deve buscar o recurso informado a partir da raiz até o ponto especificado.

### Renderizações parciais

No sentido de desacoplar ainda mais a master page (você se lembra da história do “dividir pra conquistar"?), encontra-se disponível nas view engines da Microsoft um recurso conhecido como “renderizações parciais”.

A ideia com este recurso é que você renderize porções específicas de código em pontos distintos da master page. Imagine, por exemplo, que seja importante que o menu da aplicação seja carregado de forma assíncrona enquanto o restante da view
seja carregado no primeiro post.

Você poderia utilizar a diretiva “@RenderSection()” ou “@RenderPartial()” para buscar uma view parcial que carrega (de forma assíncrona comjQuery, por exemplo) seu menu. A utilização de “@RenderPartial()” já foi demonstrada neste livro, no
capítulo 3. A diferença básica de “@RenderPartial” para “@RenderSection” é que a primeira temo poder de renderizaruma viewparcial completa, e a segunda renderiza seções especificas. Para “@RenderPartial”, você deverá especificar a view parcial que será renderizada naquele ponto.

### Agregação e minificação

Estamos falando de espaços em branco, comentários e união das linhas (minificação), assim como, a junção em um único arquivo em memória (bundle) de todos os recursos deste tipo (agregação).

Referenciando os arquivos de forma manual em uma view:

```html
<link href="~/styles/reset.css" rel="Stylesheet" />
<link href="~/styles/styles.css" rel="Stylesheet" />
<link href="~/styles/content.css" rel="Stylesheet" />
<link href="~/styles/globals.css" rel="Stylesheet" />
<link href="~/styles/forms.css" rel="Stylesheet" />
<link href="~/styles/menu.css" rel="Stylesheet" />
```

Modelo de referencia contemplando agregação e minificação:

```html
<link href="~/styles" rel="Stylesheet" />
```

Quando a view Razor ou ASPX encontrar este modelo de referência, o que ela (framework) fará é verificar todos os arquivos CSS dentro do diretório apontado (neste caso “styles”), combiná-los, minificá-los e, na sequência, devolver uma resposta HTTP de um único arquivo CSS. Assim, ao invés de seis chamadas serem realizadas junto ao servidor, este número cai para apenas uma chamada.

### Mobilidade nativa com ASP.NET MVC

Quando falamos emmobilidade comASP.NETMVC, podemos imaginar aomenos três abordagens distintas, passíveis de implementação. Estas são apresentadas resumidamente na lista a seguir:

- Utilizar os mesmos controllers e views comdiferentes layouts Razor dependendo do dispositivo do usuário. A ideia aqui é que você utilize a mesma lógica implementada nos controllers e actions, assim como as mesmas views. Neste caso, seriamrenderizados em tempo de execução apenas os layouts. Esta abordagem é recomendada, de forma geral, quando se deseja apenas exibir dados (grids, por exemplo) de forma personalizada, ajustada ao dispositivo;
- Utilizar os mesmos controllers, entretanto, renderizando views específicas, de acordo com o dispositivo. Neste caso, ao invés de renderizarmos layouts personalizados, renderizamos views específicas. Esta opção deve ser sua escolha se você identificar a necessidade de modificar muito o HTML para os diferentes dispositivos. Outro aspecto importante a ser observado é se é preciso manter o fluxo de operações da aplicação ao longo dos dispositivos;
- Criar áreas separadas para projetos mobile e projetos desktop. Este é o modelo onde a separação de camadas é mais definitiva. Existem diferentes projetos, mobile e desktop (com diferentes controllers, actions e views) em uma mesma solução. Ao receber a solicitação, o framework identifica a resolução do dispositivo e direciona o fluxo de operação para o projeto adequado na solução.

Imagine que para a aplicação “Cadê meu médico?”, escolhêssemos a primeira abordagem apresentada pela lista anterior. Para que a aplicação seja capaz de renderizar o layout correto (claro, imaginando que exista um layout chamado “_LayoutMobile.cshtml”) bastaria escrever o código apresentado pela listagem 11 no interior do arquivo “_ViewStart.cshtml”.

Verificando o layout a ser renderizado:

```csharp
@{
Layout = Request.Browser.IsMobileDevice ?
"~/Views/Shared/_LayoutMobile.cshtml"
: "~/Views/Shared/_Layout.cshtml";
}
```

Se optássemos pela segunda abordagem, poderíamos criar uma sobrecarga do método “FindView” (de “ViewEngineResult”) para encontrar a view adequada:

Encontrando views específicas (sugestão de Scott Hanselman em seu blog):

```csharp
public class MobileCapableWebFormViewEngine : WebFormViewEngine
{
    public override ViewEngineResult FindView( ControllerContext controllerContext, string viewName, string masterName, bool useCache)
    {
        ViewEngineResult result = null;
        var request = controllerContext.HttpContext.Request;
        if (request.Browser.IsMobileDevice)
        {
            result = base.FindView(controllerContext, "Mobile/" + viewName, masterName, useCache);
        }
        if (result == null || result.View == null)
        {
            result = base.FindView(controllerContext, viewName, masterName, useCache);
        }
        return result;
    }
}
```
Escolhendo a terceira opção, poderíamos criar uma área específica para uma aplicaçãomóvel dentro de nosso projeto. Para isso, bastaria clicar como botão direito sobre ele e clicar na opção Add > Area

No interior dessa área podemos adicionar controllers e views normalmente, como se fosse um projeto à parte. Assim, para fins didáticos, podemos adicionar um controller “HomeController” dentro da nova área. Este controller poderá atuar como o novo controlador padrão da aplicação em dispositivos móveis, após isso, realizar dois pequenos ajustes no padrão rotas da aplicação (através dos arquivos “RouteConfig.cs”), para garantir que tal controller poderá ser alcançado

Ajustando a rota padrão da aplicação:

```csharp
//Alterando a rota padrão...
public override void RegisterArea(AreaRegistrationContext context)
{
    context.MapRoute(
    "Mobile",
    "Mobile/{controller}/{action}/{id}",
    new { controller = "Home", action = "Index", id = UrlParameter.Optional }
    );
    }
    
    //Dando prioridade para o controller de desktop
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        routes.MapRoute(
        "Default",
        "{controller}/{action}/{id}",
        new { controller = "Home", action = "Index",
        id = UrlParameter.Optional },
        // Adicione o namespace dos controladores desktop a seguir
        new[] { "CadeMeuMedico.Controllers" }
    );
}
```
Na sequência, para coroar o trabalho, bastaria criar um filtro que redirecionaria o usuário para o conteúdo mobile, se isto for pertinente.



[Voltar ao Índice](#indice)

---

## <a name="parte7">Segurança: Criando sua área administrativa</a>

[Voltar ao Índice](#indice)

---

## <a name="parte8">Publicando sua aplicação</a>

[Voltar ao Índice](#indice)

---
