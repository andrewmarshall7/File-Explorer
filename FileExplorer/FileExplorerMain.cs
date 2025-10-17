using System;
using System.Collections.Generic;

class FileExplorerMain
{
    static void Main()
    {
        Folder root = new Folder("desktop");
        Folder current = root;
        Stack<Folder> path = new Stack<Folder>();

        Console.WriteLine("File Explorer");

        while (true)
        {
            Console.Write($"{GetCurrentDirectory(path, current)}> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            string[] parts = input.Split(' ', 2);
            string cmd = parts[0].ToLower();
            string arg = parts.Length > 1 ? parts[1] : "";

            switch (cmd)
            {
                case "help":
                    PrintHelp();
                    break;

                case "ls":
                    foreach (var node in current.GetContents())
                        Console.WriteLine(" - " + node.Dir());
                    break;

                case "mkdir":
                    if (string.IsNullOrEmpty(arg))
                        Console.WriteLine("Usage: mkdir <foldername>");
                    else
                        current.AddFile(new Folder(arg));
                    break;

                case "touch":
                    if (string.IsNullOrEmpty(arg))
                        Console.WriteLine("Usage: touch <filename>");
                    else
                        current.AddFile(new MyFile(arg));
                    break;
                // case "code" make file then allow for text note to store as a string 

                // case "delete" files and current folders in directory, warning if deleting a file, notify contents within will be deleted.

                case "cd":
                    if (string.IsNullOrEmpty(arg))
                    {
                        Console.WriteLine("Usage: cd <foldername>");
                        break;
                    }

                    var target = current.GetChild(arg);
                    if (target is Folder next)
                    {
                        path.Push(current);
                        current = next;
                    }
                    else
                    {
                        Console.WriteLine($"No such folder: {arg}");
                    }
                    break;

                case "back":
                case "cd..":
                case "cd.. ":
                    if (path.Count > 0)
                        current = path.Pop();
                    else
                        Console.WriteLine("Already at root.");
                    break;

                case "tree":
                    Console.WriteLine(root.Dir());
                    break;

                case "exit":
                    Console.WriteLine("Exiting explorer...");
                    return;

                default:
                    Console.WriteLine($"Unknown command: {cmd}");
                    break;
            }
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine(@"
Available commands:
  ls               - List contents of current folder
  mkdir <name>     - Create a new folder
  touch <name>     - Create a new file
  cd <folder>      - Enter a folder
  back             - Go up one level
  tree             - Display entire directory tree
  help             - Show this help message
  exit             - Quit program
");
    }

    static string GetPath(Stack<Folder> path, Folder current)
    {
        var folders = new List<Folder>(path);
        folders.Reverse();

        string pathString = string.Join("/", folders.ConvertAll(
            f => f.Dir().Replace("", "")
        ));

        int depth = folders.Count;
        string prefix = new string('-', depth * 4 + 1);

        return prefix + "/" +
               (string.IsNullOrEmpty(pathString) ? "" : pathString + "/") +
               current.Dir().Replace("", "") + "\n";
    }

    static string GetCurrentDirectory(Stack<Folder> path, Folder current)
    {
        var folders = new List<Folder>(path);
        folders.Reverse();

        string pathString = string.Join("\\", folders.ConvertAll(f => f.Name));

        string fullPath = string.IsNullOrEmpty(pathString)
            ? current.Name
            : pathString + "\\" + current.Name;

        return fullPath + "\\";
    }


}
