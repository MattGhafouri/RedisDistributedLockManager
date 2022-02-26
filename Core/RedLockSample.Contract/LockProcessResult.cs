using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RedLockSample.Contract
{

    public class LockProcessResult
    {

        public LockProcessResult()
        {

        }

        public void SetException(Exception ex)
        {
            this.Exception = ex;
        }

        public bool IsSuccessfullyProcessed => Exception == null;

        public Exception Exception { get; set; }
    }

    public class LockProcessResult<TInput> : LockProcessResult
    {

    }


}
