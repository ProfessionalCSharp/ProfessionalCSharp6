using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace AnonymousPipes
{
    public class Program
    {
        private string _pipeHandle;
        private ManualResetEventSlim _pipeHandleSet;
        
        public static void Main()
        {
            var p = new Program();
            p.Run();
            ReadLine();
        }

        public void Run()
        {
            _pipeHandleSet = new ManualResetEventSlim(initialState: false);

            Task.Run(() => Reader());
            Task.Run(() => Writer());
        }
        private void Writer()
        {
            WriteLine("anonymous pipe writer");
            _pipeHandleSet.Wait();

            var pipeWriter = new AnonymousPipeClientStream(PipeDirection.Out, _pipeHandle);
            using (var writer = new StreamWriter(pipeWriter))
            {
                writer.AutoFlush = true;
                WriteLine("starting writer");
                for (int i = 0; i < 5; i++)
                {
                    writer.WriteLine($"Message {i}");
                    Task.Delay(500).Wait();
                }
                writer.WriteLine("end");
            }
        }

        private void Reader()
        {
            try
            {
                WriteLine("anonymous pipe reader");
                var pipeReader = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.None);
                using (var reader = new StreamReader(pipeReader))
                {
                    _pipeHandle = pipeReader.GetClientHandleAsString();
                    WriteLine($"pipe handle: {_pipeHandle}");
                    _pipeHandleSet.Set();
                    bool end = false;
                    while (!end)
                    {
                        string line = reader.ReadLine();
                        WriteLine(line);
                        if (line == "end") end = true;
                    }
                    WriteLine("finished reading");

                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
