using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    private string tag;
    private RectTransform rectTransform;
    private Image spriteImage;

    #region PROPERTIES

    public string Tag { get => tag; set => tag = value; }

    public RectTransform RectTransform { get => rectTransform; set => rectTransform = value; }

    public Image SpriteImage { get => spriteImage; set => spriteImage = value; }


    #endregion


    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        spriteImage = GetComponent<Image>();
    }
  
}

[System.Serializable]
public class TileSetter
{
    [Header("Prefab Setub")]
    [SerializeField] private GameObject tileTemplate;

    [Header("Other Refs")]
    [SerializeField] private ACamera camera;

    private List<Tile> tiles = new List<Tile>();
    private int sizeOfTiles;

    #region PROPERTIES
    
    public int SizeOfTiles { get => sizeOfTiles; set => sizeOfTiles = value; }
    public GameObject TileTemplate { get => tileTemplate; }
    public List<Tile> Tiles { get => tiles; }

    #endregion

    public void SetTiles(MapPiecesList mapList)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].Tag = mapList.List[i].Id;
            tiles[i].RectTransform.localPosition = new Vector2(mapList.List[i].X * 100, mapList.List[i].Y * 100);
            tiles[i].RectTransform.localScale = new Vector2(mapList.List[i].Width, mapList.List[i].Height);
            tiles[i].SpriteImage.sprite = mapList.List[i].Type;
        }
    }

    public void AddToList(Tile t)
    {
        tiles.Add(t);
    }

    public string GetTileName()
    {
        string tileName = null;
        for (int i = 0; i < sizeOfTiles; i++)
        {
            if(Vector2.Distance(camera.CameraLCorner(), tiles[i].transform.position) < 3f)
            {
                tileName = tiles[i].Tag;
                break;
            }
        }
        return tileName;
    }
}
