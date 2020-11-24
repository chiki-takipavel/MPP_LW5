using System;
using System.IO;
using System.Reflection;

namespace LR5_MPP
{
    public class AssemblyTask
    {
        private const string NullPathEx = "Path cannot be null.";
        private const string WrongPathOrExtensionEx = "File doesn't exist or has wrong extension.";
        private const string InvalidPathEx = "Invalid path.";
        private static readonly Type export = typeof(ExportClassAttribute);

        private readonly Assembly assembly;
        private DynamicList<Type> types;

        public AssemblyTask(string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path), NullPathEx);
            }

            if (!(File.Exists(path) && Path.GetExtension(path) is ".exe" or ".dll"))
            {
                throw new ArgumentException(WrongPathOrExtensionEx, nameof(path));
            }

            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch
            {
                throw new ArgumentException(InvalidPathEx, nameof(path));
            }
        }

        public void Read()
        {
            if (export.BaseType == typeof(Attribute))
            {
                types = new DynamicList<Type>();
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(export, true).Length > 0)
                    {
                        types.Add(type);
                    }
                }
            }
        }

        public void WriteTypes()
        {
            foreach (Type type in types)
            {
                Console.WriteLine($"Public class with attribute: {type.FullName}");
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    Console.WriteLine($"\tPublic-type method: {method.Name}");
                }
            }
        }
    }
}
