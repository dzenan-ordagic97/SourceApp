using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace SourceApp.Mobile.Model
{
    public class ResponseWithPaging<T>
    {
        public List<T> Data { get; set; }
        public int Previous { get; set; }
        public int Next { get; set; }
        public int Last { get; set; }
        public int First { get; set; }
        public bool IsLast { get; set; }
        public bool IsFirst { get; set; }

    }
}
