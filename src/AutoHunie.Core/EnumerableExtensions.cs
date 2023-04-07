namespace AutoHunie.ConsoleApp;

public static class EnumerableExtensions
{
    public static bool IsIndexInBounds<T>(this IEnumerable<T> collection, int index)
        => index >= 0 && index < collection.Count();
}
