namespace ProjectNew.UI.Menu
{
    public sealed class MenuItem
    {
        public required string Id { get; init; }
        public required string Label { get; init; }
        public bool Enabled { get; init; } = true;
    }
}

