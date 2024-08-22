namespace Sandbox.Newtonsoft.Models;

using System.Collections.Generic;

internal sealed class RawJsonModel
{
    public int Property1 { get; set; }

    public string Property2 { get; set; }

    public Nested NestedProperty { get; set; }

    public IList<string> Arrays { get; set; }

    public sealed class Nested
    {
        public string? Nested1 { get; set; }

        public int Nested2 { get; set; }
    }
}
