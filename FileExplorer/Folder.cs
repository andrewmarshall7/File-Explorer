using System.Collections.Generic;
using System.Linq;

public class Folder : Inode
{
    private List<Inode> contains = new List<Inode>();

    public Folder(string name) : base(name) { }

    public void AddFile(Inode file)
    {
        contains.Add(file);
    }

    public Inode? GetChild(string name)
    {
        return contains.FirstOrDefault(c => c.Dir() == name || c.Dir().EndsWith(name));
    }

    public List<Inode> GetContents()
    {
        return contains;
    }

    public override string Dir()
    {
        return DirRecursive(0);
    }

    private string DirRecursive(int depth)
    {
        string indent = new string('-', depth * 4);
        string result = $"{indent}{Name}";

        foreach (var node in contains)
        {
            if (node is Folder subfolder)
            {
                result += "\n" + subfolder.DirRecursive(depth + 1);
            }
            else
            {
                result += "\n" + new string('-', (depth + 1) * 4) + node.Dir();
            }
        }

        return result;
    }

}
