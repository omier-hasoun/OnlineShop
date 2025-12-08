// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Presentation.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signIn;

        public ConfirmEmailModel(UserManager<User> userManager, SignInManager<User> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }


        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code, string returnUrl = null)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await _signIn.SignInAsync(user, isPersistent: false);
                StatusMessage = "Thank you for confirming your email.";
                return Page();
            }
            else
            {
                StatusMessage = "Error confirming your email.";
            }

            return Page();
        }
    }
}
