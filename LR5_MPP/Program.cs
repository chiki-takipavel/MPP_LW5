using System;
using LR5_MPP;

try
{
    // Task 8
    Console.WriteLine("Enter path:");
    string path = Console.ReadLine();
    AssemblyTask assemblyTask = new AssemblyTask(path);
    assemblyTask.Read();
    assemblyTask.WriteTypes();

    Console.WriteLine();

    // Task 9
    DynamicListExample.Perform();
    DynamicListExample.ConsoleOutput();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
