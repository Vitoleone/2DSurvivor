using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using Photon.Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit.Platform.Photon;
using UdpKit;

public class Menu : GlobalEventListener
{
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()//Server start olduðunda çalýþýr
    {
        BoltMatchmaking.CreateSession(sessionID: "LevelScene", sceneToLoad: "LevelScene");
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
    }
}
