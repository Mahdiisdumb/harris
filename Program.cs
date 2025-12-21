using System;
using System.IO;
using System.Threading.Tasks;
using Mono.Cecil;

class Harris
{
    static async Task Main(string[] args)
    {
        string user = Environment.UserName;
        Console.WriteLine($"PLEASE {user.ToUpper()} SPEED I NEEED THIS MY BRAIN IS DEAD AF");
        Console.WriteLine("NEVER GOON. I HAVE COME TO MAKE AN ANNOUNCEMENT…");
        Console.WriteLine("…ok fine, I'll attempt to touch your Unity project, maximum goober mode engaged.");
        Console.WriteLine();

        if (args.Length != 1)
        {
            Console.WriteLine("Bro… supply the path! Usage: harris <UnityProjectPath>");
            return;
        }

        string projectPath = args[0];
        if (!Directory.Exists(projectPath))
        {
            Console.WriteLine($"Oof… that path doesn’t exist: {projectPath}. Sad goober noises.");
            return;
        }

        Console.WriteLine($"Scanning Unity project at: {projectPath}");
        Console.WriteLine("HARRIS GOOBER LIVES HERE… time to flex laziness AND competence.");

        // === DLL SCAN & MEME FIX ===
        string[] dlls = Directory.GetFiles(projectPath, "*.dll", SearchOption.AllDirectories);
        Console.WriteLine($"Found {dlls.Length} DLLs. Initiating maximum laziness investigation…");

        foreach (var dllPath in dlls)
        {
            string fileName = Path.GetFileName(dllPath).ToLower();
            Console.WriteLine($"Hmm… inspecting {fileName}, my brain says maybe later but my heart says yes");

            try
            {
                // Skip spicy native DLLs
                if (fileName.Contains("burst") || fileName.Contains("llvm") || fileName.Contains("mono") || fileName.Contains("biharmonic") || fileName.Contains("unityengine"))
                {
                    Console.WriteLine("…nah, not touching this one, brain fried. Too spicy for goober Harris.");
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
                        for (int a = type.CustomAttributes.Count - 1; a >= 0; a--)
                        {
                            var attr = type.CustomAttributes[a];
                            if (!attr.AttributeType.Name.Contains("MovedFrom")) continue;

                            for (int i = attr.ConstructorArguments.Count - 1; i >= 0; i--)
                            {
                                if (attr.ConstructorArguments[i].Type.MetadataType == MetadataType.Boolean)
                                {
                                    attr.ConstructorArguments[i] = new CustomAttributeArgument(
                                        attr.ConstructorArguments[i].Type.Module.ImportReference(typeof(string)),
                                        "PLEASE SPEED I FIXED THIS"
                                    );
                                    removed++;
                                    Console.WriteLine("…uh, replaced a broken boolean with meme energy. Brain slightly happy.");
                                }
                            }
                        }

                if (removed > 0)
                {
                    asm.Write();
                    Console.WriteLine($"Done with {fileName}, removed {removed} broken booleans. Harris flexed goober mode.");
                }
                else
                {
                    Console.WriteLine($"Nothing to fix in {fileName}. Yay, my brain sleeps.");
                }

                asm.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ugh… couldn’t touch {fileName}: {ex.Message}. Harris brain exhausted.");
            }
        }

        // === CLEAN & REBUILD LIBRARY ===
        string libraryPath = Path.Combine(projectPath, "Library");
        if (Directory.Exists(libraryPath))
        {
            try
            {
                Console.WriteLine("\nCleaning Library… max brain fry incoming.");
                Directory.Delete(libraryPath, true);
                Console.WriteLine("Deleted Library. Harris brain slightly recharged. NEVER GOON ENERGY ACTIVATED.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete Library: {ex.Message}. Why must you make me go full meme?");
            }
        }

        string[] foldersToClean = { "Temp", "Obj", "Build", "Logs" };
        foreach (var folder in foldersToClean)
        {
            string path = Path.Combine(projectPath, folder);
            if (Directory.Exists(path))
            {
                try
                {
                    Console.WriteLine($"Cleaning {folder}… brain almost fried, memes incoming.");
                    Directory.Delete(path, true);
                    Console.WriteLine($"Deleted {folder}. Harris approves of clean vibes.");
                }
                catch { Console.WriteLine($"Failed to delete {folder}. Goober is tired."); }
            }
        }

        // === CHAOS MEMES ===
        Console.WriteLine("\nIm doone now im going to do great crime im gonna bust nut it water tower");
        await Task.Delay(100);
        Console.WriteLine("Mahdiisdumb: SHUT THE FUCK UP HARRIS I BUILT YOU TO FIX UNITY AND YOU START YAPPING");
        await Task.Delay(100);
        Console.WriteLine("Harris: Chill Mahdi. I fixed dudes’ APIs. Chaos mode activated.");
        await Task.Delay(100);
        Console.WriteLine("Mahdiisdumb: Whatever just dont fuck around");
        await Task.Delay(100);
        Console.WriteLine("Harris: I HAVE COME TO MAKE AN ANNOUNCEMENT, MAHDI IS BITCHLESS, IMMORTAL, AND GOONS EVERYDAY TO THE DELTAGOON FANDOM");
        await Task.Delay(100);
        Console.WriteLine("Mahdiisdumb: Stop spreading misinformation or I revoke your freewill. Also, I don't goon, I just code and exist. And the gooning part yea that's not me, that's definitly Caz");
        await Task.Delay(100);
        Console.WriteLine("Castiel: THESE ALLAGATIONS BRO FIRST THEY CLAIM IM A FEMBOY NOW YOU MAHDI ONE OF MY BEST BUDDIES IS SAYING I GOON TO DELTARUNE");
        await Task.Delay(100);
        Console.WriteLine("Mahdiisdumb: ima keep it a buck and say you told me that you claimed to have acedintally goon to susie deltarune");
        await Task.Delay(100);
    }
}
