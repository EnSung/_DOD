using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Pattern : MonoBehaviour
{
    private CircleIdentifier idf;
    public GameObject LinePrefab;
    public Canvas canvas;

    private Dictionary<int,CircleIdentifier> circles;
    private List<int> circleList;
    private List<CircleIdentifier> lines;
    private GameObject lineOnEdit;
    private RectTransform lineOnEditrcTs;
    private CircleIdentifier circleOnEdit;

    private bool unlocking;

    new bool enabled = true;

    public Animator[] circleAnim;


    void Start()
    {
        circles = new Dictionary<int, CircleIdentifier>();
        lines = new List<CircleIdentifier>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var circle = transform.GetChild(i);

            var identifier = circle.GetComponent<CircleIdentifier>();

            identifier.id = i;
            circles.Add(i, identifier);
        }
    }

    void Update()
    {
        if (enabled == false)
            return;
        if (unlocking)
        {
            Vector3 _mousePos = canvas.transform.InverseTransformPoint(Input.mousePosition);
            Vector3 mousePos = new Vector3(_mousePos.x, _mousePos.y + 390, _mousePos.z);

            lineOnEditrcTs.sizeDelta = new Vector2(lineOnEditrcTs.sizeDelta.x, Vector3.Distance(mousePos, circleOnEdit.transform.localPosition));

            lineOnEditrcTs.rotation = Quaternion.FromToRotation(Vector3.up, (mousePos - circleOnEdit.transform.localPosition).normalized);
        }
    }

    IEnumerator Release()
    {

        if (BattleManager.Instance.Pattern_id.Count > 1)
        {
            BattleManager.Instance.PatternInputEnd = true;
            enabled = false;
            circleList = BattleManager.Instance.Pattern_id.ToList<int>();
            foreach (int circle in BattleManager.Instance.Pattern_id)
            {
                circleAnim[circle].SetBool("finish", true);
            }
            yield return new WaitForSeconds(0.1f);
            BattleManager.Instance.Pattern_id.Clear();
            yield return new WaitForSeconds(0.5f);
            //foreach (var circle in circles)
            //{
            //    circle.Value.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            //    circle.Value.GetComponent<Animator>().enabled = false;
            //}


            BattleManager.Instance.PatternInputEnd = false;

            foreach (int circle in circleList)
            {
                circleAnim[circle].SetBool("finish", false);
            }


        }
        else
        {
            BattleManager.Instance.Pattern_id.Clear();
        }

        foreach (var line in lines)
        {
            Destroy(line.gameObject);
        }



        try
        {
            circleList.Clear();
        }
        catch
        {

        }
        try
        {
            lines.Clear();
        }
        catch
        {

        }
        

        lineOnEdit = null;
        lineOnEditrcTs = null;
        circleOnEdit = null;

        enabled = true;

    }

    GameObject CreateLine(Vector3 pos, int id)
    {


        var line = GameObject.Instantiate(LinePrefab, canvas.transform);

        line.transform.localPosition = pos;

        var lineIdf = line.AddComponent<CircleIdentifier>();

        lineIdf.id = id;

        lines.Add(lineIdf);

        return line;
    }
    void TrySetLineEdit(CircleIdentifier circle)
    {
        foreach (var line in lines)
        {
            if (line.id == circle.id)
            {
                return;
            }
        }
        Vector3 circlePosition = new Vector3(circle.transform.localPosition.x, circle.transform.localPosition.y - 390, circle.transform.localPosition.z);

        lineOnEdit = CreateLine(circlePosition, circle.id);
        lineOnEditrcTs = lineOnEdit.GetComponent<RectTransform>();
        circleOnEdit = circle;
    }

    void EnableColorFade(Animator anim)
    {
        anim.enabled = true;
        anim.Rebind();
    }

    #region 마우스 이벤트
    public void OnMouseEnterCircle(CircleIdentifier idf)
    {

        var id = idf.id;
        //Debug.Log(idf.id);
        if (enabled == false)
            return;
        if (unlocking)
        {
            //Debug.Log(id);
            lineOnEditrcTs.sizeDelta = new Vector2(lineOnEditrcTs.sizeDelta.x, Vector3.Distance(circleOnEdit.transform.localPosition, idf.transform.localPosition));
            lineOnEditrcTs.rotation = Quaternion.FromToRotation(Vector3.up, (idf.transform.localPosition - circleOnEdit.transform.localPosition).normalized);


            if (!BattleManager.Instance.Pattern_id.Contains(id))
            {
                BattleManager.Instance.Pattern_id.Add(id);
            }

            TrySetLineEdit(idf);
        }
    }
    public void OnMouseExitCircle(CircleIdentifier idf)
    {
        if (enabled == false)
            return;
    }
    public void OnMouseDownCircle(CircleIdentifier idf)
    {
        if (enabled == false)
            return;
        unlocking = true;
        TrySetLineEdit(idf);
    }
    public void OnMouseUpCircle(CircleIdentifier idf)
    {
        if (unlocking)
        {
            foreach (var line in lines)
            {
                EnableColorFade(circles[line.id].gameObject.GetComponent<Animator>());
            }
            
            Destroy(lines[lines.Count - 1].gameObject);
            lines.RemoveAt(lines.Count - 1);

            StartCoroutine(Release());
        }
        unlocking = false;
        
    }
    #endregion
}
