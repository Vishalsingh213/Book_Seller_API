using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class JsonResponse
    {
        public string Id { get; set; }
        public Int32 Status { get; set; }
        public string Message { get; set; }
        public string ErrorDetail { get; set; }

    }
}
