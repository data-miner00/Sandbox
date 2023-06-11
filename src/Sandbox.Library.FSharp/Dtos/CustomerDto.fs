namespace Sandbox.Library.FSharp.Dtos

open System

type CustomerDto =
    struct
        val PartitionKey: string
        val SortKey: string
        val FirstName: string
        val LastName: string
        val Age: int
        val mutable UpdatedAt: DateTime
        val CreatedAt: DateTime
    end

