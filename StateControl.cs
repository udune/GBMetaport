using Constants;
using System;
using System.Collections.Generic;

public class StateControl
{
    private readonly Dictionary<AppState, IState> _stateDictionary = new();
    private IState _currentState;
    private AppState _currentStateEnum;

    public ConventionExitState CurrentConventionExitState { get; private set; } = ConventionExitState.None;
    public AppState CurrentAppState => _currentStateEnum;

    /// <summary>
    /// 상태를 등록합니다. 중복된 상태는 예외 발생.
    /// </summary>
    public void AddState(AppState appState, IState state)
    {
        if (_stateDictionary.ContainsKey(appState))
        {
            throw new ArgumentException($"State {appState} is already registered.");
        }

        _stateDictionary.Add(appState, state);
    }

    /// <summary>
    /// 상태를 일괄 등록합니다.
    /// </summary>
    public void AddStates(params (AppState state, IState handler)[] states)
    {
        foreach (var (state, handler) in states)
        {
            AddState(state, handler);
        }
    }

    /// <summary>
    /// 상태를 변경합니다.
    /// </summary>
    public void SetState(AppState newState)
    {
        if (!_stateDictionary.TryGetValue(newState, out var nextState))
        {
            throw new KeyNotFoundException($"State {newState} has not been registered.");
        }

        _currentState?.OnExit();
        _currentState = nextState;
        _currentStateEnum = newState;
        _currentState.OnEnter();
    }

    /// <summary>
    /// 컨벤션 종료 상태를 설정합니다.
    /// </summary>
    public void SetConventionExitState(ConventionExitState exitState)
    {
        CurrentConventionExitState = exitState;
    }
}
