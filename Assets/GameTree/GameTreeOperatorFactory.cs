using Assets.GameTree.Functions;
using Assets.GameTree.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameTree
{
    public class GameTreeOperatorFactory
    {
        public static GameTreeOperator InstantiateOperator(GameTreeEngine _engine, string _typeName)
        {
            if (_typeName == "ParallelAnd")
                return new ParallelAnd(_engine);
            else if (_typeName == "ParallelOr")
                return new ParallelOr(_engine);
            else if (_typeName == "TimerFunction")
                return new TimerFunction(_engine);

            return null;
        }
    }
}
