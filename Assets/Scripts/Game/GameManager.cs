using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private bool gameOver;

    [Header("DoTween")]
    [SerializeField] private DoFade fade;
    [SerializeField] private DoFade DoTextFade;
    [SerializeField] private DoFade DoLifePlayer;
    [SerializeField] private DoUI DoGameOver;
    [SerializeField] private DoUI DoWin;

    [Header("Input")]
    [SerializeField] private PlayerInput input;

    [Header("SO")]
    [SerializeField] private PointsCounterSO pointsCounterSO;
    private float time;

    [Header("Cameras")]
    [SerializeField] private GameObject CameraIntro;
    [SerializeField] string[] dialogo;

    [Header("Text")]
    [SerializeField] private TextController textdialogo;
    private int index=0;

    [SerializeField] private MostrarPuntaje mostrar;


    private void Start()
    {
        TimeGame(1);
        CursorVisible(false);
        CameraIntro.SetActive(true);
        StartDialogue();
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
    private void StartDialogue()
    {
        StartCoroutine(CorrytinDialogo());
    }
    private IEnumerator CorrytinDialogo()
    {
        yield return new WaitForSeconds(1.5f);
        try
        {
            textdialogo.Print(dialogo[index]);
            ++index;
        }
        catch (IndexOutOfRangeException)
        {
            DoTextFade.FadeOut();
            CameraIntro.SetActive(false);
            input.enabled = true;
            DoLifePlayer.FadeIN();
            fade.FadeOut();
        }
    }
    public void Open(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            input.enabled = false;
            CursorVisible(true);
            TimeGame(0);
        }
    }

    public void Close()
    {
        CursorVisible(false);
        TimeGame(1);
        input.enabled = true;
    }

    public void TimeGame(float time)
    {
        Time.timeScale = time;
    }

    private void GameOver(int life)
    {
        if (life<=0&&!gameOver)
        {
            CursorVisible(true);
            gameOver = true;
            fade.FadeIN();
            StartCoroutine(Fade(DoGameOver));
            input.enabled = false;
        }
    }
    private void Win()
    {
        CursorVisible(true);
        fade.FadeIN();
        StartCoroutine(Fade(DoWin));
        input.enabled = false;
        pointsCounterSO.Add((int)time);
        mostrar.Inprimir();
    }
    private IEnumerator Fade(DoUI doUI)
    {
        yield return new WaitForSeconds(fade.TimeFadeIN);
        doUI.Open();
        TimeGame(0);
    }
    private void OnEnable()
    {
        player.eventLife += GameOver;
        textdialogo.finishingWriting += StartDialogue;
        Finished.eventFinished += Win;
    }
    private void OnDisable()
    {
        player.eventLife -= GameOver;
        textdialogo.finishingWriting -= StartDialogue;
        Finished.eventFinished -= Win;
    }

    private void CursorVisible(bool visible)
    {
        if (visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
