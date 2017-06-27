using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadeMeuMedicoAPP.Repositorios
{
    public class RepositorioCookies
    {
        public static void RegistraCookieAutenticacao(long IDUsuario)
        {
            //Criando um objeto cookie
            HttpCookie UserCookie = new HttpCookie("UserCookieAuthentication");

            //Setando o ID do usuário no cookie
            UserCookie.Values["IDUsuario"] =
                CadeMeuMedicoAPP.Repositorios.RepositorioCriptografia.Criptografar(IDUsuario.ToString());

            //Definindo o prazo de vida do cookie
            UserCookie.Expires = DateTime.Now.AddDays(1);

            //Adicionando o cookie no contexto da aplicação
            HttpContext.Current.Response.Cookies.Add(UserCookie);
        }
    }
}