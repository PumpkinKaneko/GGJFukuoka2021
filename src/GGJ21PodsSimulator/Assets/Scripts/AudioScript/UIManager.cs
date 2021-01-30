using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public void PlayBGMSoundManager() 
    {


        OverallSoundManager.instance.PlayBGMSoundFunction();


    }


    public void ChangeBGMSoundManager() 
    {


        OverallSoundManager.instance.ChangeBGMSoundFunction();
    }

    //Function 

    public void StopBGMSoundManager()
    {
        OverallSoundManager.instance.StoppingBGMSoundFunction();
    }


    public void PausingBGMSoundManager() 
    {

        OverallSoundManager.instance.PausingBGMSoundFunction();

    }

    public void DestroyBGMSoundManager() 
    {
        OverallSoundManager.instance.DestroyBGMSoundFunction();
    }

    //Function : PlaySfxSoundManager
    //Method : This is the Function that used 
    //For Playing the Sfx Sound
    public void PlaySfxSoundManager() 
    {

      
        OverallSoundManager.instance.
RandomlizeAudioClipWithoutTransformSFXFunction();


    }

    public void DestroySfxSoundManager() 
    {

        OverallSoundManager.instance.DestroySFXSoundEffectFunction();



        Debug.Log("Destroy The SFX Of The Sound ");
    }

}
