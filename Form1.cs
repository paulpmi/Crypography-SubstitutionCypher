using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> key;
        List<string> used;
        List<string> usedKeys;
        string message;
        //string alphabet = "_abcdefghijklmnopqrstuvwxyz";
        List<string> alphabet;
        private void InitializeMyButton()
        {
            // Create and initialize a Button.
            Button button1 = new Button();

            // Set the button to return a value of OK when clicked.
            button1.DialogResult = DialogResult.OK;

            // Add the button to the form.
            Controls.Add(button1);
        }

        int createCode(string k)
        {
            this.alphabet = new List<string>(new string[] { "_", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" });
            //this.alphabet = new List<string>(new string[] { "_", "a", "b", "c", "d", "e"});

            for (int i = 0; i < this.alphabet.Count; i++)
            {
                if (!used.Contains(this.alphabet[i]))
                {
                    Button tmpButton = new Button();
                    tmpButton.Top = 70 + (1 * 50 + 10);
                    tmpButton.Left = 10 + (i * 50 + 50);
                    tmpButton.Width = 30;
                    tmpButton.Height = 30;
                    tmpButton.Text = alphabet[i];
                    

                    tmpButton.Click += (s, e) => { Console.Write("\nHERE\n"); Console.Write(k); this.usedKeys.Add(k);  this.used.Add(tmpButton.Text); this.key.Add(k, tmpButton.Text); this.Controls.Clear(); tmpButton.Dispose(); this.Initialize(); };
                    //tmpButton.Click -= (s, e) => { Console.Write("\nHERE\n"); Console.Write(k); this.used.Add(tmpButton.Text); this.Controls.Remove(tmpButton); tmpButton.Dispose(); };

                    // Possible add Buttonclick event etc..
                    this.Controls.Add(tmpButton);
                }
            }
            return 0;
        }

        void Initialize()
        {
            this.alphabet = new List<string>(new string[] { "_", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" });
            //this.alphabet = new List<string>(new string[] { "_", "a", "b", "c", "d", "e" });
            for (int i = 0; i < alphabet.Count; i++)
            {
                if (!this.usedKeys.Contains(alphabet[i]))
                {
                    Button tmpButton = new Button();
                    tmpButton.Top = 10 + (1 * 10 + 10);
                    tmpButton.Left = 50 + (i * 50 + 50);
                    tmpButton.Width = 40;
                    tmpButton.Height = 40;
                    tmpButton.Text = alphabet[i];
                    //tmpButton.Click += new EventHandler(Button1_Click);
                    tmpButton.Click += (s, e) => { createCode(tmpButton.Text); this.Controls.Remove(tmpButton); tmpButton.Dispose(); };
                    tmpButton.Click -= (s, e) => { createCode(tmpButton.Text); this.Controls.Remove(tmpButton); tmpButton.Dispose(); };

                    // Possible add Buttonclick event etc..
                    this.Controls.Add(tmpButton);
                }
            }

            if (this.usedKeys.Count == alphabet.Count)
            {
                TextBox textBox = new TextBox();
                textBox.Top = 100;

                Label l = new Label();
                l.Top = 100;
                foreach (KeyValuePair<string, string> kvp in this.key)
                {
                    textBox.Text += string.Format("K:{0}, V:{1} ", kvp.Key, kvp.Value);
                    l.Text += string.Format("K:{0}, V:{1} ", kvp.Key, kvp.Value);
                }
                this.Controls.Add(textBox);

                TextBox messageBox = new TextBox();
                Button sendButton = new Button();
                Label messageLabel = new Label();
                messageLabel.Top = 250;
                messageLabel.Text = "Message: ";
                messageBox.Top = 300;
                sendButton.Top = 300;
                sendButton.Left = 100;
                sendButton.Click += (s, e) => { sendMessage(messageBox.Text); };
                //messageBox.MouseEnter += (s, e) => { sendMessage(messageBox.Text); }; // send message encrypts then sends to receiver then it decrypts the message
                this.Controls.Add(sendButton);
                this.Controls.Add(messageBox);
            }
        }

        void sendMessage(string k)
        {
            int ok = 0;
            string cryptedMessage = "";
            foreach(Char c in k)
            {
                if (this.key.ContainsKey(c.ToString()))
                    cryptedMessage += this.key[c.ToString()].ToUpper();
                else
                {
                    ok = 1;
                    break;
                }
            }
            TextBox messageBox2 = new TextBox();
            messageBox2.Top = 350;
            messageBox2.Left = 50;
            if (ok == 0)
            {
                messageBox2.Text = cryptedMessage;
                decrypt(cryptedMessage);
            }
            else
                messageBox2.Text = "Error! Invalid Message";
            this.Controls.Add(messageBox2);
            //Initialize();
        }

        void decrypt(string k)
        {
            TextBox messageBox2 = new TextBox();
            messageBox2.Top = 450;
            //messageBox2.Left = 50;

            string decryptedMessage = "";

            foreach (Char c in k.ToLower())
            {
                decryptedMessage += this.key.FirstOrDefault(x => x.Value == c.ToString()).Key.ToUpper();
            }


            messageBox2.Text = decryptedMessage;
            this.Controls.Add(messageBox2);
        }
        /*
        void click()
        {
            foreach (Button b in this.Controls)
            {
                for (int i = 0; i<alphabet.Count; i++)
                    if (b.Text == alphabet[i] && b.Click == true)
                        b.Click += (s, e) => { createCode(alphabet[i]); this.Controls.Remove(b); b.Dispose(); };
            }
        }
        */

        public Form1()
        {
            this.used = new List<string>();
            this.key = new Dictionary<string, string>();
            this.usedKeys = new List<string>();

            Initialize();
            //createCode();
            InitializeComponent();
        }
    }
}
