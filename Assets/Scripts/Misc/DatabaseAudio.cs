using UnityEngine;
using System.Linq;

public class DatabaseAudio : MonoBehaviour
{
    public AudioClip[] audioFiles;
    public AudioSource source;

    public void AudioInit()
    {
        LoadAudio("_main_dafnisL/");
    }
    public void LoadAudio(string fileName)
    {
        audioFiles = Resources.LoadAll(fileName, typeof(AudioClip)).Cast<AudioClip>().ToArray();
       
        source = Camera.main.gameObject.GetComponent<AudioSource>();
        Debug.Log("Folder Name Audio: "+fileName);
    }

    public void PlaySounds(int num)
    {
        source.clip = audioFiles[num];
        source.PlayOneShot(audioFiles[num]);
        Debug.Log("Num: " + num);
        /*for(int i = 0; i < audioFiles.Length; i++)
        {
            
        }*/
    }
}
