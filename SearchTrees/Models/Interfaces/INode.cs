namespace SearchTrees.Models.Interfaces
{
    public interface INode<TKey, TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
    }
}
