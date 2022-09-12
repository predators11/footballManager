using System.ComponentModel.DataAnnotations.Schema;

namespace FootballManager.Data.Models
{
    public class UserPlayer
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }
    }
}

//•	Has UserId – a string
//•	Has User – a User object
//•	Has PlayerId – an int
//•	Has Player – a Player object