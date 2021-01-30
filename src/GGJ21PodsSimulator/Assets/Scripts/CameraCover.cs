using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Shooting
{
    public class CameraCover : MonoBehaviour
    {
        [SerializeField] private Transform subject;
        [SerializeField] private List<string> coverLayerNameList;
        public List<Renderer> rendererHitsList  = new List<Renderer>();
        public Renderer[] rendererHitsPrevs;
        private int layerMask;
        // Start is called before the first frame update
        void Start()
        {
            layerMask = 0;
            foreach (string _layerName in coverLayerNameList)
            {
                layerMask |= 1 << LayerMask.NameToLayer(_layerName);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 _difference = (subject.transform.position - this.transform.position);
            Vector3 _direction = _difference.normalized;
            Ray _ray = new Ray(this.transform.position, _direction);
            RaycastHit[] _hits = Physics.RaycastAll(_ray, _difference.magnitude, layerMask);
            rendererHitsPrevs = rendererHitsList.ToArray();
            rendererHitsList.Clear();

            foreach (RaycastHit _hit in _hits)
            {
                if (_hit.collider.gameObject == subject)
                {
                    continue;
                }

                Renderer _renderer = _hit.collider.gameObject.GetComponent<Renderer>();
                if (_renderer != null)
                {
                    rendererHitsList.Add(_renderer);
                    _renderer.enabled = false;
                }
            }

            foreach (Renderer _renderer in rendererHitsPrevs.Except<Renderer>(rendererHitsList))
            {
                if (_renderer != null)
                {
                    _renderer.enabled = true;
                }
            }
        }
    }
}

