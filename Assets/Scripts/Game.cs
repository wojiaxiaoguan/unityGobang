using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game :MonoBehaviour{
    public GameObject chessPrefab;
    public const int MAX_LENGTH = 15; 
    private Image[] chessList;

    private Image touchChess;

    public Sprite whiteSpriteFrame;
    public Sprite blackSpriteFrame;
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
            chessList = new GameObject[MAX_LENGTH*MAX_LENGTH];
            for (int x = 0; x < MAX_LENGTH; x++)
            {
                for (int y = 0; y < MAX_LENGTH; y++)
                {
                    GameObject chess =  (GameObject)Instantiate(chessPrefab, transform.position, transform.rotation);
                    if (chess != null)
                    {
                        chess.GetComponent<Transform>().SetParent(ChessBoard.GetComponent<Transform>(),true);
                        Debug.Log(GameObject.Find("ChessBoard").transform.position);
                        Vector3 pos = new Vector3(x*30  -245 + 35,y*30-245+35 );
                        chess.transform.position = pos +parentPos;
                        EventTriggerListener.Get(chess.gameObject).onClick = OnChessClick;   
                        chess.GetComponent<Chess>().posX = x;
                        chess.GetComponent<Chess>().posY = y;
                        chess.GetComponent<Image>().overrideSprite = whiteSpriteFrame;
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
        gameState = GameState.BLACK;
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
                touchChess.overrideSprite = blackSpriteFrame; //下子后添加棋子图片使棋子显示;
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

    }

    IEnumerator Ai()
    {
        yield return new WaitForSeconds(1);    //延迟一秒电脑下棋
    }
}