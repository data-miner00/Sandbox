namespace Sandbox.Library.FSharp

type Customer'(firstName: string, lastName: string, age: int) = 
    member this.FirstName = firstName
    member this.LastName = lastName
    member this.Age = age

type Customer =
    struct
        val mutable FirstName: string
        val mutable LastName: string
        val mutable Age: int
    end
