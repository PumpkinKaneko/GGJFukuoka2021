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
        [SerializeField] private float moveSpeed;
        private GameObject cameraObj;
        private string _playerName;
        private int _materialNum;
        private int _charaNum = 1;
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
            photonView.RPC("SetNameRPC", RpcTarget.All, _playerName);
        }

        public void SetMaterial(int num)
        {
            _materialNum = num;
            photonView.RPC("SetMaterialRPC", RpcTarget.All, _materialNum);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            if (photonView.IsMine)
            {
                photonView.RPC("SetNameRPC", RpcTarget.All, _playerName);
                photonView.RPC("SetMaterialRPC", RpcTarget.All, _materialNum);
            }

        }

        public void SetCharacter(int number)
        {
            photonView.RPC("SetCharacterRPC", RpcTarget.All, number);
           
        }

        public void GameStart()
        {
            photonView.RPC("GameStartRPC", RpcTarget.All);
        }

        [PunRPC]
        private void SetNameRPC(string playerName)
        {
            nameText.text = playerName;
            
        }

        [PunRPC]
        private void SetMaterialRPC(int matNum)
        {
            meshRenderer.material = materials[matNum];
        }

        [PunRPC]
        private void SetCharacterRPC(int number)
        {
            _charaNum = number;
        }
        private void KeyCtrl()
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var inputVec = new Vector3(moveX, 0, moveY);
            if (inputVec.magnitude != 0)
            {
                var cameraFoward = cameraObj.transform.forward.normalized;
                cameraFoward = Vector3.Scale(cameraFoward, new Vector3(1, 0, 1));
                var cameraRight = cameraObj.transform.right.normalized;
                cameraRight = Vector3.Scale(cameraRight, new Vector3(1, 0, 1));
                var moveDirection = cameraFoward * moveY + cameraRight * moveX;
                CharacterMove(moveDirection);
            }
        }

        private void CharacterMove(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.Translate(direction * Time.deltaTime * moveSpeed);
            body.transform.localRotation = Quaternion.LookRotation(direction);
        }


        private IEnumerator LoadSceneCoroutine()
        {
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.IsMessageQueueRunning = false;
            yield return GameManage.Instance.LoadSceneAsync(
                new InGameScene(GameManage.Instance, "InGameScene"), UnityEngine.SceneManagement.LoadSceneMode.Single);
            PhotonNetwork.IsMessageQueueRunning = true;
            var stgmng = GameObject.Find("StageManager").GetComponent<HumanSampletScene>();
            Debug.Log(_charaNum);
            stgmng.Init(PhotonNetwork.NickName, _materialNum,_charaNum);
            Destroy(gameObject);
        }

        [PunRPC]
        private void GameStartRPC()
        {
            StartCoroutine(LoadSceneCoroutine());
        }
    }
}


