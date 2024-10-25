using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {

	private Text _counterText;
	private bool _timeCounter = true;
	private float _seconds, _minutes;

	void Start()
	{
		_counterText = GetComponent<Text>();
	}

	void Update()
	{
		if (_timeCounter)
		{
			_seconds = (int)(Time.timeSinceLevelLoad % 60f);
			_counterText.text = "Seconds: " + _seconds.ToString("00");
		}
	}

	public void EndGame()
	{
		_timeCounter = false;
		_counterText.color = Color.yellow;
	}
}
