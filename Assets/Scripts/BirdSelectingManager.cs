using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdSelectingManager : MonoBehaviour
{

    [SerializeField] private BirdDatabaseSO birdDatabase;
    [SerializeField] private SpriteRenderer artwork;


    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void NextOption()
    {
        selectedOption++;
        if(selectedOption >= birdDatabase.BirdCount)
        {
            selectedOption = 0;
        }

        UpdateBird(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;
        if(selectedOption < 0)
        {
            selectedOption = birdDatabase.BirdCount-1;
        }

        UpdateBird(selectedOption);
    }

    private void UpdateBird(int selectedOption)
    {
        BirdSO birdSO = birdDatabase.GetBird(selectedOption);
        artwork.sprite = birdSO.sprite;
    }


}
