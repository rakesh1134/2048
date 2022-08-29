using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> _buttons = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            InitButtons();
            ClearButtons();
            InitGrid();
            InitBoard();
            ColorCells();
        }

        private bool IsRowMovePossible(int row)
        {
            List<int> vals = new List<int>();
            for (int c = 0; c < 4; ++c)
            {
                 Button btnCurrent = _buttons[row * 4 + c];
                if(!string.IsNullOrWhiteSpace(btnCurrent.Content.ToString()))
                {
                    vals.Add(Convert.ToInt32(btnCurrent.Content.ToString()));
                }
            }
            for(int i = 0; i < vals.Count-1; ++i)
            {
                if (vals[i] == vals[i + 1])
                    return true;
            }
            return false;
        }

        private bool IsColMovePossible(int col)
        {
            List<int> vals = new List<int>();
            for (int r = 0; r < 4; ++r)
            {
                Button btnCurrent = _buttons[r *4 + col];
                if (!string.IsNullOrWhiteSpace(btnCurrent.Content.ToString()))
                {
                    vals.Add(Convert.ToInt32(btnCurrent.Content.ToString()));
                }
            }
            for (int i = 0; i < vals.Count - 1; ++i)
            {
                if (vals[i] == vals[i + 1])
                    return true;
            }
            return false;
        }

        private bool LeftOrRightMovePossible()
        {
            for(int i = 0; i < 4; ++i)
            {
                if (IsRowMovePossible(i))
                    return true;
            }
            return false;
        }

        private bool UpOrDownMovePossible()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (IsColMovePossible(i))
                    return true;
            }
            return false;
        }

        private bool IsGameOver()
        {
            bool bAllNotEmpty = _buttons.All(x => !string.IsNullOrWhiteSpace(x.Content.ToString()));
            if (bAllNotEmpty)
            {
                if (!LeftOrRightMovePossible() && !UpOrDownMovePossible())
                    return true;
            }
            return false;
        }

        private SolidColorBrush GetColorByVal(int val)
        {
            if (val == 2)
                return Brushes.AliceBlue;
            if(val == 4)
                return Brushes.Tomato;
            if (val == 8)
                return Brushes.Fuchsia;
            if (val == 16)
                return Brushes.PeachPuff;
            if (val == 32)
                return Brushes.Chartreuse;
            if (val == 64)
                return Brushes.Aqua;
            if (val == 128)
                return Brushes.Azure;
            if (val == 256)
                return Brushes.Cyan;
            if (val == 512)
                return Brushes.LightSalmon;
            if (val == 1024)
                return Brushes.LightGreen;
            if (val == 2048)
                return Brushes.SeaShell;

            return Brushes.Gold;
        }

        private void ColorCells()
        {
            for (int i = 0; i < 16; ++i)
            {
                Button btn = _buttons[i];
                string val = btn.Content.ToString();
                if (!string.IsNullOrWhiteSpace(val))
                {
                    btn.Background = GetColorByVal(Convert.ToInt32(val));
                }
                else
                {
                    btn.Background = Brushes.Gray;
                }
            }
        }

        private void AddNewCell()
        {
            bool bAllNotEmpty = _buttons.All(x => !string.IsNullOrWhiteSpace(x.Content.ToString()));
            if (bAllNotEmpty)
                return ;
            Random r2 = new Random();
            int val2 = r2.Next(0, 10);
            if (val2 > 8)
                val2 =  4;
            else
                val2 = 2;

            Random r = new Random();
            int f = r.Next(0, 16);
            Button b = _buttons[f];
            while (!string.IsNullOrWhiteSpace(b.Content.ToString()))
            {
                f = r.Next(0, 16);
                b = _buttons[f];
            }
            b.Content = val2;
        }

        private void InitBoard()
        {
            Random r = new Random();
            int f = r.Next(0, 16);
            int s = r.Next(0, 16);
            while(s == f)
            {
                s = r.Next(0, 16);
            }
            int v1 = 2;
            int v2 = 2;

            Random r2 = new Random();
            if (r2.Next(0, 2) == 1)
                v1 = 4;
            if (r2.Next(0, 2) == 1)
                v2 = 4;

            _buttons[f].Content = v1.ToString();
            _buttons[s].Content = v2.ToString();
        }

        private void InitButtons()
        {
            for(int i = 0; i < 16; ++i)
            {
                Button btn = new Button();
                btn.SetValue(Button.FontWeightProperty, FontWeights.Bold);
                btn.FontSize = 30;
                _buttons.Add(btn);
            }
        }

        private void ClearButtons()
        {
            for (int i = 0; i < 16; ++i)
            {
                _buttons[i].Content = "";
            }
        }

        private void RestartGame()
        {
            ClearButtons();
            InitBoard();
            ColorCells();
        }

      
        private int GetNextNonEmptyButtonColumnInRow(int row, int col)
        {
            int c = 4;
            for(int x = col+1; x < 4; ++x)
            {
                Button btn = _buttons[row * 4 + x];
                if (string.IsNullOrEmpty(btn.Content.ToString()))
                    continue;
                else
                    c = x;
            }
            return c;
        }

        private bool ShiftRowRight(int row)
        {
            bool MoveDone = false;
            List<int> vals = new List<int>();
            for (int c = 0;c< 4; ++c)
            {
                Button btnCurrent = _buttons[row * 4 + c];
                if(!string.IsNullOrWhiteSpace(btnCurrent.Content.ToString()))
                {
                    vals.Add(Convert.ToInt32(btnCurrent.Content.ToString()));
                }
                btnCurrent.Content = "";
            }
            for (int i = 0; i < vals.Count-1; )
            {
                if (vals[i] == vals[i + 1])
                {
                    vals[i + 1] = vals[i + 1] * 2;
                    vals[i] = 0;
                    i += 2;
                    MoveDone = true;
                }
                else
                    ++i;
            }
            int col = 3;
            for (int i = vals.Count-1; i >=0; --i)
            {
                if(vals[i] != 0)
                {
                    Button btnCurrent = _buttons[row * 4 + col];
                    btnCurrent.Content = vals[i].ToString();
                    col = col - 1;
                }
            }
            return MoveDone;
        }

        private bool ShiftRowLeft(int row)
        {
            bool moveDone = false;
            List<int> vals = new List<int>();
            for (int c = 0; c < 4; ++c)
            {
                Button btnCurrent = _buttons[row * 4 + c];
                if (!string.IsNullOrWhiteSpace(btnCurrent.Content.ToString()))
                {
                    vals.Add(Convert.ToInt32(btnCurrent.Content.ToString()));
                }
                btnCurrent.Content = "";
            }
            for (int i = vals.Count-1; i > 0;)
            {
                if (vals[i] == vals[i - 1])
                {
                    vals[i - 1] = vals[i - 1] * 2;
                    vals[i] = 0;
                    i -= 2;
                    moveDone = true;
                }
                else
                    --i;
            }
            int col = 0;
            for (int i = 0; i <= vals.Count-1; ++i)
            {
                if (vals[i] != 0)
                {
                    Button btnCurrent = _buttons[row * 4 + col];
                    btnCurrent.Content = vals[i].ToString();
                    col = col + 1;
                }
            }
            return moveDone;
        }

        private bool ShiftRight()
        {
            bool MoveDone = false;
            for(int row = 0; row < 4; ++row)
            {
                if (ShiftRowRight(row) == true)
                    MoveDone = true;
            }
            return MoveDone;
        }

        private bool ShiftLeft()
        {
            bool MoveDone = false;
            for (int row = 0; row < 4; ++row)
            {
                if (ShiftRowLeft(row))
                    MoveDone = true;
            }
            return MoveDone;

        }

        private bool ShiftUp()
        {
            bool MoveDone = false;
            for (int col = 0; col < 4; ++col)
            {
                if (ShiftColUp(col))
                    MoveDone = true;
            }
            return MoveDone;
        }

        private bool ShiftDown()
        {
            bool MoveDone = false;
            for (int col = 0; col < 4; ++col)
            {
                if (ShiftColDown(col))
                    MoveDone = true;
            }
            return MoveDone;
        }

        private bool ShiftColUp(int c)
        {
            bool MoveDone = false;
            List<int> vals = new List<int>();
            for (int row1 = 0; row1 < 4; ++row1)
            {
                Button btnCurrent = _buttons[row1 * 4 + c];
                if (!string.IsNullOrWhiteSpace(btnCurrent.Content.ToString()))
                {
                    vals.Add(Convert.ToInt32(btnCurrent.Content.ToString()));
                }
                btnCurrent.Content = "";
            }
            for (int i = vals.Count - 1; i > 0;)
            {
                if (vals[i] == vals[i - 1])
                {
                    vals[i - 1] = vals[i - 1] * 2;
                    vals[i] = 0;
                    i -= 2;
                    MoveDone = true;
                }
                else
                    --i;
            }
            int row = 0;
            for (int i = 0; i <vals.Count; ++i)
            {
                if (vals[i] != 0)
                {
                    Button btnCurrent = _buttons[row * 4 + c];
                    btnCurrent.Content = vals[i].ToString();
                    row = row + 1;
                }
            }
            return MoveDone;
        }

        private bool ShiftColDown(int c)
        {
            bool MoveDone = false;
            List<int> vals = new List<int>();
            for (int row1 = 0; row1 < 4; ++row1)
            {
                Button btnCurrent = _buttons[row1 * 4 + c];
                if (!string.IsNullOrWhiteSpace(btnCurrent.Content.ToString()))
                {
                    vals.Add(Convert.ToInt32(btnCurrent.Content.ToString()));
                }
                btnCurrent.Content = "";
            }
            for (int i = 0; i <vals.Count-1;)
            {
                if (vals[i] == vals[i + 1])
                {
                    vals[i + 1] = vals[i + 1] * 2;
                    vals[i] = 0;
                    i += 2;
                    MoveDone = true;
                }
                else
                    ++i;
            }

            int row = 3;
            for (int i = 0; i < vals.Count; ++i)
            {
                if (vals[i] != 0)
                {
                    Button btnCurrent = _buttons[row * 4 + c];
                    btnCurrent.Content = vals[i].ToString();
                    row = row - 1;
                }
            }
            return MoveDone;
        }

        private void InitGrid()
        {
            for(int i = 0; i < _buttons.Count; ++i)
            {
                int r = i / 4;
                int c = i % 4;

                Grid.SetRow(_buttons[i], r);
                Grid.SetColumn(_buttons[i], c);
                gridmain.Children.Add(_buttons[i]);
            }
        }

        private void onkeydown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.R)
            {
                RestartGame();
            }
            else
            {
                if(IsGameOver() == true)
            {
                MessageBox.Show("Game Over!!!");
                return;
            }
                bool MoveDone = false;
                if (e.Key == System.Windows.Input.Key.Up)
                    MoveDone = ShiftUp();
                else if (e.Key == System.Windows.Input.Key.Down)
                    MoveDone = ShiftDown();
                else if (e.Key == System.Windows.Input.Key.Left)
                    MoveDone = ShiftLeft();
                else if (e.Key == System.Windows.Input.Key.Right)
                    MoveDone = ShiftRight();
                //if(MoveDone)
                //if(!LeftOrRightMovePossible() || !UpOrDownMovePossible())
                 AddNewCell();
                ColorCells();
            }
        }
    }

}
