using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSelectMenu : MonoBehaviour
{

  public GameObject playerSelMenu;
  private void Awake()
  {
    var rootMenu = GameObject.Find("Canvas");
    if(rootMenu != null){
      var menu = Instantiate(playerSelMenu, rootMenu.transform);
    }
  }
}
