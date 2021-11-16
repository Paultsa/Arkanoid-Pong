using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenResolution : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown dropdownMenu;

    public int resWidth;
    public int resHeight;
    public int resValue;

    public void Options()
    {
        dropdownMenu.value = GameStatus.status.resValue;
        Screen.SetResolution(GameStatus.status.resWidth, GameStatus.status.resHeight, true);
    }
    public void Apply()
    {
        GameStatus.status.resWidth = resWidth;
        GameStatus.status.resHeight = resHeight;
        GameStatus.status.resValue = resValue;
    }
    void Start()
    {
        resolutions = Screen.resolutions;
        dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, true);
            Screen.fullScreen = true;
            resHeight = resolutions[dropdownMenu.value].height;
            resWidth = resolutions[dropdownMenu.value].width;
            resValue = dropdownMenu.value;
            Debug.Log(dropdownMenu.value);
        });
        for (int i = 0; i < resolutions.Length; i++)
        {
            dropdownMenu.options[i].text = ResToString(resolutions[i]);
            dropdownMenu.value = i;
            dropdownMenu.options.Add(new Dropdown.OptionData(dropdownMenu.options[i].text));

        }
        Options();
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}