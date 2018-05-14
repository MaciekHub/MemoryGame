using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GraPamieciowa
{
    public partial class Form1 : Form
    {
        private int _random;
        List<PictureBox> PictureList = new List<PictureBox>();
        List<Location> LocationList = new List<Location>();
        Random number = new Random();
        private PictureBox _clickedFirst = null, _clickedSecond = null;
        private int _clicks = 0;
        private int _pairs = 0;
        Stopwatch _watch;



        public Form1()
        {
            InitializeComponent();
            SetLocations();
            //StartGame();
            RandomizePictureBoxes();
            _watch = new Stopwatch();
            _watch.Start();
        }

        public void SetLocations()
        {
            for (int i = 0; i < 20; i++) {
                LocationList.Add(new GraPamieciowa.Location());
                LocationList[i].X = (int)Controls.Find("Location" + i, true)[0].Location.X;
                LocationList[i].Y = (int)Controls.Find("Location" + i, true)[0].Location.Y;
                if (i <= 10 && i != 0)
                {
                    PictureList.Add((PictureBox)Controls.Find("Icon" + i + "A", true)[0]);
                    PictureList.Add((PictureBox)Controls.Find("Icon" + i + "B", true)[0]);
                }
            }
        }

        public void RandomizePictureBoxes()
        {
            foreach (var pictureBox in PictureList)
            {
                var random = number.Next(0, LocationList.Count);
                pictureBox.Location = new Point(LocationList[random].X, LocationList[random].Y);
                LocationList.RemoveAt(random);
            }
        }

        private void Reset()
        {
       //     List<Location> LocationList = new List<Location>();
            PictureList = new List<PictureBox>();
            LocationList = new List<Location>();
            SetLocations();
            RandomizePictureBoxes();

            foreach (var picture in PictureList)
            {
                picture.BackColor = Color.Black;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            _clickedFirst.BackColor = Color.Black;
            _clickedSecond.BackColor = Color.Black;

            _clickedFirst = null;
            _clickedSecond = null;


        }

        private void buttonGame_Click(object sender, EventArgs e)
        {
            Reset();
            _watch = new Stopwatch();
            _watch.Start();
        }

        private void PictureClick(object sender, EventArgs e)
        {
            PictureBox ClickedPicture = sender as PictureBox;
         

            if (_clickedFirst != null && _clickedSecond != null)
                return;


            if (_clicks == 0)
            {
                _clickedFirst = ClickedPicture;
                _clicks++;
                ClickedPicture.BackColor = Color.White;
                return;
            }

            if (_clicks == 1)
            {
                _clickedSecond = ClickedPicture;
                _clickedSecond.BackColor = Color.White;

                string temp2 = _clickedFirst.Name;
                string temp1 = _clickedSecond.Name;
                temp2 = temp2.Remove(temp2.Length - 1, 1);
                temp1 = temp1.Remove(temp1.Length - 1, 1);

                if (temp1 == temp2)
                {
                    _clickedFirst = null;
                    _clickedSecond = null;
                    _clicks = 0;
                    _pairs++;
                    if (_pairs == 10)
                    {
                        _watch.Stop();
                        MessageBox.Show("You won! " + "Your time: " + _watch.Elapsed);
                        _pairs = 0;
                    }
                    return;
                }
                else
                {
                    ClickedPicture.Visible = true;
                    timer1.Start();
                }
                _clicks = 0;
                return;
            }
        }
    }
}
