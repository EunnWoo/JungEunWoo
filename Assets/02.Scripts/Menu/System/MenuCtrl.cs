using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuCtrl : MonoBehaviour
{
    public RectTransform PauseImage;
    public RectTransform PauseMenu;
    public RectTransform SoundMenu;
    public RectTransform ScreenMenu;
    [SerializeField]
    GameObject PlayerObj;
    void Start()
    {
      
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))

                Pause();
        }
    }
    public void Pause()
    {      
        if (PauseImage.gameObject.activeInHierarchy == false)
        {
            if (PauseMenu.gameObject.activeInHierarchy == false)
            {
                PauseMenu.gameObject.SetActive(true);
                SoundMenu.gameObject.SetActive(false);
                ScreenMenu.gameObject.SetActive(false);
            }
            PauseImage.gameObject.SetActive(true);
            Time.timeScale = 0f;
            PlayerObj.SetActive(false);
        }
        else
        {
            PauseImage.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            PlayerObj.SetActive(true);
        }

    }
    public void Sounds(bool isOpen)
    {
        if (isOpen)
        {

            SoundMenu.gameObject.SetActive(true);
            PauseMenu.gameObject.SetActive(false);
            ScreenMenu.gameObject.SetActive(false);
           // GameManager.isOpenSystemMenu = true;
        }
        if (!isOpen)
        {

            SoundMenu.gameObject.SetActive(false);
            PauseMenu.gameObject.SetActive(true);
           // GameManager.isOpenSystemMenu = false;
        }

    }
    public void ScreenSetting(bool isOpen)
    {
        if (isOpen)
        {
            ScreenMenu.gameObject.SetActive(true);
            SoundMenu.gameObject.SetActive(false);
            PauseMenu.gameObject.SetActive(false);
        }
        if (!isOpen)
        {
            SoundMenu.gameObject.SetActive(false);
            ScreenMenu.gameObject.SetActive(false);
            PauseMenu.gameObject.SetActive(true);

        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("BladeGirlJoyStickPlay");
    }

}
