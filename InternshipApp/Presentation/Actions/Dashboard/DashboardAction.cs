﻿using Domain.Models;
using System;

namespace Presentation.Actions.Dashboard
{
    public static class DashboardAction
    {
        public static Action Resources() { return null; }
        public static Action Users() { return null; }
        public static Action NoComment() { return null; }
        public static Action MyProfile() { return null; }
        public static Action LogOut()
        {
            CurrentUser.User = new();
            ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions);
            return null;
        }
    }
}
