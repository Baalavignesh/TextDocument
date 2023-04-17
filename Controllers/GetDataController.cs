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
        public async Task<DocumentData> GetAsync(string username)
        {

            FirebaseConfig f1 = new FirebaseConfig();
            DocumentReference docRef = f1.database.Collection("UserData").Document(username);
            string existingText = "";

            DocumentSnapshot userDocSnapshot = await docRef.GetSnapshotAsync();
            if (userDocSnapshot.Exists)
            {
                Dictionary<string, object> userData = userDocSnapshot.ToDictionary();
                // Do something with the data
                Console.WriteLine(userData.Values);
                foreach (KeyValuePair<string, object> pair in userData)
                {
                    Console.WriteLine(pair.Value);
                    existingText = Convert.ToString(pair.Value);
                }


                return new DocumentData
                {
                    name = username,
                    documentText = existingText,
                    isSuccess = true
                };
            }

            else
            {
                return new DocumentData
                {
                    name = "",
                    documentText = "",
                    isSuccess = true
                };
            }

        }

    }
}
