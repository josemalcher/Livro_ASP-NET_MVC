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
