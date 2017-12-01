using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoardBackend.Models
{
    public class Message
    {   
        //the id property below is used by EF  as a primary key
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Text { get; set; }
    }
}
