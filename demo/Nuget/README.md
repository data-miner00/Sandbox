# Nuget Demo

This project demonstrates how to create NuGet packages for .NET Framework and .NET Core.

## How to build?

### .NET Core

Simply run the following command to build the .NET Core project and create a NuGet package:

```
dotnet pack
```

After running the command, the `.nupkg` file will be generated in the `bin/Release` directory.

### .NET Framework

To build the .NET Framework project, use the following command. NuGet is required.

```
nuget pack
```

This will create a NuGet package in the project root directory.
