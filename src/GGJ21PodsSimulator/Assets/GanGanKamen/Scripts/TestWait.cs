using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Test
{
    public class TestWait : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject body;
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        private Camera camera;
        private PhotonView photonView;
        private string _playerName;
        // Update is called once per frame
        void Update()
        {
            KeyCtrl();
            nameText.transform.parent.LookAt(camera.transform);
        }

        public void Init(string playerName,Material material)
        {
            photonView = GetComponent<PhotonView>();
                camera = GameObject.FindGameObjectWithTag("MainCamera")
         .GetComponent<Camera>();
                meshRenderer.material = material;
                _playerName = playerName;
                photonView.RPC("RPCTest", RpcTarget.All, _playerName);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log(_playerName);
            if (photonView.IsMine)
            {
                photonView.RPC("RPCTest", RpcTarget.All, _playerName);
            }

        }

        [PunRPC] private void RPCTest(string playerName)
        {
            Debug.Log(playerName);
            nameText.text = playerName;
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


