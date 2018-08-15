using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OldSnakey
{
    enum Direction
    { 
        Up,Right,Down,Left
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int s,rows,cols,w,h;
        List<Node> Snake;
        Random rnd;
        Graphics g;
        Direction dir;
        Node food;



        private void Form1_Load(object sender, EventArgs e)
        {
            g = pan.CreateGraphics();
            s = 20;
            w = pan.Width;
            h = pan.Height;
            rows = h / s;
            cols = w / s;
            Snake = new List<Node>();
            rnd = new Random();
            //贪吃蛇的头
            Node head = new Node(rnd.Next(5, cols - 5), rnd.Next(5, rows - 5));
            head.color = Color.Purple;
            Snake.Add(head);

            //随机产生方向
            dir = Direction.Up;


            //贪吃蛇的3节身体
            for (int i = 0; i < 3; i++)
            {
                Node tail = Snake.Last();
                Node n = new Node(tail.x, tail.y);
                switch (dir)
                { 
                    case Direction.Up:
                        n.y++;
                        break;
                    case Direction.Right:
                        n.x++;
                        break;
                    case Direction.Down:
                        n.y--;
                        break;
                    case Direction.Left:
                        n.x++;;
                        break;
                    default:
                        break;


                }
                Snake.Add(n);
            }
            
            //随机产生一个食物
            GenerateFood();
            
            }

            private void GenerateFood()
            {
                bool overlap = true;
                while (overlap)
                {
                    overlap = false;
                    food = new Node(rnd.Next(0, cols), rnd.Next(0, rows));
                    foreach (Node n in Snake)
                    {
                        if (n.IsOverLap(food))
                        {
                            overlap = true;
                            break;
                        }
                    }
                    food.color = Color.Green;
                    DrawNode(food);
            }

        }

        private void DrawNode(Node head)
        {

            Brush b = new SolidBrush(head.color);
            g.FillRectangle(b, head.x * s+1, head.y * s+1,s-1,s-1);
        }

        private void pan_Paint(object sender, PaintEventArgs e)
        {
           
            Pen pen = new Pen(Color.Orange, 1);
            for (int i = 0; i <= rows; i++)
            {
                g.DrawLine(pen,0,i*s,w,i*s);
            }
            for (int i = 0; i <= cols; i++)
            {
                g.DrawLine(pen,i*s,0,i*s,h);
            }

            foreach (Node n in Snake)
            {

                DrawNode(n);
                
            }
            DrawNode(food);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveSnake();
        }

        private void MoveSnake()
        {
            Node head = Snake.First();
            Node tail = Snake.Last();
            Node newHead = new Node(head.x, head.y);
            newHead.color = Color.Black;

            switch(dir)
            {
                case Direction.Up:
                    newHead.y--;
                    break;
                case Direction.Right:
                    newHead.x++;
                    break;
                case Direction.Down:
                    newHead.y++;
                    break;
                case Direction.Left:
                    newHead.x--;
                    break;
                default:
                    break;
            }

            //判断游戏是否结束
            if (newHead.x < 0 || newHead.y < 0 || newHead.x > cols || newHead.y > rows)
            {
                GameOver();
                return;

            }
            foreach (Node n in Snake)
            {
                if (n.IsOverLap(newHead))
                {
                    GameOver();
                    return;
                }
            }
            Snake.Insert(0, newHead);
            DrawNode(newHead);
            head.color = Color.Red;
            DrawNode(head);
            //判断是否吃到食物
            if (newHead.IsOverLap(food))
            {
                GenerateFood();
                timer1.Interval = 300-(Snake.Count-4)*10;

            }
            else 
            {
                tail.color = pan.BackColor;
                Snake.Remove(tail);
                DrawNode(tail);
            }

        }

        private void GameOver()
        {
            timer1.Enabled = false;
            g.DrawString("游戏结束",new Font("宋体",50),new SolidBrush(Color.Purple),w/2-150,h/2-40);
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) 
            { 
                case Keys.Up:
                    dir = Direction.Up;
                    break;
                case Keys.Right:
                     dir = Direction.Right;
                   break;
                case Keys.Down:
                    dir = Direction.Down;
                    break;
                case Keys.Left:
                     dir = Direction.Left;
                   break;
                default:
                    break;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
