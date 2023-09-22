using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

namespace Noticat
{
    public partial class Dashboard : MetroFramework.Forms.MetroForm
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();
        public static string[] samples = File.ReadAllLines(@"E:\Noticat\Noticat\Sample.txt");
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = false;
            BtnStop.Enabled = true;
            clist.Add(samples);
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            try 
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error"); 
            }
        }

        private void sre_SpeechRecognized(object sender , SpeechRecognizedEventArgs e)
        {
            textBox1.Text = textBox1.Text + e.Result.Text.ToString() + " ";
        
        
        
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            BtnStart.Enabled= true;
            BtnStop.Enabled= false;
        }
    }
}
