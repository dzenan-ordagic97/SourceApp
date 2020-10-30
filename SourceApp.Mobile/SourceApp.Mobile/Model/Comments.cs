using System;
using System.Collections.Generic;
using System.Text;

namespace SourceApp.Mobile.Model
{
    public class Comments
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
        public bool IsMine { get; set; }
    }
}
