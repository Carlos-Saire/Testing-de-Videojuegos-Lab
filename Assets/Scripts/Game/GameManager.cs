using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager Instance;
    [SerializeField] private float Point;
    [SerializeField] private Slider SliderPoint;
    [SerializeField] private GameObject[] estrellas;
    [SerializeField] private GameObject win;
    [SerializeField] private DoSetting defeat;
    private float currentPoint;

    [Header("Floor")]
    [SerializeField] private GameObject[] floor;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        TimeGme(1);
        SliderPoint.maxValue = Point;
    }
    public void TimeGme(float time)
    {
        Time.timeScale = time;
    }
    private void GameOver()
    {
        TimeGme(0);
        defeat.OpenSetting();
    }
    private void Win()
    {
        win.SetActive(true);
        TimeGme(0);
    }
    private void OnEnable()
    {
        PlayerController.eventDefeat+=GameOver;
        PlayerController.eventWin += Win;
    }
    private void OnDisable()
    {
        PlayerController.eventDefeat-=GameOver;
        PlayerController.eventWin -= Win;
    }
    public void AddPoint()
    {
        ++currentPoint;
        SliderPoint.value = currentPoint;
        if (currentPoint == Point)
        {
            estrellas[1].SetActive(true);
            estrellas[0].SetActive(true);
        }
        else if(currentPoint>=Point/2)
        {
            estrellas[0].SetActive(true);
        }
    }

    public void ActiveFloor()
    {
        for(int i = 0; i < floor.Length; i++)
        {
            floor[i].gameObject.SetActive(true);
        }
    }

    public void DesactiveFloor()
    {
        for (int i = 0; i < floor.Length; i++)
        {
            floor[i].gameObject.SetActive(false);
        }
    }
}
