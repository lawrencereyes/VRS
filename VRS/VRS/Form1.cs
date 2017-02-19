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

namespace VRS{
    public partial class form : Form{
        SpeechSynthesizer sSynt = new SpeechSynthesizer();
        SpeechRecognizer sr = new SpeechRecognizer();
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRec = new SpeechRecognitionEngine();

        string []greeting = new string[6] { "Hello", "hello", "How are you", "how are you", "Hi", "hi" };
        string[] greetingRespond = new string[6] { "Hello", "Hello", "I am fine, thank you", "I am fine, thank you", "Hi" , "Hi"};

        public form(){
            InitializeComponent();
        }

        private void form_Load(object sender, EventArgs e){
            
        }

        private void btnRead_Click(object sender, EventArgs e){
            pBuilder.ClearContent();
            pBuilder.AppendText(txtBox.Text);
            sSynt.Speak(pBuilder);
        }

        private void txtBox_TextChanged(object sender, EventArgs e){

        }

        void sRec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e){
            
        }

        private void btnRespond_Click(object sender, EventArgs e){
            int resp = sRecognition();

            compSpeak(greetingRespond[resp]); 
        }

        int sRecognition(){
            int i = 0;

            for(; i < 3; i++) {
                if(greeting[i] == txtBox.Text){
                    return i;
                }
            }

            return -1;
        }

        void compSpeak(string text){
            sSynt.Speak(text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBox.Text = "";
        }
    }
}
