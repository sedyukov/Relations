using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Relations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }
        List<Button> buttons = new List<Button>();
        List<CheckBox> checks = new List<CheckBox>();
        List<TextBox> tBoxes = new List<TextBox>();
        List<Label> labels = new List<Label>();
        List<Label> byX = new List<Label>();
        List<Label> byY = new List<Label>();
        List<Label> resLabelsNum = new List<Label>();
        List<Label> resLabels = new List<Label>();
        Logic l;

        const int stepTable = 30;
        const int stepMatrix = 50;
        const int startX = 18;
        const int startY = 18;
        const int minVal = 0;
        const int maxVal = 7;
        int n = 3;
        String[] names;

        public bool[,] getMatrix()
        {
            bool[,] m = new bool[n, n];
            int j = -1;
            for (int i = 0; i < n*n; i++)
            {
                if (i % n == 0)
                {
                    j++;
                }
                m[i % n, j] = checks[i].Checked; 
            }
            return m;
        }
        private void setNames()
        {
            names = new String[n];
            names[0] = textBox2.Text;

            for (int i = 0; i < n - 1; i++)
            {
                names[i+1] = tBoxes[i].Text;
            }
        }
        private void printNames()
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(names[i]);
            }
        }
        private void updateNameTable()
        {
            while (tBoxes.Count > 0)
            {
                panel2.Controls.Remove(tBoxes[tBoxes.Count - 1]);
                tBoxes.Remove(tBoxes[tBoxes.Count - 1]);
                panel2.Controls.Remove(labels[labels.Count - 1]);
                labels.Remove(labels[labels.Count - 1]);

            }
            textBox2.Text = "1";
            for (int i = 1; i < n; i ++)
            {
                TextBox tBox = new TextBox();
                tBox.Location = new Point(textBox2.Location.X, textBox2.Location.Y + i * stepTable);
                tBox.Size = new Size(textBox2.Size.Width, textBox2.Size.Height);
                tBox.Text = (i + 1).ToString();
                panel2.Controls.Add(tBox);
                tBoxes.Add(tBox);
                Label lbl = new Label();
                lbl.Location = new Point(label2.Location.X, label2.Location.Y + i * stepTable);
                lbl.Text = (i + 1).ToString();
                panel2.Controls.Add(lbl);
                labels.Add(lbl);
            }
        }

        private void updateMatrix()
        {
            while (checks.Count > 0)
            {
                panel1.Controls.Remove(checks[checks.Count - 1]);
                checks.Remove(checks[checks.Count - 1]);

            }
            while (byX.Count > 0)
            {
                Controls.Remove(byX[byX.Count - 1]);
                byX.Remove(byX[byX.Count - 1]);
                Controls.Remove(byY[byY.Count - 1]);
                byY.Remove(byY[byY.Count - 1]);
            }
            for (int i = 0; i < n; i++)
            {
                if (i != 0) {
                    Label lbl = new Label();
                    lbl.Location = new Point(label6.Location.X + i * stepMatrix, label6.Location.Y);
                    lbl.Text = (i + 1).ToString();
                    lbl.Font = label6.Font;
                    lbl.AutoSize = true;
                    Controls.Add(lbl);
                    byX.Add(lbl);

                    Label lbl2 = new Label();
                    lbl2.Location = new Point(label7.Location.X, label7.Location.Y + i * stepMatrix);
                    lbl2.Text = (i + 1).ToString();
                    lbl2.Font = label7.Font;
                    Controls.Add(lbl2);
                    byY.Add(lbl2);
                }

                for (int j = 0; j < n; j ++)
                {
                    CheckBox chBox = new CheckBox();
                    if (j == i) chBox.Enabled = false;
                    chBox.Location = new Point(startX + i * stepMatrix, startY + j * stepMatrix);
                    chBox.Size = new Size(20, 20);
                    panel1.Controls.Add(chBox);
                    checks.Add(chBox);
                }
            }
        }
        private void showResult(List<int> res)
        {
            while (resLabelsNum.Count > 0)
            {
                panel3.Controls.Remove(resLabelsNum[resLabelsNum.Count - 1]);
                resLabelsNum.Remove(resLabelsNum[resLabelsNum.Count - 1]);

            }
            while (resLabels.Count > 0)
            {
                panel3.Controls.Remove(resLabels[resLabels.Count - 1]);
                resLabels.Remove(resLabels[resLabels.Count - 1]);
            }
            int j = 0;
            bool isEnd = false;
            for (int i = 0; i < res.Count - 1; i++)
            {
                // for numbers
                if (res[i] == -1)
                {
                    Label lbl = new Label();
                    lbl.Location = new Point(startX, startY + j * stepTable);
                    lbl.Text = (j + 1).ToString();
                    lbl.Font = label6.Font;
                    lbl.AutoSize = true;
                    panel3.Controls.Add(lbl);
                    resLabelsNum.Add(lbl);
                    j++;
                    continue;
                }
                String tmp = "";
                while (res[i] != -1)
                {
                    if (i == res.Count - 1)
                    {
                        isEnd = true;
                        break;
                    }
                    tmp += names[res[i]] + " ";
                    i++;
                }
                Label lbl2 = new Label();
                lbl2.Location = new Point(startX + stepTable, startY + (j - 1) * stepTable);
                lbl2.Text = tmp;
                lbl2.AutoSize = true;
                panel3.Controls.Add(lbl2);
                resLabels.Add(lbl2);
                if (isEnd) break;
                else i--;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label10.Text = "";
            label11.Text = "";

            if (Int32.TryParse(this.textBox1.Text, out n))
            {
                if ((n > minVal) && (n < maxVal))
                {
                    panel1.Size = new Size(n * stepMatrix, n * stepMatrix);
                    updateNameTable();
                    updateMatrix();
                    setNames();
                    button3.Enabled = true;
                    button1.Enabled = true;
                }
                else
                {
                    label10.Text = "Введите число >" + minVal.ToString() + " и <" + maxVal.ToString();
                    button3.Enabled = false;
                    button1.Enabled = false;
                }
            }
            else
            {
                label10.Text = "Некорректный ввод";
                button3.Enabled = false;
                button1.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool[,] m = getMatrix();
            l = new Logic(m, n);
            //l.printRes();
            if (l.calcMatrix())
            {
                List<int> result = l.getResult();
                showResult(result);
            }
            else label11.Text = "Родовой граф замкнут, вычисление невозможно!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setNames();
        }
    }
}
