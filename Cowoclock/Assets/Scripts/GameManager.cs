using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerController player_controller_;
    [SerializeField] private CowController cow_controller_;

    public void GameOver(float delay)
    {
        StartCoroutine(EndGame(delay));
    }

    IEnumerator EndGame(float delay)
    {
        player_controller_.enabled = false;
        cow_controller_.enabled = false;

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Greta Scene");
    }
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
