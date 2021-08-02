using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public enum InGameUIState
    {
        GameUI,
        PauseUI,
        GameOverUI,
        SkinChangeUI
    }

    public GameUI GameUI;
    public PauseUI PauseUI;
    public GameOverUI GameOverUI;
    public SkinChangeUI SkinChangeUI;
    [Space] public GameObject Player;
    public SpriteRenderer PlayerGFX;
    public Sprite[] skins;
    public int Score;

    public InGameUIState _inGameUIState;
    public AudioSource SoundUI;
    private int skinCounter;
    
    private bool a = true;
    private bool b = true;


    private void Start()
    {
        AddListeners();
        Score = Mathf.CeilToInt(Player.transform.position.y);
        _inGameUIState = InGameUIState.SkinChangeUI;
        Time.timeScale = 0f;
        skinCounter = 0;
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer
            .GetFloat("Music", out float music);
        if (music <= 0) a = false;
        GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer
            .GetFloat("Sounds", out float sounds);
        if (sounds <= 0) a = false;
    }

    void Update()
    {
        StateChange();
        if (Player.transform.position.y > Score)
            Score = Mathf.CeilToInt(Player.transform.position.y);
        GameUI.Score.text = Score.ToString();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_inGameUIState == InGameUIState.PauseUI)
            {
                _inGameUIState = InGameUIState.GameUI;
                Time.timeScale = 1f;
            }
            else if (_inGameUIState == InGameUIState.GameUI)
            {
                Time.timeScale = 0f;
                _inGameUIState = InGameUIState.PauseUI;
            }
        }

        if (_inGameUIState == InGameUIState.SkinChangeUI)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) OnPrev();
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) OnNext();
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) OnPlay();
            if (Input.GetKeyDown(KeyCode.Escape)) OnExit();
        }

        if (Player.GetComponent<Player>().isDead)
        {
            _inGameUIState = InGameUIState.GameOverUI;
            Time.timeScale = 0f;
        }
    }
    
    void StateChange()
    {
        switch (_inGameUIState)
        {
            case InGameUIState.GameUI:
                GameUI.gameObject.SetActive(true);
                PauseUI.gameObject.SetActive(false);
                GameOverUI.gameObject.SetActive(false);
                SkinChangeUI.gameObject.SetActive(false);
                break;
            case InGameUIState.PauseUI:
                GameUI.gameObject.SetActive(false);
                PauseUI.gameObject.SetActive(true);
                GameOverUI.gameObject.SetActive(false);
                SkinChangeUI.gameObject.SetActive(false);
                break;
            case InGameUIState.GameOverUI:
                GameUI.gameObject.SetActive(false);
                PauseUI.gameObject.SetActive(false);
                GameOverUI.gameObject.SetActive(true);
                SkinChangeUI.gameObject.SetActive(false);
                break;
            case InGameUIState.SkinChangeUI:
                GameUI.gameObject.SetActive(false);
                PauseUI.gameObject.SetActive(false);
                GameOverUI.gameObject.SetActive(false);
                SkinChangeUI.gameObject.SetActive(true);
                break;
        }
    }

    public void AddListeners()
    {
        PauseUI.Exit.onClick.AddListener(OnExit);
        PauseUI.Music.onClick.AddListener(OnMusic);
        PauseUI.Sounds.onClick.AddListener(OnSound);
        
        SkinChangeUI.Exit.onClick.AddListener(OnExit);
        SkinChangeUI.Play.onClick.AddListener(OnPlay);
        SkinChangeUI.Prev.onClick.AddListener(OnPrev);
        SkinChangeUI.Next.onClick.AddListener(OnNext);
    }
    
    public void OnExit()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        SceneManager.LoadScene(0);
    }

    public void OnMusic()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        a = !a;
        if (a)
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Music", 0);
        else
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Music", -80);
    }

    public void OnSound()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        b = !b;
        if (b)
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Sounds", 0);
        else
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().Mixer.audioMixer.SetFloat("Sounds", -80);
    }

    public void OnPlay()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        Time.timeScale = 1f;
        PlayerGFX.sprite = SkinChangeUI.Skin.sprite;
        _inGameUIState = InGameUIState.GameUI;
    }

    public void OnNext()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        skinCounter++;
        if (skinCounter > skins.Length - 1) skinCounter = 0;
        SkinChangeUI.Skin.sprite = skins[skinCounter];
    }

    public void OnPrev()
    {
        SoundUI.PlayOneShot(SoundUI.clip);
        skinCounter--;
        if (skinCounter < 0) skinCounter = skins.Length - 1;
        SkinChangeUI.Skin.sprite = skins[skinCounter];
    }
}
