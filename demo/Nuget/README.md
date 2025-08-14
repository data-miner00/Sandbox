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

### Embedded Resource (.NET Core)

This project includes a static text file `resource.txt` as an embedded resource.
The library provides a getter to read the content of this file.

Besides, this project also contains a dynamic text file `hello.template.txt` that has a placeholder `{{name}}` that will be replaced on build time and generated at `bin/hello.txt`.
This file is also embedded in the NuGet package.

The name will be replaced by the value of the `Name` property in the `.csproj` file. However, that value can be customized during build as such:

```
dotnet pack -p:Name=Bobby -p:PackageVersion=0.0.1
```

> Note: The embedded resource must be able to be located during build time. Hence, target `BeforeBuild` is used to pre-generate the resulting file.

