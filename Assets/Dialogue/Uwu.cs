using System.Collections;
public class Uwu {
    public static string OptionalUwufy(string text) {
        //if (text.StartsWith("Anyway")) {
        //    text = "Anyway, here's Wonderwall.";
        //}
        if (Inventory.Instance.HasItemByName("Babelfish")) {
            return Uwufy(text);
        }
        return text;
    }
    public static string Uwufy(string text) {
        return text
            .Replace("small", "smol")
            .Replace("cute", "kawaii")
            .Replace("fluff", "floof")
            .Replace("love", "luv")
            .Replace("stupid", "baka")
            .Replace("what", "nani")
            .Replace("meow", "nya")
            .Replace("Small", "Smol")
            .Replace("Cute", "Kawaii")
            .Replace("Fluff", "Floof")
            .Replace("Love", "Luv")
            .Replace("Stupid", "Baka")
            .Replace("What", "Nani")
            .Replace("Meow", "Nya")
            .Replace("r", "w")
            .Replace("R", "W")
            .Replace("l", "w")
            .Replace("L", "W")
            .Replace("no", "nyo")
            .Replace("na", "nya")
            .Replace("No", "Nyo")
            .Replace("Na", "Nya");
    }
        
}
