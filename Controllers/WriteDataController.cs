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
        public DocumentData Post([FromBody] DocumentData d1)
        {
            FirebaseConfig f1 = new FirebaseConfig();
            Dictionary<string, object> textInfo = new Dictionary<string, object>
            {
                { "TextData", d1.documentText }
            };


            DocumentReference docRef = f1.database.Collection("UserData").Document(d1.name);
            DocumentSnapshot snapshot = docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Console.WriteLine("Document data for {0} document:", snapshot.Id);
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }
            else
            {
                Console.WriteLine("Document {0} does not exist!", snapshot.Id);
            }



            DocumentReference docRef2 = f1.database.Collection("UserData").Document(d1.name);
            docRef2.SetAsync(textInfo);



            return new DocumentData
            {
                name = d1.name,
                documentText = d1.documentText   
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
