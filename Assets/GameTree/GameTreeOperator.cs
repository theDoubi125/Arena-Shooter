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

        public bool easier;
        public bool harder;

        private GameOperatorState m_state;
        public GameOperatorState State
        {
            get { return m_state; }
            protected set
            {
                m_state = value;
                harder = (m_state == GameOperatorState.SUCCESS);
                easier = (m_state == GameOperatorState.FAILURE);
            }
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

        public GameTreeOperator(GameTreeOperator _operator)
        {
            m_Engine = _operator.m_Engine;
            State = GameOperatorState.INDETERMINATE;
            Name = "Generated";
        }

        // Called when the tree is instantiated
        public virtual void Setup()
        {
            m_Childs = m_Engine.GetChildsOfOperator(this);
            Active = false;
            State = GameOperatorState.INDETERMINATE;

            foreach(var child in m_Childs)
            {
                child.Setup();
            }
        }

        public virtual void Activate()
        {
            Active = true;
        }

        public virtual void Update()
        {
            
        }

        protected virtual void Harder()
        {
            harder = false;
        }

        protected virtual void Easier()
        {
            easier = false;
        }

        public void Evolve()
        {
            if (easier)
                Easier();
            else if (harder)
                Harder();
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
