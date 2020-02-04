using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Authorization;
using File_Sharing.Models;
using File_Sharing.Hubs;
using File_Sharing.Viewmodels;
using Microsoft.EntityFrameworkCore;


namespace File_Sharing.Controllers
{
    [Authorize]
    public class SendChatController: Controller
    {
        private DataContext _dbContext;
        private IHubContext<ChatHub> _hubContext;
        //private ChatHub _Hub;
        public SendChatController(DataContext dataContext, IHubContext<ChatHub> hub/*, ChatHub chatHub*/)
        {
            this._dbContext = dataContext;
            this._hubContext = hub;
            //_Hub = chatHub;

        }
        public IActionResult Index( int id)
        {

            return View(/*_dbContext.Users.Where(x => x.Id != id).ToList()*/);
        }
       
    }

}
