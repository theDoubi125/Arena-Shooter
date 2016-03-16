using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.GameTree
{
    public abstract class GameTreeOperator
    {
        public enum GameOperatorState
        {
            INDETERMINATE,
            SUCCESS,
            FAILURE,
        }

        private GameOperatorState m_state;
        public GameOperatorState State
        {
            get { return m_state; }
            protected set { m_state = value; }
        }


        public string Name { get; set; }
        public bool Active { get; protected set; }
        protected IEnumerable<GameTreeOperator> m_Childs;
        protected GameTreeEngine m_Engine = null;

        public IEnumerable<GameTreeOperator> Childs
        {
            get { return m_Childs; }
        }

        public GameTreeOperator(GameTreeEngine _engine)
        {
            m_Engine = _engine;
            State = GameOperatorState.INDETERMINATE;
            Name = "Default";
        }

        // Called when the tree is instantiated
        public virtual void Setup()
        {
            m_Childs = m_Engine.GetChildsOfOperator(this);
        }

        public virtual void Activate()
        {
            Active = true;
        }

        public virtual void Update()
        {

        }

        public virtual void ParseAttribute(string _name, string _value)
        {

        }

        public override string ToString()
        {
            return Name + " is in " + State + " state.";
        }
    }
}
