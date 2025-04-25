using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XoGame.Properties;

namespace XoGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            questionMarkImage.Tag = "?";
            xImage.Tag = "X";
            oImage.Tag = "Y";
            Reset();
        }
        private Image questionMarkImage = Resources.question_mark_96;
        private Image xImage = Resources.X;
        private Image oImage = Resources.O;
        private PictureBox P1 = null;
        private PictureBox P2 = null;
        private PictureBox P3 = null;
        private int PlayCount = 0;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);// Parameters :opacity,Red,Green,Blue;
            Pen Pen = new Pen(White);
            Pen.Width = 15;
            Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            e.Graphics.DrawLine(Pen, 300, 250, 1000, 250);
            e.Graphics.DrawLine(Pen, 300, 400, 1000, 400);
            e.Graphics.DrawLine(Pen, 550, 100, 550, 550);
            e.Graphics.DrawLine(Pen, 800, 100, 800, 550);
        }
        private void Reset()
        {
            if(P1 != null)
            {
                P1.BackColor = Color.Black;
                P2.BackColor = Color.Black;
                P3.BackColor = Color.Black;
            }
            PlayCount = 0;
            lblTurn.Text = "Player 1";
            lblTurn.Tag = "Player 1";
            lblWinner.Text = "In Progress.";
            foreach (Control c in this.Controls)
                if (c is PictureBox p)
                {
                    p.Enabled = true;
                    p.Image = questionMarkImage;
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void WinnerBackGround()
        {
            P1.BackColor = Color.GreenYellow;
          P2.BackColor = Color.GreenYellow;
            P3.BackColor = Color.GreenYellow;
        }
        private void SetPictureBoxsOfWin(PictureBox p1,PictureBox p2,PictureBox p3)
        {
            P1 = p1;    
            P2 = p2;
            P3 = p3;
        }
        private bool CheckValues(PictureBox p1,PictureBox p2,PictureBox p3,object Tag)
        {
            if (p1.Image.Tag == Tag && p2.Image.Tag == Tag && p3.Image.Tag == Tag)
            {
                SetPictureBoxsOfWin(p1, p2, p3);
                WinnerBackGround();
                return true;
            }
            return false;
        }
       private bool CheckWin(object Tag )
        {
            if(CheckValues(pbxImages1, pbxImages2, pbxImages3,Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages4, pbxImages5, pbxImages6,Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages7, pbxImages8, pbxImages9, Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages1, pbxImages4, pbxImages7, Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages2, pbxImages5, pbxImages8, Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages3, pbxImages6, pbxImages9, Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages1, pbxImages5, pbxImages9, Tag))
            {
                return true;
            }
            if (CheckValues(pbxImages3, pbxImages5, pbxImages7, Tag ))
            {
                return true;
            }
            return false;
        }
        private void ActionsAfterWinOrDraw()
        {
            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblTurn.Text = "Game Over";
            foreach (Control c in this.Controls) if (c is PictureBox P) P.Enabled = false;
        }
        private void ActionsAfterDraw()
        {
            lblWinner.Text = "Draw";
            ActionsAfterWinOrDraw();
        }
        private void ActionsAfterWin()
        {
            lblWinner.Text = lblTurn.Text;
            ActionsAfterWinOrDraw();
        }
        private void Play(PictureBox p)
        {
            if (p.Image.Tag == questionMarkImage.Tag)
            {
                PlayCount++;
                if (lblTurn.Tag.ToString() == "Player 1")
                {
                    p.Image = xImage;
                    lblTurn.Tag = "Player 2";
                }
                else
                {
                    p.Image = oImage;
                    lblTurn.Tag = "Player 1";
                }
                if (CheckWin(p.Image.Tag))
                {
                    ActionsAfterWin();
                    return;
                }
               if(PlayCount == 9)
                {
                    ActionsAfterDraw();
                    return;
                }
                lblTurn.Text = Convert.ToString( lblTurn.Tag);
            }
            else MessageBox.Show("Wrong Choic!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void pbxImages_Click(object sender, EventArgs e)
        {
            Play((PictureBox)sender);
        }
    }
}