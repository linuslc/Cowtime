using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    private enum  COWSPAWNSTATE{
        WaitForSpawn,
        Spawned
    }  
    
    [SerializeField] private Vector3[] positions_;
    [SerializeField] private int max_spawn_count_;

    [SerializeField] private float spawn_interval_;
    [SerializeField] private float visible_duration_;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float horn_rate_;

    private float timer_;

    private COWSPAWNSTATE state_ = COWSPAWNSTATE.WaitForSpawn;

    [SerializeField] private GameObject cow_prefab_;
    public List<GameObject> cows_ = new List<GameObject>();

    public int cow_points_;

    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < positions_.Length; i++)
        {
            cows_.Add(Instantiate(cow_prefab_, positions_[i], Quaternion.identity));
        }

        DespawnCows();
    }

    // Update is called once per frame
    void Update()
    {
        timer_ += Time.deltaTime;

        switch(state_)
        {
            case COWSPAWNSTATE.WaitForSpawn:
                if(timer_> spawn_interval_)
                {
                    SpawnCows();
                    timer_ = 0.0f;
                    state_ = COWSPAWNSTATE.Spawned;
                }
                break;
            case COWSPAWNSTATE.Spawned:
                if(timer_ > visible_duration_)
                {
                    DespawnCows();
                    timer_ = 0.0f;
                    state_ = COWSPAWNSTATE.WaitForSpawn;
                }
                break;
            default:
                Debug.Log("Why Tho");
                break;
        }


    }

    private void SpawnCows()
    {
        
        int spawn_count = Random.Range(1, max_spawn_count_ +1);

        List<int> indices = new List<int>();

        // creating a list of indices (indexes) that represent the list of cows

        for (int i = 0; i < cows_.Count; i++)
        {
            indices.Add(i);
        }

        // getting a random index and activate the cow in that index and then we remove that index

        for (int i = 0; i < spawn_count; i++)
        {
            int random_index = Random.Range(0, indices.Count);

            cows_[random_index].SetActive(true);

            bool spawn_with_horns = (Random.value < horn_rate_);

            cows_[random_index].transform.GetChild(0).gameObject.SetActive(spawn_with_horns);

        // makes sure there aren't 2 in a row
            indices.RemoveAt(random_index);
        }

    }

    private void DespawnCows()
    {
        for (int i = 0; i < cows_.Count; i++)
        {
            cows_[i].SetActive(false);
        }
    }
}
