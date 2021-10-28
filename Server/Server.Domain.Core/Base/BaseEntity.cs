using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Base
{
    public class BaseEntity<T> where T: struct
    {
        public T Id { get; set; }
    }
}
