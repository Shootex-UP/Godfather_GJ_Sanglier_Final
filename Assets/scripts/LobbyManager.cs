using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Manager
{
    public class LobbyManager : MonoBehaviour {
        //private List<Player> PlayerList = new List<Player>();
        private PlayerManager playerManager;
        private Dictionary<int,bool> ReadyPlayerList;
        public Transform[] PlayerUI;

        public float ReadyTimeHold;
        public float timer;
        public bool StartGameBool = false;
        public string SceneToLoad;
        private bool GameisReady = false;

        private static bool created = false;

        //player Selection
        public GameObject[] PlayerPrefabs;
        public bool[] PrefabsAvailable;
        //private IDictionary<GameObject, bool> PrefabsAvailable;

        void Awake()
        {
            if (!created)
            {
                DontDestroyOnLoad(this.gameObject);
                created = true;
            }
            SceneManager.sceneLoaded += onSceneLoaded;
        }
        // Use this for initialization
        void Start () {
            playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
            ReadyPlayerList = new Dictionary<int, bool>();
            PrefabsAvailable = new bool[PlayerPrefabs.Length];
            /*PrefabsAvailable = new Dictionary<GameObject, bool>();
            foreach (var item in PlayerPrefabs)
            {
                PrefabsAvailable.Add(item, false);

            }*/
            //Debug.Log(Input.GetJoystickNames().);

        }
	
	    // Update is called once per frame
	    void Update () {
            if (!StartGameBool)
            {
                if(Input.anyKey)
                {
                    System.Array values = System.Enum.GetValues(typeof(KeyCode));
                    foreach (KeyCode code in values)
                    {
                        string lastKeyDown = code.ToString();
                        if (lastKeyDown.Contains("Joystick"))
                        {
                            char c_ControllerId = lastKeyDown.Substring(8, 1)[0];
                            string button = lastKeyDown.Substring(9);
                            if (char.IsDigit(c_ControllerId))
                            {
                                int i_ControllerId = (int)char.GetNumericValue(c_ControllerId);
                                if (Input.GetKey(code)){
                                    print(System.Enum.GetName(typeof(KeyCode), code));
                                    //Debug.Log("Joystick n°:" + ControllerId);
                                    //bool PlayerAlreadyJoin = PlayerList.Exists(item => item.ControllerId == ControllerId);
                                    if (!playerManager.IsPlayerAlreadyInLobby(i_ControllerId))
                                    {
                                        int selectIDprefabs = 0;
                                        while (PrefabsAvailable[selectIDprefabs] == true)
                                        {
                                            selectIDprefabs++;
                                        }
                                        
                                        PrefabsAvailable[selectIDprefabs] = true;

                                        int playerId = playerManager.AddPlayer(i_ControllerId, selectIDprefabs);
                                        Player player = playerManager.GetPlayer(playerId);
                                        ReadyPlayerList.Add(playerId, false);
                                        //Update UI
                                        GameObject prefabs = PlayerPrefabs[player.idPrefabs];
                                        Sprite sprite = prefabs.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                                        PlayerUI[playerId - 1].Find("Sprite").GetComponent<Image>().sprite = sprite;
                                        PlayerUI[playerId - 1].gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        //Debug.Log("Already joined");
                                        if (Input.GetKeyDown(code))
                                        {
                                            if (button == "Button4" || button == "Button5")
                                            {
                                                Player player = playerManager.GetPlayer(playerManager.GetPlayerByControllerId(i_ControllerId));
                                                int CurrentPrefabs = player.idPrefabs;
                                                int increment;
                                                if (button == "Button5")
                                                    increment = 1;
                                                else
                                                    increment = -1;

                                                PrefabsAvailable[CurrentPrefabs] = false;
                                                do
                                                {
                                                    CurrentPrefabs += increment;
                                                    if (CurrentPrefabs < 0)
                                                        CurrentPrefabs = PrefabsAvailable.Length - 1;
                                                    else
                                                    {
                                                        if(CurrentPrefabs >= PrefabsAvailable.Length)
                                                            CurrentPrefabs = 0;
                                                    }
                                                } while (PrefabsAvailable[CurrentPrefabs] == true);
                                                PrefabsAvailable[CurrentPrefabs] = true;
                                                player.idPrefabs = CurrentPrefabs;
                                                PlayerUI[player.id-1].Find("Sprite").GetComponent<Image>().sprite= PlayerPrefabs[player.idPrefabs].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                                            }
                                        }
                                        

                                        if(button == "Button1")
                                        {
                                            int PlayerId = playerManager.GetPlayerByControllerId(i_ControllerId);
                                            ChangeReadyState(PlayerId, true);
                                        }
                                    }
                                }
                                //
                                else
                                {
                                    if (button == "Button1" && playerManager.IsPlayerAlreadyInLobby(i_ControllerId))
                                    {
                                        int PlayerId = playerManager.GetPlayerByControllerId(i_ControllerId);
                                        ChangeReadyState(PlayerId, false);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log("Aucun Joueur n'est pret "+ReadyPlayerList.Count);
                    for (int i = 1; i < ReadyPlayerList.Count+1; i++)
                    {
                        ChangeReadyState(i, false);
                    }
                }
                CheckReadyPlayer();
            }
        }

        private void ChangePlayer()
        {

        }

        private void CheckReadyPlayer()
        {
            bool flag = true;
            foreach (KeyValuePair<int,bool> PlayerReadyState in ReadyPlayerList)
            {
                if (!PlayerReadyState.Value)
                    flag = false;
            }
            if (flag && ReadyPlayerList.    Count > 0)
                timer += Time.deltaTime;
            else
                timer = 0;

            if (timer >= ReadyTimeHold)
            {
                StartGame();
            }
        }

        private void ChangeReadyState(int PlayerId, bool state)
        {
            //Debug.Log("Player n°:" + PlayerId + (state ? "ready" : "not ready"));
            if (PlayerId > 0)
            {
                //Debug.Log("Player n°:" + PlayerId + (state ? "ready" : "not ready"));
                PlayerUI[PlayerId - 1].Find("StateText").gameObject.SetActive(state);
                ReadyPlayerList[PlayerId] = state;
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene(SceneToLoad);
            StartGameBool = true;
        }

        private void onSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (StartGameBool)
            {
                playerManager.LoadScene();
            }
        }

        public GameObject GetPrefabsById(int id)
        {
            return PlayerPrefabs[id];
        }
    }
}

