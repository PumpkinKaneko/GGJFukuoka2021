using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Wait
{
    public class TestWait : MonoBehaviourPunCallbacks
    {
        public string PlayerName { get { return _playerName; } }
        public int MaterialID { get { return _materialNum; } }
        [SerializeField] private GameObject body;
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [SerializeField] private Material[] materials;
        private GameObject cameraObj;
        private string _playerName;
        [SerializeField]private int _materialNum;
        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false) return;
            KeyCtrl();
            nameText.transform.parent.LookAt(cameraObj.transform);
        }

        public void Init(string playerName)
        {
            cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
            _playerName = playerName;
            //_materialNum = materialNum;
            photonView.RPC("RPCTest", RpcTarget.All, _playerName);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            if (photonView.IsMine)
            {
                photonView.RPC("RPCTest", RpcTarget.All, _playerName);
            }

        }

        [PunRPC]
        private void RPCTest(string playerName)
        {
            nameText.text = playerName;
            //meshRenderer.material = materials[materialNum];
        }

        private void KeyCtrl()
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            if (moveX != 0 || moveY != 0)
            {
                var dir = new Vector3(moveX, 0, moveY);
                CharacterMove(dir);
                var forward = dir.magnitude;
            }
        }

        private void CharacterMove(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.Translate(direction * Time.deltaTime);
            body.transform.localRotation = Quaternion.LookRotation(direction);

        }
    }
}


