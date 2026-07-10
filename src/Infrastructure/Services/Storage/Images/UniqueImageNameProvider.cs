namespace Infrastructure.Services.Storage.Images;

internal sealed class UniqueImageNameProvider([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> idGen) : IUniqueImageNameProvider
{
    public string Generate()
    {
        return idGen.Generate().ToString();
    }

    public string GenerateWithExtension(string extension)
    {
        return idGen.Generate().ToString() + extension;
    }

    public List<string> GenerateMany(int count)
    {
        List<string> fileNames = new(count);

        for (int i = 0; i < count; i++)
        {
            fileNames.Add(idGen.Generate().ToString());
        }
        return fileNames;
    }
}
