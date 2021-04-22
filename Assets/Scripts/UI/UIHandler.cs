using UnityEngine;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text tagText;


    private void OnEnable()
    {
        MapBuilder.OnTileTagGet += MapBuilder_OnTileTagGet;
    }

    private void OnDisable()
    {
        MapBuilder.OnTileTagGet -= MapBuilder_OnTileTagGet;
    }

    private void MapBuilder_OnTileTagGet(string obj)
    {
        tagText.text = obj;
    }
    
}
