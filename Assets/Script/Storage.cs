
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Storage : Singleton<Storage>
{
    private bool KeyPressed = false;
    private long Repeat = 1;
    private bool GameOver = false;
    private List<String> OriginalSequence = new List<String>();
    private int score = 0;
    
    protected Storage() { }


    public void setScore(int s)
    {
        this.score = s;
    }

    public int getScore()
    {
        return score;
    }
    public void Clear()
    {
        KeyPressed = false;
        Repeat = 1;
        score = 0;
        GameOver = false;
        OriginalSequence = new List<string>();
    }

    public List<String> GetOriginalSequence()
    {
        return OriginalSequence;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void LockKeyPressed()
    {
        KeyPressed = false;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UnlockKeyPressed()
    {
        KeyPressed = true;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool IsAllowKeyPressed()
    {
        return KeyPressed;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public long GetRepeat()
    {
        return Repeat;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public long IncrementRepeat()
    {
        return Repeat++;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateGameOverState(bool state)
    {
        GameOver = state;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool GetGameOverState()
    {
        return GameOver;
    }
}