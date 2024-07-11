﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException : JorneyException
    {
        public NotFoundException(string message) : base(message)
        {
            
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }

        public override IList<string> GetErrorMessages()
        {
            return [Message];
        }
    }
}
