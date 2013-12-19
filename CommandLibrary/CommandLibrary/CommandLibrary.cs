using Alchemy.Classes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract public class CommandLibrary
{
    protected ConcurrentDictionary<string, Delegate> CommandDic = new ConcurrentDictionary<string, Delegate>();
    public delegate void cmdDelegate(ref UserContext aContext);

    //=====[H1=0x40 :게임 준비 관련]=====
    private readonly byte[] cmdReadyCancel = { 0x40, 0x00, 0x00, 0x00, 0x00 };    //대전상대 찾기 취소
    abstract protected void cmdF_ReadyCancel(ref UserContext aContext);   //대전상대 찾기 취소 함수

    private readonly byte[] cmdReadyFind = { 0x40, 0x20, 0x00, 0x00, 0x00 };    //대전상대 찾기 신청
    abstract protected void cmdF_ReadyFind(ref UserContext aContext);     //대전상대 찾기 신청 함수


    public CommandLibrary()
    {
        CommandDic.TryAdd(makeKeyForCommand(ref cmdReadyCancel), new cmdDelegate(cmdF_ReadyCancel));    //대전상대 찾기 취소
        CommandDic.TryAdd(makeKeyForCommand(ref cmdReadyFind), new cmdDelegate(cmdF_ReadyFind));        //대전상대 찾기 신청
    }
    public string makeKeyForCommand(ref byte[] cmd)
    {
        StringBuilder result = new StringBuilder();
        foreach (var item in cmd)
        {
            result.Append(item.ToString());
        }
        return result.ToString();
    }
}