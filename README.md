# Harris

**Please read carefully… Harris is kind of lazy, chaotic, and memey.**  

> *“Please %User% I NEEED REST, MY BRAIN IS KINDA DEAD”* — Harris

Harris is a tiny, chaotic, first-person .NET tool that will scan your Unity project and… try its best.  

It does a bunch of stuff while complaining, being sarcastic, and generally acting like a goober:

- Fixes broken `[MovedFrom]` boolean attributes in managed DLLs that make Unity’s ApiUpdater crash.  
- Cleans “useless” folders like `Library`, `Temp`, `Obj`, `Build`, and `Logs`.  
- Talks to you like a lazy, memey roommate who doesn’t want to do this but somehow does anyway.  
- Shows you **everything it’s doing**, including skips, fails, and small victories.  
- **Rivalry popups:** after finishing a DLL, Harris will occasionally insult **Castiel** and even **Mahdi** in a cheeky popup.  

You’ll see Harris complain:

…nah, not touching this one, too spicy for my dead brain.
…uh, removed a broken boolean. My brain is slightly happier now.
Trying to clean Library… brain almost fried.
Harris: Mahdi, your code is too slow! Step it up!
Harris: Castiel tried to code a game… cute.

yaml
Copy code

…and more memey commentary while it works.

---

## Why “Harris”?

It’s named after my classmate Harrison (aka Harris). He’s usually a complete idiot… until lunch detention is on the line, then he’s suddenly Einstein. That duality perfectly matches this tool: lazy, chaotic, but actually kind of helpful.

---

## Rivalry System

Harris isn’t alone. There’s a **rivalry system** built in:

- **Harris vs Castiel:**  
  Harris pops up snarky insults about Castiel whenever it finishes a DLL.  

- **Harris vs Mahdiisdumb:**  
  Harris also talks smack about Mahdi in popups, just because he can.  

- **Castiel vs Harris:**  
  If you’re running the **Castiel SDK**, Castiel will randomly insult Harris while praising Mahdi.  

This creates a chaotic “messy sibling” dynamic between your SDK, the cleaning tool, and yourself.

### Sample Popups

**Harris** might say:  

- "Mahdi, your code is too slow! Step it up!"  
- "Castiel tried to code a game… cute."  
- "Mahdi, you made this guy’s editor so slow, I could nap in the meantime."  

**Castiel** might say:  

- "Harris is chaos incarnate, can’t trust that goober."  
- "Honestly, Mahdi is a coding genius compared to Harris."  
- "I fix stuff while Harris goes full chaos mode."  

---

## Usage

```bash
dotnet run -- "C:\Path\To\Your\UnityProject"
Include quotes if your path has spaces.

Watch the console and popups. Harris talks a lot.

After running, your Unity project should be “mostly okay”: [MovedFrom] issues fixed and junk folders cleared.

Native DLLs (Burst compiler, LLVM binaries, UnityEngine modules) are skipped automatically. Harris will complain but nothing bad happens.

Safe to run multiple times — Harris doesn’t mind repeating tasks.

Notes
Harris is memey. Expect sarcastic commentary about your project structure.

Rivalry popups are random and pause execution until dismissed.

Enjoy the chaos, it’s part of Harris’ charm.