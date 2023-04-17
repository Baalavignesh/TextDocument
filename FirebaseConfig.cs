using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace TextDocument
{
    public class FirebaseConfig
    {
        public FirestoreDb database;
        public FirebaseConfig() {
            string path = "firebaseKey.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            database = FirestoreDb.Create("texteditor-5d26c");
            Console.WriteLine("Database Created");
        }

    }
}
