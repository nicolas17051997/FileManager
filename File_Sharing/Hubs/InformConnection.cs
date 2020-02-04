﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using File_Sharing.Models;

namespace File_Sharing.Hubs
{
    public class InformConnection
    {
        private ConcurrentDictionary<string, UserInfo> _onlineUser { get; set; } = new ConcurrentDictionary<string, UserInfo>();

        public bool AddUpdate(string name, string connectionId)
        {
            var userAlreadyExists = _onlineUser.ContainsKey(name);

            var userInfo = new UserInfo
            {
                UserName = name,
                ConnectionId = connectionId
            };

            _onlineUser.AddOrUpdate(name, userInfo, (key, value) => userInfo);

            return userAlreadyExists;
        }

        public void Remove(string name)
        {
            UserInfo userInfo;
            _onlineUser.TryRemove(name, out userInfo);
        }

        public IEnumerable<UserInfo> GetAllUsersExceptThis(string username)
        {
            return _onlineUser.Values.Where(item => item.UserName != username);
        }

        public UserInfo GetUserInfo(string username)
        {
            UserInfo user;
            _onlineUser.TryGetValue(username, out user);
            return user;
        }
    }
}
