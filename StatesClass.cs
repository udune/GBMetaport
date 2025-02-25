using System.Collections.Generic;
using Constants;

public class AccountState: IState
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

public class AirPortState : IState
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

public class DutyfreeState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(3, ()=>
        {
            AppManager.Instance.companyId = -999;
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class GBSpaceState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(4,()=> 
        {
            AppManager.Instance.companyId = -999;
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class GBProvincialOfficeState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(5, ()=>
        {
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class ConventionLobbyState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(6, ()=> 
        {
            AppManager.Instance.companyId = -999;
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class ConventionHallState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(7);
    }

    public void OnExit()
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

public class ConventionExhibitionState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(8, () =>
        {
            AgoraChatManager.Instance.JoinChannel(this.ToString());
            var _mainPanel = AppManager.Instance.mainPanel;
            _mainPanel.MapButtonOn(false);
            _mainPanel.SetShareBtn(false);
        });
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
        
        AppManager.Instance.ChangeConventionExitState(ConventionExitState.CONVENTION_EXHIBITION);
    }
}

public class NomadState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(9);
    }

    public void OnExit()
    {
        AppManager.Instance.ChangeConventionExitState(ConventionExitState.NOMAD);
    }
}


public class Dutyfree3DState : IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(10, ()=>
        {
            AppManager.Instance.companyId = -999;
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class DokdoState: IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(11, () => 
        {
            AppManager.Instance.companyId = -999;
            AgoraChatManager.Instance.JoinChannel(this.ToString());
        });
    }

    public void OnExit()
    {
    }
}

public class ConventionStartUpSupport: IState
{
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(12);
    }

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
    public void OnEnter()
    {
        SceneLoadManager.Instance.LoadScene(13, () =>
        {
            AgoraChatManager.Instance.JoinChannel(this.ToString());
            var _mainPanel = AppManager.Instance.mainPanel;
            _mainPanel.MapButtonOn(false);
            _mainPanel.SetShareBtn(false);
        });
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