using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.GameTree
{
    public class GameTreeEngine : MonoBehaviour
    {
		public GameManager gameManager = null;
		
        GameTree m_CurrentTree = null;
        Dictionary<string, GameTree> m_Trees = new Dictionary<string, GameTree>();

        [SerializeField]
        private string m_File = "";

        public bool m_DebugMode = true;

        public int nbBasicEnemy = 0;
        public int nbShooterEnemy = 0;

        void Start()
        {
            Load(m_File);
            SwitchTree("ComplexTree");
        }

        void Update()
        {
            if (gameManager != null && gameManager.uiManager.isVisible())
            {
                if (Input.GetButtonDown("Submit"))
                {
                    gameManager.Reset();
                    var elems = m_CurrentTree.GetNodes().ToArray();
                    foreach (var elem in elems)
                    {
                        elem.Operator.Evolve();
                    }
                    SwitchTree("ComplexTree");
                }
            }
            else if(m_CurrentTree != null)
			{
                m_CurrentTree.Update();
			
				if(gameManager != null)
				{
					if(m_CurrentTree.GetRootOperator().State == GameTreeOperator.GameOperatorState.SUCCESS)
						gameManager.WaveWon(true);
					else if(m_CurrentTree.GetRootOperator().State == GameTreeOperator.GameOperatorState.FAILURE)
						gameManager.WaveWon(false);					
				}
				
			}
        }

        void SwitchTree(string _tree)
        {
            m_CurrentTree = m_Trees[_tree];
            m_CurrentTree.Reset();
            m_CurrentTree.Start();
            UpdateNbEnnemy();
        }

        public IEnumerable<GameTreeOperator> GetChildsOfOperator(GameTreeOperator _operator)
        {
            foreach(var elem in m_CurrentTree.GetNodes())
            {
                if(elem.Operator == _operator)
                {
                    return elem.Childs.Select<GameTreeElement, GameTreeOperator>(e => e.Operator);
                }
            }

            return null;
        }

        public GameTreeElement GetTreeElementOfOperator(GameTreeOperator _operator)
        {
            foreach (var elem in m_CurrentTree.GetNodes())
            {
                if (elem.Operator == _operator)
                {
                    return elem;
                }
            }

            return null;
        }

        void UpdateNbEnnemy()
        {
            nbBasicEnemy = 0;
            nbShooterEnemy = 0;
            foreach (var elem in m_CurrentTree.GetNodes())
            {
                Functions.SpawnEnemyFunction op = elem.Operator as Functions.SpawnEnemyFunction;
                if (op != null)
                {
                    if (op.value == "ShooterEnemy")
                    {
                        ++nbShooterEnemy;
                    }
                    else if (op.value == "BasicEnemy")
                    {
                        ++nbBasicEnemy;
                    }
                }
            }

            nbShooterEnemy /= 2;
            nbBasicEnemy /= 2;
        }

        public void OnGUI()
        {
            if(m_DebugMode && m_CurrentTree != null)
            {
                GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Tree: " + m_CurrentTree.Name);
                DebugDrawOperator(m_CurrentTree.GetRootOperator(), 0);
                GUILayout.EndArea();
            }
        }

        // ONLY CALL IN ONGUI, Draws an operator
        public void DebugDrawOperator(GameTreeOperator op, int tab)
        {
            if (op == null)
                return;

            // Setting display color
            if(op.Active)
            {
                switch(op.State)
                {
                    case GameTreeOperator.GameOperatorState.FAILURE:
                        GUI.color = Color.red;
                        break;
                    case GameTreeOperator.GameOperatorState.SUCCESS:
                        GUI.color = Color.green;
                        break;
                    case GameTreeOperator.GameOperatorState.INDETERMINATE:
                        GUI.color = Color.yellow;
                        break;
                    default:
                        GUI.color = Color.grey;
                        break;
                }
            }
            else
            {
                GUI.color = Color.grey;
            }

            string txt = "";
            for (int i = 0; i < tab; ++i)
                txt += '\t';
            txt += op.ToString();

            GUILayout.Label(txt);
            if(op.Childs != null)
            {
                foreach(var child in op.Childs)
                {
                    DebugDrawOperator(child, tab + 1);
                }

            }
        }

        #region Loading
        public void Load(string _path)
        {
            XmlDocument document = new XmlDocument();
            document.Load(_path);
            foreach(XmlNode node in document.DocumentElement.ChildNodes)
            {
                if(node.Name == "GameTree")
                {
                    var tree = LoadGameTree(node);
                    if (tree != null)
                        m_Trees.Add(tree.Name, tree);
                }
            }
        }

        public GameTree LoadGameTree(XmlNode _node)
        {
            string name = _node.Attributes["name"].Value;
            string difficulty = _node.Attributes["difficulty"].Value;

            GameTree tree = new GameTree(name, int.Parse(difficulty));
            GameTreeElement root = LoadGameTreeElement(_node.FirstChild);
            if(root != null)
            {
                tree.SetRoot(root);
                return tree;
            }
            else
            {
                return null;
            }
        }

        public GameTreeElement LoadGameTreeElement(XmlNode _node)
        {
            string name = _node.Attributes["name"].Value;
            string type = _node.Attributes["type"].Value;

            GameTreeOperator op = GameTreeOperatorFactory.InstantiateOperator(this, type);
            op.Name = name;
            if (op == null)
                return null;

            GameTreeElement elem = new GameTreeElement(op);
            foreach(XmlNode child in _node.ChildNodes)
            {
                if(child.Name == "attribute")
                {
                    op.ParseAttribute(child.Attributes["name"].Value, child.Attributes["value"].Value);
                }
                else if(child.Name == "childs")
                {
                    foreach(XmlNode element in child.ChildNodes)
                    {
                        if (element.Name == "GameTreeElement")
                        {
                            var childElem = LoadGameTreeElement(element);
                            if(childElem != null)
                            {
                                elem.Add(childElem);
                                childElem.SetFather(elem);
                            }
                        }
                    }
                }
            }

            return elem;
        }
        #endregion

    }
}
