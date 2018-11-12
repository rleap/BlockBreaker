using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    // Configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float paddleLeftLimitInUnits = 1f;
    [SerializeField] float paddleRightLimitInUnits = 15f;

    // Cached references
    GameSession gameSession;
    Ball ball;

	// Use this for initialization
	void Start () {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), paddleLeftLimitInUnits, paddleRightLimitInUnits);
        transform.position = paddlePos;
	}

    private float GetXPos()
    {
        if(gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
