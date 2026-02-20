using Microsoft.AspNetCore.Mvc;

namespace MvcNetCorePichonCSRF.Controllers
{
    public class TiendaController : Controller
    {
        public IActionResult Productos()
        {
            //SI EL USUARIO NO ESTÁ VALIDADO TODAVIA, LO LLEVAMOS
            //A DENEGADO
            if (HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Productos
            (string direccion, string[] producto)
        {
            if(HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("Denied", "Managed");
            } else
            {
                //LO LLEVAMOS A PEDIDO FINAL, ENVIANDO LA DIRECCCION
                //Y LOS PRODUCTOS
                //PARA ENVIAR INFO A OTRO CONTROLLER DEBEMOS 
                //UTILIZAR TEMPDATA
                TempData["PRODUCTOS"] = producto;
                TempData["DIRECCION"] = direccion;
                return RedirectToAction("PedidoFinal");
            }
        }

        public IActionResult PedidoFinal()
        {
            //RECUPERAMOS LOS PRODUCTOS
            string[] productos =
                TempData["PRODUCTOS"] as string[];
            ViewData["DIRECCION"] = TempData["DIRECCION"];
            return View(productos);
        }
    }
}
