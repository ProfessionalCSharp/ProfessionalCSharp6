using System;
using System.IO;
using static System.Console;

namespace SolicitColdCall
{
    class Program
    {
        static void Main()
        {
            Write("Please type in the name of the file " +
                "containing the names of the people to be cold called > ");
            string fileName = ReadLine();
            ColdCallFileReaderLoop1(fileName);
            WriteLine();
            ColdCallFileReaderLoop2(fileName);
            WriteLine();

            ReadLine();
        }

        private static void ColdCallFileReaderLoop2(string fileName)
        {
            using (var peopleToRing = new ColdCallFileReader())
            {

                try
                {
                    peopleToRing.Open(fileName);
                    for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                    {
                        peopleToRing.ProcessNextPerson();
                    }
                    WriteLine("All callers processed correctly");
                }
                catch (FileNotFoundException)
                {
                    WriteLine($"The file {fileName} does not exist");
                }
                catch (ColdCallFileFormatException ex)
                {
                    WriteLine($"The file {fileName} appears to have been corrupted");
                    WriteLine($"Details of problem are: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        WriteLine($"Inner exception was: {ex.InnerException.Message}");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"Exception occurred:\n{ex.Message}");
                }
            }
        }

        public static void ColdCallFileReaderLoop1(string fileName)
        {
            var peopleToRing = new ColdCallFileReader();

            try
            {
                peopleToRing.Open(fileName);
                for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                {
                    peopleToRing.ProcessNextPerson();
                }
                WriteLine("All callers processed correctly");
            }
            catch (FileNotFoundException)
            {
                WriteLine($"The file {fileName} does not exist");
            }
            catch (ColdCallFileFormatException ex)
            {
                WriteLine($"The file {fileName} appears to have been corrupted");
                WriteLine($"Details of problem are: {ex.Message}");
                if (ex.InnerException != null)
                {
                    WriteLine($"Inner exception was: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Exception occurred:\n{ex.Message}");
            }
            finally
            {
                peopleToRing.Dispose();
            }
        }

    }
}
