using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private int maxHits = 3;
    [SerializeField] private Sprite[] hitSprites;

    // Cached references
    private Level _level;

    // state vars
    [SerializeField] private int timesHit; // serialized for debug purposes

    // consts
    private const float SparklesLifeSpan = 2f;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        _level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            _level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CompareTag("Breakable"))
        {
            timesHit++;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
            }
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        _level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        var sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, SparklesLifeSpan);
    }
}