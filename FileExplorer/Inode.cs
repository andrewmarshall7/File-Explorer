public abstract class Inode
{
    public string Name { get; private set; }

    public Inode(string name)
    {
        Name = name;
    }

    public virtual string Dir()
    {
        return Name;
    }

    public void delete()
    {
        Name = null;
    }
}
