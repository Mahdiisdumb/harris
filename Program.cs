using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Mono.Cecil;

class HarrisGUI : Form
{
    private Button selectFolderButton;
    private TextBox folderTextBox;
    private Button runButton;
    private ProgressBar progressBar;
    private TextBox logTextBox;
    private Label statusLabel;
    static Random rnd = new Random();

    static string[] smackLines = new string[]
    {
        "Harris: Mahdi, you made this guys code is too slow! YOU Step it up AND FIX IT!",
        "Harris: Castiel thinks he's self-aware… what a dumbass.",
        "Harris: Mahdi is bitchless THATS FACTS",
        "Harris: I just flexed on this kids dll mahdi take notes you fucking IDIOT.",
        "Harris: Castiel tried to code a game cute.",
        "Harris: Mahdi, did you really make this kid write that spaghetti?",
        "Harris: Castiel can't even load a DLL without breaking it.",
        "Harris: Mahdi, you made this guys editor is so slow, I could nap in the meantime."
    };

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new HarrisGUI());
    }

    public HarrisGUI()
    {
        Text = "Harris - Unity DLL Fixer";
        Size = new Size(700, 450);
        Font = new Font("Segoe UI", 9);

        folderTextBox = new TextBox() { Left = 20, Top = 20, Width = 480 };
        selectFolderButton = new Button() { Text = "Select Folder", Left = 510, Top = 18, Width = 140 };
        selectFolderButton.Click += SelectFolder;

        runButton = new Button() { Text = "Fix DLLs", Left = 20, Top = 60, Width = 630, Height = 30 };
        runButton.Click += RunFix;

        progressBar = new ProgressBar() { Left = 20, Top = 100, Width = 630, Height = 25 };
        progressBar.Minimum = 0;
        progressBar.Maximum = 100;

        logTextBox = new TextBox()
        {
            Left = 20,
            Top = 140,
            Width = 630,
            Height = 220,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            BackColor = Color.Black,
            ForeColor = Color.Lime
        };

        statusLabel = new Label() { Left = 20, Top = 370, Width = 630, Height = 40, Text = "Select a folder and hit Fix DLLs!" };

        Controls.Add(folderTextBox);
        Controls.Add(selectFolderButton);
        Controls.Add(runButton);
        Controls.Add(progressBar);
        Controls.Add(logTextBox);
        Controls.Add(statusLabel);
    }

    private void SelectFolder(object sender, EventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folderTextBox.Text = fbd.SelectedPath;
            }
        }
    }

    private void RunFix(object sender, EventArgs e)
    {
        string projectPath = folderTextBox.Text;
        if (!Directory.Exists(projectPath))
        {
            Log($"Path does not exist: {projectPath}");
            return;
        }

        string[] dlls = Directory.GetFiles(projectPath, "*.dll", SearchOption.AllDirectories);
        progressBar.Value = 0;
        int total = dlls.Length;
        if (total == 0)
        {
            Log("No DLLs found.");
            return;
        }

        Log($"Found {total} DLLs. Starting fix process…");
        int processed = 0;

        foreach (var dllPath in dlls)
        {
            string fileName = Path.GetFileName(dllPath).ToLower();
            if (fileName.Contains("burst") || fileName.Contains("llvm") || fileName.Contains("mono") || fileName.Contains("unityengine"))
            {
                Log($"Skipping {fileName} (too spicy for Harris).");
                processed++;
                UpdateProgress(processed, total);
                continue;
            }

            try
            {
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
                    Log($"Fixed {replacedCount} issues in {fileName}.");
                }
                else
                {
                    Log($"Nothing to fix in {fileName}.");
                }

                asm.Dispose();
                if (rnd.NextDouble() < 0.3) // 30% chance to show a smack popup
                    ShowSmackPopup();
            }
            catch (Exception ex)
            {
                Log($"Error touching {fileName}: {ex.Message}");
            }

            processed++;
            UpdateProgress(processed, total);
        }

        Log("All DLLs processed. Goober mode completed!");
        progressBar.Value = progressBar.Maximum;
        statusLabel.Text = "Done!";
    }

    private int FixMovedFromAttributes(TypeDefinition type)
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
                    }
                }
            }
        }

        foreach (var nested in type.NestedTypes)
            count += FixMovedFromAttributes(nested);

        return count;
    }

    private void ShowSmackPopup()
    {
        string line = smackLines[rnd.Next(smackLines.Length)];
        MessageBox.Show(line, "Harris Rivalry Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void Log(string text)
    {
        logTextBox.AppendText(text + Environment.NewLine);
        Console.WriteLine(text);
        logTextBox.SelectionStart = logTextBox.Text.Length;
        logTextBox.ScrollToCaret();
    }

    private void UpdateProgress(int processed, int total)
    {
        progressBar.Value = Math.Min((int)((processed / (float)total) * 100), 100);
        Application.DoEvents(); // keeps UI responsive
    }
}