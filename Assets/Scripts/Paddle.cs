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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var mousePositionX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        var transform1 = transform;
        var position = transform1.position;
        var paddlePos = new Vector2(position.x, position.y);
        paddlePos.x = Mathf.Clamp(mousePositionX, minX, maxX);
        transform1.position = paddlePos;
    }
}