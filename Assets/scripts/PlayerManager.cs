using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public Transform[] SpawnPositions;
        public GameObject PlayerPrefabs;
        public Color[] PlayerColorList;
        private List<Player> PlayerList = new List<Player>();
        private IDictionary<Player, Transform> PlayerTransformList;

        private static bool created = false;



        //PJ Selection
        public Sprite[] CharacterList; //5
        public GameObject[] PicutreList; // 4
        public bool[] character_selected; // 5
        public float f_delay = 0;
        public int[] pj_selected; // Taille 4, valeurs (0-->4)

        void Awake()
        {
            if (!created)
            {
                DontDestroyOnLoad(this.gameObject);
                created = true;
            }
        }

        // Use this for initialization
        void Start()
        {
            /*
            //Spawn Player
            PlayerList.Add(new Player(1, Color.red, 1));
            PlayerList.Add(new Player(2, Color.blue, 2));
            PlayerList.Add(new Player(3, Color.green, 3));
            PlayerList.Add(new Player(4, Color.magenta, 4));
            foreach (Player player in PlayerList)
            {
                GameObject playerObject = Instantiate(PlayerPrefabs, SpawnPositions[player.GetId() - 1].position, Quaternion.identity, PlayerFolder);
                playerObject.GetComponent<SpriteRenderer>().color = player.GetColor();
            }*/

            for (int x = 0; x < character_selected.Length; x++)
            {
                character_selected[x] = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (f_delay < 1.5f)
            {
                f_delay += 0.1f;
            }
            else
            {
                if (Input.GetAxis("DPad_XAxis_1") != 0)
                {
                    if (Input.GetAxis("DPad_XAxis_1") < 0)
                    {
                        character_selected[pj_selected[0]] = false;
                        pj_selected[0]--;
                        if (pj_selected[0] < 0)
                        {
                            pj_selected[0] = 4;
                        }
                        while (character_selected[pj_selected[0]])
                        {
                            pj_selected[0]--;
                            if (pj_selected[0] < 0)
                            {
                                pj_selected[0] = 4;
                            }
                        }
                        character_selected[pj_selected[0]] = true;
                        PicutreList[0].GetComponent<Image>().sprite = CharacterList[pj_selected[0]];

                    }
                    else
                    {
                        character_selected[pj_selected[0]] = false;
                        pj_selected[0]++;
                        if (pj_selected[0] > 4)
                        {
                            pj_selected[0] = 0;
                        }
                        while (character_selected[pj_selected[0]])
                        {
                            pj_selected[0]++;
                            if (pj_selected[0] > 4)
                            {
                                pj_selected[0] = 0;
                            }
                        }
                        character_selected[pj_selected[0]] = true;
                        PicutreList[0].GetComponent<Image>().sprite = CharacterList[pj_selected[0]];
                    }
                    f_delay = 0;

                }

                if (Input.GetAxis("DPad_YAxis_1") != 0)
                {
                    if (Input.GetAxis("DPad_YAxis_1") < 0)
                    {
                        character_selected[pj_selected[1]] = false;
                        pj_selected[1]--;
                        if (pj_selected[1] < 0)
                        {
                            pj_selected[1] = 4;
                        }
                        while (character_selected[pj_selected[1]])
                        {
                            pj_selected[1]--;
                            if (pj_selected[1] < 0)
                            {
                                pj_selected[1] = 4;
                            }
                        }
                        character_selected[pj_selected[1]] = true;
                        PicutreList[1].GetComponent<Image>().sprite = CharacterList[pj_selected[1]];

                    }
                    else
                    {
                        character_selected[pj_selected[1]] = false;
                        pj_selected[1]++;
                        if (pj_selected[1] > 4)
                        {
                            pj_selected[1] = 0;
                        }
                        while (character_selected[pj_selected[1]])
                        {
                            pj_selected[1]++;
                            if (pj_selected[1] > 4)
                            {
                                pj_selected[1] = 0;
                            }
                        }
                        character_selected[pj_selected[1]] = true;
                        PicutreList[1].GetComponent<Image>().sprite = CharacterList[pj_selected[1]];
                    }
                    f_delay = 0;

                }
            }
        }

        public bool IsPlayerAlreadyInLobby(int controllerId)
        {
            bool AlreadyExist = PlayerList.Exists(item => item.ControllerId == controllerId);
            ///Debug.Log(AlreadyExist);
            return AlreadyExist;
        }


        public int AddPlayer(int ControllerId)
        {
            int id = PlayerList.Count + 1;
            GameObject Player = Instantiate(PlayerPrefabs, transform);
            Player player = Player.GetComponent<Player>();
            player.LoadPlayer(id, PlayerColorList[PlayerList.Count], ControllerId);
            //Player NewPlayer = new Player(id, PlayerColorList[PlayerList.Count], ControllerId);
            PlayerList.Add(player);
            Debug.Log("Controller id:" + ControllerId + " added as Player n°" + id);

            for (int x = 0; x < character_selected.Length; x++)
            {
                if (!character_selected[x])
                {
                    character_selected[x] = true;
                    pj_selected[id - 1] = x;
                    PicutreList[id - 1].GetComponent<Image>().sprite = CharacterList[pj_selected[id - 1]];
                    break;
                }
            }

            return id;
        }

        public int GetPlayerByControllerId(int controllerId)
        {
            //while(Player)
            if (PlayerList.Exists(item => item.ControllerId == controllerId))
            {
                Player player = PlayerList.Find(item => item.ControllerId == controllerId);
                return player.id;
            }
            else
                return -1;
        }

        public void LoadScene()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform playerTransform = transform.GetChild(i);
                playerTransform.gameObject.SetActive(true);
                Transform CurrentSpawn = GameObject.Find("SpawnTransformFolder").transform.GetChild(i);
                Transform Character = playerTransform.GetChild(0);
                Character.GetComponent<SpriteRenderer>().color = playerTransform.GetComponent<Manager.Player>().color;
                Character.position = CurrentSpawn.position;
                Transform anchor = playerTransform.GetChild(1);
                anchor.position = new Vector3(CurrentSpawn.position.x, CurrentSpawn.position.y + anchor.position.y, CurrentSpawn.position.z);
            }
        }
    }
}

