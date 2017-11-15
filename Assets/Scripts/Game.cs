using UnityEngine;
using System.Collections;
using  System.Collections.Generic;  // List 需要的依赖
using UnityEngine.UI;

public class Game :MonoBehaviour{
    public GameObject chessPrefab;
    public const int MAX_LENGTH = 15; 
    private Image[] chessList;

    private Image touchChess;

    public Sprite whiteSpriteFrame;
    public Sprite blackSpriteFrame;

    private List<int[]> fiveGroup = new List<int[]>();  
    private int[] fiveGroupScore; 

    public enum GameState  
    {  
        NONE    = 1 << 0,  
        BLACK   = 1 << 1,  
        WHITE   = 1 << 2,  
        OVER    = 1 << 3 
    }  

    private GameState gameState = GameState.NONE;
    
    void Start(){
        initChessBoard();
        StartGame();
    }

    private void initChessBoard(){
        /* 资源需要房子啊Resources 目录下 */
        // GameObject resObject =(GameObject)Instantiate(Resources.Load("Cubeprefab"), transform.position, transform.rotation);
        GameObject ChessBoard = GameObject.Find("ChessBoard");
        if (ChessBoard != null )
        {
            Vector3 parentPos = ChessBoard.transform.position;
            chessList = new Image[MAX_LENGTH*MAX_LENGTH];
            for (int x = 0; x < MAX_LENGTH; x++)
            {
                for (int y = 0; y < MAX_LENGTH; y++)
                {
                    GameObject chess =  (GameObject)Instantiate(chessPrefab, transform.position, transform.rotation);
                    if (chess != null)
                    {
                        chess.GetComponent<Transform>().SetParent(ChessBoard.GetComponent<Transform>(),true);
                        Vector3 pos = new Vector3(x*30  -245 + 35,y*30-245+35 );
                        chess.transform.position = pos +parentPos;
                        EventTriggerListener.Get(chess.gameObject).onClick = OnChessClick;   
                        chess.GetComponent<Chess>().posX = x;
                        chess.GetComponent<Chess>().posY = y;
                        // chess.GetComponent<Image>().overrideSprite = whiteSpriteFrame;
                        chessList[x*MAX_LENGTH + y] = chess.GetComponent<Image>();
                    } 
                    else
                    {
                        Debug.Log("~~~~~~Error : chess load failed~~~~");
                        return ;
                    }
                }
            }
        }
    }

    private void StartGame(){
        //开局白棋（电脑）在棋盘中央下一子
        chessList[112].overrideSprite = whiteSpriteFrame;
        chessList[112].GetComponent<Chess>().chessStatus = Chess.ChessStatus.WHITE;
        gameState = GameState.BLACK;

        //添加五元数组
        //横向
        for (int y = 0; y < MAX_LENGTH; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                fiveGroup.Add(new   int []{y*15+x,y*15+x+1,y*15+x+2,y*15+x+3,y*15+x+4});
            }
        }


        //纵向
        for (int x = 0; x < MAX_LENGTH; x++)
        {
            for (int y = 0; y < 11; y++)
            {
                fiveGroup.Add(new   int []{y*15+x,(y+1)*15+x,(y+2)*15+x,(y+3)*15+x,(y+4)*15+x});
            }
        }

        //右上斜向
        for(int b=-10;b<=10;b++){
            for(int x=0;x<11;x++){
                if(b+x<0||b+x>10){
                    continue;
                }else{
                    fiveGroup.Add(new   int []{(b+x)*15+x,(b+x+1)*15+x+1,(b+x+2)*15+x+2,(b+x+3)*15+x+3,(b+x+4)*15+x+4});
                }
            }
        }

