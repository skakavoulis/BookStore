using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Mvc.Controllers;

public class BaseController : Controller
{
    public UserModel User => GetUserFromSessionId(Request.Cookies["session_id"]);

    public UserModel GetUserFromSessionId(string sessionId)
    {
        // return DbContext.Users.firstOrDefault(x => x.SessionId == session);
        return new UserModel { SessionId = sessionId };
    }
    public string GetOwnerId(string sessionId)
    {
        // var user = DbContext.Users.firstOrDefault(x => x.SessionId == session);
        // var owner = DbContext.Owners.firstordefault(x => x.UserId == user.id);
        // return owner.Id;
        return string.Empty;
    }
}
