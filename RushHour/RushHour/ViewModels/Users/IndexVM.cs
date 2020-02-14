using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Users
{
    public class IndexVM:BaseIndexVM<User,FilterVM,OrderBy>
    {
    }
}