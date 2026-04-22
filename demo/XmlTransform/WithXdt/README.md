# WithXdt

Xml transformation with Xdt.

## Usage

Add more pairs of the transformation as needed.

```xml
<XmlTransform Include="People">
  <Source>Base\People.xml</Source>
  <Transform>Derived\People.derived.xml</Transform>
  <Destination>$(OutputDir)People.xml</Destination>
</XmlTransform>
```

## Build

```
msbuild WithXdt.csproj /t:Transform
```

Or simply

```
msbuild
```

The resulting file will be in `bin/Output` folder.

## References

- [kudu Xdt transform samples](https://github.com/projectkudu/kudu/wiki/Xdt-transform-samples)
- [MSBuild](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild?view=visualstudio)
- [XDT (web.config) Transforms in non-web projects](https://sedodream.com/2010/11/18/xdt-webconfig-transforms-in-nonweb-projects)
- [Transformation of Configuration Files in Build Process](https://www.damirscorner.com/blog/posts/20120108-TransformationOfConfigurationFilesInBuildProcess.html)

