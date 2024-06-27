using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruSec.BLL.DTOs
{
    public class ResponseResult<T> where T : class
    {
        public T? Response { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
