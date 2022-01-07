using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerPreSchool : MonoBehaviour
{

    public AudioSource sourceCam;
    public AudioSource sourceDef;
    public AudioClip[] clips;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip victoryClip;
    public Button btnSound;

    //public ButtonManager[] buttonManager;
    public GameManager gameManager;


    public bool isLyra, isAulos, isCorrect, isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        sourceCam = Camera.main.gameObject.GetComponent<AudioSource>();
        sourceDef = GetComponent<AudioSource>();
        //buttonManager = FindObjectsOfType<ButtonManager>();
        SubscribeToButtonsMain();
    }
    public void AudioInit()
    {
        LoadAudio("soundsForGame/");
    }
    void SubscribeToButtonsMain()
    {
        btnSound.onClick.AddListener(RandomSound);
    }
    public void LoadAudio(string fileName)
    {
        clips = Resources.LoadAll(fileName, typeof(AudioClip)).Cast<AudioClip>().ToArray();
        //RandomSound();
        //Debug.Log("Folder Name Audio: " + fileName);
    }
    void IsFinished()
    {
        //when audio finish play then leave player to press buttons.
        for(int i = 0; i < gameManager.buttonManager.Length; i++)
        {
            gameManager.buttonManager[i].btnInstrument.interactable = true;
        }
        gameManager.PlayAtSound();
        gameManager.txtMainComments.text = "Σε ποιο οργανο ανηκει ο ηχος που ακουσες;";
        
    }
    void RandomSound()
    {
        isLyra = false;
        isAulos = false;
        float randPick = Random.value;
        Debug.Log("RandomNum: "+randPick);
        int clipPick = Random.Range(0, clips.Length +(int)randPick);
        sourceCam.clip = clips[clipPick];

        gameManager.imgContainer.GetComponent<Image>().sprite = gameManager.imgMainCommentAfterSound;
        gameManager.imgContainer.GetComponent<Button>().interactable = false;

        for (int i = 0; i < gameManager.buttonManager.Length; i++)
        {
            if (gameManager.buttonManager[i].newIndex <= 2) sourceCam.Play();
            if (gameManager.buttonManager[i].newIndex > 2)
            {
                sourceCam.Play();
                sourceCam.pitch = 2f;
            }

            gameManager.buttonManager[i].btnInstrument.interactable = false;
            gameManager.buttonManager[i].btnInstrument.onClick.RemoveAllListeners();
        }
        
        Invoke("IsFinished", sourceCam.clip.length);
        gameManager.InactiveButtons();
    }


    void IsFinishedWinLose()
    {
        //when audio finish play then leave player to press buttons.
        for (int i = 0; i < gameManager.buttonManager.Length; i++)
        {
            gameManager.buttonManager[i].btnInstrument.interactable = true;
        }
        gameManager.PlayAtSound();
        gameManager.ScoreUpdate();
        gameManager.AnswerGraphic();
        gameManager.ResetUIElements();
    }
}
