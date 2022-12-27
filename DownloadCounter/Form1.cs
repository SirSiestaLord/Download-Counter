using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Media;

using System.Diagnostics;
namespace DownloadCounter
{
    public partial class Form1 : Form
    {
        public static bool isdone = false;
        public static bool ischeckedformusic;
        public static bool shouldvisible;
        public static string filepath;
        public static bool musicon;

        public CountDownTimer timer = new CountDownTimer();
        public static bool ischecked;
        public Form1()
        {
            InitializeComponent();
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            label1.Text = "00:00:00";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
           
        }

        public void Time_setter()
        {
            Worker.i = 0;
            timer.Stop();
            timer.Dispose();
            //set to 30 mins
            timer.SetTime(int.Parse(Hournum.Value.ToString()), int.Parse(minutnum.Value.ToString()), int.Parse(secondnum.Value.ToString()));


            timer.Start();

            //update label text
            timer.TimeChanged += () => label1.Text = timer.TimeLeftStr;


            //timer step. By default is 1 second
            timer.StepMs = 77; // for nice milliseconds time switch

            radioButton1.Visible = false;
            radioButton2.Visible = false; radioButton3.Visible = false;

        }

        public void Time_setter(int hour, int minute, int second)
        {
            Worker.i = 0;
            timer.Stop();
            timer.Dispose();
            //set to 30 mins
            timer.SetTime(hour, minute, second);


            timer.Start();

            //update label text
            timer.TimeChanged += () => label1.Text = timer.TimeLeftStr;


            //timer step. By default is 1 second
            timer.StepMs = 77; // for nice milliseconds time switch
        }

        public void Resume()
        {
            int hour = 0, minut = 0, sec = 0;
            hour = int.Parse(timer.TimeLeft.Hours.ToString());
            minut = int.Parse(timer.TimeLeft.Minutes.ToString());
            sec = int.Parse(timer.TimeLeft.Seconds.ToString());
            timer.Reset();
            Time_setter(hour, minut, sec);




        }
        public class musicker
        {
           public static SoundPlayer player = new SoundPlayer();

            public static void load()
            {
                musicker.player.SoundLocation = filepath;

            }
            public static void play()
            {
                musicker.player.Play();

            }
            public static void stop()
            {
                musicker.player.Stop();
            }

        }

   

   
        public class Worker
        {
          
            public static bool x=false;
            public static int i = 0;
            public static bool isclicked = false;

          
           public static void exitter(string k)
            {
              
                if (i==0) {
                    
                    Form2 form2 = new Form2();
                    form2.a =k;
                    Form1 form1 = (Form1)ActiveForm;
                    form1.InitializeComponent();
                    form1.radioButton1.Visible = true;
                    form1.radioButton2.Visible = true;
                    form1.radioButton3.Visible = true;
                    if (ischeckedformusic)
                    {
                        musicker.load();
                        musicker.play();

                    }



                    form2.Show();

              

                    Console.WriteLine(i);
                }
                
               

            }

            public static bool Shutter()
            {
                                    //Kapatıcı
                                                        // MessageBox.Show("You shall not pass");
                if (ischecked)
                {
                    if (CheckNet()==true)
                    {
                        Process.Start("shutdown", "/s /t 0");

                    }
                    else
                    {

                        exitter("No internet connection");


                    }

                }
                else
                {
                    if (CheckNet() == true)
                    {


                        exitter("Time Is Up !");
                    }
                    else
                    {
                

                        exitter("No internet connection");


                    }
                }
                return true;

            }
        }
        public class CountDownTimer : IDisposable
        {
            public Stopwatch _stpWatch = new Stopwatch();

            public Action TimeChanged;

            public bool IsRunnign => timer.Enabled;

            public int StepMs
            {
                get => timer.Interval;
                set => timer.Interval = value;
            }

            private Timer timer = new Timer();

            private TimeSpan _max = TimeSpan.FromMilliseconds(30000);

            public TimeSpan TimeLeft => (_max.TotalMilliseconds - _stpWatch.ElapsedMilliseconds) > 0 ? TimeSpan.FromMilliseconds(_max.TotalMilliseconds - _stpWatch.ElapsedMilliseconds) : TimeSpan.FromMilliseconds(0);

            private bool _mustStop => (_max.TotalMilliseconds - _stpWatch.ElapsedMilliseconds) < 0;

            public string TimeLeftStr => TimeLeft.ToString(@"hh\:mm\:ss");
            
            public string TimeLeftMsStr => TimeLeft.ToString(@"mm\:ss\.fff");
            int i = 0;
            private void TimerTick(object sender, EventArgs e)
            {
                TimeChanged?.Invoke();

                if (_mustStop)
                {
                    
                        Worker.Shutter();
                    
                    _stpWatch.Stop();
                    timer.Enabled = false;
                }
            }

            public CountDownTimer(int min, int sec)
            {
                SetTime(min, sec);
                Init();
            }

            public CountDownTimer(int hour,int min, int sec)
            {
                SetTime(hour,min, sec);
                Init();
            }
            public CountDownTimer(TimeSpan ts)
            {
                SetTime(ts);
                Init();
            }

            public CountDownTimer()
            {
                Init();
            }

            private void Init()
            {
                StepMs = 1000;
                timer.Tick += new EventHandler(TimerTick);
            }

            public void SetTime(TimeSpan ts)
            {
                _max = ts;
                TimeChanged?.Invoke();
            }

            public void SetTime(int min, int sec = 0) => SetTime(TimeSpan.FromSeconds(min * 60 + sec));
            public void SetTime(int hour,int min, int sec = 0) => SetTime(TimeSpan.FromSeconds(hour*60*60+min * 60 + sec));

            public void Start()
            {
                timer.Start();
                _stpWatch.Start();
            }

            public void Pause()
            {
                timer.Stop();
                _stpWatch.Stop();
            }

        
            public static void Stopper(Stopwatch _stpWatch)
            {
                _stpWatch.Reset();
            }

            public  void Stop()
            {
                Reset();
                Pause();
            }

            public void Reset()
            {
                _stpWatch.Reset();
            }
          

            public void Restart()
            {
                _stpWatch.Reset();
                timer.Start();
            }

            public void Dispose() => timer.Dispose();
        }
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        public void button1_Click(object sender, EventArgs e)
        {

            if (CheckNet())
            {
                Time_setter();
            }
            else
            {
                MessageBox.Show("No internet connection");
            }
           


        }
        public void button2_Click(object sender, EventArgs e)
        {
            Worker.i = 0;
            timer.Stop();
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Pause();
            radioButton1.Visible = true;
            radioButton2.Visible = true;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Worker.i = 0;

            timer.Reset();
            radioButton1.Visible = true;
            radioButton2.Visible = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Resume();
            radioButton1.Visible = false;
            radioButton2.Visible = false;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {

            }
            else { ischeckedformusic = false; }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {

            }
            else { ischeckedformusic = false; }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter= "Music Files (*.wav)| *.wav";
            fileDialog.ShowDialog();
            filepath = fileDialog.FileName;
            ischeckedformusic = true;
            
        }
    }
}
