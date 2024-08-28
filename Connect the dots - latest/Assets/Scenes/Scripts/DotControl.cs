using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotControl : MonoBehaviour
{
    public Camera mainCamera;

    public int n;
    int[,] adj = new int[100, 100]; // ma tran ke: -1: ko ton tai, 0: chua to, 1: to roi
    int[,] w = new int[100, 100];
    int[,] Basew = new int[100, 100];
    int from = 0, to = 0;
    int[,] pre = new int[100, 100];
    int[] ans = new int[100];
    string D, L, C, Arr;
    Color RandomColor;
    Vector3[,] Basecap = new Vector3[100, 100];
    Vector3[,] BaseArr = new Vector3[100, 100];
    Vector3[] ScaleBase = new Vector3[100];
    Vector3[] ScaleChange = new Vector3[100];
    public int difficulty;

    Vector3 BaseVic, BaseMenu, BaseAgain, BaseNextLV, BaseBack, BaseHand;
    LevelScriptt lvl = new LevelScriptt();
    int m = 0, nedge = 0;

    private void InitRandomColor()
    {
        int RantInt = Random.Range(1, 5);
        if (RantInt == 1) RandomColor = Color.red;
        if (RantInt == 2) RandomColor = Color.black;
        if (RantInt == 3) RandomColor = Color.blue;
        if (RantInt == 4) RandomColor = Color.magenta;
    }

    GameObject Dot, VIC, MENU, AGAIN, NEXTLV, BACK, HAND;
    public Button Undo, Reset, Hint;
    public Text scoretext;
    bool UsingHint = false;
    private void Start()
    {
        VIC = GameObject.Find("VictoryNoti");
        MENU = GameObject.Find("Menu");
        AGAIN = GameObject.Find("PlayAgain");
        NEXTLV = GameObject.Find("NextLevel");
        BACK = GameObject.Find("back");
        HAND = GameObject.Find("Hand");

        BaseVic = VIC.transform.position;
        BaseMenu = MENU.transform.position;
        BaseAgain = AGAIN.transform.position;
        BaseNextLV = NEXTLV.transform.position;
        BaseBack = BACK.transform.position;
        BaseHand = HAND.transform.position;

        VIC.transform.position = new Vector3(BaseVic.x, BaseVic.y - 1000, BaseVic.z);
        MENU.transform.position = new Vector3(BaseMenu.x, BaseMenu.y - 1000, BaseMenu.z);
        AGAIN.transform.position = new Vector3(BaseAgain.x, BaseAgain.y - 1000, BaseAgain.z);
        NEXTLV.transform.position = new Vector3(BaseNextLV.x, BaseNextLV.y - 1000, BaseNextLV.z);

        //mainCamera.GetComponent<Camera>().backgroundColor = Color.white;
        if (!UsingHint) InitRandomColor();
        for (int i = 1; i <= n; i++)
            for (int j = 1; j <= n; j++)
                w[i, j] = 1;
        for (int i = 1; i <= n; i++)
        {
            for (int j = i; j <= n; j++)
            {
                C = string.Format("{0}{1}{2}", "Cap", i, j);
                if (GameObject.Find(C) != null)
                {
                    Basecap[i, j] = GameObject.Find(C).transform.position;
                    int tmp = int.Parse(GameObject.Find(C).GetComponent<Text>().text);
                    w[i, j] += tmp - 1;
                    w[j, i] += tmp - 1;
                    nedge += tmp - 1;
                }
                L = string.Format("{0}{1}{2}", "Line", i, j);
                if (GameObject.Find(L) == null) adj[i, j] = adj[j, i] = -1;
                else
                {
                    Arr = string.Format("{0}{1}{2}", "Arrow", i, j);
                    if (GameObject.Find(Arr) != null)
                    {
                        adj[j, i] = -1;
                        adj[i, j] = 0;
                        BaseArr[i, j] = GameObject.Find(Arr).transform.position;
                    }
                    else adj[i, j] = 0;
                    Arr = string.Format("{0}{1}{2}", "Arrow", j, i);
                    if (GameObject.Find(Arr) != null)
                    {
                        adj[i, j] = -1;
                        adj[j, i] = 0;
                        BaseArr[j, i] = GameObject.Find(Arr).transform.position;
                    }
                    else if (adj[j,i] != -1) adj[j, i] = 0;//TH co arrij thi adj[j,i] phai la -1
                    ColorFromTo(i, j, Color.grey);
                    m = m + 1;
                    nedge++;
                }
            }
            D = string.Format("{0}{1}", "Dot", i);
            Dot = GameObject.Find(D);
            Dot.GetComponent<SpriteRenderer>().color = RandomColor;
            ScaleBase[i] = Dot.transform.localScale;
            ScaleChange[i] = new Vector3(ScaleBase[i].x + 20, ScaleBase[i].y + 20, ScaleBase[i].z + 20);
        }
        for (int i = 1; i <= n; i++)
            for (int j = 1; j <= n; j++)
                Basew[i, j] = w[i, j];
        Undo.interactable = false;
        //Debug.Log(nedge);
    }

    private void ColorFromTo(int i, int j, Color ColorChange)
    {
        int st = i, en = j;
        if (st > en)
        {
            int temp = st;
            st = en;
            en = temp;
        }
        C = string.Format("{0}{1}{2}", "Cap", st, en);
        if (GameObject.Find(C) != null) GameObject.Find(C).GetComponent<Text>().text = w[st, en].ToString();
        L = string.Format("{0}{1}{2}", "Line", st, en);
        GameObject.Find(L).GetComponent<SpriteRenderer>().color = ColorChange;
    }

    private bool WinCheck()
    {
        for (int i = 1; i <= n; i++)
        {
            for (int j = i; j <= n; j++)
            {
                L = string.Format("{0}{1}{2}", "Line", i, j);
                if (GameObject.Find(L) != null)
                    if (GameObject.Find(L).GetComponent<SpriteRenderer>().color == Color.grey)
                        return false;
                C = string.Format("{0}{1}{2}", "Cap", i, j);
                if (GameObject.Find(C) != null)
                    if (GameObject.Find(C).GetComponent<Text>().text != "0")
                        return false;
            }
        }
        return true;
    }
    Vector3 pos;
    bool bigger = true;

    private void ScaleUpdate()
    {
        for (int i = 1; i <= n; i++)
        {
            D = string.Format("{0}{1}", "Dot", i);
            Dot = GameObject.Find(D);
            if (from == i)
            {
                if (ScaleChange[i].x - Dot.transform.localScale.x <= 1
                    && ScaleChange[i].y - Dot.transform.localScale.y <= 1) bigger = false;
                if (Dot.transform.localScale.x - ScaleBase[i].x <= 0.5
                    && Dot.transform.localScale.y - ScaleBase[i].y <= 0.5) bigger = true;
                if (bigger)
                {
                    Dot.transform.localScale = Vector3.Lerp(Dot.transform.localScale, ScaleChange[i], 8 * Time.deltaTime);
                }
                else
                {
                    Dot.transform.localScale = Vector3.Lerp(Dot.transform.localScale, ScaleBase[i], 8 * Time.deltaTime);
                }
            }
            else Dot.transform.localScale = ScaleBase[i];
        }
    }

    int CurDot = 1;
    double TimeStart = 1.0;
    private void HandMoving()
    {
        D = string.Format("{0}{1}", "Dot", ans[CurDot]);
        Dot = GameObject.Find(D);
        if (HAND.transform.position == BaseHand) HAND.transform.position = Dot.transform.position;
        D = string.Format("{0}{1}", "Dot", ans[CurDot % (nedge + 1) + 1]);
        Dot = GameObject.Find(D);
        HAND.transform.position = Vector3.MoveTowards(HAND.transform.position, Dot.transform.position, 3 * Time.deltaTime);
        float x1 = HAND.transform.position.x, y1 = HAND.transform.position.y, x2 = Dot.transform.position.x, y2 = Dot.transform.position.y;
        if ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) <= 0.1)
        {
            CurDot = CurDot % (nedge + 1) + 1;
            if (CurDot == nedge + 1)
            {
                HAND.transform.position = BaseHand;
                TimeStart = 0;
                CurDot = 1;
            }
            else
            {
                D = string.Format("{0}{1}", "Dot", ans[CurDot]);
                Dot = GameObject.Find(D);
                HAND.transform.position = Dot.transform.position;
            }
        }
    }

    double TimeWin = 0;
    private void Update()
    {
        ScaleUpdate();
        TimeStart = TimeStart + Time.deltaTime;
        if (UsingHint && TimeStart >= 1.0) HandMoving();

        if (WinCheck())
        {
            TimeWin = TimeWin + Time.deltaTime;
            if (TimeWin >= 0.5)
            {
                VIC.transform.position = BaseVic;
                MENU.transform.position = BaseMenu;
                AGAIN.transform.position = BaseAgain;
                NEXTLV.transform.position = BaseNextLV;
                HAND.transform.position = BaseHand;
                Undo.interactable = Reset.interactable = Hint.interactable = false;
                BACK.transform.position = new Vector3(BaseBack.x, BaseBack.y - 1000, BaseBack.z);
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                    {
                        Arr = string.Format("{0}{1}{2}", "Arrow", i, j);
                        if (GameObject.Find(Arr) != null)
                        {
                            GameObject.Find(Arr).transform.position = new Vector3(BaseArr[i, j].x, BaseArr[i, j].y - 1000, BaseArr[i, j].z);
                        }
                    }
                lvl.Pass();
            }
        }
        if (!Input.GetMouseButton(0) && Input.touchCount==0) return;
        for (int i = 1; i <= n; i++)
        {
            D = string.Format("{0}{1}", "Dot", i);
            if (Input.GetMouseButton(0)) pos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - GameObject.Find(D).transform.position;
            else if (Input.touchCount > 0) pos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position) - GameObject.Find(D).transform.position;
            if (pos.x * pos.x + pos.y * pos.y <= 0.2)
            {
                if (from == 0)
                {
                    Undo.interactable = true;
                    from = i;
                }
                else to = i;
                break;
            }
        }

        if (to != 0 && adj[from, to] == 0)
        {
            w[from, to]--;
            w[to, from]--;
            int u = from;
            int v = to;
            if (u>v)
            {
                int tmp = u;
                u = v;
                v = tmp;
            }
            C = string.Format("{0}{1}{2}", "Cap", u, v);
            if (w[from, to] == 0 && GameObject.Find(C) != null) GameObject.Find(C).transform.position = new Vector3(Basecap[u,v].x, Basecap[u,v].y - 1000, Basecap[u,v].z);
            ColorFromTo(from, to, RandomColor);
            if (w[from, to] == 0)
            {
                if (adj[from, to] != -1) adj[from, to] = 1;
                if (adj[to, from] != -1) adj[to, from] = 1;
            }
            for (int j = 0; j < 100; j++)
            {
                if (pre[to, j] == 0)
                {
                    pre[to, j] = from;
                    break;
                }
            }

            //diem ket thuc tro thanh diem bat dau moi
            from = to;
            to = 0;
        }
        else to= 0; //diem bat dau van giu nguyen, reset diem ket t
       }

    public void BtnReset()
    {
        VIC.transform.position = BaseVic;
        MENU.transform.position = BaseMenu;
        AGAIN.transform.position = BaseAgain;
        NEXTLV.transform.position = BaseNextLV;
        HAND.transform.position = BaseHand;
        BACK.transform.position = BaseBack;
        Reset.interactable = true;
        for (int i = 1; i <= n; i++)
        {
            for (int j = i; j <= n; j++)
            {
                C = string.Format("{0}{1}{2}", "Cap", i, j);
                if (GameObject.Find(C) != null)
                {
                    GameObject.Find(C).GetComponent<Text>().text = Basew[i, j].ToString();
                    GameObject.Find(C).transform.position = Basecap[i, j];
                }
            }
            for (int j = 1; j <= n; j++)
            {
                Arr = string.Format("{0}{1}{2}", "Arrow", i, j);
                if (GameObject.Find(Arr) != null)
                {
                    GameObject.Find(Arr).transform.position = BaseArr[i, j];
                }
            }
            for (int j = 0; j < 100; j++) pre[i, j] = 0;
            D = string.Format("{0}{1}", "Dot", i);
            Dot = GameObject.Find(D);
            Dot.transform.localScale = ScaleBase[i];
        }
        nedge = m = from = to = 0;
        Start();
        UsingHint = false;
        Hint.interactable = true;
    }

    int prev;
    public void BtnUndo()
    {
        if (pre[from, 0] == 0) prev = 0;
        else
        {
            for (int i = 99; i >= 0; i--)
            {
                if (pre[from, i] != 0)
                {
                    prev = pre[from, i];
                    pre[from, i] = 0;
                    break;
                }
            }
        }
        if (prev != 0)
        {
            if (adj[prev, from] != -1) adj[prev, from] = 0;
            if (adj[from, prev] != -1) adj[from, prev] = 0;
            int u = prev;
            int v = from;
            if (u > v)
            {
                int tmp = u;
                u = v;
                v = tmp;
            }
            C = string.Format("{0}{1}{2}", "Cap", u, v);
            if (w[prev, from] == 0 && GameObject.Find(C) != null) GameObject.Find(C).transform.position = Basecap[u, v];
            w[prev, from]++;
            w[from, prev]++;
            if (w[prev, from] == Basew[prev, from]) ColorFromTo(prev, from, Color.grey);
            else ColorFromTo(prev, from, RandomColor);
        }
        from = prev;
        if (from == 0) Undo.interactable = false;
    }

    bool[] vis = new bool[100];
    int it = 0, it2 = 0;
    int St = 1;
    void dfs(int u)
    {
        for (int i = 1; i <= n; i++)
        {
            int t = adj[u, i];
            if (t != -1 && !vis[t])
            {
                w[u, i]--;
                w[i, u]--;
                if (w[u,i] == 0) vis[t] = vis[2 * m - t + 1] = true;
                dfs(i);
            }
        }
        it2 = it2 + 1;
        ans[it2] = u;
    }

    public void BtnHint()
    {
        if (CoinCounter.score < difficulty) return;
        CoinCounter.score -= difficulty;
        scoretext.text = CoinCounter.score.ToString();
        PlayerPrefs.SetInt("score", CoinCounter.score);
        for (int i = 1; i <= m * 2; i++) vis[i] = false;
        for (int i = 1; i <= n; i++)
        {
            int cnt = 0;
            for (int j = 1; j <= n; j++)
            {
                int u = i;
                int v = j;
                if(u > v)
                {
                    int tmp = u;
                    u = v;
                    v = tmp;
                }
                L = string.Format("{0}{1}{2}", "Line", u, v);
      
                if (GameObject.Find(L) != null)
                {
                    if (i > j)
                    { 
                        cnt+=Basew[i,j];
                        continue;
                    }
                    it = it + 1;
                    adj[i, j] = it;
                    adj[j, i] = 2 * m - it + 1;
                    cnt+=Basew[i,j];
                }
            }
            //Debug.Log("so bac cua dinh" + i + " " + cnt);
            if (cnt % 2 != 0) St = i;
        }
        //Debug.Log("St" + St);
        for (int i = 1; i <= n; i++)
            for (int j = 1; j <= n; j++)
                w[i, j] = Basew[i, j];
        dfs(St);
        //Debug.Log(St);
        //for (int i = 1; i <= nedge + 1; i++) Debug.Log(ans[i]);
        UsingHint = true;
        BtnReset();
        Hint.interactable = false;
        UsingHint = true;
        /*
        Debug.Log(m);
        for (int i = 1; i <= m; i++) Debug.Log(ans[i]);*/
    }
}
