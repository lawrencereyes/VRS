using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VRS{
    public partial class form : Form{

        //NECESSARY COMPONENTS FOR PROGRAM
        StreamReader rCommand = new StreamReader(@"C:/ Users / Lawrence Reyes / Desktop / Lawrence / AI(Janna) / VRS / Commands.txt");
        SpeechSynthesizer sSynt = new SpeechSynthesizer();
        SpeechRecognizer sr = new SpeechRecognizer();
        PromptBuilder pBuilder = new PromptBuilder();
        GrammarBuilder gBuilder = new GrammarBuilder();
        SpeechRecognitionEngine sRec = new SpeechRecognitionEngine();

        Point lastPosition;
        bool exit = false;

        Process pr = new Process();

        //BEGIN 
        public form(){
            InitializeComponent();
            MessageBox.Show("It works!");
        }

        //PROGRAM LOADS
        private void form_Load(object sender, EventArgs e){
            startup();
        }

        //SETS UP THE GRAMMAR OF THE SOFTWARE
        void startup(){
            try{
                gBuilder.Append(new Choices(System.IO.File.ReadAllLines(@"C:/ Users / Lawrence Reyes / Desktop / Lawrence / AI(Janna) / VRS / Commands.txt")));
            }
            catch{
                MessageBox.Show("The 'commands' file is not working.");
            }

            Grammar gr = new Grammar(gBuilder);

            try{
                sRec.UnloadAllGrammars();
                sRec.RecognizeAsyncCancel();
                sRec.RequestRecognizerUpdate();
                sRec.LoadGrammar(gr);
                sRec.SpeechRecognized += sRec_SpeechRecognized;

                sRec.SetInputToDefaultAudioDevice();
                sRec.RecognizeAsync(RecognizeMode.Multiple);
            }catch{
                MessageBox.Show("Grammar Builder Error");
            }
        }

        //COMMAND: LOCKS COMPUTER
        public void lockComputer(){
            System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }

        //ALL THE COMMANDS THAT THE COMPUTER RESPONDS TO
        void sRec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e){
            if(exit){
                Thread.Sleep(100);
                if(e.Result.Text == "yes"){
                    sSynt.SpeakAsyncCancelAll();
                    sRec.RecognizeAsyncCancel();
                    Application.Exit();
                }else{
                    exit = false;
                    compSpeak("Exit Cancelled");
                }
            }

            switch (e.Result.Text)
            {
                case "hello":
                    compSpeak("Hello Lawrence");
                    break;

                case "hi":
                    compSpeak("Hello Lawrence");
                    break;

                case "exit":
                    compSpeak("Are you sure you want to exit?");
                    exit = true;
                    break;

                case "thank you":
                    compSpeak("Anything for you Lawrence");
                    break;

                case "thanks":
                    compSpeak("Anything for you Lawrence");
                    break;

                case "lock computer":
                    lockComputer();
                    break;

                case "stop listening":
                    compSpeak("OK");
                    sRec.RecognizeAsyncCancel();
                    sRec.RecognizeAsyncStop();
                    break;

                case "open google":
                    compSpeak("Right away");
                    pr.StartInfo.FileName = "https://google.ca/";
                    pr.Start();
                    break;

                case "maximize":
                    try{
                        ActiveForm.WindowState = FormWindowState.Maximized;
                        break;
                    }catch{
                        compSpeak("Im already mazimized Lawrence");
                        break;
                    }
            }
        }

        //IT ALLOWS THE SOFTWARE TO REPLY TO THE USER
        void compSpeak(string text){
            sRec.RecognizeAsyncCancel();
            sRec.RecognizeAsyncStop();
            pBuilder.ClearContent();
            pBuilder.AppendText(text.ToString());
            sSynt.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            sSynt.SpeakAsync(pBuilder);
            sRec.RecognizeAsyncCancel();
            sRec.RecognizeAsyncStop();
            sRec.RecognizeAsync(RecognizeMode.Multiple);
        }

        /*EXTRA*/
        private void button3_Click(object sender, EventArgs e)
        {
            sRec.RecognizeAsyncCancel();
            sRec.RequestRecognizerUpdate();
            sRec.UnloadAllGrammars();


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pBuilder.ClearContent();
            sRec.RecognizeAsyncCancel();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                lastPosition = e.Location;
            }
            else { return; }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left += e.X - lastPosition.X;
                this.Top += e.Y - lastPosition.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sSynt.SpeakAsyncCancelAll();
        }







        internal void cancelSpeech()
        {
            sSynt.SpeakAsyncCancelAll();
        }



        private void btnStop_Click(object sender, EventArgs e)
        {
            sRec.RecognizeAsync(RecognizeMode.Multiple);
            //     btnStop.Visible = false; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //    name = comboBox1.Text.ToString();
            //    sSynth.SelectVoice(comboBox1.Text);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
        
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {



        }
    }
}
