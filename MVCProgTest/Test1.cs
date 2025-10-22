
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCprog.Data;
using MVCprog.Models;
using System.Linq;

namespace MVCProgTest
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void AddClaimTest()
        {
            var claim = new Claim { LecturerId = 1, HoursWorked = 5, HourlyRate = 200 };

            ClaimTemp.AddClaim(claim);
            var allClaims = ClaimTemp.GetAllClaims();
            Assert.IsTrue(allClaims.Any(c => c.ClaimId == claim.ClaimId));
            Assert.IsTrue(claim.ClaimId > 0);
        }

        [TestMethod]
        public void UpdateStatusTest()
        {
            var claim = new Claim { LecturerId = 2, HoursWorked = 4, HourlyRate = 150 };

            ClaimTemp.AddClaim(claim);
            ClaimTemp.UpdateClaimStatus(claim.ClaimId, "Approved");
            var updated = ClaimTemp.GetAllClaims().First(c => c.ClaimId == claim.ClaimId);
            Assert.AreEqual("Approved", updated.Status);
        }

        [TestMethod]
        public void AddDocumentTest()
        {
            var doc = new Document
            {
                ClaimId = 1,
                FileName = "proof.pdf",
                FilePath = "/uploads/proof.pdf"
            };
            DocTemp.AddDocument(doc);
            var docs = DocTemp.GetDocuments(1);
            Assert.IsTrue(docs.Any(d => d.FileName == "proof.pdf"));
        }

        [TestMethod]
        public void CalculateTotalTest()
        {
            var claim = new Claim { HoursWorked = 10, HourlyRate = 100 };
            var total = claim.TotalAmount;
            Assert.AreEqual(1000, total);
        }
    }
}
