using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Hubs
{
    interface IChatHubClient
    {
        void AddNewMessageToPage(string name, string message);
    }
}
