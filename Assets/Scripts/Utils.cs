public static class Utils
{
    public static System.Random Random { get; private set; }

    static Utils()
    {
        Random = new System.Random();
    }
}
