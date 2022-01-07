using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public float speed = 2.0f; //for animation speed value can be changed on editor
    
    public Button btnInstrument; //the button for the instrument that is on "behind" on each character
    public Transform startPos, endPos,insantiatePos; //the starting postion, the end position we need for the references in order to instantiate the instantiatePos object
    private Transform posBegin, posEnd; //private transform objects for using the instantiated objects and use them in lerp - enumerator method
    public Transform[] currentPos; //the array with all the instantiated objects
   
    float distanceMake; //private floate value to calculate the distance for instantiated objects
    [HideInInspector]public int newIndex; //index to use for the positions when player goes from one to position to the other, is used to change the beginning and ending positions when lerping
    public int currentPosIndex; //value to say how many objects to instantiate 
    public Animator anim; //the animator where this script is attached
    SpriteRenderer _sprite; //the sprite of the object the script is attached in order to flip when going forward or backwards

    [Space]
    [Header("Managers")]
    public AudioManagerPreSchool audioManager; //audiomanager to use some specific methods
    public GameManager manager; //gamemanager to use some specific methods
   
    //initializing the values for our game object, plus create the positions
    void Start()
    {
        currentPosIndex += 0;
        newIndex = 0;
        currentPos = new Transform[currentPosIndex];

        btnInstrument.interactable = false;
        _sprite = GetComponent<SpriteRenderer>();
        CreatePoints();
    }

    //when scene is loading, we have a beggining point in the the ditor and an end point and we are making the middle ones dynamically according to the currentPosIndex value
    void CreatePoints()
    {
        distanceMake = 1 / (float)(currentPosIndex);//calculates the diastance between points depending on the value
        for (int i = 0; i < currentPosIndex; i++)
        {
            Vector2 createPos = startPos.position + (endPos.position - startPos.position) * (distanceMake * i);
            Transform newPos = Instantiate(insantiatePos, createPos, Quaternion.identity);

            //insert objects to array
            currentPos[i] = newPos;
        }
    }

    //setting the begin and last point in order for the player to go one step forward when is correct. We need to define those points to use them in lerping
    void SetPoints()
    {
        posBegin = currentPos[newIndex];
        posEnd = currentPos[newIndex+1];
        distanceMake = Vector3.Distance(posBegin.position, posEnd.position);
        
    }

    //setting the begin and last point in order for the player to go one step back when is wrong. We need to define those points to use them in lerping
    void SetOppositePoints()
    {
        //the below statement is mostly for the animator, when player is in the beginning and gives wrong answer, the graphic won't start playing and will stay in position.
        if (newIndex == 0)
        {
            //currentPosIndex = 1;
            posBegin = currentPos[newIndex];
            posEnd = currentPos[newIndex];
            //anim.SetBool("isWalking", false);
        }
        else
        {
            posBegin = currentPos[newIndex];
            posEnd = currentPos[newIndex - 1];
        }
        
        distanceMake = Vector3.Distance(posEnd.position, posBegin.position);

        if (distanceMake == 0)
        {
            audioManager.Invoke("IsFinishedWinLose", audioManager.sourceCam.clip.length);
            
            //Debug.Log("Distance on Opposite " + distanceMake);
        }
        //Debug.Log("Distance on Opposite "+distanceMake);
    }

    //when player gives correct answer, lerping is working opposite to the wrong one. The graphic faces the end and goes 1 step forward
    IEnumerator MoveTargetToPosition(Transform[] pos,float duration)
    {
        SetPoints();

        float time = 0;
        while (time <duration)
        {
            transform.position = Vector2.Lerp(posBegin.position, posEnd.position, time/duration);
            time += Time.deltaTime;
            anim.SetBool("isWalking", true);
            _sprite.flipX = false;
            yield return null;
        }
       
        if (newIndex < pos.Length - 1)
        {
            newIndex++;
            transform.position = pos[newIndex].position;
            
            anim.SetBool("isWalking", false);
            manager.ResetUIElements();
        }

        if (newIndex == pos.Length - 1)
        {
            anim.SetBool("isWalking", false);
            if (transform.position == pos[newIndex].position)
            {
                audioManager.sourceDef.PlayOneShot(audioManager.victoryClip, 1.0f);
                manager.pnlContainer.SetActive(true);
                manager.pnlInfo.SetActive(true);
            }

            yield return null;

        }
        yield break;
    }

    //when player gives wrong answer, lerping is working opposite to the correct one. The graphic flips to the begin and goes 1 step back
    IEnumerator MoveBack(Transform[] pos, float duration)
    {
        SetOppositePoints();
        
        float time = 0;
        while (time < duration)
        {
            if (newIndex == 0) yield break;
           
            transform.position = Vector2.Lerp(posBegin.position, posEnd.position, time / duration);
            time += Time.deltaTime;
            
            anim.SetBool("isWalking", true);
            
            _sprite.flipX = true;
            yield return null;
        }
        
        if (transform.position == posEnd.position)
        {
            
            anim.SetBool("isWalking", false);
            
            yield break;

        }

        if (newIndex < pos.Length )
        {
            
            transform.position = pos[newIndex-1].position;

            
            anim.SetBool("isWalking", false);
            newIndex--;
            manager.ResetUIElements();
        }
        _sprite.flipX = false;
        yield break;
       
    }

    //call coroutine and specific variables and methods that corespond for when player gives correct answer
    public void StartThePath()
    {
        StartCoroutine(MoveTargetToPosition(currentPos, speed));
        audioManager.sourceDef.PlayOneShot(audioManager.winClip, 1.0f);
        manager.InactiveButtons();
        btnInstrument.onClick.RemoveAllListeners();
        manager.ScoreUpdate();
        manager.AnswerGraphic();
        
    }

    //call coroutine and specific variables and methods that corespond for when player gives wrong answer
    public void StartWrongPath()
    {
        StartCoroutine(MoveBack(currentPos, speed));
        audioManager.sourceDef.PlayOneShot(audioManager.loseClip, 1.0f);
        manager.InactiveButtons();
        btnInstrument.onClick.RemoveAllListeners();
        manager.ScoreUpdate();
        manager.AnswerGraphic();
    }
    
}
