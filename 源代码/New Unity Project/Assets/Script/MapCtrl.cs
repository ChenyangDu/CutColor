using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCtrl : MonoBehaviour {

    static public Object line_pf;

    public class edge
    {
        public GameObject a, b;
        public GameObject line;
        public edge(GameObject a,GameObject b,GameObject line)
        {
            this.a = a;
            this.b = b;
            this.line = line;
        }
    }


    private void Start()
    {
        line_pf = (GameObject)Resources.Load("Prefab/line");
        list = new List<edge>();
    }
    static List<edge> list = new List<edge>();

    static public bool find(GameObject a, GameObject b)
    {
        foreach(edge e in list)
        {
            if(e.a.name == a.name && e.b.name == b.name)
            {
                return true;
            }
            if(e.a.name == b.name && e.b.name == a.name)
            {
                return true;
            }
        }
        return false;
    }

    static public bool insertEdge(GameObject a,GameObject b)
    {
        if (find(a, b))
        {
            return false;
        }
        GameObject line = (GameObject)Instantiate(line_pf);
        line.GetComponent<line>().set(a.transform.position, b.transform.position);
        list.Add(new edge(a, b,line));
        return true;
    }

    static public List<edge> allEdges()
    {
        return list;
    }
}