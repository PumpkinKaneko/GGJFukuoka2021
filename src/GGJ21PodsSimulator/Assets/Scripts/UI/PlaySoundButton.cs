using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundButton : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text_ui;
    public int button_index;
    public SoundFurniture sound_furniture;

    bool m_is_playing;

    void Start()
    {
        
    }

    public void OnClicked()
    {
        if (m_is_playing)
        {
            sound_furniture.StopSound();
        }
        else
        {
            sound_furniture.PlaySound();
        }
    }
}
