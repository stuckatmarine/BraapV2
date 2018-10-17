using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sledController : MonoBehaviour {

    // Public
    public bool onPC = true;
    public int eState = 0;
    public int eAudioState;
    public Text gCurrentScoreText;
    public Button bBraapButton;
    public GameObject gGameManager;
    public GameObject gPointer;
    public GameObject gTerrain;
    public GameObject gCameraObject;
    public GameObject gCenterParent;
    public GameObject gCrashPopup;
    

    // Audio
    public AudioClip mirror;
    public AudioClip snap;
    public AudioClip gunShot;
    public AudioClip bubbles;
    public float fBraapVolume = .2f;

    // Private
    public float fCurrentSpeed = 0.0f;
    public float fNormalForwardSpeed = 10.0f;
    public float fTurnAmount = 100.0f;
    public float fBraapTurnMultiplier = 1.25f;
    private float fLastForwardSpeed;
    private float fLastTurnAmount;
    public float fMaxTurnAmount = 200.0f;
    public float fTurnRampUpAmount = 1.0f;
    public float fTurnRampUpIncrement = 0.25f;
    private float fHighscore;
    public float fBraapPower = 100.0f;
    public float fPointMultiplier = 1.0f;
    public float fScoreIncriment = 1.0f;
    public float fCurrentScore = 0.0f;
    private float fPointerValue;
    private float fMinPointer = 429f;
    private float fMaxPointer = 307f;
    public float fWaterThreshold = 125.0f;
    public bool bForward = false;
    public bool bBraapDown = false;
    public bool bRightDown = false;
    private bool bLeftDown = false;
    private bool braapMute = false;
    private bool bWater = false;
    private bool bWaterApplied = false;
    public float fTurnWaterDrag = 0.4f;
    private bool bCrashSound = false;
    private int sledColor;
    private int iSuperSled;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        gGameManager = GameObject.Find("manager");
        fHighscore = gGameManager.GetComponent<GameManager>().GetLocalHighscore();
        eState = gGameManager.GetComponent<GameManager>().GetState();
        gGameManager.GetComponent<GameManager>().FindGamePanel();
        gGameManager.GetComponent<GameManager>().FindLoadingPanel();
        eAudioState = gGameManager.GetComponent<GameManager>().eAudioState;
        sledColor = gGameManager.GetComponent<GameManager>().iSledColor;
        iSuperSled = gGameManager.GetComponent<GameManager>().iSuperSled;

        if (eState == 3)
        {
            gGameManager.GetComponent<GameManager>().SetLoadingDisabled();
        }
    }
    // Use this for initialization
    void Start () {
        gCurrentScoreText.text = fCurrentScore.ToString();   
        rb = GetComponent<Rigidbody2D>();
        
        anim = GameObject.Find("sledAnimationsRed").GetComponent<Animator>();
        if (iSuperSled == 2)
        {
            Destroy(GameObject.Find("sledAnimationsRed"));
            Destroy(GameObject.Find("sledAnimationsYellow"));
            Destroy(GameObject.Find("sledAnimationsBlue"));
            anim = GameObject.Find("sledAnimationsSuper").GetComponent<Animator>();
        }
        else if (sledColor == 1)
        {
            Destroy(GameObject.Find("sledAnimationsSuper"));
            Destroy(GameObject.Find("sledAnimationsYellow"));
            Destroy(GameObject.Find("sledAnimationsRed"));
            anim = GameObject.Find("sledAnimationsBlue").GetComponent<Animator>();
        }
        else if (sledColor == 2)
        {
            Destroy(GameObject.Find("sledAnimationsSuper"));
            Destroy(GameObject.Find("sledAnimationsBlue"));
            Destroy(GameObject.Find("sledAnimationsRed"));
            anim = GameObject.Find("sledAnimationsYellow").GetComponent<Animator>();
        }
        else
        {
            Destroy(GameObject.Find("sledAnimationsSuper"));
            Destroy(GameObject.Find("sledAnimationsBlue"));
            Destroy(GameObject.Find("sledAnimationsYellow"));
            anim = GameObject.Find("sledAnimationsRed").GetComponent<Animator>();
        }

        fPointerValue = (fMinPointer);
        gPointer.transform.eulerAngles = new Vector3(0, 0, fPointerValue);

        if (eState == 2)
        {
            gGameManager.GetComponent<GameManager>().SetGamePanelDisabled();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (eState == 4)
        {
            //-------for keyboard inputs------
            if (onPC)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    bRightDown = true;
                    bLeftDown = false;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    bRightDown = false;
                    bLeftDown = true;
                }
                else
                {
                    bRightDown = false;
                    bLeftDown = false;
                }

              /*  if (Input.GetKey(KeyCode.Space))
                {
                    bBraapDown = true;
                }
                else
                {
                    bBraapDown = false;
                }*/
            }

            // water condition
            if (bWater && !bWaterApplied)
                EnterWater();
            else if (bWater && fCurrentSpeed < fWaterThreshold)
            {
                Crash("Water");
            }

            // speedometer
            fPointerValue = (fMinPointer + (fMaxPointer - fMinPointer) * (rb.velocity.magnitude / fBraapPower) * fPointMultiplier);
            gPointer.transform.eulerAngles = new Vector3(0, 0, fPointerValue);

            // sled controls
            if (bBraapDown)
            {
                rb.AddForce(transform.up * fBraapPower);

                fCurrentScore += fScoreIncriment; // incriment score variable
                //   scoreString.text = braapMeter.ToString("F" + 1.ToString()); // score update

                //braap turning, more torque then normal turning
                if (bLeftDown)
                {
                    anim.SetInteger("turn", -1);
                    rb.AddTorque(fTurnAmount * fTurnRampUpAmount * fBraapTurnMultiplier);
                    if (fTurnAmount * fTurnRampUpAmount <= fMaxTurnAmount * fBraapTurnMultiplier)
                    {
                        fTurnRampUpAmount += fTurnRampUpIncrement;
                    }
                }
                else if (bRightDown)
                {
                    anim.SetInteger("turn", 1);
                    rb.AddTorque(-fTurnAmount * fTurnRampUpAmount * fBraapTurnMultiplier );
                    if (fTurnRampUpAmount * fTurnAmount <= fMaxTurnAmount * fBraapTurnMultiplier)
                    {
                        fTurnRampUpAmount += fTurnRampUpIncrement;
                    }
                }
                else
                {
                    anim.SetInteger("turn", 0);
                    fTurnRampUpAmount = 1.0f;
                }
            }
            else
            {
                //constand forward speed
                rb.AddForce(transform.up * fNormalForwardSpeed);

                //kills drifting motion after braap
                rb.velocity = forwardVelocity();
                GetComponent<AudioSource>().volume = 0f;

                //turning
                if (bLeftDown)
                {
                    rb.AddTorque(fTurnAmount);
                    anim.SetInteger("turn", -1);
                }
                else if (bRightDown)
                {
                    rb.AddTorque(-fTurnAmount);
                    anim.SetInteger("turn", 1);
                }
                else
                    anim.SetInteger("turn", 0);
            }
        }
	}

    public void EnterWater()
    {
        if (!bWaterApplied)
        {
            bWaterApplied = true;
            fLastTurnAmount = fTurnAmount;
            fTurnAmount = fTurnAmount * fTurnWaterDrag;
        }
    }

    public void ExitWater()
    {
        if (bWaterApplied) { 
            bWaterApplied = false;
            fTurnAmount = fLastTurnAmount;
        }
    }

    public void Crash(string type = "Tree")
    {
        gGameManager.GetComponent<GameManager>().StopGame();
                
        GameObject.Find("Camera").GetComponent<CameraFollow>().crashed = true; //stopping camera motion and sound
        rb.velocity = new Vector2(0f, 0f); // stop sled
        anim.SetBool("crashed", true); // crashed animation

        if (fCurrentScore > fHighscore)
        {
            fHighscore = fCurrentScore;
            gGameManager.GetComponent<GameManager>().SetLocalHighscore(fHighscore);
        }

        if (GetComponent<AudioSource>().volume > 0)
        {
            WaitFor(.25f);
            GetComponent<AudioSource>().volume = 0f; // mute music
        }

        //if volume on play crash sound
        if (eAudioState <= 1)
        {
            int num = Random.Range(0, 2);
            if (num == 0)
            {
                AudioSource.PlayClipAtPoint(mirror, transform.position);
            }
            else if (num == 1)
            {
                AudioSource.PlayClipAtPoint(snap, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(gunShot, transform.position);
            }
        }

        WaitFor(1.2f);
        gCrashPopup.transform.position = gCenterParent.transform.position;
        Debug.Log("crashed and popup moved");
    }

    public void BraapSoundOn()
    {
        if (eAudioState <= 1)
        {
            GetComponent<AudioSource>().volume = fBraapVolume;
        }
    }

    public void BraapSoundOff()
    {
        if (eAudioState <= 1)
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }
    }

    //forward velocity without drifting
    Vector2 forwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }

    //braap, speedboost toggle
    public void BraapButtonDown()
    {
        bBraapDown = true;
        GetComponent<AudioSource>().volume = fBraapVolume;
    }

    //braap, speedboost toggle
    public void BraapButtonUp()
    {
        bBraapDown = false;
        GetComponent<AudioSource>().volume = 0.0f;
    }

    //turn right, null left
    public void RightButton()
    {
        if (bRightDown)
        {
            bRightDown = false;
        }
        else
        {
            bRightDown = true;
            bLeftDown = false;
        }
    }

    //turn left null right
    public void LeftButton()
    {
        if (bLeftDown)
        {
            bLeftDown = false;
        }
        else
        {
            bLeftDown = true;
            bRightDown = false;
        }
    }

    //null all controls
    public void SteeringFalse()
    {
        bLeftDown = false;
        bRightDown = false;
    }

    // nullBraap
    public void BraapFalse()
    {
        bBraapDown = false;
    }

    public void StartGameCaller()
    {
        gGameManager.GetComponent<GameManager>().StartGame();
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = 0.0f;
    }

    public void LoadingDisabledCaller()
    {
        gGameManager.GetComponent<GameManager>().SetLoadingDisabled();
    }

    IEnumerator WaitFor(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
    }
}
