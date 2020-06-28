using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackPoint : MonoBehaviour {
    private void OnMouseDown()
    {
        GameObject startPoint = StartPoint.getStartPoint();
        if(startPoint == null)
        {
            StartPoint.setStartPoint(gameObject);
            GameObject DrawLine = GameObject.Find("DrawLine");
            DrawLine.GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            Vector3 sPoint, ePoint;
            sPoint = startPoint.transform.position;

            //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            //获取鼠标在场景中坐标
            Vector3 mousePositionOnScreen = Input.mousePosition;
            //让场景中的Z=鼠标坐标的Z
            mousePositionOnScreen.z = screenPosition.z;
            //将相机中的坐标转化为世界坐标
            ePoint = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

            //参数为：起点坐标，方向向量
            Ray2D ray = new Ray2D(sPoint, Vector3.Normalize(ePoint - sPoint));

            RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction);

            if(info.collider && info.collider.gameObject.name == gameObject.name)
            {
                MapCtrl.insertEdge(startPoint,gameObject);
                StartPoint.setStartPoint(gameObject);
                if (GameObject.Find("CheckWin").GetComponent<CheckWin>().checkWin())
                {
                    StartCoroutine(wait(2f));
                    GameObject DrawLine = GameObject.Find("DrawLine");
                    DrawLine.GetComponent<LineRenderer>().enabled = false;
                }
            }
        }
    }
    IEnumerator wait(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        SceneManager.LoadScene("Catalog");
    }
}
