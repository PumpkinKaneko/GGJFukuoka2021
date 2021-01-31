using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFurniture : MonoBehaviour
{
    BGMSoundManager _bgm;

    private void Start()
    {
        _bgm = this.GetComponent<BGMSoundManager>();
    }


    public void PlaySound()
    {
        _bgm.PlayBGMSoundFunction();
    }

    public void StopSound()
    {
        _bgm.StopBGMSoundFunction();
    }
}
