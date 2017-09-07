namespace BudgetThis2017.Helper
{
  public class Utility
    {
    public static string IntSuffix(int i)
    {
      string Return = "th";

      switch (i)
      {
        case 1:
        case 21:
        case 31:
          Return = "st";
          break;
        case 2:
        case 22:
          Return = "nd";
          break;
        case 3:
        case 23:
          Return = "rd";
          break;
      }

      return Return;
    }
  }
}
