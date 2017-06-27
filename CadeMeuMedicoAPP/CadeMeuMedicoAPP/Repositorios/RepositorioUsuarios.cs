using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CadeMeuMedicoAPP.Models;
using CadeMeuMedicoAPP.Repositorios;

namespace CadeMeuMedicoAPP.Repositorios
{
    public class RepositorioUsuarios
    {
        public static bool AutenticarUsuario(string Login, string Senha)
        {
            var SenhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
            try
            {
                using (EntidadesCadeMeuMedicoBDEntities db = new EntidadesCadeMeuMedicoBDEntities())
                {
                    var queryAutenticaUsuarios = db.Usuarios.Where(x => x.Login == Login && x.Senha == SenhaCriptografada).SingleOrDefault();

                    if (queryAutenticaUsuarios == null)
                    {
                        return false;
                    }
                    else
                    {
                        RepositorioCookies.RegistraCookieAutenticacao(queryAutenticaUsuarios.IDUsuario);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Usuario RecuperaUsuarioPorID(long IDUsuario)
        {
            try
            {
                using (EntidadesCadeMeuMedicoBDEntities db = new EntidadesCadeMeuMedicoBDEntities())
                {
                    //var usuario = db.Usuarios.Where(u => u.IDUsuario == IDUsuario).SingleOrDefault();
                    var usuario = db.Usuarios.Where(u => u.IDUsuario == IDUsuario).SingleOrDefault();
                    return usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuario VerificaSeOUsuarioEstaLogado()
        {
            var usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            if (usuario == null)
            {
                return null;
            }
            else
            {
                long iDUsuario = Convert.ToInt64(RepositorioCriptografia.Descriptografar(Usuario.Value["IDUsuario"]));

                var usuarioRetornado = RecuperaUsuarioPorID(iDUsuario);
                return usuarioRetornado;

            }
        }
    }
}