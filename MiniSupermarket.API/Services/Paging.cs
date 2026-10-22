namespace MiniSupermarket.API.Services;

public static class Paging
{
    public static (int Page, int Size, int Pages) Normalize(int page, int size, int total)
    {
        size = size is < 1 or > 50 ? 5 : size;
        int pages = (int)Math.Ceiling(total / (double)size);
        page = Math.Clamp(page, 1, Math.Max(1, pages));
        return (page, size, pages);
    }
}
