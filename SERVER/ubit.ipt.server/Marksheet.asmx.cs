using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;


namespace ubit.ipt.server
{
    /// <summary>
    /// Summary description for Marksheet
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Marksheet : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string calculateMarksheet()
        {
            string subjectStr = HttpContext.Current.Request.Params["request"];
            List<SubjectModel> subjects = JsonConvert.DeserializeObject<List<SubjectModel>>(subjectStr);
            double totalMarks = 0;
            double min = subjects[0].obtainedMarks;
            string minSub = subjects[0].name;
            double max = subjects[0].obtainedMarks;
            string maxSub = subjects[0].name;
            for (int i = 0; i < subjects.Count; i++)
            {
                totalMarks += subjects[i].obtainedMarks;
                if (min > subjects[i].obtainedMarks)
                {
                    min = subjects[i].obtainedMarks;
                    minSub = subjects[i].name;
                }

                if (max < subjects[i].obtainedMarks)
                {
                    max = subjects[i].obtainedMarks;
                    maxSub = subjects[i].name;
                }
            }

            double percent = (totalMarks / (subjects.Count * 100)) * 100;

            MarksheetModel marksheetModel = new MarksheetModel();
            marksheetModel.percentage = percent;
            marksheetModel.minMarks = min;
            marksheetModel.maxMarks = max;
            marksheetModel.minSubjectMarks = minSub;
            marksheetModel.maxSubjectMarks = maxSub;

            string str = JsonConvert.SerializeObject(marksheetModel);
            return str;
        

        }


        public class SubjectModel
        {
            public string name { get; set; }
            public double obtainedMarks { get; set; }
        }

        public class MarksheetModel
        {
            public double percentage { get; set; }
            public double minMarks { get; set; }
            public double maxMarks { get; set; }
            public string minSubjectMarks { get; set; }
            public string maxSubjectMarks { get; set; }
        }


    }
}

