using PrevisaoTempo.Aplicacao.Interface;
using PrevisaoTempo.Aplicacao.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PrevisaoTempo.Controllers
{
    public class CidadesController : Controller
    {
        private readonly ICidadeAppService _cidadeAppService;
        private readonly IPrevisaoTempoCidadeAppService _previsaoTempoCidadeAppService;

        public CidadesController(
            ICidadeAppService cidadeAppService,
            IPrevisaoTempoCidadeAppService previsaoTempoCidadeAppService
        )
        {
            _cidadeAppService = cidadeAppService;
            _previsaoTempoCidadeAppService = previsaoTempoCidadeAppService;
        }

        [HttpGet]
        public ActionResult BuscarTodas()
        {
            return Json(_cidadeAppService.BuscarTodas(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> BuscarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Nome da cidade é obrigatório.",
                    status = HttpStatusCode.BadRequest
                }, JsonRequestBehavior.AllowGet);
            }

            var cidadesVM = await _previsaoTempoCidadeAppService.BuscarCidadesPeloNomeAsync(nome);

            if (cidadesVM == null || !cidadesVM.Any())
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Cidade não encontrada.",
                    status = HttpStatusCode.NotFound
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                sucesso = true,
                conteudo = cidadesVM,
                status = HttpStatusCode.OK
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> PrevisaoTempo(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Id da cidade é obrigatório.",
                    status = HttpStatusCode.BadRequest
                }, JsonRequestBehavior.AllowGet);
            }

            var previsaoTempoVM = await _previsaoTempoCidadeAppService
                .BuscarPrevisaoTempoCidadeAsync(id);

            if (previsaoTempoVM == null || !previsaoTempoVM.Any())
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Previsão do tempo não encontrada para cidade.",
                    status = HttpStatusCode.NotFound
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                sucesso = true,
                conteudo = previsaoTempoVM,
                status = HttpStatusCode.OK
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Adicionar(CidadeViewModel cidadeVM)
        {
            try
            {
                _cidadeAppService.Adicionar(cidadeVM);
            }
            catch (ValidationException ex)
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = ex.Message,
                    status = HttpStatusCode.BadRequest
                });
            }

            return Json(new { sucesso = true, status = HttpStatusCode.Created });
        }

        [HttpPost]
        public ActionResult Excluir(long id)
        {
            if (id == 0)
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Id da cidade é obrigatório.",
                    status = HttpStatusCode.BadRequest
                }, JsonRequestBehavior.AllowGet);
            }

            var cidade = _cidadeAppService.BuscarPorId(id);
            _cidadeAppService.Excluir(cidade);

            return Json(new { sucesso = true, status = HttpStatusCode.OK });
        }
    }
}
