using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{
    public class Player : MonoBehaviour
    {
        public int id;
        //public Color color;
        public int idPrefabs;
        public GameObject prefabs;
        public int ControllerId { get; set; }
        // Use this for initialization

        public Player(int Id, int IdPrefabs, int Controller)
        {
            id = Id;
            idPrefabs = IdPrefabs;
            ControllerId = Controller;
        }

        private void Awake()
        {
            if (ControllerId == 0)
                transform.GetChild(0).GetComponent<CharacterController>().controllerId = 1;
        }


        // Update is called once per frame
        void Update()
        {

        }

        public int GetId()
        {
            return id;
        }

        /*public Color GetColor()
        {
            return color;
        }*/

        public int GetControllerId()
        {
            return ControllerId;
        }
        public void Load(int Id, int IdPrefabs, int Controller)
        {

            id = Id;
            idPrefabs = IdPrefabs;
            ControllerId = Controller;
        }
        /*public void LoadPlayer(int Id, Color PlayerColor, int controllerId)
        {
            id = Id;
            color = PlayerColor;
            ControllerId = controllerId;
        }*/
    }
}
