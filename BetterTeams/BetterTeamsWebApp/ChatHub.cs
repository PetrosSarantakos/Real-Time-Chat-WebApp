using BetterTeamsWebApp.Models.ViewModels;
using Microsoft.AspNet.SignalR;
using Models;
using Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BetterTeamsWebApp
{
    [Authorize]
    public class ChatHub : Hub
    {

        // References: 
        // https://github.com/SignalR/SignalR/wiki/Hubs
        // https://github.com/SignalR/Samples/blob/master/BasicChat/ChatWithTracking.cs
        // https://github.com/davidfowl/MessengR/blob/master/MessengR/Hubs/Chat.cs

        private static readonly ConcurrentDictionary<string, UserCId> Users
            = new ConcurrentDictionary<string, UserCId>(StringComparer.InvariantCultureIgnoreCase);

        public void Send(string Text, string To)
        {

            UserCId receiver;
            
            if (Users.TryGetValue(To, out receiver))
            {
                
                UserCId sender =  GetUser(Context.User.Identity.Name);
                
                IEnumerable<string> allReceivers;
                lock (receiver.ConnectionIds)
                {
                    lock (sender.ConnectionIds)
                    {

                        allReceivers = receiver.ConnectionIds.Concat(sender.ConnectionIds);
                    }
                }

                foreach (var cid in allReceivers)
                {
                    Clients.Client(cid).received(new { sender = sender.Username, Message = Text, rcver = To,  isPrivate = true });
                }
            }

            MessageRepository messageRepo = new MessageRepository();
            Message message = new Message
            {
                Sender = Context.User.Identity.Name,
                Receiver = To,
                Text = Text,
                DateTime = DateTime.Now,
                Deleted = false
            };

            messageRepo.Add(message);
            Clients.All.addNewMessageToPage(message.Text, message.Receiver, message.DateTime, message.Deleted);
        }



        public IEnumerable<string> GetConnectedUsers()
        {

            return Users.Where(x => {

                lock (x.Value.ConnectionIds)
                {

                    return !x.Value.ConnectionIds.Contains(Context.ConnectionId, StringComparer.InvariantCultureIgnoreCase);
                }

            }).Select(x => x.Key);


        }

        public override  Task OnConnected()
        {

            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserCId
            {
                Username = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {

                user.ConnectionIds.Add(connectionId);

                // // broadcast this to all clients other than the caller
                // Clients.AllExcept(user.ConnectionIds.ToArray()).userConnected(userName);

                // Or you might want to only broadcast this info if this 
                // is the first connection of the user
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }

            return base.OnConnected();
        }


        public override Task OnDisconnected(bool True)
        {

            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            UserCId user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {

                lock (user.ConnectionIds)
                {

                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                    if (!user.ConnectionIds.Any())
                    {

                        UserCId removedUser;
                        Users.TryRemove(userName, out removedUser);

                        // You might want to only broadcast this info if this 
                        // is the last connection of the user and the user actual is 
                        // now disconnected from all connections.
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(True);
        }

        private UserCId GetUser(string username)
        {

            UserCId user;
            Users.TryGetValue(username, out user);

            return user;
        }
    }
}
