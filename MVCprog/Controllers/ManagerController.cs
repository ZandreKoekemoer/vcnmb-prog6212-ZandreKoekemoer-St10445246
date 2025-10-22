using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using System.Linq;

namespace MVCprog.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult FinalApproval()
        {
        // Retrieves all claims that have already been approved or rejected by the Coordinator
            var claims = ClaimTemp.GetAllClaims().Where(c => c.Status == "Approved by Coordinator" || c.Status == "Rejected by Coordinator").ToList();
            return View(claims);
        }

        [HttpPost]
        public IActionResult FinalApprove(int id)
        {
        // Updates the selected claim’s status to show that the Manager has approved it
            ClaimTemp.UpdateClaimStatus(id, "Approved by Manager");
            return RedirectToAction("FinalApproval");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
        // Updates the selected claim’s status to show that the Manager has arejected it
            ClaimTemp.UpdateClaimStatus(id, "Rejected by Manager");
            return RedirectToAction("FinalApproval");
        }
    }
}

/*
 Tutorialsteacher. 2025. C# List<T> Collection (Version 2.0) [Source code].
Available at: <https://www.tutorialsteacher.com/csharp/csharp-list>
[Accessed 22 October 2025].
 */
