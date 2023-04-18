using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using TextDocument.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextDocument.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WriteDataController : ControllerBase
    {
        // GET: api/<WriteDataController>
        [HttpPost]
        public async Task<DocumentData> PostAsync([FromBody] DocumentData d1)
        {
            FirebaseConfig f1 = new FirebaseConfig();

            DocumentReference docRef = f1.database.Collection("UserData").Document(d1.name);
            // change this to create multiple documents
            Dictionary<string, object> textInfo = new Dictionary<string, object>
            {
                { d1.fileName, d1.fileContent }
            };

            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if(snapshot.Exists)
            {
                Dictionary<string, object> data = snapshot.ToDictionary();
                Console.WriteLine("File Exists");
                Console.WriteLine(d1.OldFileName);

                if (data.ContainsKey(d1.OldFileName) && d1.OldFileName != d1.fileName)
                {
                    Console.WriteLine("Field Already Exists");
                    Dictionary<string, object> updates = new Dictionary<string, object>
                    {
                        { d1.OldFileName, FieldValue.Delete }
                    };
                    await docRef.UpdateAsync(updates);
                }
                
                
                Dictionary<string, object> newData = new Dictionary<string, object>
                {
                    {d1.fileName, d1.fileContent},
                };
                
                await docRef.UpdateAsync(newData);
                



            }





            return new DocumentData
            {
                name = d1.name,
                fileName = d1.fileName,
                fileContent = d1.fileContent,
                OldFileName = d1.OldFileName,
                isSuccess = true,
            };
        }

        // GET api/<WriteDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
