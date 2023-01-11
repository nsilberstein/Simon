namespace Simongame
{
    public partial class frmSimon : Form
    {

        System.Windows.Forms.Timer timerobj = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerobj2 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerobj3 = new System.Windows.Forms.Timer();
        List<Button> lstcolors = new List<Button>();
        List<Button> lstColorSequence = new List<Button>();
        List<Button> lstColorSequenceCopy;
        Random rnd = new Random();
        Button buttonsequence = new Button();
        Button Selectedbtn;
        int ButtonClickedIndex = 0;
        bool winner = false;
        Color newgreen = Color.FromArgb(144, 238, 144);
        Color newblue = Color.FromArgb(173, 216, 230);
        Color newyellow = Color.FromArgb(255, 250, 202);
        Color newred = Color.FromArgb(255, 127, 127);
        Color NewColor = Color.Black;

        public frmSimon()
        {
            InitializeComponent();
            lstcolors = new List<Button>() {btnBlue, btnGreen, btnRed, btnYellow};
            lstcolors.ForEach(c => c.Click += C_Click);
            timerobj.Interval = 400;
            timerobj.Tick += Timerobj_Tick;
            timerobj2.Interval = 300;
            timerobj2.Tick += Timerobj2_Tick;
            timerobj3.Interval = 500;
            timerobj3.Tick += Timerobj3_Tick;
            btnStart.Click += BtnStart_Click;
            DisplayMessage("Welcome to Simon Says! Press Start to begin!");
        }

        private void StartGame()
        {
            foreach(Button btn in lstcolors)
            {
                btn.BackColor = GetColor(btn.Name, false);
            }
            lstColorSequence.Clear();
            DisplayMessage("");
            NewRound();
        }

        private void NewRound()
        {
            winner = false;
            buttonsequence = lstcolors[rnd.Next(0, 4)];
            lstColorSequence.Add(buttonsequence);
            lstColorSequenceCopy = new List<Button>(lstColorSequence);
            ButtonClickedIndex = 0;
            LightUpNextButton();
        }
        // SimonGame Game
        // Game.SelectedButton { get; private set; }
        // Game.LightUpNextButton()
        private void LightUpNextButton()
        {
            Selectedbtn = lstColorSequenceCopy.First();
            Selectedbtn.BackColor = GetColor(Selectedbtn.Name, true);
            lstColorSequenceCopy.Remove(Selectedbtn);
            timerobj.Enabled = true;
        }

        private Color GetColor(string buttonname, bool lightup)
        {
            if(lightup == true)
            {            
                switch(buttonname)
                {
                    case "btnBlue":
                        NewColor = newblue;
                        break;
                    case "btnGreen":
                        NewColor = newgreen;
                        break;
                    case "btnYellow":
                        NewColor = newyellow;
                        break;
                    case "btnRed":
                        NewColor = newred;
                        break;
                }
            }
            else
            {
                switch (buttonname)
                {
                    case "btnBlue":
                        NewColor = Color.Blue;
                        break;
                    case "btnGreen":
                        NewColor = Color.DarkGreen;
                        break;
                    case "btnYellow":
                        NewColor = Color.Yellow;
                        break;
                    case "btnRed":
                        NewColor = Color.Red;
                        break;
                }
            }
            return NewColor;
        }

        private void PlayerGoes(Button btn)
        {
            Selectedbtn = btn;
            List<string> lstPlayerLost = new List<string>()
                {
                "You picked the wrong color. You are out!",
                "Wrong color! Better luck next time!",
                "Nope! Try Again!",
                "You're out! Press start to play again!"
               } 
            ;
                btn.BackColor = GetColor(btn.Name, true);
            if (lstColorSequence[ButtonClickedIndex] == btn)
            {
                timerobj.Enabled = true;
                ButtonClickedIndex = ButtonClickedIndex + 1;
                if (ButtonClickedIndex == lstColorSequence.Count)
                {
                    winner = true;
                }
            }
            else
            {
                DisplayMessage(lstPlayerLost[rnd.Next(0, 4)] + Environment.NewLine + "Your score is " + (lstColorSequence.Count() - 1));
                winner = false;
            }
        }
        private void DisplayMessage(string message)
        {
            lblMessage.Text = message;
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

        private void TimerGoes()
        {
            timerobj.Enabled = false;
            Selectedbtn.BackColor = GetColor(Selectedbtn.Name, false);
            if (winner == true)
            {
                timerobj3.Enabled = true;
            }
            else if (lstColorSequenceCopy.Count > 0)
            {
                timerobj2.Enabled = true;
            }
        }
        private void C_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            PlayerGoes(btn);
        }
        private void Timerobj_Tick(object? sender, EventArgs e)
        {
            //LB: Code below should be moved into its own procedure and be called from here.
            TimerGoes();
    
        }

        private void Timerobj2_Tick(object? sender, EventArgs e)
        {
            timerobj2.Enabled = false;
            LightUpNextButton();
        }

        private void Timerobj3_Tick(object? sender, EventArgs e)
        {
            timerobj3.Enabled = false;
            NewRound();
        }

    }
}