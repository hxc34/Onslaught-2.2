using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}