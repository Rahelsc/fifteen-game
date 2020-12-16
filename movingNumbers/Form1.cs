using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace movingNumbers
{
    public partial class Form1 : Form
    {
        Button[][] btnArr = new Button[4][];
        int currentRandom = 0;
        ArrayList randomNumbers;
        int counter = 1;
        public Form1()
        {
            Program.OpenDetailFormOnClose = false;
            InitializeComponent();
            this.Width = 380;
            this.Height = 400;

            randomNumbers = new ArrayList(16);
            for (int i = 1; i < 16; i++)
            {
                randomNumbers.Add(i);
            }

            Random r = new Random();
            for (int i = 0; i < btnArr.Length; i++)
            {
                btnArr[i] = new Button[4];
                for (int j = 0; j < btnArr[i].Length; j++)
                {
                    btnArr[i][j] = new Button();
                    if (!(j==btnArr[i].Length-1 && i==btnArr.Length-1))
                    {
                        currentRandom = r.Next(0, randomNumbers.Count);
                        btnArr[i][j].Text = Convert.ToString(randomNumbers[currentRandom]);
                        randomNumbers.RemoveAt(currentRandom);
                    }

                    btnArr[i][j].BackColor = Color.FromArgb(r.Next(0, 250), r.Next(0, 250), r.Next(0, 250));
                    btnArr[i][j].Size = new Size(90, 90);
                    btnArr[i][j].Location = new Point(j * btnArr[i][j].Height, i * btnArr[i][j].Width);
                    btnArr[i][j].Click += moveNumbersAround;
                    btnArr[i][j].Name = Convert.ToString(i) +","+ Convert.ToString(j);
                    this.Controls.Add(btnArr[i][j]);
                }
            }

            btnArr[3][3].Text = "";
            btnArr[3][3].BackColor = Color.Gray;
            btnArr[3][3].Visible = false;
        }

        private void moveNumbersAround(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] pos = b.Name.Split(',');
            Color tempColor = b.BackColor;
            string numTemp = b.Text;
            int row = Convert.ToInt32(pos[0]);
            int col = Convert.ToInt32(pos[1]);

            if (col - 1>= 0 && 
                btnArr[row][col-1].Visible==false)
            {
                b.Visible = false;
                btnArr[row][col - 1].Visible = true;
                btnArr[row][col - 1].BackColor = tempColor;
                btnArr[row][col - 1].Text = numTemp;
            }
            else if (col + 1<btnArr.Length && 
                btnArr[row][col + 1].Visible == false)
            {
                b.Visible = false;
                btnArr[row][col + 1].Visible = true;
                btnArr[row][col + 1].BackColor = tempColor;
                btnArr[row][col + 1].Text = numTemp;
            }
            else if (row - 1>=0 && 
                btnArr[row-1][col].Visible == false)
            {
                b.Visible = false;
                btnArr[row - 1][col].Visible = true;
                btnArr[row - 1][col].BackColor = tempColor;
                btnArr[row - 1][col].Text = numTemp;
            }
            else if (row + 1<btnArr[row].Length && 
                btnArr[row +1][col].Visible == false)
            {
                b.Visible = false;
                btnArr[row + 1][col].Visible = true;
                btnArr[row + 1][col].BackColor = tempColor;
                btnArr[row + 1][col].Text = numTemp;
            }

            assessVictory();
        }
        private void assessVictory()
        {
            for (int i = 0; i < btnArr.Length; i++)
            {
                for (int j = 0; j < btnArr[i].Length; j++)
                {
                    if (counter == Convert.ToInt32(btnArr[i][j].Text))
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 1;
                        return;
                    }
                }
            }

            DialogResult dialogResult = MessageBox.Show("new Game?", "Game Over!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Program.OpenDetailFormOnClose = true;
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }

        }
    }
}
