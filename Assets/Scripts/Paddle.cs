using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] private float minX = 1f;
    [SerializeField] private float maxX = 15f;

    private GameStatus myGameStatus;
    private Ball myBall;
    
    // Start is called before the first frame update
    void Start()
    {
        myGameStatus = FindObjectOfType<GameStatus>();
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        var paddlePos = new Vector2(position.x, position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform1.position = paddlePos;
    }

    private float GetXPos()
    {
        if (myGameStatus.IsAutoPlayEnabled())
        {
            return myBall.transform.position.x;
        }
        return Input.mousePosition.x / Screen.width * screenWidthInUnits; // mousePosInUnits
    }
}