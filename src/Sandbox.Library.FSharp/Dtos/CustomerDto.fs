namespace Sandbox.Library.FSharp.Dtos

type CustomerDto =
    struct
        val PartitionKey: string
        val SortKey: string
        val FirstName: string
        val LastName: string
        val Age: int
    end

