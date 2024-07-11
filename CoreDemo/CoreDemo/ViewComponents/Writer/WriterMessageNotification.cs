﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        Message2Menager mm = new Message2Menager(new EfMessage2Repository());
        public IViewComponentResult Invoke() 
        {
            int id = 4;
            var values = mm.GetInboxListByWriter(id);
            return View(values);
        }
    }
}
