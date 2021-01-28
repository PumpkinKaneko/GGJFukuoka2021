using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleController : MonoBehaviourPunCallbacks
{
    public float MoveSpeed = 5f;
    public float RotateSpeed = 90f;

    private Vector3 _axis;


    private void Update()
    {
        if (photonView.IsMine)
        {   // キャラ別に操作させる
            _axis.x = Input.GetAxis("Horizontal");
            _axis.y = Input.GetAxis("Vertical");

            this.Move(this.transform.forward * _axis.y * this.MoveSpeed);
            this.Rot(this.transform.up * _axis.x * this.RotateSpeed);
        }
    }


    private void Move(Vector3 velocity)
    {
        this.transform.position += velocity * Time.deltaTime;
    }


    private void Rot (Vector3 rotate)
    {
        this.transform.Rotate(rotate * Time.deltaTime);
    }
}
