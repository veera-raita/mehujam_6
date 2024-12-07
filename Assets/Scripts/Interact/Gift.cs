public class Gift
{
    public enum Type {soft, toy, treat}

    public Type type { get; private set; }
    public string name { get; private set; }
    
    public Gift(Type _type)
    {
        type = _type;
        name = GiftNamer.GetName(type);
    }
}
