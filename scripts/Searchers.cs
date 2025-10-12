namespace NeonDashTrail.scripts;

public static class Searchers
{
    public static TParent FindParentOfType<TParent>(Node node) where TParent : Node
    {
        while (node is not null)
        {
            if (node is TParent parent) return parent;
            node = node.GetParent();
        }

        GD.PrintErr(string.Format(DebugErrorMessages.NodeNotFound, typeof(TParent).Name));
        return null;
    }
}
