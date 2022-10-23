using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class ServerCallbacks : GlobalEventListener
{
  //Sahne yüklendiğinde çalışacaklar bunun içerisinde.
  public override void SceneLoadLocalDone(string scene, IProtocolToken token)
  {
    if (BoltNetwork.IsServer)
    {
      BoltNetwork.Instantiate(BoltPrefabs.Challanger, new Vector2(0, 3),Quaternion.identity);
    }
    else if(BoltNetwork.IsClient)
    {
      BoltNetwork.Instantiate(BoltPrefabs.Challanger2, new Vector2(0, -3), Quaternion.identity);
    }
  }
}
