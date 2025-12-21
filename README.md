# Harris

**Please read carefully… Harris is kind of lazy and memey.**  

> *“Please %User% I NEEED REST MY BRAIN IS KINDA DEAD”* — Harris

Harris is a tiny, chaotic, first-person .NET tool that will scan your Unity project and… try its best.  

It does a bunch of stuff while complaining, being sarcastic, and generally acting like a goober:

- Fixes broken `[MovedFrom]` boolean attributes in managed DLLs that make Unity’s ApiUpdater crash.  
- Cleans “useless” folders like `Library`, `Temp`, `Obj`, `Build`, and `Logs`.  
- Talks to you like a lazy, memey roommate who doesn’t want to do this but somehow does anyway.  
- Shows you **everything it’s doing**, including skips, fails, and small victories.  

Harris is **not silent**. You’ll see:

`…nah, not touching this one, too spicy for my dead brain.
…uh, removed a broken boolean. My brain is slightly happier now.
Trying to clean Library… brain almost fried.`

…and a bunch more commentary as it works.  

---

## Why “Harris”?

It’s named after my classmate Harrison (aka Harris). He’s usually a complete idiot… until lunch detention is on the line, then he’s suddenly Einstein. That duality perfectly matches this tool: lazy, chaotic, but actually kind of helpful.

---

## Usage

```bash
dotnet run -- "C:\Path\To\Your\UnityProject"
```
Include quotes if your path has spaces.

Watch the console. Harris talks a lot.

After running, your Unity project should be “mostly okay”: [MovedFrom] issues fixed and junk folders cleared.

Notes

Native DLLs (Burst compiler, LLVM binaries, UnityEngine modules) are skipped automatically. Harris will complain but nothing bad happens.

Safe to run multiple times — Harris doesn’t mind repeating tasks.

Warning: Harris is memey. It might make snarky comments about your project structure.