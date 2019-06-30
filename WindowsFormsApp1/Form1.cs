using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		StreamReader sr;
		int equip_num,map_num,char_num,wear_num,cur_char;
		Equip[] equip;
		Chara[] chara;
		Button[] map;
		Equip cur_equip;
		bool equip_filter;

		private void button1_Click(object sender, EventArgs e)
		{
			chara[cur_char].cur_wear[0] = 1- chara[cur_char].cur_wear[0];
			change();
		}

		string line;

		int strcmp(string a,string b)
		{
			for (int i = 0; i < a.Length&&i<b.Length; i++)
				if (a[i] - b[i] != 0)
					return a[i] - b[i];
			return 0;
		}

		void change()
		{
			textBox1.Text = (chara[cur_char].cur_rank + 5).ToString();
			button1.Image = chara[cur_char].equip_img[chara[cur_char].cur_rank * 6];
			button2.Image = chara[cur_char].equip_img[chara[cur_char].cur_rank * 6 + 1];
			button3.Image = chara[cur_char].equip_img[chara[cur_char].cur_rank * 6 + 2];
			button4.Image = chara[cur_char].equip_img[chara[cur_char].cur_rank * 6 + 3];
			button5.Image = chara[cur_char].equip_img[chara[cur_char].cur_rank * 6 + 4];
			button6.Image = chara[cur_char].equip_img[chara[cur_char].cur_rank * 6 + 5];
			textBox2.Text = (chara[cur_char].obj_rank + 5).ToString();
			button7.Image = chara[cur_char].equip_img[chara[cur_char].obj_rank * 6];
			button8.Image = chara[cur_char].equip_img[chara[cur_char].obj_rank * 6 + 1];
			button9.Image = chara[cur_char].equip_img[chara[cur_char].obj_rank * 6 + 2];
			button10.Image = chara[cur_char].equip_img[chara[cur_char].obj_rank * 6 + 3];
			button11.Image = chara[cur_char].equip_img[chara[cur_char].obj_rank * 6 + 4];
			button12.Image = chara[cur_char].equip_img[chara[cur_char].obj_rank * 6 + 5];
			button1.Text = chara[cur_char].cur_wear[0] == 0 ? "" : "O";
			button2.Text = chara[cur_char].cur_wear[1] == 0 ? "" : "O";
			button3.Text = chara[cur_char].cur_wear[2] == 0 ? "" : "O";
			button4.Text = chara[cur_char].cur_wear[3] == 0 ? "" : "O";
			button5.Text = chara[cur_char].cur_wear[4] == 0 ? "" : "O";
			button6.Text = chara[cur_char].cur_wear[5] == 0 ? "" : "O";
			button7.Text = chara[cur_char].obj_wear[0] == 0 ? "" : "O";
			button8.Text = chara[cur_char].obj_wear[1] == 0 ? "" : "O";
			button9.Text = chara[cur_char].obj_wear[2] == 0 ? "" : "O";
			button10.Text = chara[cur_char].obj_wear[3] == 0 ? "" : "O";
			button11.Text = chara[cur_char].obj_wear[4] == 0 ? "" : "O";
			button12.Text = chara[cur_char].obj_wear[5] == 0 ? "" : "O";
			for (int i = 0; i < equip_num; i++)
				equip[i].need = 0;
			for (int i = 0; i < char_num; i++)
			{
				chara[i].b.Visible = !equip_filter;
				chara[i].b.Text = "X";
				for (int j = chara[i].cur_rank; j <= chara[i].obj_rank; j++)
					for (int k = 0; k < 6; k++)
						if (j == chara[i].cur_rank && j == chara[i].obj_rank)
						{
							if(chara[i].obj_wear[k] != 0 && chara[i].cur_wear[k] == 0)
							{
								chara[i].equip[j * 6 + k].need += 1;
								if (chara[i].equip[j * 6 + k] == cur_equip)
									chara[i].b.Visible = true;
								chara[i].b.Text = "";
							}
						}
						else if (j == chara[i].cur_rank)
						{
							if(chara[i].cur_wear[k] == 0)
							{
								chara[i].equip[j * 6 + k].need += 1;
								if (chara[i].equip[j * 6 + k] == cur_equip)
									chara[i].b.Visible = true;
								chara[i].b.Text = "";
							}
						}
						else if (j == chara[i].obj_rank)
						{
							if(chara[i].obj_wear[k] != 0)
							{
								chara[i].equip[j * 6 + k].need += 1;
								if (chara[i].equip[j * 6 + k] == cur_equip)
									chara[i].b.Visible = true;
								chara[i].b.Text = "";
							}
						}
						else
						{
							chara[i].equip[j * 6 + k].need += 1;
							chara[i].b.Text = "";
                            if (chara[i].equip[j * 6 + k] == cur_equip)
                                chara[i].b.Visible = true;
                        }
			}
			for(int i=0; i<equip_num;i++)
				for(int j=i+1; j<equip_num;j++)
					if(equip[i].need<equip[j].need)
					{
						Equip e = equip[i];
						equip[i] = equip[j];
						equip[j] = e;
					}
			for (int i = 0; i < map_num; i++)
				map[i].Tag = 0;
			int n = 0;
			for (int i = 0; i < equip_num; i++)
				if(equip[i].need>0)
				{
					equip[i].b.Text = equip[i].dc?"X":equip[i].need.ToString();
					equip[i].b.Location = new Point(n%5*64,n++/5*64);
					if(!equip[i].dc)
						for (int j = 0; j < equip[i].map_num; j++)
							equip[i].map[j].Tag = (int)equip[i].map[j].Tag + 1;
				}
				else
					equip[i].b.Location = new Point(-99,0);
			for(int i=0; i<map_num;i++)
				for(int j=i+1; j<map_num;j++)
                    if((int)map[i].Tag < (int)map[j].Tag)
					{
						Button b = map[i];
						map[i] = map[j];
						map[j] = b;
					}
			n = 0;
			for (int i = 0; i < map_num; i++)
				if ((int)map[i].Tag > 0)
					map[i].Location = new Point(n % 5 * 64 + 320, n++ / 5 * 64);
				else
					map[i].Location = new Point(-99, 0);
			n = 0;
			for (int i = 0; i < char_num; i++)
				if (chara[i].b.Text == "")
					chara[i].b.Location = new Point((n % 6) * 64 + 640, (n++ / 6) * 64);
            for (int i = 0; i < char_num; i++)
                if (chara[i].b.Text == "X")
                    chara[i].b.Location = new Point((n % 6) * 64 + 640, (n++ / 6) * 64);
        }

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			try
			{
				int n = Convert.ToInt16(textBox2.Text)-5;
				if (n >= 0 && n <= wear_num / 6)
                {
                    for (int i = 0; i < char_num; i++)
                        chara[i].obj_rank = n;
                    change();
                }
			}
			catch { }

		}

		private void button2_Click(object sender, EventArgs e)
		{
			chara[cur_char].cur_wear[1] = 1 - chara[cur_char].cur_wear[1];
			change();

		}

		private void button3_Click(object sender, EventArgs e)
		{
			chara[cur_char].cur_wear[2] = 1 - chara[cur_char].cur_wear[2];
			change();

		}

		private void button4_Click(object sender, EventArgs e)
		{
			chara[cur_char].cur_wear[3] = 1 - chara[cur_char].cur_wear[3];
			change();

		}

		private void button5_Click(object sender, EventArgs e)
		{
			chara[cur_char].cur_wear[4] = 1 - chara[cur_char].cur_wear[4];
			change();

		}

		private void button6_Click(object sender, EventArgs e)
		{
			chara[cur_char].cur_wear[5] = 1 - chara[cur_char].cur_wear[5];
			change();

		}

		private void button7_Click(object sender, EventArgs e)
		{
            for(int i=0; i<char_num;i++)
			    chara[i].obj_wear[0] = 1 - chara[i].obj_wear[0];
			change();

		}

		private void button8_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < char_num; i++)
                chara[i].obj_wear[1] = 1 - chara[i].obj_wear[1];
			change();

		}

		private void button9_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < char_num; i++)
                chara[i].obj_wear[2] = 1 - chara[i].obj_wear[2];
			change();

		}

		private void button10_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < char_num; i++)
                chara[i].obj_wear[3] = 1 - chara[i].obj_wear[3];
			change();

		}

		private void button11_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < char_num; i++)
                chara[i].obj_wear[4] = 1 - chara[i].obj_wear[4];
			change();

		}

		private void button12_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < char_num; i++)
                chara[i].obj_wear[5] = 1 - chara[i].obj_wear[5];
			change();

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			StreamWriter sw = new StreamWriter("save/save.txt");
			sw.WriteLine(char_num);
			for (int i = 0; i < char_num; i++)
			{
				sw.WriteLine(chara[i].name);
				sw.WriteLine(chara[i].cur_rank);
				sw.WriteLine(chara[i].obj_rank);
				for (int j = 0; j < 6; j++)
				{
					sw.WriteLine(chara[i].cur_wear[j]);
					sw.WriteLine(chara[i].obj_wear[j]);
				}
			}
			sw.Close();
			sw = new StreamWriter("save/dc.txt");
			sw.WriteLine(equip_num);
			for (int i = 0; i < equip_num; i++)
			{
				sw.WriteLine(equip[i].name);
				sw.WriteLine(equip[i].dc?"1":"0");
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			change();
		}

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            change();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < char_num; i++)
            {
                chara[i].cur_rank = wear_num / 6 - 1;
                chara[i].obj_rank = wear_num / 6 - 1;
                chara[i].cur_wear = new int[6];
                chara[i].obj_wear = new int[6];
                for (int k = 0; k < 6; k++)
                    if (chara[i].equip[wear_num - 6 + k] != null)
                    {
                        chara[i].cur_wear[k] = 1;
                        chara[i].obj_wear[k] = 1;
                    }
            }

            for (int i = 0; i < equip_num; i++)
                equip[i].dc = false;
            change();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
		{
			try
			{
				int n = Convert.ToInt16(textBox1.Text)-5;
				if (n >= 0 && n <= wear_num / 6)
                {
                    chara[cur_char].cur_rank = n;
                    change();
                }
			}
			catch { }
				
		}

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
            int size;
            if (System.IO.File.Exists("save/size.txt"))
            {
                StreamReader sizereader = new StreamReader("save/size.txt");
                size = Convert.ToInt16(sizereader.ReadLine());
            }
            else
                size = 20;

            sr = new StreamReader("resources/equip.txt");

			map = new Button[900];
			map_num = 0;

			equip_num = Convert.ToInt32(sr.ReadLine());
			equip = new Equip[equip_num];
			for(int i=0; i<equip_num;i++)
			{
				equip[i] = new Equip();
				equip[i].name = sr.ReadLine();
				equip[i].map_num = Convert.ToInt32(sr.ReadLine());
				equip[i].map = new Button[equip[i].map_num];
				for(int j=0; j<equip[i].map_num; j++)
				{
					line = sr.ReadLine();
					bool ok=false;
					for (int k = 0; k < map_num; k++)
						if (map[k].Text == line)
						{
							equip[i].map[j] = map[k];
							ok = true;
						}
					if(!ok)
					{
						map[map_num] = new Button();
						map[map_num].Size = new Size(64, 64);
						map[map_num].Text = line;
						map[map_num].MouseDown += (s, ev) =>
						{
							for(int k=0; k<equip_num;k++)
							{
								bool okk = false;
								for (int l = 0; l < equip[k].map_num; l++)
									if (equip[k].map[l] == (Button)s)
										okk = true;
								if (!okk)
									equip[k].b.Visible = false;
							}
						};
						map[map_num].MouseUp += (s, ev) =>
						{
							for (int k = 0; k < equip_num; k++)
								equip[k].b.Visible = true;
						};
							equip[i].map[j] = map[map_num];
						this.Controls.Add(map[map_num++]);
					}
				}

				equip[i].b = new Button();
				equip[i].b.Size = new Size(64, 64);
				equip[i].b.Image = new Bitmap(Image.FromFile("resources/" + equip[i].name), new Size(64, 64));
				equip[i].b.Font = new Font("微軟正黑體",size,FontStyle.Bold);
				equip[i].b.MouseDown += (s, ev) =>
				{
					for (int j = 0; j < map_num; j++)
						map[j].Visible = false;
					for (int j = 0; j < equip_num; j++)
						if (equip[j].b == (Button)s)
						{
							for (int k = 0; k < equip[j].map_num; k++)
								equip[j].map[k].Visible = true;
							if (ev.Button == MouseButtons.Right)
								equip[j].dc = equip[j].dc ? false : true;
							cur_equip = equip[j];
							equip_filter = true;
							change();
						}
				};
				equip[i].b.MouseUp += (s, ev) =>
				{
					equip_filter = false;
					change();
					for (int j = 0; j < map_num; j++)
						map[j].Visible = true;
				};
				this.Controls.Add(equip[i].b);
			}
            
			sr = new StreamReader("resources/char.txt");
			char_num = Convert.ToInt16(sr.ReadLine());
			chara = new Chara[char_num];
			for(int i=0; i<char_num;i++)
			{
				chara[i] = new Chara();
				chara[i].name = sr.ReadLine();
				wear_num = Convert.ToInt16(sr.ReadLine());
				chara[i].equip = new Equip[wear_num];
				chara[i].equip_img = new Bitmap[wear_num];
				for (int j=0; j<wear_num;j++)
				{
					line = sr.ReadLine();
					chara[i].equip_img[j] = new Bitmap(Image.FromFile("resources/" + line), new Size(64, 64));
					for (int k = 0; k < equip_num; k++)
						if (equip[k].name == line)
							chara[i].equip[j] = equip[k];
				}

				chara[i].b = new Button();
				chara[i].b.Font = button1.Font;
				chara[i].b.Size = new Size(64, 64);
				chara[i].b.Image = new Bitmap(Image.FromFile("resources/" + chara[i].name), new Size(64, 64));
				int ii = i;
				chara[i].b.Click += (s, ev) =>
				{
					cur_char = ii;
					change();
				};
				this.Controls.Add(chara[i].b);

				chara[i].cur_rank = wear_num/6-1;
				chara[i].obj_rank = wear_num/6-1;
				chara[i].cur_wear = new int[6];
				chara[i].obj_wear = new int[6];
				for (int k = 0; k < 6; k++)
					if(chara[i].equip[wear_num-6+k] !=null)
					{
						chara[i].cur_wear[k] = 1;
						chara[i].obj_wear[k] = 1;
					}	
			}
			if (System.IO.File.Exists("save/save.txt"))
			{
				StreamReader sr2 = new StreamReader("save/save.txt");
				int num = Convert.ToInt16(sr2.ReadLine());
				for (int i = 0; i < num; i++)
				{
					line = sr2.ReadLine();
					for (int j = 0; j < char_num; j++)
						if (chara[j].name == line)
						{
							chara[j].cur_rank = Convert.ToInt32(sr2.ReadLine());
							chara[j].obj_rank = Convert.ToInt32(sr2.ReadLine());
							for (int k = 0; k < 6; k++)
							{
								chara[j].cur_wear[k] = Convert.ToInt32(sr2.ReadLine());
								chara[j].obj_wear[k] = Convert.ToInt32(sr2.ReadLine());
							}
						}
				}
				sr2.Close();
			}
			if (System.IO.File.Exists("save/dc.txt"))
			{
				StreamReader sr2 = new StreamReader("save/dc.txt");
				int num = Convert.ToInt16(sr2.ReadLine());
				for (int i = 0; i < num; i++)
				{
					line = sr2.ReadLine();
					for (int j = 0; j < equip_num; j++)
						if (equip[j].name == line)
							equip[j].dc = Convert.ToInt32(sr2.ReadLine()) == 1;
				}
				sr2.Close();
            }
            change();
		}

		private void B_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}

	class Equip
	{
		public string name;
		public int map_num;
		public Button[] map;
		public Button b;
		public int need;
		public bool dc;
	}
	class Chara
	{
		public string name;
		public Equip[] equip;
		public Bitmap[] equip_img;
		public Button b;
		public int cur_rank;
		public int obj_rank;
		public int[] cur_wear;
		public int[] obj_wear;
	}
}
