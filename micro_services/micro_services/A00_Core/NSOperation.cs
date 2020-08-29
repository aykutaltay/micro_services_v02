using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace micro_services.A00
{
    public class NSOperation
    {
        public cResponse UserAuth (cRequest model)
        {
            cResponse result = new cResponse()
            {
                message_code = AppStaticInt.msg0001WrongUserNamePass_i,
                message = AppStaticStr.msg0001WrongUserNamePass,
                token = string.Empty,
                data = string.Empty
            };






            return result;
        }
    }
}
