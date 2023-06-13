using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ProjetoClinica.App.Extensions;
using ProjetoClinica.App.ViewModels;
using ProjetoClinica.Business.Contracts;
using ProjetoClinica.Business.Models;
using ProjetoClinica.Business.Services;
using ProjetoClinica.Data.Repository;

namespace ProjetoClinica.App.Controllers
{
    [Route("login")]
    public class ContasController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IMapper _mapper;

        public ContasController(IAuthenticationService authService, IMapper mapper, IUrlHelperFactory urlHelperFactory)
        {
            _authService = authService;
            _urlHelperFactory = urlHelperFactory;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] UsuarioViewModel usuarioViewmodel)
        {
            var usuario = _mapper.Map<Usuario>(usuarioViewmodel);
           
            if (ModelState.IsValid)
            {
                if (_authService.Login(usuario.Email, usuario.Senha))
                {
                    HttpContext.Session.SetString("UsuarioLogado", "true");
                    // Autenticação bem-sucedida, redirecionar para a página inicial
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou Senha inválidos!");
                }
            }

            return View(usuarioViewmodel);
        }

        public IActionResult Logout()
        {
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);
            var autenticacaoFilter = new AutenticacaoFilterAttribute(_urlHelperFactory);
            autenticacaoFilter.Logout(actionContext);

            return RedirectToAction("Login", "Contas");
        }
    }
}
