using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerController : MonoBehaviour
{
    const byte FIRE_BUTTON_COUNT = 2;

    [SerializeField]
    float m_camera_rotate_speed;
    [SerializeField]
    float m_max_speed;
    [SerializeField]
    float m_move_speed;
    [SerializeField]
    float m_camera_rotate_clamp;
    [SerializeField,Tooltip("trueなら入力を受け付ける")]
    bool m_is_active_move;

    [SerializeField]
    Rigidbody m_rb;
    [SerializeField]
    PhotonView m_photon_view;
    [SerializeField]
    Camera m_camera;
    [SerializeField]
    Transform m_camera_transform;
    [SerializeField]
    Transform m_muzzle;

    float m_vertical_input;
    float m_horizontal_input;
    bool m_is_shot_button_down;
    Vector2 m_mouse_input;

    Vector2 m_rot;

    void Start()
    {
        m_rot.x = transform.rotation.eulerAngles.y;
        m_rot.y = m_camera_transform.localRotation.eulerAngles.x;
    }

    void Update()
    {
        if (m_photon_view.IsMine)
        {
            m_is_active_move = true;
            m_camera.enabled = true;
        }
        else
        {
            m_is_active_move = false;
            m_camera.enabled = false;
        }

        if (!m_is_active_move) { return; }

        InputUpdate();
        CameraMove();
        ShotCheck();
    }

    private void FixedUpdate()
    {
        if (!m_is_active_move) { return; }

        BodyMove();
    }

    private void InputUpdate()
    {
        m_horizontal_input = Input.GetAxisRaw("Horizontal");
        m_vertical_input = Input.GetAxisRaw("Vertical");

        m_mouse_input = new Vector3(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"),
            0
            );

        m_is_shot_button_down = Input.GetButtonDown("Fire1");
    }

    private void CameraMove()
    {
        m_rot.x += m_mouse_input.x * m_camera_rotate_speed * Time.deltaTime;
        m_rot.y -= m_mouse_input.y * m_camera_rotate_speed * Time.deltaTime;

        m_rot.y = Mathf.Clamp(m_rot.y, -m_camera_rotate_clamp, m_camera_rotate_clamp);
        m_rot.x = m_rot.x % 360f;//オーバーフロー防止
        transform.rotation = Quaternion.Euler(0, m_rot.x, 0);
        m_camera_transform.localRotation = Quaternion.Euler(m_rot.y, 0, 0);
    }

    private void ShotCheck()
    {
        if (!m_is_shot_button_down) { return; }

        Quaternion rot = Random.rotation;
        var bullet = PhotonNetwork.Instantiate("Prefabs/cube_object", m_muzzle.position, rot);
        Vector3 shot_vel = m_camera_transform.forward * Random.Range(1f,10f);
        bullet.GetComponent<Rigidbody>().AddForce(shot_vel, ForceMode.Impulse);
        bullet.GetComponent<Renderer>().material.color = Random.ColorHSV(0,1);
    }

    void BodyMove()
    {
        float dt = Time.deltaTime;
        Vector3 velocity = m_rb.velocity;
        Vector2 plane_velocity = new Vector2(velocity.x, velocity.z);
        float current_speed = plane_velocity.magnitude;

        if (current_speed <= m_max_speed)
        {
            Vector3 camera_forward = m_camera_transform.forward;
            camera_forward.y = 0;
            Vector3 vertical_force = camera_forward.normalized * m_vertical_input * m_move_speed;
            Vector3 horizontal_force = m_camera_transform.right * m_horizontal_input * m_move_speed;
            m_rb.velocity = horizontal_force+ vertical_force;
        }
    }
}
