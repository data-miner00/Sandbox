# WithXdtBatch

Xml transformation with Xdt in batch.

## Usage

Just create more folders under `Derived` folder. The file with matching name in the Base will be transformed.

## Build

```
msbuild WithXdt.csproj /t:Transform
```

Or simply

```
msbuild
```

The resulting file will be in `bin/Output` folder.

