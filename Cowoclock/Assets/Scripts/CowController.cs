using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    [SerializeField] private Vector3[] positions_;
    [SerializeField] private int max_spawn_count_;

    [SerializeField] private float spawn_interval_;
    [SerializeField] private float visible_suration_;

    private float timer_;

    private bool spawned_ = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer_ += timer_.deltaTime;

        if(timer_ > spawn_interval_ && !spawned_)
        {
            SpawnCows();
            timer_ = 0.0f;
        }
    }

    private void SpawnCows()
    {
        int spawn_count = Random.Range(0, max_spawn_count_ +1);
    }
}
