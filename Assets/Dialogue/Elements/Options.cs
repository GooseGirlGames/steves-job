using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Type {
  Text, Item
}

public abstract class Option : DialogueElement {

}

public class TextOption : Option {
    public string Text;
    public TextOption(string text) {
        Text = text;
    }
}

public class ItemOption : Option {
    public Item Item;
    public ItemOption(Item item) {
        Item = item;
    }
}

public class Options : Sentence {
    public List<Option> options = new List<Option>();
    public Options(string text) : base(text) {}
    public Options AddOption(Option option) {
        options.Add(option);
        return this;
    }
    public void OptionChosen(Option option) {
        Append(option);
    }
}