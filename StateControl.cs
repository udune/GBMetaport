using Constants;
using System.Collections.Generic;

public class StateControl
{
    public IState _currentState;
    public AppState _currentStateEnum;
    public ConventionExitState _conventionExitStateEnum = ConventionExitState.None;
    Dictionary<AppState, IState> stateDic = new Dictionary<AppState, IState>();

    public void AddState(AppState _eAppState, IState _state)
    {
        stateDic.Add(_eAppState, _state);
    }

    public void SetState(AppState _state)
    {
        _currentState?.OnExit();
        _currentState = stateDic[_state];
        _currentStateEnum = _state;
        _currentState.OnEnter();
    }

    public void SetConventionExitState(ConventionExitState _state)
    {
        _conventionExitStateEnum = _state;
    }
}