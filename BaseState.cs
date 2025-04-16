public abstract class BaseState : IState
{
    protected int sceneIndex;
    protected bool setCompanyId = false;
    protected bool joinChannel = false;

    protected BaseState(int sceneIndex, bool setCompanyId = false, bool joinChannel = false)
    {
        this.sceneIndex = sceneIndex;
        this.setCompanyId = setCompanyId;
        this.joinChannel = joinChannel;
    }

    public virtual void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(sceneIndex, () =>
        {
            if (setCompanyId)
                AppManager.Instance.companyId = -999;

            if (joinChannel)
                AgoraChatManager.Instance.JoinChannel(GetType().Name);

            AfterSceneLoad();
        });
    }

    public virtual void OnExit()
    {
        // override if needed
    }

    protected virtual void AfterSceneLoad() { }
}
