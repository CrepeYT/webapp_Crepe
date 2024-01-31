using System;

namespace API.Helpers;
#nullable disable
public class UserParams : PaginationParams
{
   public string OrderBy { get; set; } = "lastActive";
   public string CurrentUserName { get; set; }
   public string Gender { get; set; }
   public int MinAge { get; set; } = 18;
   public int MaxAge { get; set; } = 80;
}
