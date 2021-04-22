using UnityEngine;
using System;

public class MapBuilder : MonoBehaviour
{
    [Header("JSON Setup")]
    [SerializeField] private TextAsset mapJsonFile;

    [Header("JSON Data List")]
    [SerializeField] private MapPiecesList mapPiecesList = new MapPiecesList();

    [Header("Tiles Setup Class")]
    [SerializeField] private TileSetter tileSetter;

    private Sprite[] sprites;

    #region EVENTS

    public static event Action<string> OnTileTagGet;

    #endregion

    private void OnEnable()
    {
        mapPiecesList = JsonUtility.FromJson<MapPiecesList>(mapJsonFile.text);
        sprites = Resources.LoadAll<Sprite>("Sprites");
        SortSprites(mapPiecesList);
    }


    private void Start()
    {
        CreateTile(mapPiecesList);
        tileSetter.SetTiles(mapPiecesList);
    }

    public void SetTileName()
    {
        tileSetter.GetTileName();
        OnTileTagGet?.Invoke(tileSetter.GetTileName());
    }

    private void SortSprites(MapPiecesList mapList)
    {
        if(sprites.Length <= 0)
        {
            Debug.Log("SpriteList is empty!!!");
            return;
        }

        for (int i = 0; i < mapList.List.Count; i++)
        {
            for (int j = i; j < sprites.Length; j++)
            {
                if(sprites[j].name != mapList.List[j].Id)
                { 
                    mapList.List[j].Type = sprites[j + 1];

                    if(sprites[j+1].name != mapList.List[j].Id)
                    {
                        mapList.List[j].Type = sprites[j + 2];
                        break;
                    }
                }
                else
                {
                    mapList.List[j].Type = sprites[j];
                    break;
                }
            }
        }
    }

    private void CreateTile(MapPiecesList mapList)
    {
        tileSetter.SizeOfTiles = mapList.List.Count;

        for (int i = 0; i < tileSetter.SizeOfTiles; i++)
        {
            var createdTile = Instantiate(tileSetter.TileTemplate,transform);
            tileSetter.AddToList(createdTile.GetComponent<Tile>());
        }
    }
}
