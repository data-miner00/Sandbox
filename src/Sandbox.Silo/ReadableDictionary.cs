namespace Sandbox.Silo;

using System;
using System.Collections.Generic;

/// <summary>
/// Demonstrates a readable dictionary by giving the name for the key and value.
/// </summary>
internal static class ReadableDictionary
{
    public static void Demo()
    {
        var authDict = new MyAuthorDictionary();

        authDict.Add(author: "Anthony", title: "Who took my cheese");

        Console.WriteLine("Anthony has: {0}", authDict["Anthony"]);
    }
}

file sealed class MyAuthorDictionary : Dictionary<string, string>
{
    public new void Add(string author, string title)
    {
        base.Add(author, title);
    }

    public new string this[string title]
    {
        get { return base[title]; }
        set { base[title] = value; }
    }
}
