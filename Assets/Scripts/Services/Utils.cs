
public static class Utils 
{
    public static string FormatDigit(int digit)
    {
        if (digit < 10)
        {
            return "0" + digit.ToString();
        }
        else
        {
            return digit.ToString();
        }
    }
}
