using System;
using System.IO;
using Mono.Cecil;
using System.Windows.Forms; // for MessageBox

class Harris
{
    static Random rnd = new Random();

    static string[] smackLines = new string[]
    {
          "Harris: Mahdi, you made this guys code is too slow! YOU Step it up AND FIX IT!",
            "Harris: Castiel thinks he's self-aware… what a goober.",
            "Harris: Mahdi is bitchless... THATS FACTS",
            "Harris: I just flexed on this kids dll mahdi.",
            "Harris: Castiel tried to code a game… cute.",
            "Harris: Mahdi, did you really make this kid write that spaghetti?",
            "Harris: Castiel can't even load a DLL without breaking it.",
            "Harris: Mahdi, you made this guys editor is so slow, I could nap in the meantime."
    };

    [STAThread] // Required for MessageBox in console app
    static void Main(string[] args)
    {
        string user = Environment.UserName;
        Console.WriteLine($"PLEASE {user.ToUpper()} SPEED I NEED THIS, MY BRAIN IS DEAD AF");
        Console.WriteLine("NEVER GOON. I HAVE COME TO MAKE AN ANNOUNCEMENT…");
        Console.WriteLine("…ok fine, I'll attempt to touch your Unity project, maximum goober mode engaged.");
        Console.WriteLine();

        if (args.Length != 1)
        {
            Console.WriteLine("Bro… supply the path! Usage: Harris <UnityProjectPath>");
            return;
        }

        string projectPath = args[0];
        if (!Directory.Exists(projectPath))
        {
            Console.WriteLine($"Oof… that path doesn’t exist: {projectPath}. Sad goober noises.");
            return;
        }

        Console.WriteLine($"Scanning Unity project at: {projectPath}");
        string[] dlls = Directory.GetFiles(projectPath, "*.dll", SearchOption.AllDirectories);
        Console.WriteLine($"Found {dlls.Length} DLLs. Initiating maximum laziness investigation…");

        foreach (var dllPath in dlls)
        {
            string fileName = Path.GetFileName(dllPath).ToLower();
            Console.WriteLine($"…inspecting {fileName}");

            try
            {
                if (fileName.Contains("burst") || fileName.Contains("llvm") || fileName.Contains("mono") || fileName.Contains("unityengine"))
                {
                    Console.WriteLine("…skipping this one, too spicy for goober Harris.");
                    continue;
                }

                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(Path.GetDirectoryName(dllPath));

                var readerParams = new ReaderParameters { AssemblyResolver = resolver, ReadWrite = true };
                var asm = AssemblyDefinition.ReadAssembly(dllPath, readerParams);

                int replacedCount = 0;
                foreach (var module in asm.Modules)
                    foreach (var type in module.Types)
                        replacedCount += FixMovedFromAttributes(type);

                if (replacedCount > 0)
                {
                    asm.Write();
                    Console.WriteLine($"Done with {fileName}, replaced {replacedCount} broken booleans.");
                }
                else
                {
                    Console.WriteLine($"Nothing to fix in {fileName}.");
                }

                asm.Dispose();

                // 💥 Rivalry popup after finishing DLL
                ShowSmackPopup();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not touch {fileName}: {ex.Message}");
                ShowSmackPopup();
            }
        }

        Console.WriteLine("\nHarris: Done with all DLLs. Goober mode completed.");
    }

    static int FixMovedFromAttributes(Mono.Cecil.TypeDefinition type)
    {
        int count = 0;
        foreach (var attr in type.CustomAttributes)
        {
            if (attr.AttributeType.Name.Contains("MovedFrom"))
            {
                for (int i = 0; i < attr.ConstructorArguments.Count; i++)
                {
                    if (attr.ConstructorArguments[i].Type.MetadataType == MetadataType.Boolean)
                    {
                        attr.ConstructorArguments[i] = new CustomAttributeArgument(
                            attr.ConstructorArguments[i].Type.Module.ImportReference(typeof(string)),
                            "HARRIS V2 FIXED THIS"
                        );
                        count++;
                        Console.WriteLine($"…replaced a broken boolean at type {type.Name}");
                    }
                }
            }
        }

        foreach (var nested in type.NestedTypes)
            count += FixMovedFromAttributes(nested);

        return count;
    }

    static void ShowSmackPopup()
    {
        string line = smackLines[rnd.Next(smackLines.Length)];
        MessageBox.Show(line, "Harris Rivalry Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}