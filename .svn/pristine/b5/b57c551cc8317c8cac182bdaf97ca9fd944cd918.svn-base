using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace EHR.Recruitment
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void IsUpload_Click(object sender, EventArgs e)
        {
            string filenameUpload;
            if (fileUploadCan.HasFile)
            {
                //string dir = @"E:\EHR\Recruitment\docs\xls\";
                //string[] files;
                //int numFiles;
                

                //files = Directory.GetFiles(dir);
                //numFiles = files.Length;
                 //int nextNum = numFiles + 1;
                DateTime RecordedDateTime = DateTime.Now;
                string strFileDate = String.Format("{0:MMddyyyy}", RecordedDateTime);
                string extFile = System.IO.Path.GetExtension(this.fileUploadCan.PostedFile.FileName);
                filenameUpload = "Candidate_" + strFileDate + extFile;
                fileUploadCan.SaveAs(@"E:\Projects\EHR\Recruitment\docs\xls\" + filenameUpload);
            }
            else
            {
                filenameUpload = "";
            }
        }
    }
}
