using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using TextDocument.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextDocument.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetDataController : ControllerBase
    {

        // GET api/<GetTextController>/5
        [HttpGet("{username}")]
        public async Task<List<DocumentData>> GetAsync(string username)
        {

            FirebaseConfig f1 = new FirebaseConfig();
            DocumentReference docRef = f1.database.Collection("UserData").Document(username);
            string existingText = "";

            List<DocumentData> allDocuments = new List<DocumentData>();

            DocumentSnapshot userDocSnapshot = await docRef.GetSnapshotAsync();
            if (userDocSnapshot.Exists)
            {
                Dictionary<string, object> userData = userDocSnapshot.ToDictionary();
                // Do something with the data
                foreach (KeyValuePair<string, object> pair in userData)
                {
                    existingText = Convert.ToString(pair.Value);
                    DocumentData singleDoc =  new DocumentData
                    {
                        name = username,
                        fileName = Convert.ToString(pair.Key),
                        fileContent = existingText,
                        isSuccess = true
                    };
                    allDocuments.Add(singleDoc);


                }


            }

            else
            {
                allDocuments.Add(new DocumentData
                {
                    name = "",
                    fileContent = "",
                    fileName = "",
                    isSuccess = true
                });
            }

            return allDocuments;


        }

    }
}
