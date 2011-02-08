using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace RealWorldMvc3.Core.Castle.Interceptors
{
    public class LoggingInterceptor : StandardInterceptor 
    {
        private readonly ILogger logger;

        public LoggingInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        protected override void PreProceed(IInvocation invocation)
        {
            logger.Debug("Entering Method: {0}", invocation.Method);
        }
    }
}