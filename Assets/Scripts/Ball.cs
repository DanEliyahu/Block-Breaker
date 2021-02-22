using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 15f;
    [SerializeField] private AudioClip[] ballSounds;

    // state
    private Vector2 _paddleToBallVector;
    private bool _hasStarted = false;
    
    // Cached component references
    private AudioSource _myAudioSource; 

    // Start is called before the first frame update
    void Start()
    {
        _paddleToBallVector = transform.position - paddle1.transform.position;
        _myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasStarted) return;
        LockBallToPaddle();
        LaunchOnMouseClick();
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            _hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        var position = paddle1.transform.position;
        var paddlePos = new Vector2(position.x, position.y);
        transform.position = paddlePos + _paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_hasStarted)
        {
            var clip = ballSounds[Random.Range(0, ballSounds.Length)];
            _myAudioSource.PlayOneShot(clip);
        }
    }
}