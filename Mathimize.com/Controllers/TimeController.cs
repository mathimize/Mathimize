using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mathimize.com.Models;
using Mathimize.com.Services;
using System.IO;
using EO.Pdf;
using System.Configuration;

namespace Mathimize.com.Controllers
{
    public class TimeController : Controller
    {
        //
        // GET: /Time/

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult OClocks(string resultId)
        {
            if (!string.IsNullOrEmpty(resultId))
            {
                TimeResultViewModel model = (TimeResultViewModel)Session[resultId];
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model );
            }
            else
            {
                TimeResultViewModel model = new TimeResultViewModel() { Op = "OClocks", HourMinutes = null, Cols = 3, Rows = 6 };
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model);
            }
        }


        [HttpPost]
        public ActionResult OClocks(TimeResultViewModel model)
        {
            if (ModelState.IsValid)
            {
                MathService svc = new MathService();
                TimeResultViewModel newModel = svc.GetTimeResultViewModel(model);
                newModel.Op = "OClocks";
                return RedirectToAction("OClocks", new { resultId = newModel.ResultId });
            }
            else
            {
                return View(model);
            }
        }






        [HttpGet]
        public ActionResult Halves(string resultId)
        {
            if (!string.IsNullOrEmpty(resultId))
            {
                TimeResultViewModel model = (TimeResultViewModel)Session[resultId];
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model );
            }
            else
            {
                TimeResultViewModel model = new TimeResultViewModel() { Op = "Halves", HourMinutes = null, Cols = 3, Rows = 6 };
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model);
            }
        }


        [HttpPost]
        public ActionResult Halves(TimeResultViewModel model)
        {
            if (ModelState.IsValid)
            {
                MathService svc = new MathService();
                TimeResultViewModel newModel = svc.GetTimeResultViewModel(model);
                newModel.Op = "Halves";
                return RedirectToAction("Halves", new { resultId = newModel.ResultId });
            }
            else
            {
                return View(model);
            }
        }





        public string RenderViewAsString(string viewName, object model)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    stringWriter
                    );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }

        [HttpGet]
        public ActionResult ExportResultToPDF2(string resultId)
        {
            TimeResultViewModel model = (TimeResultViewModel)Session[resultId];
            string htmlToConvert = RenderViewAsString("PDFView", model);


            HttpResponseBase response = HttpContext.Response;
            response.Clear();
            response.ClearHeaders();
            response.ContentType = "application/pdf";

            //Convert to the output stream
            string key = ConfigurationManager.AppSettings["PDFKey"];
            if (!string.IsNullOrEmpty(key))
                EO.Pdf.Runtime.AddLicense(key);



            String baseUrl = this.ControllerContext.HttpContext.Request.Url.Host + ":" + this.ControllerContext.HttpContext.Request.Url.Port.ToString() + "/";

            HtmlToPdfOptions options = new HtmlToPdfOptions();
            options.FooterHtmlFormat = "Mathimize.com - Where your kids can Maximize Their Math Skills";
            options.BaseUrl = baseUrl;
            EO.Pdf.HtmlToPdf.ConvertHtml(htmlToConvert, response.OutputStream, options);

            response.End();

            return null;
        }





    }
}
