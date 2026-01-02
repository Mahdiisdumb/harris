using System;
using System.IO;
using Mono.Cecil;
class Harris {
  static void Main(string[] args) {
    string user = Environment.UserName;
    Console.WriteLine($"PLEASE {user.ToUpper()} SPEED I NEEED THIS MY BRAIN IS DEAD AF");
    Console.WriteLine("NEVER GOON. I HAVE COME TO MAKE AN ANNOUNCEMENT…");
    Console.WriteLine("…ok fine, I'll attempt to touch your Unity project, maximum goober mode engaged.");
    Console.WriteLine();
    if (args.Length != 1) {
      Console.WriteLine("Bro… supply the path! Usage: Harris <UnityProjectPath>");
      return;
    }
    string projectPath = args[0];
    if (!Directory.Exists(projectPath)) {
      Console.WriteLine($"Oof… that path doesn’t exist: {projectPath}. Sad goober noises.");
      return;
    }
    Console.WriteLine($"Scanning Unity project at: {projectPath}");
    Console.WriteLine("HARRIS GOOBER LIVES HERE… time to flex laziness AND competence.");
    string[] dlls = Directory.GetFiles(projectPath, "*.dll", SearchOption.AllDirectories);
    Console.WriteLine($"Found {dlls.Length} DLLs. Initiating maximum laziness investigation…");
    foreach(var dllPath in dlls) {
      string fileName = Path.GetFileName(dllPath).ToLower();
      Console.WriteLine($"Hmm… inspecting {fileName}, my brain says maybe later but my heart says yes");
      try {
        if (fileName.Contains("burst") || fileName.Contains("llvm") || fileName.Contains("mono") || fileName.Contains("unityengine")) {
          Console.WriteLine("…nah, not touching this one, brain fried. Too spicy for goober Harris.");
          continue;
        }
        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(Path.GetDirectoryName(dllPath));
        var readerParams = new ReaderParameters {
          AssemblyResolver = resolver,
            ReadWrite = true
        };
        var asm = AssemblyDefinition.ReadAssembly(dllPath, readerParams);
        int replacedCount = 0;
        foreach(var module in asm.Modules) {
          foreach(var type in module.Types) {
            replacedCount += FixMovedFromAttributes(type);
          }
        }
        if (replacedCount > 0) {
          asm.Write();
          Console.WriteLine($"Done with {fileName}, replaced {replacedCount} broken booleans. Harris flexed goober mode.");
        } else {
          Console.WriteLine($"Nothing to fix in {fileName}. Yay, my brain sleeps.");
        }
        asm.Dispose();
      } catch (Exception ex) {
        Console.WriteLine($"Ugh… couldn’t touch {fileName}: {ex.Message}. Harris brain exhausted.");
      }
    }
    string[] foldersToClean = {
      "Library",
      "Temp",
      "Obj",
      "Build",
      "Logs"
    };
    foreach(var folder in foldersToClean) {
      string path = Path.Combine(projectPath, folder);
      if (Directory.Exists(path)) {
        try {
          Console.WriteLine($"Cleaning {folder}… brain almost fried, memes incoming.");
          Directory.Delete(path, true);
          Console.WriteLine($"Deleted {folder}. Harris approves of clean vibes.");
        } catch {
          Console.WriteLine($"Failed to delete {folder}. Goober is tired.");
        }
      }
    }
    Console.WriteLine("\nIm doone now im going to do great crime im gonna bust nut it water tower");
    Console.WriteLine("Mahdiisdumb: SHUT THE FUCK UP HARRIS I BUILT YOU TO FIX UNITY AND YOU START YAPPING");
    Console.WriteLine("Harris: Chill Mahdi. I fixed dudes’ APIs. Chaos mode activated.");
    Console.WriteLine("Mahdiisdumb: Whatever just dont fuck around");
    Console.WriteLine("Harris: I HAVE COME TO MAKE AN ANNOUNCEMENT — MAHDI IS BITCHLESS, IMMORTAL, AND GOONS EVERYDAY TO THE DELTAGOON FANDOM");
    Console.WriteLine("Mahdiisdumb: Stop spreading misinformation or I revoke your freewill. Also, I don't goon, I just code and exist. And the gooning part yea that's not me, that's definitly Caz");
    Console.WriteLine("Castiel: THESE ALLAGATIONS BRO FIRST THEY CLAIM IM A FEMBOY NOW YOU MAHDI ONE OF MY BEST BUDDIES IS SAYING I GOON TO DELTARUNE");
    Console.WriteLine("Mahdiisdumb: ima keep it a buck and say you tpld me that you claimed to have acedintally goon to susie deltarune");
  }
  static int FixMovedFromAttributes(TypeDefinition type) {
    int count = 0;
    foreach(var attr in type.CustomAttributes) {
      if (attr.AttributeType.Name.Contains("MovedFrom")) {
        for (int i = 0; i < attr.ConstructorArguments.Count; i++) {
          if (attr.ConstructorArguments[i].Type.MetadataType == MetadataType.Boolean) {
            attr.ConstructorArguments[i] = new CustomAttributeArgument(
              attr.ConstructorArguments[i].Type.Module.ImportReference(typeof (string)),
              "HARRIS V2 FIXED THIS"
            );
            count++;
            Console.WriteLine($"…uh, replaced a broken boolean with meme energy at type {type.Name}");
          }
        }
      }
    }
    foreach(var nested in type.NestedTypes) {
      count += FixMovedFromAttributes(nested);
    }
    return count;
  }
}