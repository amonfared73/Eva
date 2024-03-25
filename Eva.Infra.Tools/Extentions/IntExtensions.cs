namespace Eva.Infra.Tools.Extentions
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
