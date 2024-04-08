namespace Eva.Infra.Tools.Extensions
{
    public static class IntExtensions
    {
        public static bool IsNullOrZero(this int value)
        {
            if (value == null || value == 0) 
                return true;
            return false;
        }
    }
}
