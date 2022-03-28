using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneController : UI_Base
{
    enum Images
    {
        Progressbar
    }


    Image progressBar;
    public override void Init()
    {
        Bind<Image>(typeof(Images));

        progressBar = GetImage((int)Images.Progressbar);


        StartCoroutine(LoadSceneProcess());
    }

    


    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(Managers.Scene.loadSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;

        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f) //fake
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount>= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
