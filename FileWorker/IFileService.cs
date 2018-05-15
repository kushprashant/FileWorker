using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
namespace FileWorker
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        [WebGet(UriTemplate = "DownloadFile/{fileName}")]
        Stream DownloadFile(string fileName);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/UploadFile?fileName={fileName}")]
        string UploadFile(string fileName, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/UploadFile1?fileName={fileName}")]
        string UploadFile1(string fileName);
    }

    [DataContract]
    public class result{
        public string code { get; set; }
        public string message { get; set; }
    }
}
