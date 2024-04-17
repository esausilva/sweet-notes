using IdGen;

namespace Application.Providers.SnowflakeId;

internal class SnowflakeIdProvider : IUniqueIdProvider<long>
{
    public long GenerateUniqueId()
    {
        var generator = new IdGenerator(0);

        return generator.CreateId();
    }
}