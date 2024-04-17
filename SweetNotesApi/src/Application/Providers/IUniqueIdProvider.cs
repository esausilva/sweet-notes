namespace Application.Providers;

public interface IUniqueIdProvider<out T> where T : struct
{
    T GenerateUniqueId();
}