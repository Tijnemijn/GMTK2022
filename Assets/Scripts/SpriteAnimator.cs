using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] List<SpriteAnimation> animations;

    private int currentFrame = 0;
    private SpriteAnimation currentAnimation;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentAnimation = animations[0];
    }
    private void Start()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimationCoroutine());        
    }
    public void SwitchAnimation(string name)
    {
        var result = animations.Find(a => a.name == name);
        if (result != currentAnimation)
        {
            if (result != null)
                currentAnimation = result;
            currentFrame = 0;
        }
    }
    public void JumpToFrame(int frame)
    {
        currentFrame = frame;
        spriteRenderer.sprite = currentAnimation.GetFrame(frame);
    }
    private void UpdateFrame()
    {
        spriteRenderer.sprite = currentAnimation.GetFrame(currentFrame);
    }

    private IEnumerator AnimationCoroutine()
    {
        while (true)
        {
            while(currentAnimation.Paused)
            {
                yield return null;
            }            
            yield return Utils.WaitNonAlloc(1f / currentAnimation.FPS);
            
            currentFrame++;
            if (currentFrame == currentAnimation.FrameCount)
            {
                currentFrame = 0;
            }
            UpdateFrame();

            while (currentFrame == currentAnimation.FrameCount - 1 && !currentAnimation.Looping)
            {
                yield return null;
            }
            
        }
    }
}
