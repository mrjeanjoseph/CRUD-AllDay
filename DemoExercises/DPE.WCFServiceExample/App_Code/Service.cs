// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
using System.Collections.Generic;

public class Service : IService
{
    public string GetData(int value)
    {
        var result = "";

        if (value < 10) result = string.Format("You entered: {0} - Value is between 0 and 10", value);
        else if (value < 20) result = string.Format("You entered: {0} - Value is between 10 and 20", value);
        else result = "Value entered is greater than 20";

        return result;
    }

    public List<int> GetAllData(int value)
    {
        List<int> result = new List<int>(); 
        
        for (int i = value - 5; i <= value + 5; i++)        
            result.Add(i);
        
        return result;
    }
}
