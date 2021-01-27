using System;
using System.Threading.Tasks;
using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.EmailService.Interface;

namespace BulletinBoard.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserAuthService _userAuthService;
        private readonly IEmailSenderService _emailSender;

        public AuthenticationController(IUserAuthService userAuthService, IEmailSenderService emailSender)
        {
            _userAuthService = userAuthService;
            _emailSender = emailSender;
        }

        [AllowAnonymous, HttpGet]
        public async Task<IActionResult> Register()
        {
            await _userAuthService.SignOutUserAsync();
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Register(UserForRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registrationResult = await _userAuthService.RegisterNewUserAsync(model);

                if (registrationResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in registrationResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [AllowAnonymous, HttpGet]
        public IActionResult LogIn(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> LogIn(UserForLogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginSucceeded = await _userAuthService.SignInUserAsync(model, returnUrl);

                if (loginSucceeded) return Redirect(returnUrl ?? "/Forum");

                ModelState.AddModelError(nameof(UserForLogInViewModel.Email), "Invalid user or password");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _userAuthService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous, HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(PasswordForgotViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _userAuthService.GetPasswordResetTokenAsync(model);

            if (!String.IsNullOrEmpty(token))
            {
                var callback = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = model.Email }, Request.Scheme);
                var message = new Services.EmailService.Message(new string[] { model.Email }, "Reset password token", callback, null);
                await _emailSender.SendEmailAsync(message);
            }

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous, HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new PasswordResetViewModel { Token = token, Email = email };
            return View(model);
        }

        [AllowAnonymous, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(PasswordResetViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var resetPassResult = await _userAuthService.ResetUserPasswordAsync(resetPasswordModel);

            if (resetPassResult == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));

            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [AllowAnonymous, HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}