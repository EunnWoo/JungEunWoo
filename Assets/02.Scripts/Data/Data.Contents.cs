using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stat
[Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int attack;
}
[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();
    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

        foreach (Stat stat in stats)
            dict.Add(stat.level, stat);
        return dict;
    }


}
#endregion

public class TalkMessage
{
    public int id;
    public string message; 
        
}
public class TalkData : ILoader<int,TalkMessage>
{

    public List<TalkMessage> talks = new List<TalkMessage>();
    public Dictionary<int, TalkMessage> MakeDict()
    {
        Dictionary<int, TalkMessage> dict = new Dictionary<int, TalkMessage>();

        foreach (TalkMessage talk in talks)
            dict.Add(talk.id, talk);
        return dict;

    }
}