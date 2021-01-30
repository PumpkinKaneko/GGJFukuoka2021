using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen.Wait
{
    public class RoomData : MonoBehaviour
    {
        public List<int> NowMaterialsList { get { return materialNumList; } }
        public int PlayerInRoom { get { return _playerInRoom; } }
        private List<int> materialNumList;
        private int _playerInRoom = 0;

        public void Init(int[] nowMaterials)
        {
            materialNumList = new List<int>();
            materialNumList.AddRange(nowMaterials);
            _playerInRoom = 1;
        }

        public void PlayerJoined(int matNum)
        {
            if(matNum > 3)
            {
                Debug.LogError("4以上のMaterialNumber");
                return;
            }
            materialNumList.Add(matNum);
        }
    }
}

