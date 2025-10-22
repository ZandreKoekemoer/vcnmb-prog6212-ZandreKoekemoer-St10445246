using MVCprog.Models;

namespace MVCprog.Data
{
    // Reference: Tutorialsteacher C# List<T> Collection
    // According to Tutorialsteacher (2025), List<T> is a generic collection used to store strongly typed objects in memory.
    // I used this reference to implement temporary inmemory storage for documents in my system.
    public static class DocTemp
    {
        private static List<Document> _documents = new List<Document>();
        private static int _nextId = 1;

        public static void AddDocument(Document document)
        {
            document.DocumentId = _nextId++;
            _documents.Add(document);
        }

        public static List<Document> GetDocuments(int claimId)
        {
            return _documents.Where(d => d.ClaimId == claimId).ToList();
        }
    }
}
/*
 Tutorialsteacher. 2025. C# List<T> Collection (Version 2.0) [Source code].
Available at: <https://www.tutorialsteacher.com/csharp/csharp-list>
[Accessed 22 October 2025].
 */