using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    //public int lives;
    public GameObject heart;
    public float gap_size;
    private List<GameObject> hearts;

    void Start()
    {
        hearts = new List<GameObject>();

        SetLife(Player.main.health);
    }

    public void RemoveLife()
    {
        Destroy(hearts[hearts.Count - 1]);
        hearts.RemoveAt(hearts.Count - 1);
    }

    public void AddLife()
    {
        hearts.Add(Instantiate(heart, transform.position + new Vector3(gap_size * hearts.Count, 0.0f, 0.0f), Quaternion.identity, this.gameObject.transform));
    }

    public void SetLife(int num_lives)
    {
        int counter;

        counter = 0;

        hearts.Clear();

        for (int i = 0; i < num_lives; i++) {
            AddLife();
        }
    }
}
