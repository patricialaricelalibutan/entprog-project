using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FINALTEST1.Models
{
    public class Email
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
