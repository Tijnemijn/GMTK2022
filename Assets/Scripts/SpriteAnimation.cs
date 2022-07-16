using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "2D/Sprite Animation", fileName = "New Sprite Animation", order = 100)]
public class SpriteAnimation : ScriptableObject
{
    [field: SerializeField] public bool Paused { get; private set; } = false;
    [field: SerializeField] public bool Looping { get; private set; } = true;
    [field: SerializeField] public int FPS { get; private set; } = 10;

    [SerializeField] private List<Sprite> frames;
    
    public int FrameCount => frames.Count;

    public Sprite GetFrame(int frame) => frames[frame];
}
