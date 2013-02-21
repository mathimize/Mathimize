using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Mathimize.com.Models;

namespace Mathimize.com.Services
{
    public class MathService
    {
        public BasicArithmeticResultViewModel GetBasicArithmeticResultViewModel(BasicArithmeticResultViewModel model)
        {
            BasicArithmeticResultViewModel sessionModel = (BasicArithmeticResultViewModel)HttpContext.Current.Session[model.ResultId];

            // if the model is in the session and the parameters are all the same, then retrieve session model. O/W generate new terms
            if (sessionModel != null 
                && sessionModel.Rows == model.Rows
                && sessionModel.Cols == model.Cols
                && sessionModel.MaxInt == model.MaxInt)
            {
                model = (BasicArithmeticResultViewModel)HttpContext.Current.Session[model.ResultId];
            }
            else
            {
                string resultId = Guid.NewGuid().ToString();

                Random rnd = new Random(DateTime.Now.Millisecond);
                int totalNumber = model.Rows * model.Cols;
                IList<Terms> termsList = new List<Terms>();

                for (int i = 0; i < totalNumber; i++)
                {
                    Terms terms = new Terms();
                    terms.Term1 = rnd.Next(0, model.MaxInt + 1);
                    terms.Term2 = rnd.Next(0, model.MaxInt + 1);

                    termsList.Add(terms);


                }
                model.Terms = termsList;
                model.ResultId = resultId;

                HttpContext.Current.Session[resultId] = model;
            }
            return model;

        }

        public TimeResultViewModel GetTimeResultViewModel(TimeResultViewModel model)
        {
            TimeResultViewModel sessionModel = (TimeResultViewModel)HttpContext.Current.Session[model.ResultId];

            // if the model is in the session and the parameters are all the same, then retrieve session model. O/W generate new terms
            if (sessionModel != null
                && sessionModel.Rows == model.Rows
                && sessionModel.Cols == model.Cols)
            {
                model = (TimeResultViewModel)HttpContext.Current.Session[model.ResultId];
            }
            else
            {
                string resultId = Guid.NewGuid().ToString();

                Random rnd = new Random(DateTime.Now.Millisecond);
                int totalNumber = model.Rows * model.Cols;
                IList<HourMinutes> termsList = new List<HourMinutes>();

                for (int i = 0; i < totalNumber; i++)
                {
                    HourMinutes hourMinutes = new HourMinutes();
                    hourMinutes.Hour = rnd.Next(1, 12);
                    hourMinutes.Minutes = rnd.Next(0, 60);
                    
                    termsList.Add(hourMinutes);


                }
                model.HourMinutes = termsList;
                model.ResultId = resultId;

                HttpContext.Current.Session[resultId] = model;
            }
            return model;

        }
    }
}