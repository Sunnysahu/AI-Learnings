namespace AIAgent.Microsoft.Api.Tools;

public static class DateTimeTool
{
    public static string CurrentDate()
    {
        return DateTime.Now.ToLongDateString();
    }

    public static string CurrentTime()
    {
        return DateTime.Now.ToLongTimeString();
    }
}