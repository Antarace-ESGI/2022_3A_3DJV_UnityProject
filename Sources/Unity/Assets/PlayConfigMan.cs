using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayConfigMan : MonoBehaviour
{
  private List<PlayerConfiguration> playerConfigs;

  [SerializeField]
  private int maxPlayers = 4;

  public static PlayConfigMan Instance { get; private set; }

  private PlayerInputManager playerInputManager;

  private void Awake()
  {
    playerInputManager = FindObjectOfType<PlayerInputManager>();

    if(Instance != null)
    {
      Debug.Log("Singleton : trying to create another ");
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(Instance);
      playerConfigs = new List<PlayerConfiguration>();
    }
  }



  private void OnEnable()
  {
    playerInputManager.onPlayerJoined += HandlePlayerJoin;
  }

  private void OnDisable()
  {
    playerInputManager.onPlayerJoined -= HandlePlayerJoin;
  }







  // Il fait une méthod pour affecter une couleur sur un joueur
  public void ReadyPlayer(int index)
  {
    playerConfigs[index].IsReady = true;

    if(playerConfigs.All(p => p.IsReady == true))
    {
      SceneManager.LoadScene("");
    }

  }

  // Trucs à faire quand un joueur spawn
  // je comprends pas trop pourquoi mais il rattache les transform des player input de chaque instance au player input manager
  // OWS Fait un peu différemment pour la posiition de Spawn, on verra plus tard
  public void HandlePlayerJoin(PlayerInput pi)
  {
    if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
    {
      pi.transform.SetParent(transform);
      playerConfigs.Add(new PlayerConfiguration(pi));
    }
  }



}

public class PlayerConfiguration
{

  public PlayerConfiguration(PlayerInput pi)
  {
    PlayerIndex = pi.playerIndex;
    Input = pi;
  }

  public PlayerInput Input { get; set; }
  public int PlayerIndex { get; set; }
  public bool IsReady { get; set; }

  // Ici il rajoute un material pck ses joueurs n'ont pas de propriétés, juste des couleurs
  // Je ne sais pas encore comment je vais faire mais je dois comprendre le principe
}
