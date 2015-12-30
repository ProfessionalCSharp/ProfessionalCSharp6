using System.Diagnostics.Tracing;

namespace EventSourceSampleInheritance
{
    public class SampleEventSource : EventSource
    {
        private SampleEventSource()
          : base("Wrox-SampleEventSource2")
        {
        }

        public static SampleEventSource Log = new SampleEventSource();

        public void Startup()
        {
            base.WriteEvent(1);
        }

        public void CallService(string url)
        {
            base.WriteEvent(2, url);
        }

        public void CalledService(string url, int length)
        {
            base.WriteEvent(3, url, length);
        }


        public void ServiceError(string message, int error)
        {
            base.WriteEvent(4, message, error);
        }

        public void ProcessingStart(int x)
        {
            base.WriteEvent(5, x);
        }
        public void Processing(int x)
        {
            base.WriteEvent(6, x);
        }
        public void ProcessingStop(int x)
        {
            base.WriteEvent(7, x);
        }

        public void RequestStart()
        {
            base.WriteEvent(8);
        }

        public void RequestStop()
        {
            base.WriteEvent(9);
        }
    }

}
