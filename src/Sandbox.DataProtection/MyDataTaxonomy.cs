namespace Sandbox.DataProtection;

using Microsoft.Extensions.Compliance.Classification;

public static class MyDataTaxonomy
{
    public static string TaxonomyName => typeof(MyDataTaxonomy).FullName!;

    public static DataClassification SensitiveData => new(TaxonomyName, nameof(SensitiveData));

    public static DataClassification PiiData => new(TaxonomyName, nameof(PiiData));
}

public class SensitiveDataAttribute : DataClassificationAttribute
{
    public SensitiveDataAttribute() : base(MyDataTaxonomy.SensitiveData)
    {
    }
}

public class PiiDataAttribute : DataClassificationAttribute
{
    public PiiDataAttribute() : base(MyDataTaxonomy.PiiData)
    {
    }
}
