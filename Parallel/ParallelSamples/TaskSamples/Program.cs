using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace TaskSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
                return;
            }
            switch (args[0])
            {
                case "-p":
                    TasksUsingThreadPool();
                    break;
                case "-s":
                    RunSynchronousTask();
                    break;
                case "-l":
                    LongRunningTask();
                    break;
                case "-r":
                    TaskWithResultDemo();
                    break;
                case "-c":
                    ContinuationTasks();
                    break;
                case "-pc":
                    ParentAndChild();
                    break;
                default:
                    ShowUsage();
                    break;
            }
            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine("TaskSamples option");
            WriteLine("options");
            WriteLine("\t-p\tUse Thread Pool");
            WriteLine("\t-s\tUse Synchronous Task");
            WriteLine("\t-l\tUse Long Running Task");
            WriteLine("\t-r\tTask with Result");
            WriteLine("\t-c\tContinuation Tasks");
            WriteLine("\t-pc\tParent and Child");
        }

        public static void ParentAndChild()
        {
            var parent = new Task(ParentTask);
            parent.Start();
            Task.Delay(2000).Wait();
            WriteLine(parent.Status);
            Task.Delay(4000).Wait();
            WriteLine(parent.Status);
        }

        private static void ParentTask()
        {
            WriteLine($"task id {Task.CurrentId}");
            var child = new Task(ChildTask);
            child.Start();
            Task.Delay(1000).Wait();
            WriteLine("parent started child");
        }

        private static void ChildTask()
        {
            WriteLine("child");
            Task.Delay(5000).Wait();
            WriteLine("child finished");
        }


        public static void ContinuationTasks()
        {
            
            Task t1 = new Task(DoOnFirst);
            Task t2 = t1.ContinueWith(DoOnSecond);
            Task t3 = t1.ContinueWith(DoOnSecond);
            Task t4 = t2.ContinueWith(DoOnSecond);
            t1.Start();
        }

        private static void DoOnFirst()
        {
            WriteLine($"doing some task {Task.CurrentId}");
            Task.Delay(3000).Wait();
        }

        private static void DoOnSecond(Task t)
        {
            WriteLine($"task {t.Id} finished");
            WriteLine($"this task id {Task.CurrentId}");
            WriteLine("do some cleanup");
            Task.Delay(3000).Wait();
        }


        public static void TaskWithResultDemo()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create(8, 3));
            t1.Start();
            WriteLine(t1.Result);
            t1.Wait();
            WriteLine($"result from task: {t1.Result.Item1} {t1.Result.Item2}");
        }


        private static Tuple<int, int> TaskWithResult(object division)
        {
            Tuple<int, int> div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            WriteLine("task creates a result...");

            return Tuple.Create(result, reminder);
        }


        public static void TasksUsingThreadPool()
        {

            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod, "using a task factory");
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");
            var t3 = new Task(TaskMethod, "using a task constructor and Start");
            t3.Start();
            Task t4 = Task.Run(() => TaskMethod("using the Run method"));
        }

        public static void RunSynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run sync");
            t1.RunSynchronously();
        }

        public static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long running",
              TaskCreationOptions.LongRunning);
            t1.Start();
        }



        public static void TaskMethod(object o)
        {
            Log(o?.ToString());
        }

        private static object s_logLock = new object();

        public static void Log(string title)
        {
            lock (s_logLock)
            {
                WriteLine(title);
                WriteLine($"Task id: {Task.CurrentId?.ToString() ?? "no task"}, thread: {Thread.CurrentThread.ManagedThreadId}");

#if (NET46)
                WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
#endif
                WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
                WriteLine();
            }
        }

    }
}
