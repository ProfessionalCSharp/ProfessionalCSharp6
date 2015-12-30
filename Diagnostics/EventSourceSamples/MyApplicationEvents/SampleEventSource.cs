using System.Diagnostics.Tracing;

namespace MyApplicationEvents
{
    public class SampleEventSource : EventSource
    {
        private SampleEventSource()
          : base("Wrox-SampleEventSource")
        {
        }

        public static SampleEventSource Log = new SampleEventSource();


        public void ProcessingStart(int x)
        {
            base.WriteEvent(1, x);
        }
        public void Processing(int x)
        {
            base.WriteEvent(2, x);
        }
        public void ProcessingStop(int x)
        {
            base.WriteEvent(3, x);
        }

        public void RequestStart()
        {
            base.WriteEvent(4);
        }

        public void RequestStop()
        {
            base.WriteEvent(5);
        }
    }
}
