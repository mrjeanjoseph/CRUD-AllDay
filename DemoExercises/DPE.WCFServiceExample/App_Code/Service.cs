// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService {
    public string GetData(int value) {

        return string.Format("You entered: {0}", value);

    }
}
