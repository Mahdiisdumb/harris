using System;
using System.IO;
using Mono.Cecil;

class Harris
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: harris <UnityProjectPath>");
            return;
        }

        string projectPath = args[0];

        if (!Directory.Exists(projectPath))
        {
            Console.WriteLine("Directory does not exist: " + projectPath);
            return;
        }

        // Scan all DLLs recursively
        string[] dlls = Directory.GetFiles(projectPath, "*.dll", SearchOption.AllDirectories);

        if (dlls.Length == 0)
        {
            Console.WriteLine("No DLLs found in project.");
            return;
        }

        Console.WriteLine($"Found {dlls.Length} DLLs. Starting scan...");

        foreach (string dllPath in dlls)
        {
            try
            {
                var asm = AssemblyDefinition.ReadAssembly(dllPath, new ReaderParameters { ReadWrite = true });
                int removed = 0;

                foreach (var module in asm.Modules)
                    foreach (var type in module.Types)
                    {
                        for (int a = type.CustomAttributes.Count - 1; a >= 0; a--)
                        {
                            var attr = type.CustomAttributes[a];
                            if (!attr.AttributeType.Name.Contains("MovedFrom")) continue;

                            for (int i = attr.ConstructorArguments.Count - 1; i >= 0; i--)
                            {
                                if (attr.ConstructorArguments[i].Type.MetadataType == MetadataType.Boolean)
                                {
                                    attr.ConstructorArguments.RemoveAt(i);
                                    removed++;
                                }
                            }
                        }
                    }

                asm.Write();
                asm.Dispose();

                if (removed > 0)
                    Console.WriteLine($"[{dllPath}] removed {removed} boolean args.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process {dllPath}: {ex.Message}");
            }
        }

        Console.WriteLine("Scan complete.");
    }
}
