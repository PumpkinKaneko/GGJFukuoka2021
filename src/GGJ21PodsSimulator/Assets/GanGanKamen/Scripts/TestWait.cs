using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen.Test
{
    public class TestWait : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        // Start is called before the first frame update
        void Start()
        {

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


