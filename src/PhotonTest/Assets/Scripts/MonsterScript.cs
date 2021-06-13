using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class MonsterScript : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private Animator animator;
    public string _name;
    // Start is called before the first frame update
    void Start()
    {
        
    }
     public void Init(string name)
    {
        _name = name;
    }
    // Update is called once per frame
    void Update()
    {
        KeyCtrl();
    }

    private void KeyCtrl()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");
        if(moveX != 0 || moveY != 0)
        {
            var dir = new Vector3(moveX, 0, moveY);
            CharacterMove(dir);
            var forward = dir.magnitude;
            animator.SetFloat("Forward", forward);
        }
        else
        {
            animator.SetFloat("Forward", 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _name = "defualt";
        }
            if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                var view = GetComponent<PhotonView>();
                view.RPC("SetNameRPC", RpcTarget.Others);
            }
        }
    }


    [PunRPC]
    public void SetNameRPC()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            var view = obj.GetComponent<PhotonView>();
            if (view.IsMine == false) return;
            obj.GetComponent<MonsterScript>()._name = "eee";
            Debug.Log(view.ViewID);
        }
    }

    private void CharacterMove(Vector3 _direction)
    {
        var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
        transform.Translate(direction * Time.deltaTime);
        body.transform.localRotation = Quaternion.LookRotation(direction);
        
    }
}

