using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using Photon.Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit.Platform.Photon;
using UdpKit;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = System.Random;

public class Menu : GlobalEventListener
{
    public Button joinGameButtonInstance;
    public GameObject serverListPanel;
    private List<Button> joinServerButtons = new List<Button>();
    public float buttonSpacing = 250f;
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()//Server start oldu�unda �al���r
    {
        int randomInt = UnityEngine.Random.Range(0, 9999);
        BoltMatchmaking.CreateSession(sessionID: "LevelScene" + randomInt.ToString(), sceneToLoad: "LevelScene");
        
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }
    
    //Bir oda oluşturulduğunda veya silindiğinde çalışır.
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        int randomInt = 0;
        ClearSessions();
        foreach (var session in sessionList)
        {
            randomInt = UnityEngine.Random.Range(0, 9999);
            UdpSession photonSession = session.Value as UdpSession;
            Button joingameButtonClone = Instantiate(joinGameButtonInstance);
            joingameButtonClone.transform.parent = serverListPanel.transform;
            joingameButtonClone.transform.localPosition = new Vector3(0,buttonSpacing * joinServerButtons.Count,0);
            joingameButtonClone.gameObject.SetActive(true);
            
            joingameButtonClone.onClick.AddListener(() => JoinGame(photonSession));
            joingameButtonClone.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "LevelScene" + randomInt.ToString();
            joinServerButtons.Add(joingameButtonClone);
            
            

        }

      
    }
    private void JoinGame(UdpSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);
    }

    private void ClearSessions()
    {
        foreach (Button button in joinServerButtons )
        {
            joinServerButtons.Remove(button);
            Destroy((button.gameObject));
        }
        joinServerButtons.Clear();
    }
}
