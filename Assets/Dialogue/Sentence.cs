using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Sentence
{
    [SerializeField]
    public string text;
    private List<DialogueAction> actions = new List<DialogueAction>(); 
    private List<DialogueCondition> conditions = new List<DialogueCondition>();
    public List<DialogueOption> options = new List<DialogueOption>();
    public Sentence(string text) {
        this.text = text;
    }

    public bool ConditionsFulfilled() {
        return DialogueCondition.ConditionsFullFilled(conditions);
    }

    public void Act() {
        foreach (DialogueAction a in actions) {
            a.Run();
        }
    }

    public Sentence If(DialogueCondition condition) {
        conditions.Add(condition);
        return this;
    }
    public Sentence If(FunctionCondition.ConditionFunc func) {
        DialogueCondition condition = new FunctionCondition(func);
        return If(condition);
    }
    public Sentence Do(DialogueAction.RunFunc func) {
        DialogueAction action = new DialogueAction(func);
        return Do(action);
    }
    public Sentence Do(DialogueAction action) {
        actions.Add(action);
        return this;
    }

    public Sentence Choice(DialogueOption option) {
        options.Add(option);
        return this;
    }
}

public class EmptySentence : Sentence {
    public EmptySentence() : base("") {}
}