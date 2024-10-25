using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public static bool DO_NOT = false;
    [SerializeField] private int _state;
    [SerializeField] private int _cardValue;
    [SerializeField] private bool _initialized = false;
    private Sprite _cardBack;
    private Sprite _cardFace;
    private GameObject _manager;

    private void Start(){
        _state = 1;
        _manager = GameObject.FindGameObjectWithTag("Manager");
    }

    public void SetupGraphics() {
        _cardBack = _manager.GetComponent<GameManager> ().GetCardBack ();
        _cardFace = _manager.GetComponent<GameManager> ().GetCardFace (_cardValue);

        Flipcard ();
    }

    public void Flipcard() {
        if (_state == 0)
            _state = 1;
        else if (_state == 1)
            _state = 0;
		
        if (_state == 0 && !DO_NOT)
            GetComponent<Image> ().sprite = _cardBack;
        else if (_state == 1 && !DO_NOT)
            GetComponent<Image> ().sprite = _cardFace;
    }

    public int CardValue {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public int State {
        get { return _state; }
        set { _state = value; }
    }

    public bool Initialized {
        get { return _initialized; }
        set { _initialized = value; }
    }

    public void FalseCheck() {
        StartCoroutine (Pause ());
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(0.5f);
        
        if(_state == 0)
            GetComponent<Image>().sprite = _cardBack;
        else if(_state == 1)
            GetComponent<Image>().sprite = _cardFace;
        
        DO_NOT = false;
    }
}
