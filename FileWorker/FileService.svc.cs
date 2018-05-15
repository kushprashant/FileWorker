using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace FileWorker
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FileService : IFileService
    {
        public Stream DownloadFile(string fileName)
        {
            string downloadFilePath = Path.Combine(HostingEnvironment.MapPath("~/Files/Downloads"), fileName);
            String headerInfo = "attachment; filename=" + fileName;
            WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
            return File.OpenRead(downloadFilePath);
        }

        public string UploadFile(string fileName, Stream stream)
        {
            result obj = new result();
            try
            {
                string FilePath = Path.Combine(HostingEnvironment.MapPath("~/Files/Uploads"), fileName);

                int length = 0;
                using (FileStream writer = new FileStream(FilePath, FileMode.Create))
                {
                    int readCount;
                    var buffer = new byte[8192];
                    while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        length += readCount;
                    }
                }


                obj.code = "200";
                obj.message = "Successful";
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream memoryStream = new MemoryStream();
                serializer.WriteObject(memoryStream, obj);
                string json = new JavaScriptSerializer().Serialize(obj);
                // Return the results serialized as JSON
                // string json = Encoding.Default.GetString(memoryStream.ToArray());
                return json;
            }
            catch (Exception ex)
            {
                obj.code = "500";
                obj.message = "UnSuccessful";
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream memoryStream = new MemoryStream();
                serializer.WriteObject(memoryStream, obj);

                // Return the results serialized as JSON
                string json = new JavaScriptSerializer().Serialize(obj);
                return json;
                // throw new Exception(ex.Message);
            }
        }

        public string UploadFile1(string fileName)
        {
            result obj = new result();
            obj.code = "200";
            obj.message = "Successful";
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, obj);
            string json = new JavaScriptSerializer().Serialize(obj);
            // Return the results serialized as JSON
            // string json = Encoding.Default.GetString(memoryStream.ToArray());
            return json;
        }

    }
}
