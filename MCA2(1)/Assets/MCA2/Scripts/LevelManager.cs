using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}

    public float levelTime = 15;

    public TMP_Text timerText;

    public TMP_Text scoreText;

    public TMP_Text messageText;

    private float countdown;

    public AudioSource audioSource;

    public AudioClip winSFX;

    public AudioClip loseSFX;

    public GameObject nextButton;

    public string nextLevel;

    public GameObject messageHolder;

    private string sceneName;

    public PlayerController playerController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextButton.SetActive(false);
        countdown = levelTime;
        SetScoreText(0);
        IsPlaying = true;
        messageHolder.SetActive(false);

        //check to see if this is the final level
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            LevelTimer();
            SetTimerText();

            //not the cleanest code ever, but what this is doing is checking if the current
            //scene name is finalevel, if it is adds the flag as a win condition
            //also when I tried to clean it up I broke it so I'm just not gonna touch it

            if (sceneName == "FinalLevel")
            {
                if (PickUpBehavior.pickupCount <= 0 && playerController.flagHit == true)
                {
                    //win
                    LevelBeat();
                }

                else if (countdown <= 0)
                {
                    //lose
                    LevelLost();
                }
            }

            else
            {
                if (PickUpBehavior.pickupCount <= 0)
                {
                    //win
                    LevelBeat();
                }

                else if (countdown <= 0)
                {
                    //lose
                    LevelLost();
                }
            } 
        }
    }

    void LevelTimer()
    {
        countdown -= Time.deltaTime;
        
        if (countdown <= 0)
        {
            countdown = 0;
        }

        Debug.Log("Countdown: " + countdown.ToString("0.00"));
    }

    void SetTimerText()
    {
        timerText.text = countdown.ToString("0.00");
    }

    public void SetScoreText(int currentScore)
    {
        scoreText.text = "Score: " + PickUpBehavior.totalScore.ToString();
    }

    public void LevelBeat()
    {
        IsPlaying = false;
        //play soundfx
        PlaySoundClip(winSFX);
        messageHolder.SetActive(true);

        //check if this is the final scene
        if (sceneName == "FinalLevel")
        {
            DisplayGameMessage("GAME COMPLETED");
            nextButton.SetActive(true);
            PickUpBehavior.ResetPickups();
        }

        else
        {
            //show message
            DisplayGameMessage("YOU WIN");

            nextButton.SetActive(true);

            PickUpBehavior.ResetPickups();
        }
    }

    public void LevelLost()
    {
        IsPlaying = false;
        //play soundfx
        PlaySoundClip(loseSFX);

        messageHolder.SetActive(true);

        //show message
        DisplayGameMessage("GAME OVER");

        PickUpBehavior.ResetPickups();

        Invoke("ReloadSameScene", 2);
        //ReloadSameScene();
        
    }

    void PlaySoundClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void DisplayGameMessage (string message)
    {
        if (messageText)
        {
            messageText.text = message;
            messageText.enabled = true;
        }  
    }

    void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    void ReloadSameScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadNextLevel()
    {
        //should check if current level is the last one, if yes load first level
        if (nextLevel.Length > 0 && sceneName == "FinalLevel")
        {
            SceneManager.LoadScene(0); //loads first level in build index
        }
        else if (nextLevel.Length > 0)
        {
            LoadSceneByName(nextLevel);
        }
        else
        {
            Debug.LogWarning("No nextlevel is specified");
        }
    }
}
