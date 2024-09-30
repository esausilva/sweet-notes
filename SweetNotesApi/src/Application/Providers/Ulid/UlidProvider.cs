namespace Application.Providers.Ulid;

public class UlidProvider : IUniqueIdProvider<System.Ulid>
{
    public System.Ulid GenerateUniqueId()
    {
        return System.Ulid.NewUlid();
    }
}