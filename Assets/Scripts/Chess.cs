using UnityEngine;
using System.Collections;
using System;

public class Chess:MonoBehaviour{
    public enum ChessStatus  
    {  
        NONE    = 1 << 0,  
        BLACK   = 1 << 1,  
        WHITE   = 1 << 2,  
    }  
    public int posX;
    public int posY;

    public ChessStatus chessStatus =  ChessStatus.NONE;
}