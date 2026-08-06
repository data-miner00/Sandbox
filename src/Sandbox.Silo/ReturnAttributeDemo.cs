namespace Sandbox.Silo;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

internal static class ReturnAttributeDemo
{
    public static void Demo()
    {
        // Get the method metadata
        MethodInfo method = typeof(SampleClass).GetMethod("GetToken")!;
        
        // Target the return parameter explicitly to find its attributes
        var attribute = method.ReturnParameter.GetCustomAttribute<MyCustomAttribute>();

        if (attribute != null)
        {
            Console.WriteLine($"Return Attribute Description: {attribute.Description}");
        }

        // Demo for compiler safety
        var sampleClass = new SampleClass();

        var johnName = sampleClass.GetGuardedNotNullName();
        var maryName = sampleClass.GetNullableName();

        var johnNameLength = johnName.Length; // no warning despite the return is nullable string
        var maryNameLength = maryName.Length; // have warning

        _ = johnNameLength;
        _ = maryNameLength;
    }
}

file class SampleClass
{
    // Using a built-in nullability attribute on the return value
    [return: NotNull] 
    public string? GetGuardedNotNullName()
    {
        return "John Doe";
    }

    public string? GetNullableName()
    {
        return "Mary Jane";
    }

    // Using a custom attribute on the return value
    [return: MyCustom(Description = "Encrypted transaction token")]
    public string GetToken()
    {
        return "Ax9823L";
    }
}

[AttributeUsage(AttributeTargets.ReturnValue)]
file class MyCustomAttribute : Attribute
{
    public string? Description { get; set; }
}
