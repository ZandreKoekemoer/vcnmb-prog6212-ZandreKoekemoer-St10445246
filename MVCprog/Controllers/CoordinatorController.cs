using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;

namespace MVCprog.Controllers
{
    public class CoordinatorController : Controller
    {
        public IActionResult VerifyClaims()
        {
            var claims = ClaimTemp.GetAllClaims().Where(c => c.Status == "Pending").ToList();

            return View(claims);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            ClaimTemp.UpdateClaimStatus(id, "Approved by Coordinator");
            return RedirectToAction("VerifyClaims");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            ClaimTemp.UpdateClaimStatus(id, "Rejected by Coordinator");
            return RedirectToAction("VerifyClaims");
        }
    }
}

/*
 Tutorialsteacher. 2025. C# List<T> Collection (Version 2.0) [Source code].
Available at: <https://www.tutorialsteacher.com/csharp/csharp-list>
[Accessed 22 October 2025].
 */