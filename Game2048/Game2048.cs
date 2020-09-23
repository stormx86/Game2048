using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{
    public delegate void DelegateShow(int x, int y, int number);
    public partial class Game2048 : Form
    {
        static int size = 4;
        Label [,] box;
        Dictionary<int, Color> back_colors;
        Logic logic;
        public Game2048()
        {
            InitializeComponent();
            InitBackColors();
            InitLables();
            logic = new Logic(size, Show);
            logic.init_game();
        }

        private void InitBackColors()
        {
            back_colors = new Dictionary<int, Color>();
            back_colors.Add(0, this.BackColor);
            back_colors.Add(2, Color.LimeGreen);
            back_colors.Add(4, Color.SaddleBrown);
            back_colors.Add(8, Color.DarkSlateBlue);
            back_colors.Add(16, Color.Orange);
            back_colors.Add(32, Color.LightPink);
            back_colors.Add(64, Color.PapayaWhip);
            back_colors.Add(128, Color.MediumVioletRed);
            back_colors.Add(256, Color.LightSkyBlue);
            back_colors.Add(512, Color.MintCream);
            back_colors.Add(1024, Color.LightSeaGreen);
            back_colors.Add(2048, Color.Honeydew);
            back_colors.Add(4096, Color.LimeGreen);
            back_colors.Add(8192, Color.Blue);
            back_colors.Add(16384, Color.Red);
            back_colors.Add(32768, Color.Lime);
            back_colors.Add(65536, Color.Magenta);

        }
    
        private void InitLables()
        {
            int w = panel1.Width / size;
            int h = panel1.Height / size;
            box = new Label[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    box[x, y] = CreateLable();
                    box[x, y].Size = new System.Drawing.Size(w - 10, h - 10);
                    box[x, y].Location = new Point(x * w + 10, y * h + 10);
                    panel1.Controls.Add(box[x, y]);
                    //tableLayoutPanel1.Controls.Add(box[x,y], x, y);
                }

        }

        private Label CreateLable()
        {
            Label label = new Label();
            label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            //label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.Text = "";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            return label;
        }


        public void Show(int x, int y, int number)
        {
            box[x, y].Text = number > 0 ? number.ToString() : "";
            box[x, y].BackColor = back_colors[number];
        }

        private void Game2048_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left: logic.shift_left(); break;
                case Keys.Right: logic.shift_right(); break;
                case Keys.Up: logic.shift_up(); break;
                case Keys.Down: logic.shift_down(); break;
                case Keys.Escape: logic.init_game(); break;
                default: break;
            }
            if (logic.game_over())
            {
                MessageBox.Show("Вы проиграли!");
                logic.init_game();
            }
        }
    }
}
