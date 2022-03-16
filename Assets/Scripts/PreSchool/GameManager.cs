using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    [Header("UI Elements")]
    public GameObject pnlContainer;//parent container of panel info
    public GameObject pnlInfo; //final panel with info.,etc
    public TextMeshProUGUI scoreText; //tries text object
    //public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI txtMainComments;//main text comments and change of img depending on state we are in
    public GameObject imgContainer;
    public Sprite imgMainComment;//the image that will change with the top text
    public Sprite imgMainCommentAfterSound;//the image that will change with the top text
    public GameObject spContainerA;//is the sprite container for aulos in order to show the correct or wrong answer
    public GameObject spContainerL;//is the sprite container for lyra in order to show the correct or wrong answer
    public Sprite spCorrect;//correct sprite
    public Sprite spWrong;//wrong sprite

    public Button btnClosePnlInfo; //when final panel active, to close it

    [Space]
    [Header("Managers")]
    public ButtonManager[] buttonManager; //to get some methods from current manager
    public AudioManagerPreSchool audioManager; //to get some methods from current manager

    UIManager iManager;
    int scoreTries/*, highscoreTries*/;//tries for a player
   
    // Start is called before the first frame update
    void Start()
    {
        pnlContainer.SetActive(false);
        pnlInfo.SetActive(false);
        SubscribeToButtonsManager();
        scoreText.text = "Προσπαθειες: ";
        scoreTries = 0;
        txtMainComments.text = "Πατα το κουμπι για να παιξεις τον ηχο.";
        imgContainer.GetComponent<Image>().sprite = imgMainComment;
        audioManager.AudioInit();
        buttonManager = FindObjectsOfType<ButtonManager>();
        iManager = FindObjectOfType<UIManager>();
        //foreach (GameObject go in spContainer) go.SetActive(false);
        //highscoreText.text = "Συνολικές Προσπάθειες¨";
        spContainerA.SetActive(false);
        spContainerL.SetActive(false);
    }

    void SubscribeToButtonsManager()
    {
        btnClosePnlInfo.onClick.AddListener(ClosePanel);
    }

    //close final panel info
    private void ClosePanel()
    {
        pnlContainer.SetActive(false);
        ResetUIElements();
        iManager.Reset();
    }

    //when we hear the sound, the instrument buttons won't be interactable
    public void InactiveButtons()
    {
        for (int i = 0; i < buttonManager.Length; i++)
        {
            audioManager.isCorrect = false;

            buttonManager[i].btnInstrument.interactable = false;

        }
        
    }

    //to check if player was correct on their prediction and the specific animal will continue their journey or move back 
    public void PlayAtSound()
    {
        if (audioManager.sourceCam.clip.name.Contains("lyra"))
        {
            audioManager.isCorrect = true;
            audioManager.isLyra = true;
            buttonManager[1].btnInstrument.onClick.AddListener(buttonManager[1].StartThePath);
        }
        else
        {
            audioManager.isLyra = false;
            audioManager.isCorrect = false;
            buttonManager[1].btnInstrument.onClick.AddListener(buttonManager[1].StartWrongPath);
        }
        if (audioManager.sourceCam.clip.name.Contains("aulos"))
        {

            audioManager.isCorrect = true;
            audioManager.isAulos = true;
            buttonManager[0].btnInstrument.onClick.AddListener(buttonManager[0].StartThePath);
        }
        else
        {
            audioManager.isAulos = false;
            audioManager.isCorrect = false;
            buttonManager[0].btnInstrument.onClick.AddListener(buttonManager[0].StartWrongPath);
        }

    }

    //update the score and show on game text
    public void ScoreUpdate()
    {
        scoreTries += 1;
        scoreText.text = "Προσπαθειες: " + scoreTries;
    }

    //reset ui elements to original values when the player has listened to the clip, selected the correct/wrong answer
    public void ResetUIElements()
    {
        txtMainComments.text = "Πατα το κουμπι για να παιξεις τον ηχο.";
        imgContainer.GetComponent<Image>().sprite = imgMainComment;
        spContainerA.SetActive(false);
        spContainerL.SetActive(false);
        audioManager.isLyra = false;
        audioManager.isAulos = false;
        imgContainer.GetComponent<Button>().interactable = true;
    }

    //when player gives an answer by pressing the button, the graphics containers will activate and show which one is the correct and which is wrong
    public void AnswerGraphic()
    {
        //Debug.Log("answer");
        spContainerA.SetActive(true);
        spContainerL.SetActive(true);
        if (audioManager.isAulos)
        {
            spContainerA.GetComponent<Image>().sprite = spCorrect;
            spContainerL.GetComponent<Image>().sprite = spWrong;
            //Debug.Log("aulos");
        }
        else if (audioManager.isLyra)
        {
            spContainerA.GetComponent<Image>().sprite = spWrong;
            spContainerL.GetComponent<Image>().sprite = spCorrect;
            //Debug.Log("lyra");
        }
    }
}
