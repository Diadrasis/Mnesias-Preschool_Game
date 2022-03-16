using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Space]
    [Header("Help Panel")]
    public GameObject pnlHelp;
    public Button btnCloseHelp;
    public Button btnCloseHelpOut;
    public string txtHelp;
    public TextMeshProUGUI txtParent;

    [Space]
    [Header("Reset")]
    public int creditsSceneNum;
    public int openSceneNum;

    [Header("Menu Elements")]
    public Button btnBack;
    public Button btnCredits;
    public Button btnHelp;
    public Button btnResetScene;
    public Button btnExitApp;

    [Space]
    [Header("Values")]
    public float numValue;
    public float timeValue;
    // Start is called before the first frame update
    void Start()
    {
        SubscribeToButtonsUI();
        CheckCurrentScene();
    }

    void SubscribeToButtonsUI()
    {
        btnResetScene.onClick.AddListener(Reset);
        btnBack.onClick.AddListener(() => OpenScene(openSceneNum));
        btnCredits.onClick.AddListener(() => OpenScene(creditsSceneNum));
        btnHelp.onClick.AddListener(() => EaseInPanel(pnlHelp, new Vector3(pnlHelp.transform.position.x, pnlHelp.transform.position.y - numValue, pnlHelp.transform.position.z), timeValue));
        btnCloseHelp.onClick.AddListener(() => EaseInPanel(pnlHelp, new Vector3(pnlHelp.transform.position.x, pnlHelp.transform.position.y + numValue, pnlHelp.transform.position.z), timeValue));
        btnCloseHelpOut.onClick.AddListener(() => EaseInPanel(pnlHelp, new Vector3(pnlHelp.transform.position.x, pnlHelp.transform.position.y + numValue, pnlHelp.transform.position.z), timeValue));
        btnExitApp.onClick.AddListener(Application.Quit);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OpenScene(int num)
    {
        SceneManager.LoadScene(num);
    }


    public void EaseInPanel(GameObject gameObject, Vector3 vector, float time)
    {
        iTween.MoveTo(gameObject, vector, time);
        txtParent.text = txtHelp;
    }

    public void CheckCurrentScene()
    {

        if(openSceneNum == SceneManager.GetActiveScene().buildIndex)
        {
            btnResetScene.gameObject.SetActive(false);
        }
    }
}
