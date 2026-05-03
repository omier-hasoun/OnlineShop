
namespace Infrastructure.LocalServices.FileNameGeneratorService;

internal sealed class FileNameGenerator([FromKeyedServices("Snowflake")] IPrimitiveTypeIdGenerator<long> idGen) : IUniqueFileNameGenerator
{
    public string Generate()
    {
        return idGen.Generate().ToString();
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
