using System.Collections;
using UnityEngine;
using System.IO;

public class Logic : MonoBehaviour
{
    [SerializeField] GameObject ProgresText;
    [SerializeField] GameObject InvertText;
    [SerializeField] GameObject CubesStand;
    [SerializeField] GameObject HitCubeBlue;
    [SerializeField] GameObject HitCubeOrange;
    [SerializeField] GameObject HitCubeGreen;
    [SerializeField] GameObject CubesSpawner;
    [SerializeField] GameObject SpawnerBall;
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject Camera;

    public int ProgresCount;
    private int heightLines;

    void Start()
    {
        RestartGame();
    }

    //Key down events
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnCubes();
            SpawnBall();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (CameraFlip()) InvertText.GetComponent<TMPro.TextMeshProUGUI>().text = "Je invertování";
            else InvertText.GetComponent<TMPro.TextMeshProUGUI>().text = "Není invertování";
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    private bool CameraFlip()
    {
        Camera.GetComponent<CameraScript>().flipHorizontal = !Camera.GetComponent<CameraScript>().flipHorizontal;
        return Camera.GetComponent<CameraScript>().flipHorizontal;
    }

    IEnumerator WinRestartGame()
    {
        yield return new WaitForSeconds(2);
        SpawnCubes();
        SpawnBall();
    }

    private void RestartGame()
    {
        SpawnCubes();
        SpawnBall();
    }

    private void SpawnCubes()
    {
        DeleteAllCubes();
        SpawnHitCubes();
    }

    private void RefreshProgresInfo(int CountOfCubes)
    {
        ProgresCount = 0;
        ProgresText.GetComponent<TMPro.TextMeshProUGUI>().text = $"Srazil/a kostek 0/{CountOfCubes}";
    }

    public void ChangeProgresInfo() 
    {
        string Cubes = ProgresText.GetComponent<TMPro.TextMeshProUGUI>().text.Split('/')[2];
        ProgresCount++;
        ProgresText.GetComponent<TMPro.TextMeshProUGUI>().text = $"Srazil/a kostek {ProgresCount}/{Cubes}";

        //WIN
        if (ProgresCount == int.Parse(Cubes))
        {
            StartCoroutine(WinRestartGame());
            Camera.GetComponent<Sounds_Manager>().PLayWin();
        }
    }

    public void SpawnBall()
    {
        DeleteBall();
        Instantiate(Ball, SpawnerBall.transform.position, CubesSpawner.transform.rotation);
        Camera.GetComponent<Sounds_Manager>().PLayBallRespawnSound();
    }

    private void DeleteBall()
    {
        Destroy(GameObject.FindGameObjectWithTag("Ball"));
    }

    private void DeleteAllCubes()
    {
        GameObject[] gameObjectsArr = GameObject.FindGameObjectsWithTag("HitCube");
        foreach (GameObject item in gameObjectsArr)
        {
            Destroy(item);
        }
    }

    private void SpawnHitCubes()
    {
        string[] mapArr = File.ReadAllLines("Map.txt");
        int CountOfCubes = 0;
        heightLines = mapArr.Length;

        //change stand
        CubesStand.transform.localScale = new Vector3((mapArr[mapArr.Length - 1].Length * 1f),  CubesStand.transform.localScale.y, CubesStand.transform.localScale.z);

        //change cubes spawner
        CubesSpawner.transform.localPosition = new Vector3(CubesStand.transform.localScale.x / 2 *-1 -1.3f + mapArr[mapArr.Length - 1].Length * 0.06f, CubesSpawner.transform.localPosition.y, CubesSpawner.transform.localPosition.z);

        //spawn cubes
        for (int i = 0; i < heightLines; i++)
        {
            char[] charArr = mapArr[i].ToCharArray();
            for (int y = 0; y < charArr.Length;)
            {
                char currentChar = charArr[y];
                if (currentChar == ' ')
                {
                    y++;
                }
                if (currentChar.ToString().ToLower() == "m" || currentChar.ToString().ToLower() == "z" || currentChar.ToString().ToLower() == "o")
                {
                    GameObject CurrentCube = HitCubeBlue;
                    //cube color pick
                    switch (currentChar.ToString().ToLower())
                    {
                        case "m":
                            CurrentCube = HitCubeBlue;
                            break;
                        case "z":
                            CurrentCube = HitCubeGreen;
                            break;
                        case "o":
                            CurrentCube = HitCubeOrange;
                            break;
                    }

                    y += 3;

                    Vector3 targetPosition = CubesSpawner.transform.position;

                    //vertical
                    targetPosition.y = targetPosition.y + heightLines * 0.24f - i * 0.24f;

                    //horisontal
                    targetPosition.x = targetPosition.x + y * 0.075f;

                    Instantiate(CurrentCube, targetPosition, CubesSpawner.transform.rotation).transform.SetParent(CubesSpawner.transform);
                    CountOfCubes++;
                }
            }
        }

        RefreshProgresInfo(CountOfCubes);
    }
}