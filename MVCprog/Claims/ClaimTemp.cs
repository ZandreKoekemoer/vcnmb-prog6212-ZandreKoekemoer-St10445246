using MVCprog.Models;

namespace MVCprog.Data
{
    public static class ClaimTemp
    {
        // Reference: Tutorialsteacher C# List<T> Collection
        // According to Tutorialsteacher (2025), List<T> is a generic collection used to store strongly typed objects in memory.
        // I used this reference to implement temporary in memory storage for claims in my program.
        private static List<Claim> _claims = new List<Claim>();
        private static int _nextId = 1;

        public static void AddClaim(Claim claim)
        {
            claim.ClaimId = _nextId++;
            _claims.Add(claim);
        }
        public static List<Claim> GetDocument()
        {
            var claims = _claims.ToList();
            foreach (var claim in claims)
            {
                claim.Documents = DocTemp.GetDocuments(claim.ClaimId);
            }
            return claims;
        }
        public static List<Claim> GetAllClaims()
        {
            foreach (var claim in _claims)
            {
                claim.Documents = DocTemp.GetDocuments(claim.ClaimId);
            }
            return _claims;
        }

        public static void UpdateClaimStatus(int id, string status)
        {
            var claim = _claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = status;
            }
        }
    }
}
/*
 Tutorialsteacher. 2025. C# List<T> Collection (Version 2.0) [Source code].
Available at: <https://www.tutorialsteacher.com/csharp/csharp-list>
[Accessed 22 October 2025].
 */