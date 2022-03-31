using System;
using System.Collections.Generic;

public class FSM
{
    private FSMNode _currentState;
    //map all state to transitions, use states as key and transitions as value;
    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    private List<Transition> _currentTransitions = new List<Transition>();

    //dose the job of Update()
    public void updateFSM()
    {
        var getTrans = getTransition();
        if (getTrans != null) 
        { 
            setState(getTrans.nextState);
        }
        if (_currentState != null)
        {
          _currentState.Do();
        }
    }
    public void setState(FSMNode state)
    {
        if (state == _currentState)
            return;
        if(_currentState != null)
        {
          _currentState.Exit();
        }
        _currentState = state;
        _transitions.TryGetValue(_currentState.GetType(),out _currentTransitions);
        _currentState.Entry();
    }
    //use delegate so I can use a method that does checks a bunch of conditions and return a bool
    public void addTransitions(FSMNode previous, FSMNode next, Func<bool> condition)
    {
        if (_transitions.TryGetValue(previous.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[previous.GetType()] = transitions;
        }
        transitions.Add(new Transition(next, condition));
    }
    private class Transition
    {
        public Func<bool> conditions{get;}
        public FSMNode nextState{get;}
        public Transition(FSMNode to, Func<bool> condition)
        {
            nextState = to;
            conditions = condition;
        }
    }
    private Transition getTransition()
    {
        foreach (var transition in _currentTransitions)
            if (transition.conditions())
                return transition;
        return null;
    }
}
