using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mathimize.com.Models;
using Mathimize.com.Services;
using EO.Pdf;
using System.IO;
using System.Configuration;

namespace Mathimize.com.Controllers
{
    public class AdditionController : Controller
    {
        //
        // GET: /Addition/

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Tens(string resultId)
        {
            if (!string.IsNullOrEmpty(resultId))
            {
                BasicArithmeticResultViewModel model = (BasicArithmeticResultViewModel)Session[resultId];
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model );
            }
            else
            {
                BasicArithmeticResultViewModel model = new BasicArithmeticResultViewModel() { Op = "Tens", Terms = null, Cols = 7, Rows = 9, MaxInt = 10 };
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model);
            }
        }


        [HttpPost]
        public ActionResult Tens(BasicArithmeticResultViewModel model)
        {
            if (ModelState.IsValid)
            {
                MathService svc = new MathService();
                model.MaxInt = 9;
                BasicArithmeticResultViewModel newModel = svc.GetBasicArithmeticResultViewModel(model);
                newModel.Op = "Tens";
                return RedirectToAction("Tens", new { resultId = newModel.ResultId });
            }
            else
            {
                return View("WebsiteView", model);
            }
        }





        [HttpGet]
        public ActionResult Doubles(string resultId)
        {
            if (!string.IsNullOrEmpty(resultId))
            {
                BasicArithmeticResultViewModel model = (BasicArithmeticResultViewModel)Session[resultId];
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model );
            }
            else
            {
                BasicArithmeticResultViewModel model = new BasicArithmeticResultViewModel() { Op = "Doubles", Terms = null, Cols = 7, Rows = 9, MaxInt = 10 };
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model);
            }
        }


        [HttpPost]
        public ActionResult Doubles(BasicArithmeticResultViewModel model)
        {
            if (ModelState.IsValid)
            {
                MathService svc = new MathService();
                BasicArithmeticResultViewModel newModel = svc.GetBasicArithmeticResultViewModel(model);
                newModel.Op = "Doubles";
                return RedirectToAction("Doubles", new { resultId = newModel.ResultId });
            }
            else
            {
                return View("WebsiteView", model);
            }
        }


        [HttpGet]
        public ActionResult BasicAddition(string resultId)
        {
            if (!string.IsNullOrEmpty(resultId))
            {
                BasicArithmeticResultViewModel model = (BasicArithmeticResultViewModel)Session[resultId];
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model );
            }
            else
            {
                BasicArithmeticResultViewModel model = new BasicArithmeticResultViewModel() { Op = "BasicAddition", Terms = null, Cols = 7, Rows = 9, MaxInt = 10 };
                return View("WebsiteView", model);
                //return View("Tens", "_FormLayout", model);
            }
        }


        [HttpPost]
        public ActionResult BasicAddition(BasicArithmeticResultViewModel model)
        {
            if (ModelState.IsValid)
            {
                MathService svc = new MathService();
                BasicArithmeticResultViewModel newModel = svc.GetBasicArithmeticResultViewModel(model);
                newModel.Op = "BasicAddition";
                return RedirectToAction("BasicAddition", new { resultId = newModel.ResultId });
            }
            else
            {
                return View("WebsiteView", model);
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
            BasicArithmeticResultViewModel model = (BasicArithmeticResultViewModel)Session[resultId];
            string htmlToConvert = RenderViewAsString("PDFView", model);


            HttpResponseBase response = HttpContext.Response;
            response.Clear();
            response.ClearHeaders();
            response.ContentType = "application/pdf";

            //Convert to the output stream
            string key = ConfigurationManager.AppSettings["PDFKey"];
            if(!string.IsNullOrEmpty(key))
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
