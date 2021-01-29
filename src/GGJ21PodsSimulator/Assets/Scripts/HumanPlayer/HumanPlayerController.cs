using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerController : MonoBehaviour
{
    struct InputData
    {
        public float vertical_input;
        public float horizontal_input;
        public bool is_shot_button_down;
        public Vector2 mouse_input;
        public bool is_play_button_down;
        public bool is_jump_button_down;
        public bool is_peep_button_down;
        public bool is_blow_button_down;
    }

    [SerializeField]
    float m_camera_rotate_speed;
    [SerializeField]
    float m_max_speed;
    [SerializeField]
    float m_move_speed;
    [SerializeField]
    float m_jump_impulse;
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
    Transform m_normal_camera_transform;
    [SerializeField]
    Transform m_muzzle;
    [SerializeField]
    Transform m_peeping_camera_transform;

    InputData m_input;
    bool m_cursor_lock;
    Vector2 m_rot;
    bool m_is_peeping;

    void Start()
    {
        m_rot.x = transform.rotation.eulerAngles.y;
        m_rot.y = m_normal_camera_transform.localRotation.eulerAngles.x;
    }

    void Update()
    {
        //所有権が自分になければカメラと動作フラグを切る
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
        AtherUpdate();
    }

    private void FixedUpdate()
    {
        if (!m_is_active_move) { return; }

        BodyMove();
    }

    private void InputUpdate()
    {
        m_input.horizontal_input = Input.GetAxisRaw("Horizontal");
        m_input.vertical_input = Input.GetAxisRaw("Vertical");

        m_input.mouse_input = new Vector3(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"),
            0
            );

        m_input.is_shot_button_down = Input.GetButtonDown("Fire1");
        m_input.is_play_button_down = Input.GetKeyDown(KeyCode.Alpha1);
        if (Input.GetButtonDown("Jump"))
        {
            m_input.is_jump_button_down = true;
        }


        m_input.is_peep_button_down = 
            Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.RightShift) ;

        //カーソルが邪魔になるためQでオンオフできるように
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_cursor_lock = !m_cursor_lock;
            if (m_cursor_lock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void CameraMove()
    {
        m_rot.x += m_input.mouse_input.x * m_camera_rotate_speed * Time.deltaTime;
        m_rot.y -= m_input.mouse_input.y * m_camera_rotate_speed * Time.deltaTime;

        m_rot.y = Mathf.Clamp(m_rot.y, -m_camera_rotate_clamp, m_camera_rotate_clamp);
        m_rot.x = m_rot.x % 360f;//オーバーフロー防止
        transform.rotation = Quaternion.Euler(0, m_rot.x, 0);
        m_normal_camera_transform.localRotation = Quaternion.Euler(m_rot.y, 0, 0);

        if (m_input.is_peep_button_down)
        {
            m_is_peeping = !m_is_peeping;

            if (m_is_peeping)
            {
                m_camera.transform.parent = m_normal_camera_transform;
                m_camera.transform.localPosition = Vector3.zero;
                m_camera.transform.localRotation = Quaternion.identity;
            }
            else
            {
                m_camera.transform.parent = m_peeping_camera_transform;
                m_camera.transform.localPosition = Vector3.zero;
                m_camera.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void AtherUpdate()
    {
        if (m_input.is_play_button_down)
        {
            //TODO:airpodsから音楽を再生
        }

        if (m_input.is_blow_button_down)
        {
            const float RAY_LENGTH = 5f;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin,ray.direction * RAY_LENGTH);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, RAY_LENGTH))
            {
                //hit.collider
            }
        }
    }

    void BodyMove()
    {
        float dt = Time.deltaTime;
        Vector3 velocity = m_rb.velocity;
        Vector2 plane_velocity = new Vector2(velocity.x, velocity.z);
        float current_speed = plane_velocity.magnitude;

        //最大速度を超えていたら速度の加算はしない
        if (current_speed <= m_max_speed)
        {
            Vector3 camera_forward = m_normal_camera_transform.forward;
            camera_forward.y = 0;
            Vector3 vertical_force = camera_forward.normalized * m_input.vertical_input * m_move_speed;
            Vector3 horizontal_force = m_normal_camera_transform.right * m_input.horizontal_input * m_move_speed;
            Vector3 curremt_velocity = m_rb.velocity;
            m_rb.velocity = horizontal_force + vertical_force + Vector3.up * curremt_velocity.y;
        }

        //ジャンプ処理
        if (m_input.is_jump_button_down)
        {
            //TODO:無限ジャンプ防止
            m_rb.AddForce(Vector3.up * m_jump_impulse,ForceMode.Impulse);
            m_input.is_jump_button_down = false;
        }
    }
}
