# GBMetaport

## **State 패턴으로 씬 관리를 하도록 했습니다.**
###### *디테일한 코드는 유출 불가로 참조하지 못하는 점 양해 부탁드립니다.*

### 딕셔너리(Dictionary)를 활용한 상태 관리
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

### 각 상태에서 OnEnter()와 OnExit()을 명확하게 정의
  #### - OnEnter(): 씬 로드 및 초기 설정
  #### - OnExit(): 정리 작업 및 네트워크 관련 처리 수행

### 새로운 상태(State) 추가가 용이함
  #### - 새로운 IState 클래스 추가 후 StateControl에 등록하면 확장 가능
  #### - 특정 상태별로 OnEnter() 및 OnExit()만 구현하면 되므로 확장성이 뛰어남
