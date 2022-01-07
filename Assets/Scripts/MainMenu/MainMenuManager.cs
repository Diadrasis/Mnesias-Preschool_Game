using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    [Header("MainUIElements")]
    public GameObject pnlMainMenuButtons;
    public GameObject pnlNextSelection;
    public Button btnBack;
    public Image btnImage;
    public Color imgColor;
    public GameObject topBar;
    public Button btnPlay;

    [Space]
    [Header("PreSchoolElements")]
    public GameObject pnlPreS;
    public Button btnPreSchool;

    [Space]
    [Header("ElementarySchoolElements")]
    public GameObject pnlElementary;
    public Button btnElementary;


    [Space]
    [Header("HighSchoolElements")]
    public GameObject pnlHighS;
    public Button btnHigh;

    

    // Start is called before the first frame update
    void Start()
    {
        OpenClosePanels(true,false,false,false,false,true);
        btnBack.gameObject.SetActive(false);
        SubscribeToMenuButtons();
        imgColor = btnImage.color;
        
    }

    private void SubscribeToMenuButtons()
    {
        btnElementary.onClick.AddListener(OpenElementary);
        btnPreSchool.onClick.AddListener(OpenPreSchool);
        btnHigh.onClick.AddListener(OpenHighSchool);
        btnBack.onClick.AddListener(BackPanels);
        
    }

    private void BackPanels()
    {
        if (pnlNextSelection.activeSelf)
        {
            pnlNextSelection.SetActive(false);
            pnlMainMenuButtons.SetActive(true);
            btnBack.gameObject.SetActive(false);
        }
    }

    private void OpenHighSchool()
    {
        OpenClosePanels(false, true, false, false, true, false);
        btnBack.gameObject.SetActive(true);
        StartCoroutine(ImageOpacity());
        btnPlay.onClick.AddListener(() => LoadLevelGame("HighSchool"));
    }

    private void OpenPreSchool()
    {
        OpenClosePanels(false, true, true, false, false, false);
        btnBack.gameObject.SetActive(true);
        StartCoroutine(ImageOpacity());
        btnPlay.onClick.AddListener(() => LoadLevelGame("PreSchoolGame"));
    }

    private void OpenElementary()
    {
        OpenClosePanels(false, true, false, true, false, false);
        btnBack.gameObject.SetActive(true);
        StartCoroutine(ImageOpacity());
        btnPlay.onClick.AddListener(()=>LoadLevelGame("ElementarySchool"));
    }

    public void LoadLevelGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    void OpenClosePanels(bool pnlMain, bool pnlNext, bool pnlPre, bool pnlEle, bool pnlHigh, bool topB)
    {
        pnlMainMenuButtons.SetActive(pnlMain);
        pnlNextSelection.SetActive(pnlNext);
        pnlPreS.SetActive(pnlPre);
        pnlElementary.SetActive(pnlEle);
        pnlHighS.SetActive(pnlHigh);
        topBar.SetActive(topB);
    }

    IEnumerator ImageOpacity()
    {
        imgColor.a = 0;
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                imgColor.a += 0.1f;
                btnImage.color = imgColor;

                yield return new WaitForSeconds(0.1f);

            }
            for (int j = 0; j < 10; j++)
            {
                imgColor.a -= 0.1f;
                btnImage.color = imgColor;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        
        /*yield return null;*/
    }

    #region NotInUse
    /*public List<GameObject> sprImagesPre = new List<GameObject>();
    GameObject currentImage;*/


    /*void ShowFirstImage()
    {
        if (isPre)
        {
            if (sprImagesPre.Count > 0)
            {
                foreach (GameObject item in sprImagesPre) item.SetActive(false);
                sprImagesPre[0].SetActive(true);
                currentImage = sprImagesPre[0];
                btnPrevious.gameObject.SetActive(false);
            }
        }
        if (isHigh)
        {
            Debug.Log("HighSchool Here");
        }
        if (isEle)
        {
            Debug.Log("Elementary Here");
        }
    }

    void NextImage()
    {
        int indx = sprImagesPre.IndexOf(currentImage);
        indx++;

        if (indx >= sprImagesPre.Count)
        {
            btnNext.gameObject.SetActive(false);
            return;
        }

        if (indx > 0) btnPrevious.gameObject.SetActive(true);

        currentImage.SetActive(false);
        sprImagesPre[indx].SetActive(true);
        currentImage = sprImagesPre[indx];
    }

    void PrevImage()
    {
        int indx = sprImagesPre.IndexOf(currentImage);
        indx--;

        if (indx <=0 ) btnPrevious.gameObject.SetActive(false);

        if (indx < sprImagesPre.Count-1) btnNext.gameObject.SetActive(true);

        currentImage.SetActive(false);
        sprImagesPre[indx].SetActive(true);
        currentImage = sprImagesPre[indx];
    }*/
    #endregion
}
