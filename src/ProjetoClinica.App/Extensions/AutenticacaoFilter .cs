using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoClinica.App.Extensions;

public class AutenticacaoFilterAttribute : Attribute, IAuthorizationFilter
{
    public string UsuarioLogado { get; set; }
    private readonly IUrlHelperFactory _urlHelperFactory;

    public AutenticacaoFilterAttribute(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var usuarioLogado = context.HttpContext.Session.GetString("UsuarioLogado");
        UsuarioLogado = usuarioLogado;
        if (string.IsNullOrEmpty(usuarioLogado))
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(context);
            var loginUrl = urlHelper.Action("Login", "Contas");

            context.Result = new RedirectResult(loginUrl);
        }
    }

    public void Logout(ActionContext context)
    {
        // Limpar as informações de autenticação, como a sessão ou cookies

        // Exemplo de limpeza da sessão
        context.HttpContext.Session.Clear();

        // Redirecionar para a página de login ou para outra página de sua escolha
        var urlHelper = _urlHelperFactory.GetUrlHelper(context);
        var loginUrl = urlHelper.Action("Login", "Contas");

        context.HttpContext.Response.Redirect(loginUrl);
    }
}
