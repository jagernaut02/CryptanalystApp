using System.Collections.Generic;

namespace CryptanalystApp.Core.Frameworck.Models
{
    public class ListResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }

    public class Response<T>
    {
        public T Data { get; set; }
    }
}
