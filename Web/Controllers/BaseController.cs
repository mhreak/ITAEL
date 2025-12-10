using Microsoft.AspNetCore.Mvc;
using System;

using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        private void SetToast(string type, string title, string text)
        {
            ViewData["ShowToast"] = true;
            ViewData["ToastType"] = type;
            ViewData["ToastTitle"] = !String.IsNullOrEmpty(title) ? title : (type == "danger" ? "خطا" : (type == "warning" ? "هشدار" : "توجه"));
            ViewData["ToastText"] = text;

            TempData["ShowToast"] = "true";
            TempData["ToastType"] = type ?? "";
            TempData["ToastTitle"] = title ?? "";
            TempData["ToastText"] = text ?? "";
        }

        public void ShowInfoToast(string title, string text)
        {
            SetToast("info", !String.IsNullOrEmpty(title) ? title : "توجه", text);
        }

        public void ShowSuccessToast(string title, string text)
        {
            SetToast("success", !String.IsNullOrEmpty(title) ? title : " ", !String.IsNullOrEmpty(text) ? text : "عملیات انجام شد");
        }

        public void ShowWarningToast(string title, string text)
        {
            SetToast("warning", !String.IsNullOrEmpty(title) ? title : "هشدار", text);
        }

        public void ShowDangerToast(string title, string text)
        {
            SetToast("danger", !String.IsNullOrEmpty(title) ? title : "خطا", !String.IsNullOrEmpty(text) ? text : "خطایی رخ داد");
        }
    }

}
