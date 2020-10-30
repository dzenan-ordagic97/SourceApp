using System;
using System.Collections.Generic;
using System.Text;

namespace SourceApp.Mobile.Requests
{
    public class CommentInsertRequest
    {
        public int postId { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}
