using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace SnakeMaxApp
{
    class Program
    {
        private bool gameOver;
        const int width = 40;
        const int height = 20;
        public int x, y, fruitX, fruitY, score;
        public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        int[] tailX = new int[100];
        int[] tailY = new int[100];
        int ntail;
        int prevX, prevY;
        string dir, pre_dir;
        bool reset, isprinted, horizontal, vertical;

       

//        enum eDirection
//        {
//            STOP = 0,
//            LEFT,
//            RIGHT,
//            UP,
//            DOWN
//        };

        
        public Random rand = new Random();

        static void Main(string[] args)
        {
            Program program = new Program();
        }

        public Program()
        {
            Setup();
            while (!gameOver)
            {
                Draw();
                Input();
                Logic();
                Thread.Sleep(100);
            }
        }


        public void Setup()
        {
            gameOver = false;
            dir = " ";
            x = width / 2 - 1;
            y = height / 2 - 1;

            fruitX = rand.Next(1, width - 1);
            fruitY = rand.Next(1, height - 1);
            score = 0;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);

     

            for (int j = 0; j < width + 1; j++)
            {
                Console.Write("#");
            }

            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 0; j < width - 1; j++)
                {
                    if (j == 0 || j == width)
                    {
                        Console.Write("U"); // левая сторона
                    }

                    if (i == y && j == x)
                    {
                        Console.Write("O");
                    }
                    else if (i == fruitY && j == fruitX)
                    {
                        Console.Write("F");
                    }
                    else
                    {
                        bool print = false;
                        for (int k = 0; k < ntail; k++)
                        {
                            if (tailX[k] == j && tailY[k] == i) {
                                print = true;
                            Console.Write("o");
                        }
                    }
                    if (!print)
                    Console.Write(" ");
                    }
                }

                Console.WriteLine("R");
            }

            for (int j = 0; j < width + 1; j++)
            {
                Console.Write("#");
                
            }
            Console.WriteLine(" ");
            Console.Write("Score : " + score);


            
        }

        void Input()
        {
            // массив_нажатых_клавишь
            // Console.ReadKey(true) достаёт нажатую клавишу из начала массива нажатых клавишь
            // Console.KeyAvailable говорит что есть нажатые клавишы
            // 
            
            if (Console.KeyAvailable)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                if (keypress.Key == ConsoleKey.S)
                {
                    pre_dir = dir;
                    dir = "STOP";
                }
                else if (keypress.Key == ConsoleKey.LeftArrow)
                {
                    pre_dir = dir;
                    dir = "LEFT";
                }
                else if (keypress.Key == ConsoleKey.RightArrow)
                {
                    pre_dir = dir;
                    dir = "RIGHT";
                }
                else if (keypress.Key == ConsoleKey.UpArrow)
                {
                    pre_dir = dir;
                    dir = "UP";
                }
                else if (keypress.Key == ConsoleKey.DownArrow)
                {
                    pre_dir = dir;
                    dir = "DOWN";
                }
            }
        }


        void Logic()
        {
            prevX = tailX[0];
            prevY = tailY[0];
            int prev2X, prev2Y;
            tailX[0] = x;
            tailY[0] = y;
            for (int i = 1; i < ntail; i++)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevX;
                tailY[i] = prevY;
                prevX = prev2X;
                prevY = prev2Y;
                
            }
            switch (dir)
            {
                case "RIGHT":
                    x++;
                    break;
                case "LEFT":
                    x--;
                    break;
                case "UP":
                    y--;
                    break;
                case "DOWN":
                    y++;
                    break;
                case "STOP":
                    gameOver = true;
                    break;
            }

            if (x > width || x < 0 || y > height || y < 0)
                    {
                    gameOver = true;
                        Console.SetCursorPosition(15, 10);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("GAME OVER");
                     }
            else
            {
                gameOver = false;
            }

            /*for (int i = 0; i < ntail; i++)
            {
                if (tailX[i] == x && tailY[i] == y)
                    gameOver = true;
                Console.SetCursorPosition(15, 10);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("GAME OVER");
                }*/
                    
                  

                    if (x == fruitX && y == fruitY)
                    {
                        score += 1;
                        ntail++;
                        fruitX = rand.Next(1, width - 1);
                        fruitY = rand.Next(1, height - 1);
                    }
                    
        }
            
      }
 }
