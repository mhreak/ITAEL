using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        { }

        public void ShowInfoToast(string title, string text)
        {
            ViewData["ShowToast"] = true;
            ViewData["ToastType"] = "info";
            ViewData["ToastTitle"] = !String.IsNullOrEmpty(title) ? title : "توجه";
            ViewData["ToastText"] = text;
        }

        public void ShowSuccessToast(string title, string text)
        {
            ViewData["ShowToast"] = true;
            ViewData["ToastType"] = "success";
            ViewData["ToastTitle"] = !String.IsNullOrEmpty(title) ? title : " ";
            ViewData["ToastText"] = !String.IsNullOrEmpty(text) ? text : "عملیات انجام شد";
        }

        public void ShowWarningToast(string title, string text)
        {
            ViewData["ShowToast"] = true;
            ViewData["ToastType"] = "warning";
            ViewData["ToastTitle"] = !String.IsNullOrEmpty(title) ? title : "هشدار";
            ViewData["ToastText"] = text;
        }

        public void ShowDangerToast(string title, string text)
        {
            ViewData["ShowToast"] = true;
            ViewData["ToastType"] = "danger";
            ViewData["ToastTitle"] = !String.IsNullOrEmpty(title) ? title : "خطا";
            ViewData["ToastText"] = !String.IsNullOrEmpty(text) ? text : "خطایی رخ داد";
        }
    }
}
