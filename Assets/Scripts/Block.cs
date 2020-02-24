using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockAnimationVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    Level level;
    GameStatus gameStatus;

    [SerializeField] int timesHit; // TODO debug, remove later

    private void Start()
    {
        CountBlocksThatAreBreakable();
    }

    private void CountBlocksThatAreBreakable()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandeHit();
        }
    }

    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
    }

    private void HandeHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject, 0f);
        level.BlockDestroyed();
        gameStatus.AddToScore();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockAnimationVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
