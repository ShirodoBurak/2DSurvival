using System.Collections;
using UnityEngine;

public class GeneratePlainChunkL : MonoBehaviour
{
    [Header("World Objects")] public GameObject stone1;
    public GameObject stone2;
    [Space(20)] public GameObject dirt1;
    public GameObject dirt2;
    [Space(20)] public GameObject grass;
    [Space(20)] [Header("World Settings")] public int width = 16;
    [Range(5, 150)] public float heightMultiplier;
    public int heightAddition;
    [Range(5, 30)] public float smoothness;
    [HideInInspector] GenerateChunks gnrt = new GenerateChunks();
    [HideInInspector] GameObject newTile;
    int a;
    int b;
    GameObject SelectedTile;

    void Start()
    {
        //Generate();
        StartCoroutine(SlowGenerator());
    }

    void PlaceThese()
    {
        newTile = Instantiate(SelectedTile, new Vector2(0, 0), Quaternion.identity) as GameObject;
        newTile.transform.SetParent(this.gameObject.transform);
        newTile.transform.localPosition = new Vector2(a, b);
    }

    //public void Generate()
    //{
    //    for (int i = 0; i < width; i++)
    //    {
    //        int h = Mathf.RoundToInt(Mathf.PerlinNoise(gnrt.seed, (i + transform.position.x) / smoothness) *
    //                                 heightMultiplier) + heightAddition;

    //        for (int j = 0; j < h; j++)
    //        {
    //            int stoneHeightRandomizer = Random.Range(5, 8);
    //            if (j < h - stoneHeightRandomizer)
    //            {
    //                int randomStone = Random.Range(0, 2);
    //                if (randomStone == 0)
    //                {
    //                    SelectedTile = stone1;
    //                }
    //                else
    //                {
    //                    SelectedTile = stone2;
    //                }
    //            } //Stone creation, randomization and height randomization
    //            else if (j < h - 1)
    //            {
    //                int randomDirt = Random.Range(0, 1);
    //                if (randomDirt == 0)
    //                {
    //                    SelectedTile = dirt1;
    //                }
    //                else
    //                {
    //                    SelectedTile = dirt2;
    //                }
    //            } // Dirt creation and randomization.
    //            else
    //            {
    //                SelectedTile = grass;
    //            } //Grass creation

    //            a = i;
    //            b = j;
    //            PlaceThese();
    //        }
    //    }
    //}

    IEnumerator SlowGenerator()
    {
        for (int i = 0; i < width; i++)
        {
            int h = Mathf.RoundToInt(Mathf.PerlinNoise(gnrt.seed, (i + transform.position.x) / smoothness) *
                                     heightMultiplier) + heightAddition;
            yield return new WaitForSeconds(.005f);
            for (int j = 0; j < h; j++)
            {
                int stoneHeightRandomizer = Random.Range(5, 8);
                if (j < h - stoneHeightRandomizer)
                {
                    int randomStone = Random.Range(0, 2);
                    if (randomStone == 0)
                    {
                        SelectedTile = stone1;
                    }
                    else
                    {
                        SelectedTile = stone2;
                    }
                } //Stone creation, randomization and height randomization
                else if (j < h - 1)
                {
                    int randomDirt = Random.Range(0, 1);
                    if (randomDirt == 0)
                    {
                        SelectedTile = dirt1;
                    }
                    else
                    {
                        SelectedTile = dirt2;
                    }
                } // Dirt creation and randomization.
                else
                {
                    SelectedTile = grass;
                } //Grass creation

                a=Mathf.RoundToInt(-i);
                b=Mathf.RoundToInt(j);
                PlaceThese();
            }
        }
    }
}