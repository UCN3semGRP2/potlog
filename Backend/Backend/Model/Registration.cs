using System;

namespace Model
{
    public class Registration
    {
        public int Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}