        //右下斜向
        for(int b=4;b<=24;b++){
            for(int y=0;y<11;y++){
                if(b-y<4||b-y>14){
                    continue;
                }else{
                    fiveGroup.Add(new   int []{y*15+b-y,(y+1)*15+b-y-1,(y+2)*15+b-y-2,(y+3)*15+b-y-3,(y+4)*15+b-y-4});
                }
            }
        }
    }

    private void OnChessClick(GameObject obj){
        Image temp = obj.GetComponent<Image>();
        if (temp)
        {
            touchChess = temp;
            Chess chess = touchChess.GetComponent<Chess>();
            int posX = chess.posX;
            int posY = chess.posY;
            
            if (gameState == GameState.BLACK && chess.chessStatus == Chess.ChessStatus.NONE )
            {
                Debug.Log("blackSpriteFrame");
                touchChess.overrideSprite = blackSpriteFrame; //下子后添加棋子图片使棋子显示;
                chess.chessStatus = Chess.ChessStatus.BLACK;
                judgeOver();
                if (gameState == GameState.WHITE)
                {
                    StartCoroutine(Ai());
                }
            }
            Debug.Log("OnChessClick" + posX + "    "+ posY);
        }
        

        // if(self.gameState ===  'black' && this.getComponent(cc.Sprite).spriteFrame === null){
        //                 this.getComponent(cc.Sprite).spriteFrame = self.blackSpriteFrame;//下子后添加棋子图片使棋子显示
        //                 self.judgeOver();
        //                 if(self.gameState == 'white'){
        //                     self.scheduleOnce(function(){self.ai()},1);//延迟一秒电脑下棋
        //                 }
        //             }
        
    }

    private void judgeOver()
    {
        int x0 = touchChess.GetComponent<Chess>().posX;
        int y0 = touchChess.GetComponent<Chess>().posY;

        // 判断横向
        int fiveCount  = 0;
        for (int x = 0; x < MAX_LENGTH; x++)
        {
            if (chessList[y0 *MAX_LENGTH + x].GetComponent<Chess>().chessStatus == touchChess.GetComponent<Chess>().chessStatus)
            {
                fiveCount ++;
                if (judgeOver(fiveCount)){return ;}
            }
            else
            {
                fiveCount=0;
            }
        }

        // 判断纵向
        fiveCount  = 0;
        for (int y = 0; y < MAX_LENGTH; y++)
        {
            if (chessList[y0 *MAX_LENGTH + x0].GetComponent<Chess>().chessStatus == touchChess.GetComponent<Chess>().chessStatus)
            {
                fiveCount ++;
                if (judgeOver(fiveCount)){return ;}
            }
            else
            {
                fiveCount=0;
            }
        }

        fiveCount = 0;
        int f = y0 - x0;
        for (int x = 0; x < MAX_LENGTH; x++)
        {
            if(f+x < 0 || f+x > MAX_LENGTH - 1){
                continue;
            }
            if (chessList[(f+x) *MAX_LENGTH + x].GetComponent<Chess>().chessStatus == touchChess.GetComponent<Chess>().chessStatus)
            {
                fiveCount ++;
                if (judgeOver(fiveCount)){return ;}
            }
            else
            {
                fiveCount=0;
            }
        }

        fiveCount = 0;
        f = y0 + x0;
        for (int x = 0; x < MAX_LENGTH; x++)
        {
            if(f+x < 0 || f+x > MAX_LENGTH - 1){
                continue;
            }
            if (chessList[(f-x) *MAX_LENGTH + x].GetComponent<Chess>().chessStatus == touchChess.GetComponent<Chess>().chessStatus)
            {
                fiveCount ++;
                if (judgeOver(fiveCount)){return ;}
            }
            else
            {
                fiveCount=0;
            }
        }

        if (gameState == GameState.BLACK)
        {
            gameState = GameState.WHITE;
        }
        else
        {
            gameState = GameState.BLACK;
        }
    }

    IEnumerator Ai()
    {
        yield return new WaitForSeconds(1);    //延迟一秒电脑下棋
        Debug.Log("Ai");
        fiveGroupScore = new int[fiveGroup.Count];
        for (int i = 0; i < fiveGroup.Count; i++)
        {
            int b = 0;
            int w = 0;
            for (int j = 0; j < 5; j++)
            {
                if (chessList[fiveGroup[i][j]].GetComponent<Chess>().chessStatus == Chess.ChessStatus.BLACK)
                    b++;
                else if(chessList[fiveGroup[i][j]].GetComponent<Chess>().chessStatus == Chess.ChessStatus.WHITE)
                    w++;
            }

            if(b+w==0){
                fiveGroupScore[i] = 7;
            }else if(b>0&&w>0){
                fiveGroupScore[i] = 0;
            }else if(b==0&&w==1){
                fiveGroupScore[i] = 35;
            }else if(b==0&&w==2){
                fiveGroupScore[i] = 800;
            }else if(b==0&&w==3){
                fiveGroupScore[i] = 15000;
            }else if(b==0&&w==4){
                fiveGroupScore[i] = 800000;
            }else if(w==0&&b==1){
                fiveGroupScore[i] = 15;
            }else if(w==0&&b==2){
                fiveGroupScore[i] = 400;
            }else if(w==0&&b==3){
                fiveGroupScore[i] = 1800;
            }else if(w==0&&b==4){
                fiveGroupScore[i] = 100000;
            }

           
        }
         //找最高分的五元组
        int hScore=0;
        int mPosition=0;
        for(int i=0;i<fiveGroup.Count ;i++){
            if(fiveGroupScore[i]>hScore){
                hScore = fiveGroupScore[i];
                mPosition = i;
            }
        }

        //在最高分的五元组里找到最优下子位置
        bool flag1 = false;//无子
        bool flag2 = false;//有子
        int nPosition = 0;
        for(var i=0;i<5;i++){
            if(!flag1&&chessList[fiveGroup[mPosition][i]].GetComponent<Chess>().chessStatus == Chess.ChessStatus.NONE){
                nPosition = i;
            }
            if(!flag2&&chessList[fiveGroup[mPosition][i]].GetComponent<Chess>().chessStatus != Chess.ChessStatus.NONE){
                flag1 = true;
                flag2 = true;
            }
            if(flag2&&chessList[fiveGroup[mPosition][i]].GetComponent<Chess>().chessStatus == Chess.ChessStatus.NONE){
                nPosition = i;
                break;
            }
        }
        //在最最优位置下子
        chessList[fiveGroup[mPosition][nPosition]].overrideSprite = whiteSpriteFrame;
        touchChess = chessList[fiveGroup[mPosition][nPosition]];
        touchChess.GetComponent<Chess>().chessStatus = Chess.ChessStatus.WHITE;
        judgeOver();
    }

    private bool judgeOver(int count)
    {
        if (count == 5)
        {
            if (gameState == GameState.BLACK)
            {
                Debug.Log("你赢了");
            }
            else
            {
                Debug.Log("你输了");
            }
            gameState = GameState.OVER;
            return true;
        }
        return false;
    }
}