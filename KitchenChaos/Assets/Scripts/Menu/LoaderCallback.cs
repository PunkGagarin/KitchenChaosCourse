using Menu;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{

    private bool isFirstFrame = true;

    // Update is called once per frame
    private void Update()
    {
        if (isFirstFrame)
            isFirstFrame = false;
        
        Loader.LoadingCallback();
    }
}
