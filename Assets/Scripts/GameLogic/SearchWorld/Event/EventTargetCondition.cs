using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EventTargetCondition : MonoBehaviour
{
    //TODO: CalcOperatorにCustomを追加し，複雑な条件式を入力できるようにする
    private enum CalcOperator
    {
        AND,
        OR
    }
    [SerializeField] private UnityEvent OnConditionMet;
    [SerializeField] private List<Flag> flags;
    [SerializeField] private CalcOperator calcOperator = CalcOperator.AND;

    public void OnEventTriggered()
    {
        Logger.Log($"{gameObject.name}: OnEventTriggered()");
        if (IsConditionMeet())
        {
            OnConditionMet.Invoke();
        }
        else
        {
            Logger.Log("このイベントは発生条件を満たしていません．");
            //TODO: どのフラグが阻害しているか出力
        }
    }

    private bool IsConditionMeet()
    {
        if (IsEventActive())
        {
            if (flags.Count == 0)
            {
                return true;
            }
            else
            {
                if (calcOperator == CalcOperator.AND)
                {
                    if (flags.All(f => IsFlagMeet(f)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else /*if (calcOperator == CalcOperator.OR)*/
                {
                    if (flags.Any(f => IsFlagMeet(f)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            Logger.Log($"{gameObject.name}のイベントは現在有効ではありません．");
            return false;
        }
    }
    private bool IsFlagMeet(Flag flag)
    {
        return flag.Value.Equals(FlagManager.Instance.Get(flag.Key));
    }
    private bool IsEventActive()
    {
        return SearchWorldManager.Instance.CurrentViewPoint.ActiveEvents.Contains(this);
    }
}