using UnityEngine;
using UnityEngine.Audio;



public class BGMSoundManager : MonoBehaviour
{

    public AudioClip mainSoundAudioClip;

    public AudioClip newClip;

    public AudioMixerGroup mainSoundAudioMixerGroup;



    //PlayBGMSound

    public void PlayBGMSoundFunction() 
    {
 
        isBGMSound
            isBGMSound = 
            
            FindObjectOfType<isBGMSound>();

        if(isBGMSound != null) 
        {
            AudioSource localAudioSources = isBGMSound.GetComponent<AudioSource>();


            if (!localAudioSources.isPlaying) 
            {  
                localAudioSources.Play();
               
            }


         

        }
        else 
        {

            InstantiateNewObjectFunction(mainSoundAudioClip);

        }

    }

    //Change BGM Sound
    public void ChangeBGMSoundFunction() 
    {
        isBGMSound
           isBGMSound =

           FindObjectOfType<isBGMSound>();

        if(isBGMSound != null) 
        {

            AudioSource localAudioSources = isBGMSound.GetComponent<AudioSource>();
            
            localAudioSources.Stop();

            localAudioSources.clip = newClip;

            localAudioSources.Play();


        }
        else 
        {
            InstantiateNewObjectFunction(newClip); ;





        }



    }


    // Stop The BGM Sound
    public void StopBGMSoundFunction() 
    {
        isBGMSound
          isBGMSound =

          FindObjectOfType<isBGMSound>();


        if (isBGMSound) 
        {
            AudioSource localAudioSources = isBGMSound.GetComponent<AudioSource>();

            if (localAudioSources.isPlaying) 
            {
                localAudioSources.Stop();
            }

        }



    }

    //Pausing The BGM Sound 
    public void PausingBGMSoundFunction() 
    {

        isBGMSound
         isBGMSound =

         FindObjectOfType<isBGMSound>();


        if (isBGMSound)
        {
            AudioSource localAudioSources = isBGMSound.GetComponent<AudioSource>();

            if (localAudioSources.isPlaying)
            {
                localAudioSources.Pause();
            }

        }



    }




    //Destroy  The Objects Of the BGM Sound 
    public void DestroyBGMSoundFunction() 
    {
        isBGMSound
          isBGMSound =

          FindObjectOfType<isBGMSound>();

        if (isBGMSound) 
        {
            Destroy(isBGMSound.gameObject);

        }
        else 
        {


            Debug.LogWarning("This Object isn't have isBGMSound Objects");

        }





    }

//Instantiate The Objects Of The Audio Source Objects 
    void InstantiateNewObjectFunction(AudioClip clips) 
    {
        GameObject newGameObject = new GameObject();

        newGameObject.name = "BGM Sound ";

        newGameObject.AddComponent<isBGMSound>();
        AudioSource localAudioSource = newGameObject.AddComponent<AudioSource>();

        localAudioSource.loop = true;

        if (!localAudioSource.isPlaying) 
        {

            localAudioSource.Stop();

        }

        localAudioSource.outputAudioMixerGroup = 
            mainSoundAudioMixerGroup != 
            null ? mainSoundAudioMixerGroup: null;




        localAudioSource.clip = clips;

        localAudioSource.Play();






    }


}
