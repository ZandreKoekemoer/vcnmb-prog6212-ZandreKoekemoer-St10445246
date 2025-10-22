using Microsoft.AspNetCore.Mvc;
using MVCprog.Models;
using MVCprog.Data;

namespace MVCprog.Controllers
{
    // Reference: Tutorialsteacher C# List<T> Collection
    // According to Tutorialsteacher (2025), List<T> is a generic collection used to store strongly typed objects in memory.
    // I used this reference to implement ClaimTemp and DocTemp using lists for temporary storage and retrieval of claims and documents in my program.

    public class LecturerController : Controller
    {
        [HttpGet]
        public IActionResult MyClaims()
        {
            var claims = ClaimTemp.GetAllClaims().Where(c => c.LecturerId == 1).ToList();
            return View(claims); // Fetches all claims that the lecturer already submitted when running the program
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(Claim claim, IFormFile supportingFile)
        {
        //If the user has not uploaded a document,program returns an error msg syaing he has to upload a document
            if (supportingFile == null || supportingFile.Length <= 0)
            {
                ViewBag.Error = "You must upload a document.";
                return View(claim);
            }
            //If user leaves out a field,program returns an erro
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill in all required fields.";
                return View(claim);
            }

            // Reference: GeeksforGeeks. 2024. Exception Handling in C#
            // According to GeeksforGeeks (2024), exception handling in C# helps catch and handle runtime errors using try and catch blocks.
            // I used this reference to implement error handling in my system to identify issues that occur when the user runs the program.

            try
            {
            //Restriction what the user can upload
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var extension = Path.GetExtension(supportingFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ViewBag.Error = "Only pdf, docx and xlsx files are allowed.";
                    return View(claim);
                }

                if (supportingFile.Length > 5 * 1024 * 1024)
                {
                    ViewBag.Error = "File is too large.";
                    return View(claim);
                }

                claim.LecturerId = 1; // Lecturer ID auto-set to 1 since data is only stored temporarily
                ClaimTemp.AddClaim(claim);

                // Reference: Microsoft. 2024. File Uploads in ASP.NET Core
                // According to Microsoft (2024), IFormFile represents a file sent with the HttpRequest, and FileStream can be used to save files to disk.
                // I used this reference to implement the upload of documents for claims in my program.

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(folder, supportingFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    supportingFile.CopyTo(stream);
                }

                DocTemp.AddDocument(new Document
                {
                    ClaimId = claim.ClaimId,
                    FileName = supportingFile.FileName,
                    FilePath = supportingFile.FileName
                });

                ViewBag.Message = "Claim uploaded successfully.";
                ModelState.Clear();
                return View();
            }
            catch
            {
                ViewBag.Error = "Error submitting claim. Try again later.";
                return View(claim);
            }
        }
    }
}


/*
GeeksforGeeks. 2024. Exception Handling in C# (Version 2.0) [Source code].  
Available at: <https://www.geeksforgeeks.org/c-sharp/exception-handling-in-c-sharp/>  
[Accessed 22 October 2025].

Tutorialsteacher. 2025. C# List<T> Collection (Version 2.0) [Source code].  
Available at: <https://www.tutorialsteacher.com/csharp/csharp-list>  
[Accessed 22 October 2025].

Microsoft. 2024. File Uploads in ASP.NET Core (Version 8.0) [Source code].
Available at: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads
[Accessed 22 October 2025].

Microsoft. 2024. File Uploads in ASP.NET Core (Version 2.0) [Source code].  
Available at: <https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads>  
[Accessed 22 October 2025].

*/
