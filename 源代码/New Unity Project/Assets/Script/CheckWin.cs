using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour {

    GameObject[] blackPoints;
    GameObject[] colorPoints;
    Dictionary<string, int> map = new Dictionary<string, int>();
    List<List<int>> circle = new List<List<int>>();
    List<int>[] graph;

    bool[] usd;

    void Start()
    {
        blackPoints = GameObject.FindGameObjectsWithTag("blackPoint");
        colorPoints = GameObject.FindGameObjectsWithTag("colorPoint");
        print(blackPoints);
        foreach (GameObject point in blackPoints)
        {
            print(point.name + map.Count.ToString());
            map.Add(point.name, map.Count);
            
        }
        
        usd = new bool[blackPoints.Length];
        graph = new List<int>[blackPoints.Length];
        
    }
    public bool checkWin()
    {
        circle = new List<List<int>>();
        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = new List<int>();
            usd[i] = false;
        }
        List<MapCtrl.edge> edges = MapCtrl.allEdges();
        foreach(MapCtrl.edge e in edges)
        {
            graph[map[e.a.name]].Add(map[e.b.name]);
            graph[map[e.b.name]].Add(map[e.a.name]);
        }
        string tmp = "";

        ans = new List<int>();
        dfs(map[edges[0].a.name]);
        solve_circle();
        tmp = "circle:\n";
        foreach(List<int> l in circle)
        {
            foreach(int a in l)
            {
                tmp += a.ToString() + " ";
            }
            tmp += "\n";
        }
        print(tmp);
        if (countColorInCircle())
        {
            winShow();
            return true;
        }
        return false;
    }

    private void winShow()
    {
        foreach(GameObject color in colorPoints)
        {
            Material material = color.GetComponent<GetWinMaterial>().getWinMaterial();
            Transform[] vertices = null;

            foreach(List<int> cir in circle)
            {
                if (pointInPolygon(cir, color.transform.position))
                {
                    vertices = new Transform[cir.Count-1];
                    for(int i = 0;i < vertices.Length;i++)
                    {
                        vertices[i] = blackPoints[cir[i]].transform;
                    }
                    break;
                }
            }
            GameObject pologon = new GameObject("pologon");
            pologon.transform.position = color.transform.position;
            pologon.AddComponent<PolygonDrawer>();
            pologon.AddComponent<TurnBigger>();
            pologon.GetComponent<PolygonDrawer>().setMaterial(material);
            pologon.GetComponent<PolygonDrawer>().setVertices(vertices);
        }
    }

    List<int> ans;
    private bool countColorInCircle()
    {
        int N = circle.Count;
        int[] count = new int[N + 1];
        foreach(GameObject color in colorPoints)
        {
            count[N]++;
            for(int i = 0; i < N; i++)
            {
                List<int> cir = circle[i];
                if (pointInPolygon(cir, color.transform.position))
                {
                    count[N]--;
                    count[i]++;
                    break;
                }
            }
        }
        for(int i = 0; i < N; i++)
        {
            if (count[i] > 1) return false;
        }
        if (count[N] > 0) return false;
        return true;
    }
    private void dfs(int s)
    {
        if (usd[s] == true)
        {
            int index = ans.LastIndexOf(s);
            if(index == ans.Count - 2)
            {
                return;
            }
            List<int> tmp = new List<int>();
            ans.Add(s);
            for(int i = index; i < ans.Count; i++)
            {
                tmp.Add(ans[i]);
            }
            ans.RemoveAt(ans.Count - 1);
            circle.Add(tmp);
            return;
        }

        ans.Add(s);

        usd[s] = true;
        foreach(int t in graph[s])
        {
            dfs(t);
        }
        ans.RemoveAt(ans.Count - 1);
        usd[s] = false;
    }

    private void solve_circle()
    {
        for(int i = 0; i < circle.Count; i++)
        {
            for(int j = i + 1; j < circle.Count; j++)
            {
                if (equal(circle[i], circle[j]))
                {
                    circle.RemoveAt(j);
                }
            }
        }
        List<MapCtrl.edge> edges = MapCtrl.allEdges();

        for (int i = 0; i < circle.Count; i++)
        {
            /*
            foreach(GameObject point in blackPoints)
            {
                if (circle[i].Contains(map[point.name]))
                {
                    continue;
                }
                if (pointInPolygon(circle[i], point.transform.position))
                {
                    print("poly " + point.name + " in " + circle[i].ToString());
                    circle.RemoveAt(i);
                    i--;
                    goto next;
                }
            }*/
            foreach(MapCtrl.edge e in edges)
            {
                if (edgeInList(circle[i], e))
                {
                    continue;
                }
                Vector3 point_v3 = e.a.transform.position + e.b.transform.position;
                point_v3 /= 2;
                if (pointInPolygon(circle[i], point_v3))
                {
                    circle.RemoveAt(i);
                    i--;
                    goto next;
                }
            }
            next:;
        }
    }
    private bool edgeInList(List<int> list, MapCtrl.edge edge)
    {
        int a = map[edge.a.name];
        int b = map[edge.b.name];
        for(int i = 0; i < list.Count - 1; i++)
        {
            if (list[i] == a && list[i + 1] == b) return true;
            if (list[i] == b && list[i + 1] == a) return true;
        }
        return false;
    }
    private bool pointInPolygon(List<int> o_list, Vector3 point)
    {
        List<Vector3> list = new List<Vector3>();
        foreach(int i in o_list)
        {
            list.Add(Vector3.Normalize(blackPoints[i].transform.position - point));
        }
        float total = 0;

        for(int i = 0; i < list.Count-1; i++)
        {
            float angle = Mathf.Acos(list[i].x * list[i + 1].x + list[i].y * list[i + 1].y);
            if(list[i].x * list[i + 1].y - list[i].y * list[i + 1].x > 0)
            {
                angle = -angle;
            }
            total += angle;
        }

        if(Mathf.Abs(total) < 0.1f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool equal(List<int> oa,List<int> ob)
    {
        if(oa.Count != ob.Count)
        {
            return false;
        }
        List<int> a = new List<int>();
        List<int> b = new List<int>();
        foreach (int i in oa) a.Add(i);
        foreach (int i in ob) b.Add(i);

        a.Sort();b.Sort();
        for(int i = 0; i < a.Count; i++)
        {
            if(a[i] != b[i])
            {
                return false;
            }
        }
        return true;
    }
}
