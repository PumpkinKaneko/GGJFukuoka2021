using UnityEngine;


//OverallSoundManager 
//Method : This is the Classes which is used 
//For Overall The Sound Manager 
public class OverallSoundManager : MonoBehaviour
{
    public static OverallSoundManager instance 
    {
        private set;
        get;
    }


    BGMSoundManager bgmSoundManager;


    SFXSoundManager sfxSoundManager;


    public AudioClip selectedAudioClip;
   
    public Transform selectedTransform;




    public AudioClip[] randAudioClipArray;

    private void Awake()
    {


        GetComponentFunction();

    }


    //Function : GetComponentFunction
    //Method : 

    void GetComponentFunction() 
    {
        if (instance == null)
                instance = this;

        if (bgmSoundManager == null)
                bgmSoundManager = GetComponent<BGMSoundManager>();


        if (sfxSoundManager == null)
            sfxSoundManager = GetComponent<SFXSoundManager>();

    }

    #region BGMSoundRegion

    public void PlayBGMSoundFunction() 
    {

        bgmSoundManager.PlayBGMSoundFunction();
    }

    public void ChangeBGMSoundFunction() 
    {


        bgmSoundManager.ChangeBGMSoundFunction();
    }


    public void PausingBGMSoundFunction() 
    {

        bgmSoundManager.PausingBGMSoundFunction();


    }

    public void StoppingBGMSoundFunction() 
    {
        bgmSoundManager.StopBGMSoundFunction();
    }


    //Function
    public void DestroyBGMSoundFunction() 
    {
        bgmSoundManager.DestroyBGMSoundFunction();



    }



    #endregion


    #region SFXSoundRegion




    //Selected Audio Clip and Have Position
    public void 
        selectedAudioClipAndPositionFunction
        (bool doRandomPosition = false,
                bool doRandomVolumeSound = false) 
    {

        sfxSoundManager.SFXSoundEffectOnFunction
            (selectedAudioClip,selectedTransform,
            doRandomPosition,doRandomVolumeSound);

    }
    
    //Selected Audio Clip
    public void SelectAudioClipWithoutTransformSFXFunction
        (bool doRandomVolumeSound = false) 
    {

        sfxSoundManager.SFXSoundEffectOnFunction(selectedAudioClip,doRandomVolumeSound);



    }



    //Randomlize Audio Clip And Have Position
    public void
        RandomlizeAudioClipAndPositionFunction
        (bool doRandomPosition = false,
                bool doRandomVolumeSound = false)
    {
        int rand = Random.Range(0, randAudioClipArray.Length);

        if (rand > -1) 
        {
  sfxSoundManager.SFXSoundEffectOnFunction
          (randAudioClipArray[rand], selectedTransform,
          doRandomPosition, doRandomVolumeSound);

        }

      

    }

    //Randomlize Audio Clip And Havent Position

    public void RandomlizeAudioClipWithoutTransformSFXFunction
        (bool doRandomVolumeSound = false)
    {
        int rand = Random.Range(0, randAudioClipArray.Length);

        if (rand > -1)
            sfxSoundManager.SFXSoundEffectOnFunction(selectedAudioClip, doRandomVolumeSound);

    }


    //Destroy All Of The SFX Sound Effect
    public void DestroySFXSoundEffectFunction() 
    {
        sfxSoundManager.DestroyAllofTheSFXSoundFunction();
    }

    #endregion


}
