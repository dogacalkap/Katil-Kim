using UnityEngine;
using System.Collections.Generic;
using static FlashbackManager;

public class ClueObject : MonoBehaviour
{
    public FlashbackManager flashbackManager;

    // Sprite adlarý (Resources klasöründen alýnýr)
    public string leftIdleName;
    public string rightIdleName;
    public string leftArgueName;
    public string rightArgueName;
    public string leftFinalName;
    public string rightFinalName;

    // Diyaloglar
    public string text1;
    public string text2;
    public string text3;

    private void OnMouseDown()
    {
        var entry = new FlashEntry
        {
            text1 = text1,
            text2 = text2,
            text3 = text3,
            leftIdle = LoadSprite(leftIdleName),
            rightIdle = LoadSprite(rightIdleName),
            leftArgue = LoadSprite(leftArgueName),
            rightArgue = LoadSprite(rightArgueName),
            leftFinal = LoadSprite(leftFinalName),
            rightFinal = LoadSprite(rightFinalName)
        };

        flashbackManager.StartFlashback(new List<FlashEntry> { entry });
    }

    private Sprite LoadSprite(string name)
    {
        return Resources.Load<Sprite>("Sprites/Characters/" + name);
    }
}
