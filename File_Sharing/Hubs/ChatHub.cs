using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using File_Sharing.Hubs;
using File_Sharing.Models;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace File_Sharing.Hubs
{
    public class ChatHub : Hub
    {
        private DataContext _dbContext;
        public ChatHub(DataContext context)
        {
            _dbContext = context;
        }

        private static readonly ConcurrentDictionary<string, User> Users
        = new ConcurrentDictionary<string, User>();

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            
        }

        public override Task OnConnectedAsync()
        {

            // Retrieve user.
            var user = _dbContext.Users
            .Include(z => z.Rooms)
            .SingleOrDefault(u => u.Name == Context.User.Identity.Name);
                

                // If user does not exist in database, must add.
                if (user == null)
                {
                    user = new User()
                    {  Name = Context.User.Identity.Name };
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                        
                 }
                else
                {
                    // Add to each assigned group.
                    foreach (var item in user.Rooms)
                    {
                        Groups.AddToGroupAsync(Context.ConnectionId, item.RoomName);
                    }
                }
            
            return base.OnConnectedAsync();
        }

        public async Task AddToRoom(string roomName)
        {

            var room = await _dbContext.Conversations.FindAsync(roomName);
               // Retrieve room.

               if (room != null)
               {
                   var user = new User() { Name = Context.User.Identity.Name };
                _dbContext.Users.Attach(user);
                   

                   room.Users.Add(user);
                 await _dbContext.SaveChangesAsync();
                 await  Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                 
            }
            
        }
        public async Task Send(string groupName,string message)
        {
            await Clients.Group(groupName).SendAsync("Send", message);
        }

        public async Task RemoveFromRoom(string roomName)
        {
            var room = await _dbContext.Conversations.FindAsync(roomName);           
              // Retrieve room.
              if (room != null)
              {
                  var user = new User() { Name = Context.User.Identity.Name };
                  _dbContext.Users.Attach(user);

                  room.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

                await Clients.Group(roomName).SendAsync("Send", $"{Context.ConnectionId} has left the group {roomName}.");
            }
           
        }
    }
}
