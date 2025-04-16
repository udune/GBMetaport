using System.Collections.Generic;
using Constants;

public class AccountState: BaseState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(1, () => SceneLoadManager.Instance.isCompleted = 0);
    }

    public void OnExit()
    {
        SceneLoadManager.Instance.isCompleted = 1;
        AppManager.Instance.isNeedGuide = true;
        PhotonManager.Instance.PhotonConnectSetting();
    }
}

public class AirPortState : BaseState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(2, () => 
        {
            if (!AppManager.Instance.isFirstEnter)
            {
                // 로그인 유저 입장시 이벤트 팝업
                AppManager.Instance.GetEventDataFromServer();
                AppManager.Instance.isFirstEnter = true;
            }

            AppManager.Instance.companyId = -999;
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class DutyfreeState : BaseState
{
    public DutyfreeState() : base(3, setCompanyId: true, joinChannel: true) { }
}

public class GBSpaceState : IState
{
    public GBSpaceState() : base(4, setCompanyId: true, joinChannel: true) { }
}

public class GBProvincialOfficeState : IState
{
    public GBProvincialOfficeState() : base(5, setCompanyId: true, joinChannel: true) { }
}

public class ConventionLobbyState : IState
{
    public ConventionLobbyState() : base(6, setCompanyId: true, joinChannel: true) { }
}

public class ConventionHallState : BaseState
{
    public ConventionHallState() : base(7) { }

    public override void OnExit()
    {
        Dictionary<string, object> data = new()
        {
            { "userId", AppManager.Instance.myData.userId },
            { "conferenceId", AppManager.Instance.conferenceId }
        };

        GBSocketManager.Instance.SendRTCMessage("conf/leave", data);
        AppManager.Instance.conferenceId = 0;
        AppManager.Instance.ChangeConventionExitState(ConventionExitState.CONVENTION_HALL);
    }
}

public class ConventionExhibitionState : BaseState
{
    public ConventionExhibitionState() : base(8, setCompanyId: true, joinChannel: true) { }

    protected override void AfterSceneLoad()
    {
        var panel = AppManager.Instance.mainPanel;
        panel.MapButtonOn(false);
        panel.SetShareBtn(false);
    }

    public override void OnExit()
    {
        var panel = AppManager.Instance.mainPanel;
        panel.MapButtonOn(true);
        panel.SetShareBtn(true);

        Dictionary<string, object> data = new()
        {
            { "userId", AppManager.Instance.myData.userId },
            { "boothCompanyId", AppManager.Instance.companyId }
        };

        GBSocketManager.Instance.SendRTCMessage("booth/leave", data);
        AppManager.Instance.ChangeConventionExitState(ConventionExitState.CONVENTION_EXHIBITION);
    }
}

public class NomadState : IState
{
    public NomadState() : base(9, setCompanyId: false, joinChannel: false) { }

    public void OnExit()
    {
        AppManager.Instance.ChangeConventionExitState(ConventionExitState.NOMAD);
    }
}


public class Dutyfree3DState : IState
{
    public Dutyfree3DState() : base(10, setCompanyId: true, joinChannel: true) { }
}

public class DokdoState: IState
{
    public DokdoState() : base(11, setCompanyId: true, joinChannel: true) { }
}

public class ConventionStartUpSupport: IState
{
    public ConventionStartUpSupport() : base(12, setCompanyId: false, joinChannel: false) { }

    public void OnExit()
    {
        Dictionary<string, object> data = new()
        {
            { "userId", AppManager.Instance.myData.userId },
            { "seminarId", AppManager.Instance.seminarId }
        };

        GBSocketManager.Instance.SendRTCMessage("seminar/leave", data);
        AppManager.Instance.seminarId = 0;
        AppManager.Instance.ChangeConventionExitState(ConventionExitState.SEMINAR);
    }
}

public class ConventionStartUpExhibition : IState
{
    public ConventionStartUpExhibition() : base(13, setCompanyId: false, joinChannel: true) 
    {
        var _mainPanel = AppManager.Instance.mainPanel;
            _mainPanel.MapButtonOn(false);
            _mainPanel.SetShareBtn(false);
    }

    public void OnExit()
    {
        var _mainPanel = AppManager.Instance.mainPanel;
        _mainPanel.MapButtonOn(true);
        _mainPanel.SetShareBtn(true);

        Dictionary<string, object> data = new()
        {
            { "userId", AppManager.Instance.myData.userId },
            { "boothCompanyId", AppManager.Instance.companyId }
        };

        GBSocketManager.Instance.SendRTCMessage("booth/leave", data);
    }
}
