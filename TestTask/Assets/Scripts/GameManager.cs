using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _signs = 0;

    public int Signs
    {
        get
        {
            return _signs;    
        }
        set
        {
            _signs = value;
        }
    }

    private int _golds = 0;
    [SerializeField] private Text _signsText;
    [SerializeField] private Text _goldsText;
    [SerializeField] private Text _finalText;
    [SerializeField] private Slider _chekPointsSlider;
    [SerializeField] private GameObject _starPanel;
    [SerializeField] private List<GameObject> _chekPoints= new List<GameObject>();
    private int _carentCheckPoint=0;

    private void Awake()
    {
        Instance= this;
        _chekPointsSlider.maxValue=_chekPoints.Count;
    }

    private void OnEnable()
    {
        PlayerInput.OnClicked += HideStartPanel;
        PlayerMove.OnDestroy += PrintFinalTExt;
        CheckPoint.OnTurn += UpgradeSlider;
        PlayerCollecting.OnCollecting += AddItems;
        SpawnSigns.OnSpawn += RemoweSign;
    }

    private void OnDisable()
    {
        PlayerInput.OnClicked -= HideStartPanel;
        PlayerMove.OnDestroy -= PrintFinalTExt;
        CheckPoint.OnTurn -= UpgradeSlider;
        PlayerCollecting.OnCollecting -= AddItems;
        SpawnSigns.OnSpawn -= RemoweSign;
    }

    private void HideStartPanel()
    {
        _starPanel.SetActive(false);
    }

    private void PrintFinalTExt(string text)
    {
        _finalText.gameObject.SetActive(true);
        _finalText.text = text;
        Invoke("Restar", 1);
    }

    private void Restar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);     
    }

    private void UpgradeSlider(string a)
    {
        _carentCheckPoint++;
        _chekPointsSlider.value= _carentCheckPoint;
    }

    private void AddItems(string name)
    {
        if (name == "coin")
        {
            _golds++;
            _goldsText.text = _golds.ToString();
        }
        else
        {
            _signs+=3;
            _signsText.text = _signs.ToString();
        }
    }

    private void RemoweSign()
    {
        _signs--;
        _signsText.text = _signs.ToString();
    }
}
