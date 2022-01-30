using UnityEngine.SceneManagement;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
