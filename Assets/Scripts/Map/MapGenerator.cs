using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Node Generation Settings")]
    [SerializeField] private int MapY;
    [SerializeField] private int MapX;
    [SerializeField] private float MapWidth;
    [SerializeField] private float NodeyPadding;
    [SerializeField] private List<GameObject> PoIprefabs;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private int NStartingPoints;

    [Header("Node Parents")]
    [SerializeField] private Transform nodeParent;
    [SerializeField] private Transform lineParent;

    private GameObject[][] PoIs;

    private void Start() {
        // load seed
        if (Globals.seed == -1)
        {
            GenerateRandomSeed();
        }
        else
        {
            // load seed
            Random.InitState(Globals.seed);
        }

        PoIs = new GameObject[MapY][];
        for (int i = 0; i < PoIs.Length; i++)
        {
            PoIs[i] = new GameObject[MapX];
        }

        List<int> startPoints = new List<int>();
        while (startPoints.Count < NStartingPoints)
        {
            int n = Random.Range(0, MapX);

            if (!startPoints.Contains(n))
            {
                startPoints.Add(n);
            }
        }

        foreach (int startPoint in startPoints)
        {
            GameObject startNode =  InitializePoI(0, startPoint);
            if (!Globals.isGenerated) Globals.nextPoIs.Add(new Vector2(startPoint, 0));
        }

        // Generate end node and lines
        float xSize = MapWidth / MapX;
        float xPos = (xSize * MapX / 2 - 1) + (xSize / 2f);
        float yPos = NodeyPadding * MapY;
        GameObject end = Instantiate(PoIprefabs[0], new Vector3(xPos, yPos, 0), Quaternion.identity, nodeParent);

        for (int i = 0; i < MapX; i++)
        {
            if (PoIs[MapY-1][i] != null)
            {
                DrawLineBetween(PoIs[MapY-1][i], end);
            }
        }

        Globals.isGenerated = true;

        // load path
        foreach(Vector2 pastNode in Globals.path)
        {
            PoIs[(int)pastNode.x][(int)pastNode.y].GetComponent<NodeHover>().Ring.SetActive(true);
        }
    }

    private void GenerateRandomSeed()
    {
        int tempSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(tempSeed);
        Globals.seed = tempSeed;
    }

    private GameObject InitializePoI(int y, int x)
    {
        if (PoIs[y][x] != null)
        {
            return PoIs[y][x];
        }
        bool created = false;

        float xSize = MapWidth / MapX;
        float xPos = (xSize * x) + (xSize / 2f);
        float yPos = NodeyPadding * y;

        //Add a random padding
        xPos += Random.Range(-xSize / 4f, xSize / 4f);
        yPos += Random.Range(-NodeyPadding / 4f, NodeyPadding / 4f);

        Vector3 pos = new Vector3(xPos, yPos, 0);

        // Randomly choose a node type with weight
        GameObject randomPOI = null;
        float rand = Random.Range(0f, 1f);
        if (rand < 0.8f)
        {
            randomPOI = PoIprefabs[0];
        }
        else
        {
            randomPOI = PoIprefabs[1];
        }

        GameObject instance = Instantiate(randomPOI, pos, Quaternion.identity, nodeParent);
        PoIs[y][x] = instance;
        instance.GetComponent<PoI>().x = x;
        instance.GetComponent<PoI>().y = y;

        while (!created && y < MapY - 1)
        {
            if (x > 0 && Random.Range(0f, 1f) < 0.25f)
            {
                if (PoIs[y + 1][x] == null)
                {
                    GameObject nextPOI = InitializePoI(y + 1, x - 1);
                    DrawLineBetween(instance, nextPOI);
                    // save connections
                    instance.GetComponent<PoI>().nextPoIs.Add(new Vector2(x-1, y+1));
                    created = true;
                }
            }

            if (x < MapX - 1 && Random.Range(0f, 1f) < 0.25f)
            {
                if (PoIs[y + 1][x] == null)
                {
                    GameObject nextPOI = InitializePoI(y + 1, x + 1);
                    DrawLineBetween(instance, nextPOI);
                    // save connections
                    instance.GetComponent<PoI>().nextPoIs.Add(new Vector2(x+1, y+1));
                    created = true;
                }
            }

            if (Random.Range(0f, 1f) < 0.5f)
            {
                GameObject nextPOI = InitializePoI(y + 1, x);
                DrawLineBetween(instance, nextPOI);
                // save connections
                instance.GetComponent<PoI>().nextPoIs.Add(new Vector2(x, y+1));
                created = true;
            }
        }

        return instance;
    }

    private void DrawLineBetween(GameObject currNode, GameObject nextNode)
    {
        float len = 1f;
        float height = 1f;

        Vector3 dir = (nextNode.transform.position - currNode.transform.position).normalized;
        float dist = Vector3.Distance(currNode.transform.position, nextNode.transform.position);

        // number of line prefabs to be generated
        int num = (int)(dist / len);

        // find the position of first line
        float pad = (dist - (num * len)) / (num + 1);
        Vector3 pos_i = currNode.transform.position + (dir * (pad + (len / 2f)));

        // Generate all lines
        for (int i = 0; i < num; i++)
        {
            Vector3 pos = pos_i + ((len + pad) * i * dir);
            GameObject line = Instantiate(pathPrefab, pos, Quaternion.identity, lineParent);
            // rotate line towards the next node
            Vector3 diff = nextNode.transform.position - line.transform.position;
            float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            line.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            line.transform.position -= Vector3.up * (height / 2f);
        }
    }
}
