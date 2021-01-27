using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Vector3[] positions_;
    private int player_position_index_;
    private GameObject beam_;
    [SerializeField] private float beam_active_duration_;

    [SerializeField] private CowController cow_controller_;

    private int points_ = 0;

    public int hp_;

    [SerializeField] private Text score_;
    
    // Start is called before the first frame update
    void Start()
    {
        beam_ = transform.GetChild(0).gameObject;
        beam_.SetActive(false);

        player_position_index_ = positions_.Length / 2;

        transform.position = positions_[player_position_index_];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) &&
            player_position_index_ > 0)
        {
            player_position_index_ -= 1;
            transform.position = positions_[player_position_index_];
            beam_.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) &&
            player_position_index_ < positions_.Length -1)
        {
            player_position_index_ += 1;
            transform.position = positions_[player_position_index_];
            beam_.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(ActivateBeam());

            CheckCow();
        }
    }

   IEnumerator ActivateBeam()
    {
        beam_.SetActive(true);

        yield return new WaitForSeconds(beam_active_duration_);

        beam_.SetActive(false);
    }

    private void CheckCow()
    {
        if (!cow_controller_.cows_[player_position_index_].activeInHierarchy) return;

        if (cow_controller_.cows_[player_position_index_].transform.GetChild(0).gameObject.activeInHierarchy)
        {
            //Damage
            hp_--;

            if (hp_ <= 0) SceneManager.LoadScene("Greta Scene");
        }
        else
        {
            //Points
            points_ += cow_controller_.cow_points_;
            score_.text = points_.ToString();
        }

        cow_controller_.cows_[player_position_index_].SetActive(false);
    }

}
