using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameManager game_manager_;

    [SerializeField] private Vector3[] positions_;
    private int player_position_index_;
    private GameObject beam_;
    [SerializeField] private float beam_active_duration_;

    [SerializeField] private CowController cow_controller_;

    private int points_ = 0;

    public int hp_;

    [SerializeField] private Text score_;

    [SerializeField] private GameObject damage_indicator_;
    [SerializeField] private float damage_duration_;
    [SerializeField] private GameObject death_indicator_;

    [SerializeField] private GameObject[] hp_ui_elements_;

    [SerializeField] private AudioClip beam_sound_;
    [SerializeField] private AudioClip damage_;

    private AudioSource sound_player_;


    // Start is called before the first frame update
    void Start()
    {
        sound_player_ = GetComponent<AudioSource>();

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
            sound_player_.clip = beam_sound_;
            sound_player_.Play();

            StopCoroutine("ActivateBeam");
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

    IEnumerator TakeDamage()
    {
        hp_--;

        hp_ui_elements_[hp_].SetActive(false);

        damage_indicator_.SetActive(true);

        if (hp_ <= 0)
        {
            game_manager_.GameOver(3.0f);
            death_indicator_.SetActive(true);
        }

        yield return new WaitForSeconds(damage_duration_);

        damage_indicator_.SetActive(false);
    }

    private void CheckCow()
    {
        if (!cow_controller_.cows_[player_position_index_].activeInHierarchy) return;

        if (cow_controller_.cows_[player_position_index_].transform.GetChild(0).gameObject.activeInHierarchy)
        {

            cow_controller_.BullCollected();

            StopCoroutine("TakeDamage");
            StartCoroutine(TakeDamage());
            
        }
        else
        {
            cow_controller_.CowCollected(points_>=1000); //how many points to collect before game goes batshit

            //Points
            points_ += cow_controller_.cow_points_;
            score_.text = points_.ToString();
        }

        cow_controller_.cows_[player_position_index_].SetActive(false);
    }

}
