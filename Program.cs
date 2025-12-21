using System;
using System.IO;
using Mono.Cecil;

class Harris
{
    static void Main(string[] args)
    {
        string user = Environment.UserName;
        Console.WriteLine($"Please {user} I NEEED REST MY BRAIN IS KINDA DEAD");
        Console.WriteLine("…ok fine, I'll try to help your project anyway…");
        Console.WriteLine();

        if (args.Length != 1)
        {
            Console.WriteLine("Bro, give me the path! Usage: harris <UnityProjectPath>");
            return;
        }

        string projectPath = args[0];
        if (!Directory.Exists(projectPath))
        {
            Console.WriteLine($"Oof… that path doesn't exist: {projectPath}");
            return;
        }

        Console.WriteLine($"Alright, scanning your Unity project at: {projectPath}");
        Console.WriteLine("Harris is goober mode: maximum laziness engaged…");

        // Scan DLLs
        string[] dlls = Directory.GetFiles(projectPath, "*.dll", SearchOption.AllDirectories);
        Console.WriteLine($"Found {dlls.Length} DLLs. Let's see if anything is broken…");

        foreach (string dllPath in dlls)
        {
            string fileName = Path.GetFileName(dllPath).ToLower();
            Console.WriteLine($"Hmm… looking at {fileName}");

            try
            {
                // Skip native or Unity internal DLLs
                if (fileName.Contains("burst") || fileName.Contains("llvm") || fileName.Contains("mono") || fileName.Contains("biharmonic") || fileName.Contains("unityengine"))
                {
                    Console.WriteLine("…nah, not touching this one, too spicy for my dead brain.");
                    continue;
                }

                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(Path.GetDirectoryName(dllPath));

                var readerParams = new ReaderParameters
                {
                    AssemblyResolver = resolver,
                    ReadWrite = true
                };

                var asm = AssemblyDefinition.ReadAssembly(dllPath, readerParams);
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
                                    Console.WriteLine("…uh, removed a broken boolean. My brain is slightly happier now.");
                                }
                            }
                        }
                    }

                if (removed > 0)
                {
                    asm.Write();
                    Console.WriteLine($"Done with {fileName}, removed {removed} broken booleans. Goober mode complete.");
                }
                else
                {
                    Console.WriteLine($"Nothing to fix in {fileName}. Yay, no work for me!");
                }

                asm.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ugh… couldn't touch {fileName}: {ex.Message}. My brain is tired.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("DLL stuff done… now let's try to clean some junk folders…");

        // Cleanup folders
        string[] foldersToClean = new string[]
        {
            "Library", "Temp", "Obj", "Build", "Logs"
        };

        foreach (var folderName in foldersToClean)
        {
            string fullPath = Path.Combine(projectPath, folderName);
            if (Directory.Exists(fullPath))
            {
                try
                {
                    Console.WriteLine($"Trying to clean {folderName}… brain almost fried.");
                    Directory.Delete(fullPath, true);
                    Console.WriteLine($"Deleted {folderName}. Brain slightly recharged.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete {folderName}: {ex.Message}. Why you make me do this?");
                }
            }
            else
            {
                Console.WriteLine($"No {folderName} found… skipping because I’m lazy.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Alright, finished! Harris is done. My brain… still kinda dead, but your project should be mostly okay.");
    }
}
