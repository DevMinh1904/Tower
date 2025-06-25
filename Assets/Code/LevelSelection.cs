using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[]  lvlButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 0);

        for(int i = 1;i < lvlButtons.Length; i++)
        {
            if(i+2 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
    }
}
