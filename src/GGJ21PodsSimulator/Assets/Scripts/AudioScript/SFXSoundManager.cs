using UnityEngine;
using UnityEngine.Audio;



public class SFXSoundManager : MonoBehaviour
{
   static int audioSourceNameInteger = 0;
 
    public AudioMixerGroup sfxSoundAudioMixer;



    private void Start()
    {
        audioSourceNameInteger = 0;
    }

    //HavePosition 
    public void SFXSoundEffectOnFunction
        (AudioClip localAudioClip,
                Transform localTransform ,
                bool doRandomPosition = false,
                bool doRandomVolumeSound = false) 
    {
        GameObject newObject = new GameObject();
        
        newObject.name = "instan sound:"+audioSourceNameInteger.ToString();
        
        audioSourceNameInteger++;
        
        newObject.transform.position =doRandomPosition?
            new Vector3(Random.Range(float.MinValue,float.MaxValue), Random.Range(float.MinValue, float.MaxValue)) : localTransform.position;

        newObject.transform.rotation = localTransform.rotation;


AudioSource localaudioSources =        newObject.AddComponent<AudioSource>();

        newObject.AddComponent<isSFXSoundScript>().isSFXSound = true;
        




        
        if(sfxSoundAudioMixer!= null) 
        {
            localaudioSources.outputAudioMixerGroup = sfxSoundAudioMixer;
        }
        else 
        {
            localaudioSources.outputAudioMixerGroup = null;
        }

        if (localaudioSources.isPlaying)
            localaudioSources.Stop();


        if(localAudioClip != null)
            localaudioSources.clip = localAudioClip;

        localaudioSources.loop = false;




        if (doRandomVolumeSound)
            localaudioSources.volume = Random.Range(0.25f, 0.95f);

        localaudioSources.Play();

        Destroy(newObject, localAudioClip.length);

    }


    //Without Position
    public void SFXSoundEffectOnFunction
        (AudioClip localAudioClip, 
            bool doRandomVolumeSound = false) 
    {
        GameObject newGameObject = new GameObject();
        
        newGameObject.name = "instan sound:" + audioSourceNameInteger.ToString();

        audioSourceNameInteger++;


        AudioSource localaudioSources = newGameObject.AddComponent<AudioSource>();

        newGameObject.AddComponent<isSFXSoundScript>().isSFXSound = true;

        if (sfxSoundAudioMixer != null)
        {
            localaudioSources.outputAudioMixerGroup = sfxSoundAudioMixer;
        }
        else
        {
            localaudioSources.outputAudioMixerGroup = null;
        }

        localaudioSources.loop = false;

        if(localAudioClip != null)
                localaudioSources.PlayOneShot(localAudioClip);

        if(doRandomVolumeSound)
        localaudioSources.volume = Random.Range(0.25f,0.95f);

        Destroy(newGameObject, localAudioClip.length);

    }


    //Function: Destroy All of The SFX Sound
    public void DestroyAllofTheSFXSoundFunction() 
    {

        AudioSource[] localAudioSourcesArray = FindObjectsOfType<AudioSource>();


        isSFXSoundScript[] isSFXSoundsScript = FindObjectsOfType<isSFXSoundScript>();


        

        if(isSFXSoundsScript != null) 
        {

            foreach (var sf in isSFXSoundsScript)
            {
                if(sf.GetComponent<AudioSource>().isPlaying)
                sf.GetComponent<AudioSource>().Stop();

                if(sf.isSFXSound)
                Destroy(sf.gameObject,0.05f);

            }

        }

    }

}


