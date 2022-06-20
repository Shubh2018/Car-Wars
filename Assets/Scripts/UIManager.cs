using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Weapon _playerWeapon;
    private float _timeLimit;

    [SerializeField] private TMP_Text _playerAmmo;
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private CanvasGroup _winPanel;
    [SerializeField] private CanvasGroup _losePanel;
    [SerializeField] private CanvasGroup _pausePanel;

    private static UIManager instance;

    public static UIManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        DisableWinPanel();
        DisableLosePanel();
        DisablePausePanel();

        _timeLimit = SelectedCarInfo.Instance.TimeLimit;
    }

    private void Start()
    {
        _playerWeapon = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Gun>().weapon;
    }

    private void Update()
    {
        _playerAmmo.text = _playerWeapon.CurrentAmmo().ToString();
    }

    public void EnableWinPanel()
    {
        _winPanel.alpha = 1;
        _winPanel.blocksRaycasts = true;
        _winPanel.interactable = true;
    }

    private void DisableWinPanel()
    {
        _winPanel.alpha = 0;
        _winPanel.blocksRaycasts = false;
        _winPanel.interactable = false;
    }

    public void EnableLosePanel()
    {
        _losePanel.alpha = 1;
        _losePanel.blocksRaycasts = true;
        _losePanel.interactable = true;
    }

    private void DisableLosePanel()
    {
        _losePanel.alpha = 0;
        _losePanel.blocksRaycasts = false;
        _losePanel.interactable = false;
    }

    public void EnablePausePanel()
    {
        _pausePanel.alpha = 1;
        _pausePanel.blocksRaycasts = true;
        _pausePanel.interactable = true;
        Time.timeScale = 0.0f;
    }

    public void DisablePausePanel()
    {
        _pausePanel.alpha = 0;
        _pausePanel.blocksRaycasts = false;
        _pausePanel.interactable = false;
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Replay()
    {
        SceneManager.LoadScene("CarSelection");
    }

    public void Timer()
    {
        if(SelectedCarInfo.Instance.IsTimeLimitGame)
        {
            _timeLimit -= Time.deltaTime;

            int sec = (int)(_timeLimit % 60);
            int min = (int)(_timeLimit / 60);

            if (_timeLimit < 10)
                _timerText.text = min.ToString() + ":0" + sec.ToString();
            else
                _timerText.text = min.ToString() + ":" + sec.ToString();

            if (_timeLimit < 0)
                _timeLimit = 0;
        }

        else
        {
            _timeLimit = Mathf.Infinity;
            _timerText.text = _timeLimit.ToString();
        }
    }

    public float ReturnRemainingTime()
    {
        return _timeLimit;
    }
}
