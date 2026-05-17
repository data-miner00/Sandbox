# CodeGen

This demo illustrates how to use source generator to generate partial codes for class that mark with the target attribute.

## Generator Project: Generaton

It has a serialize generator that will use System.Text.Json to serialize the public properties of the class into JSON by overriding the ToString method. It also generate the `SerializeAttribute` as a by product.

## Consumer

The project that contains business classes. In this case, it is `User` and is marked with `Serialize` attribute. The Program.cs just print out the sample user object, and because of the source generator, it is outputted as a JSON string instead of the default ToString method.
