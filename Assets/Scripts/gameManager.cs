using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	[field: SerializeField] private Sprite[] _cardFace;
    [field: SerializeField] private Sprite _cardBack;
    [field: SerializeField] private GameObject[] _cards;
    [field: SerializeField] private GameObject _gameTime;
    
    private bool _initialized;
    private int _matches = 4;

    private void Update () {
        if (!_initialized)
            InitializeCards ();

        if (Input.GetMouseButtonUp (0))
            CheckCards ();
    }

    private void InitializeCards() {
        for (int id = 0; id < 2; id++) {
            for (int i = 1; i < 5; i++) {

                bool test = false;
                int choice = 0;
                while (!test) {
                    choice = Random.Range (0, _cards.Length);
                    test = !(_cards [choice].GetComponent<Card> ().Initialized);
                }
                _cards [choice].GetComponent<Card> ().CardValue = i;
                _cards [choice].GetComponent<Card> ().Initialized = true;
            }
        }

        foreach (GameObject c in _cards)
            c.GetComponent<Card> ().SetupGraphics ();

        if (!_initialized)
            _initialized = true;
    }

    public Sprite GetCardBack() {
        return _cardBack;
    }

    public Sprite GetCardFace(int i) {
        return _cardFace[i - 1];
    }

    private void CheckCards() {
        List<int> c = new List<int> ();

        for (int i = 0; i < _cards.Length; i++) {
            if (_cards [i].GetComponent<Card> ().State == 1)
                c.Add (i);
        }

        if (c.Count == 2)
            CardComparison (c);
    }

    private void CardComparison(List<int> c){
        Card.DO_NOT = true;
        int x = 0;

        if (_cards [c [0]].GetComponent<Card> ().CardValue == _cards [c [1]].GetComponent<Card> ().CardValue) {
            x = 2;
            _matches--;
            if (_matches == 0)
                _gameTime.GetComponent<GameTime>().EndGame();
        }

        for (int i = 0; i < c.Count; i++) {
            _cards [c[i]].GetComponent<Card>().State = x;
            _cards [c[i]].GetComponent<Card>().FalseCheck ();
        }
	
    }
}
