namespace TextDocument.Modals
{
    public class DocumentData
    {
        public string name { get; set; }

        public string fileName { get; set; }

        public string OldFileName { get; set; }
        public string fileContent { get; set; }

        public bool isSuccess { get; set; }
    }
}
