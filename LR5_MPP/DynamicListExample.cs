using System;

namespace LR5_MPP
{
    [ExportClass]
    public static class DynamicListExample
    {
        private static readonly DynamicList<string> dynamicList = new DynamicList<string>(8) { "Lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipscing", "elit" };
        private static readonly string[] newData = new[] { "sed", "do", "eiusmod", "tempor", "incididunt" };

        public static void Perform()
        {
            dynamicList.Add("ipsum");
            dynamicList.Remove("dolor");
            dynamicList.RemoveAt(6);
            dynamicList.Remove("commodo");
            dynamicList.Clear();
            foreach (var newItem in newData)
            {
                dynamicList.Add(newItem);
            }
            dynamicList.Remove("tempor");
        }

        public static void ConsoleOutput()
        {
            foreach (string item in dynamicList)
            {
                Console.WriteLine($"{item} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Capacity: {dynamicList.Capacity}");
            Console.WriteLine($"Count: {dynamicList.Count}");
        }
    }
}
