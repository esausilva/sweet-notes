namespace Api.Exceptions;

public class PendingDbMigrationsException(string message) : Exception(message);