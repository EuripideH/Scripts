
using UnityEngine;
using TMPro;
 




public class DisplayScore : MonoBehaviour
{

    private Timer timeR;
    public float timer;
    string currentTime;

    // Start is called before the first frame update
    void Awake()
    {
       GameObject timerScript = GameObject.Find("TimeCountCanvas");
        timeR = timerScript.GetComponent<Timer>();
       float timer  = timeR.timer;
       // currentTime = timer.currentTime[2];
        UpdateTimerDisplay();

    }

    // Update is called once per frame
    void Start()
    {
        
        Debug.Log("Your total time is '" + timer + "'.");
    }

    void UpdateTimerDisplay()
    {
        timeR.UpdateTimerDisplay(timer);

    }





}
