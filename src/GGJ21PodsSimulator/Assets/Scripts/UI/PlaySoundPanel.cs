using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPanel : MonoBehaviour
{
    [SerializeField]
    SoundFurniture[] m_furnitures;
    [SerializeField]
    GameObject m_button;
    [SerializeField]
    Transform m_button_rayout_group;

    void Start()
    {
        for (int i = 0; i < m_furnitures.Length; ++i)
        {
            GameObject obj;
            if(i == 0)
            {
                obj = m_button;
            }
            else
            {
                obj = Instantiate(m_button, m_button_rayout_group);
            }
            PlaySoundButton sound_button = obj.GetComponent<PlaySoundButton>();
            sound_button.button_index = i;
            sound_button.text_ui.text =
                m_furnitures[i].gameObject.name;
            sound_button.sound_furniture = m_furnitures[i];
        }
    }

    void Update()
    {
        
    }
}
