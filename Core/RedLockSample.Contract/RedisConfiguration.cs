namespace RedLockSample.Contract
{
    public class RedisConfiguration
    {
        public string BaseUrl { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Connection => $"{BaseUrl}:{Port}, password={Password}";

        public string UrlPort => $"{BaseUrl}:{Port}";



        //lock properties (ExpiryTime,WaitTime,RetryTime)

        public int ExpiryTimeFromSeconds { get; set; }
        public int WaitTimeFromSeconds { get; set; }
        public int RetryTimeFromMilliseconds { get; set; }
        public bool LogLockingProcess { get; set; }
    }
}
