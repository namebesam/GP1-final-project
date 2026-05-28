using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]


public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying { get; private set;}

    public float levelTime = 15;
    public string sceneName;

    public TMP_Text timerText;

    public TMP_Text scoreText;

    public TMP_Text messageText;

    private float countdown;

    AudioSource audioSource;

    public AudioClip winSFX;

    public AudioClip loseSFX;

    public GameObject nextButton;

    public string nextLevel;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = levelTime;
        SetScoreText(0);
        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            LevelTimer();
            SetTimerText();

            if (PickUpBehavior.pickupCount < 1)
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

    void LevelBeat()
    {
        IsPlaying = false;
        //play soundfx
        PlaySoundClip(winSFX);

        //show message
        DisplayGameMessage("YOU WIN");

        nextButton.SetActive(true);

        PickUpBehavior.ResetPickups();
    }

    public void LevelLost()
    {
        IsPlaying = false;
        //play soundfx
        PlaySoundClip(loseSFX);

        //show message
        DisplayGameMessage("YOU LOSE");

        PickUpBehavior.ResetPickups();

        Invoke("ReloadSameScene", 5);
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

    void LoadScenebyIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    void ReloadSameScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadNextLevel()
    {
        if (nextLevel.Length > 0)
        {
            LoadSceneByName(nextLevel);
        }
        else
        {
            Debug.LogWarning("No nextlevel is specified");
        }
    }
}
