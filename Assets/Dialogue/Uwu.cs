using System.Collections;
public class Uwu {
    public static string OptionalUwufy(string text) {
        if (Inventory.Instance.HasItemByName("Babelfish")) {
            return Uwufy(text);
        }
        return text;
    }
    public static string Uwufy(string text) {
        return text
            .Replace("r", "w")
            .Replace("R", "W")
            .Replace("l", "w")
            .Replace("L", "W")
            .Replace("no", "nyo")
            .Replace("na", "nya")
            .Replace("No", "Nyo")
            .Replace("Na", "Nya")
        ;
    }
        
}