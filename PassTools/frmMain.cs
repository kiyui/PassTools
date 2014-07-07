    //Pass Tools, a simple password generator & manager.
    //Copyright (C) 2014  Timur Lavrenti Kiyivinski

    //This program is free software: you can redistribute it and/or modify
    //it under the terms of the GNU General Public License as published by
    //the Free Software Foundation, either version 3 of the License, or
    //(at your option) any later version.

    //This program is distributed in the hope that it will be useful,
    //but WITHOUT ANY WARRANTY; without even the implied warranty of
    //MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    //GNU General Public License for more details.

    //You should have received a copy of the GNU General Public License
    //along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PassTools
{
    public partial class frmMain : Form
    {
        #region "Declaration"
        public struct PassWord
        {
            public string Name;
            public string Password;
            public string Details;
        }
        public List<PassWord> PassWordList = new List<PassWord>();
        public List<string> includeValues = new List<string>();
        public string DBPath = Directory.GetCurrentDirectory() + "\\passDB";
        public string userPassWord;
        //To access about.txt
        Assembly _assembly;
        Stream _imageStream;
        StreamReader _textStreamReader;
        #endregion
        public frmMain()
        {
            InitializeComponent();
            unlockDB();
            updatePasswords();
        }
        #region "Functions & Procedures"
        //GUI
        private void updatePasswords()
        {

            if(cbPass.Items.Count >= 0)
            {
                cbPass.Items.Clear();
            }
            cbPass.Items.Add("Add a new password");
            foreach(PassWord existingPassWord in PassWordList)
            {
                cbPass.Items.Add(existingPassWord.Name);
            }
            cbPass.SelectedIndex = 0;
        }
        private void clearChildren(GroupBox parentBox)
        {
            foreach(Control childControl in parentBox.Controls)
            {
                if(childControl.Name.Substring(5) == "Name" || childControl.Name.Substring(5) == "Pass" || childControl.Name.Substring(5) == "Details")
                {
                    childControl.Text = "...";
                }
            }
        }
        private void loginSuccess()
        {
            GL.Visible = false;
            updatePasswords();
        }
        private void loginFail()
        {
            GLtxtPass.Focus();
            GLtxtPass.SelectAll();
        } 
        //Password generation
        private string[] requestAlphabet()
        {
            string[] alphabet = new string[52];
            string[] lowAlphabet = new string[26] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            for (int loopVar = 0; loopVar < 26; loopVar++ )
            {
                alphabet[loopVar] = lowAlphabet[loopVar];
                alphabet[loopVar + 25] = lowAlphabet[loopVar].ToUpper();
            }
            return alphabet;
        }
        private void PassListUpdate()
        {
            cbAlgorithm.Items.Clear();
            cbAlgorithm.Items.Add("Simple");
            cbAlgorithm.Items.Add("Complex");
            cbAlgorithm.SelectedIndex = 0;
        } //Updates the password list
        //Password generation types
        private string genSimple(string passwordSeed, double passwordLength)
        {
            Random randomGen = new Random();
            int sequenceSeed;
            string returnPassword = string.Empty;
            string[] alphabet = requestAlphabet();
            while (returnPassword.Length != passwordLength)
            {
                if (passwordSeed.Length > passwordLength)
                {
                    sequenceSeed = randomGen.Next(Convert.ToInt32(passwordLength), passwordSeed.Length);
                }
                else
                {
                    sequenceSeed = randomGen.Next(passwordSeed.Length, Convert.ToInt32(passwordLength));
                }
                if (sequenceSeed > 51)
                {
                    sequenceSeed -= 51;
                }
                returnPassword += alphabet[sequenceSeed];
            }
            return returnPassword;
        }
        private string genComplex(string passwordSeed, double passwordLength)
        {
            Random randomGen = new Random();
            int sequenceSeed;
            string returnPassword = string.Empty;
            string[] alphabet = requestAlphabet();
            List<string> randomAlphabet = new List<string>();
            List<int> addedIndex = new List<int>();
            List<int> loopInt = new List<int>();
            for (int loopVar = 0; loopVar < 52; loopVar++ )
            {
                sequenceSeed = randomGen.Next(0, 52);
                if (addedIndex.IndexOf(sequenceSeed) == -1)
                {
                    randomAlphabet.Add(alphabet[sequenceSeed]);
                    addedIndex.Add(sequenceSeed);
                }
                else
                {
                    loopVar--;
                }
            }
            Console.WriteLine(addedIndex.Count.ToString());
            for (int loopVar = 0; loopVar < passwordLength; loopVar++)
            {
                if (returnPassword.Length == passwordLength)
                {
                    break;
                }
                else
                {
                    loopInt.Add(Convert.ToInt32(loopVar % passwordSeed.Length));
                }
            }
            for (int loopVar = 0; loopVar < passwordLength; loopVar++ )
            {
                int thisInt = randomAlphabet.IndexOf(passwordSeed[loopInt[loopVar]].ToString());
                int nextInt; 
                try
                {
                    nextInt = randomAlphabet.IndexOf(passwordSeed[loopInt[loopVar] + 1].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    nextInt = thisInt;
                }
                if (thisInt == -1 || nextInt == -1)
                {
                    loopVar--;
                }
                else
                {
                    int nextRand;
                    if (thisInt <= nextInt)
                    {
                        nextRand = randomGen.Next(thisInt, nextInt + 1);
                    }
                    else
                    {
                        nextRand = randomGen.Next(nextInt, thisInt + 1);
                    }
                    returnPassword += randomAlphabet[nextRand];
                }
            }
            return returnPassword;
        }
        private int includeLength()
        {
            string includeChars = string.Empty;
            foreach (string includeString in includeValues)
            {
                includeChars += includeString;
            }
            return Convert.ToInt32(includeChars.Length);
        } //Calculates the include length
        private string addInclude(string generatedPassword, double passwordLength)
        {
            Random randomGen = new Random();
            int placeIndex;
            string includePassword = string.Empty;
            string beforeStr;
            string afterStr;
            includePassword = generatedPassword.Substring(0, generatedPassword.Length - includeLength());
            foreach(string charInclude in includeValues)
            {
                placeIndex = randomGen.Next(0, includePassword.Length);
                beforeStr = includePassword.Substring(0, placeIndex);
                afterStr = includePassword.Substring(placeIndex);
                includePassword = beforeStr + charInclude + afterStr;
            }
            return includePassword;
        } //Adds include values into the password
        //Database
        private void unlockDB()
        {
            //We test if a database exists and determine whether this is first run
            if (File.Exists(DBPath) == false)
            {
                GL.Text = "New User";
                string welcomeMessage;
                welcomeMessage = "Hi! It looks like you're new. Add an encryption password to begin using Pass Tools!";
                MessageBox.Show(welcomeMessage, "Welcome to 'Pass Tools'!");
            }
            else
            {
                GL.Text = "Login";
            }
        } //Determine if there is any existing database.
        private void readDB()
        {
            //Read the files
            XmlDocument DB = new XmlDocument();
            DB.Load(DBPath + ".tmp");
            XmlElement DBRoot = DB.DocumentElement;
            XmlNodeList DBNodes = DBRoot.SelectNodes("/Database/Password");
            try
            {
                foreach(XmlNode DBNode in DBNodes)
                {
                    PassWord addPassWord;
                    addPassWord.Name = DBNode["Name"].InnerText;
                    addPassWord.Password = DBNode["PassVal"].InnerText;
                    addPassWord.Details = DBNode["DetailVal"].InnerText;
                    PassWordList.Add(addPassWord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("This exception was caught: " + ex.ToString());
            }
            //Delete the files
            //System.IO.File.Delete(DBPath);
            System.IO.File.Delete(DBPath + ".tmp");
        } //Reads the database
        private void writeDB()
        {
            XmlWriterSettings writer_settings = new XmlWriterSettings();
            writer_settings.Indent = true;
            writer_settings.OmitXmlDeclaration = true;
            writer_settings.Encoding = Encoding.ASCII;
            using (XmlWriter writer = XmlWriter.Create(DBPath + ".tmp", writer_settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Database");//Database
                //----------
                foreach(PassWord existingPassWord in PassWordList)
                {
                    writer.WriteStartElement("Password");
                    writer.WriteElementString("Name", existingPassWord.Name);
                    writer.WriteElementString("PassVal", existingPassWord.Password);
                    writer.WriteElementString("DetailVal", existingPassWord.Details);
                    writer.WriteEndElement();
                }
                //----------
                writer.WriteEndElement();//Database
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
        } //Writes out the database
        private void lockDB()
        {
            //Delete previous database
            System.IO.File.Delete(DBPath);
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(userPassWord);
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(userPassWord.Substring(0, 16));
                    //aes.Padding = PaddingMode.None;
                    using (FileStream fsCrypt = new FileStream(DBPath, FileMode.Create))
                    {
                        Console.WriteLine("File created.");
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            Console.WriteLine("Encrypt mode initiated.");
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                Console.WriteLine("Encrypt stream initiated.");
                                using (FileStream fsIn = new FileStream(DBPath + ".tmp", FileMode.Open, FileAccess.Read))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                    Console.WriteLine("File encrypted.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("This exception was caught: " + ex.ToString());
                MessageBox.Show("An error occured while encrypting the database, please report the following error to the developer:" + System.Environment.NewLine + ex.ToString());
            }
        } //Encrypts the database
        //String testing
        public bool testStringREGEX(string testString, int regextype)
        {
            Regex r0REGEX = new Regex("^[a-zA-Z0-9]*$");
            Regex r1REGEX = new Regex("^[a-zA-Z]*$");
            Regex r2REGEX = new Regex("^[0-9]*$");
            Regex testREGEX;
            if (regextype == 3)
            {
                string[] testAlphabet = requestAlphabet();
                List<char> allowCharList = new List<char>();
                for (int loopVar = 0; loopVar < 51; loopVar++)
                {
                    allowCharList.Add(testAlphabet[loopVar][0]);
                    if (loopVar < 10)
                    {
                        allowCharList.Add(loopVar.ToString()[0]);
                    }
                }
                char[] symbols = new char[] {'!', '@', '#', '$', '%', '&', '*', '(', ')', '-', '_', '=', '+', ';', ':', ',', '.', '?' };
                foreach (char testChar in symbols)
                {
                    allowCharList.Add(testChar);
                }
                foreach (char testChar in testString) 
                {
                    if (allowCharList.IndexOf(testChar) == -1)
                    {
                        return false;
                    }
                }
                return true;
            }
            if(regextype == 0)
            {
                testREGEX = r0REGEX;
            }
            else if(regextype == 1)
            {
                testREGEX = r1REGEX;
            }
            else
            {
                testREGEX = r2REGEX;
            }
            if (testREGEX.IsMatch(testString) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        } //Test if a string matches a certain REGEX type
        public bool hasSpace(string testString)
        {
            for(int loopVar = 0; loopVar < testString.Length; loopVar++)
            {
                if (testString[loopVar] == ' ')
                {
                    return true;
                }
            }
            return false;
        } //Tests if a string has a space
        //Password
        public string modifyPassword(string password)
        {
            if (password.Length < 32)
            {
                while (password.Length < 32)
                {
                    password += password[0];
                }
            }
            else if (password.Length > 32)
            {
                password = password.Substring(0, 32);
            }
            return password;
        }
        #endregion

        #region "Event Handlers"
        private void btnPass_Click(object sender, EventArgs e)
        {
            gGen.Visible = false;
            gPass.Visible = true;
        }  //Switching to the password list

        private void btnGen_Click(object sender, EventArgs e)
        {
            gGen.Visible = true;
            gPass.Visible = false;
            PassListUpdate();
        } //Switching to the password generator

        private void passNSave_Click(object sender, EventArgs e)
        {
            string newName = passNName.Text;
            bool allowPassName = testStringREGEX(newName, 0);
            //Test if the password name already exists
            foreach (PassWord existingPassWord in PassWordList)
            {
                if (existingPassWord.Name == newName)
                {
                    MessageBox.Show("The password for " + existingPassWord.Name + " already exists!");
                    passNName.Focus();
                    passNName.SelectAll();
                    return;
                }
            }
            //We only want alphanumeric password names
            if(allowPassName == false)
            {
                MessageBox.Show("The password name must be alphanumeric!");
                passNName.Focus();
                passNName.SelectAll();
                return;
            }
            //Because all seems fine, we can add the new password
            PassWord newPassWord;
            newPassWord.Name = newName;
            newPassWord.Password = passNPass.Text;
            newPassWord.Details = passNDetails.Text;
            PassWordList.Add(newPassWord);
            updatePasswords();
            //Focus on new password in details
            cbPass.SelectedIndex = cbPass.Items.Count - 1;
        } //Saves a new password to the password list

        private void cbPass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbPass.SelectedIndex == 0)
            {
                clearChildren(gPassDetails);
                gPassDetails.Enabled = false;
                gPassNew.Enabled = true;
            }
            else
            {
                clearChildren(gPassNew);
                gPassDetails.Enabled = true;
                gPassNew.Enabled = false;
                //We load the selected password
                foreach(PassWord existingPassWord in PassWordList)
                {
                    if(cbPass.SelectedItem.ToString() == existingPassWord.Name)
                    {
                        passDName.Text = existingPassWord.Name;
                        passDPass.Text = existingPassWord.Password;
                        passDDetails.Text = existingPassWord.Details;
                        break; //No point to iterate over
                    }
                }
                
            }
        } //Switches to a selected password

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (PassWord existingPassWord in PassWordList)
            {
                if (cbPass.SelectedItem.ToString() == existingPassWord.Name)
                {
                    PassWordList.Remove(existingPassWord);
                    updatePasswords();
                    break;
                }
            }
        } //Removes a selected password

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PassWordList.Count <= 0)
            {
                return;
            }
            else
            {
                writeDB();
                lockDB();
                System.IO.File.Delete(DBPath + ".tmp");
                File.SetAttributes(DBPath, FileAttributes.Hidden);
            }
        } //When the form closes

        private void GLbtnGo_Click(object sender, EventArgs e)
        {
            userPassWord = modifyPassword(GLtxtPass.Text);
            if (File.Exists(DBPath) == false)
            {
                if(testStringREGEX(userPassWord, 3) == false)
                {
                    MessageBox.Show("Password must be alphanumeric and may only contain these symbols: !@#$%^&*()_+-=,.?;:", "Invalid password!");
                    loginFail();
                    return;
                }
            }
            else
            {
                try
                {
                    using (RijndaelManaged aes = new RijndaelManaged())
                    {
                        byte[] key = ASCIIEncoding.UTF8.GetBytes(userPassWord);
                        byte[] IV = ASCIIEncoding.UTF8.GetBytes(userPassWord.Substring(0, 16));
                        //aes.Padding = PaddingMode.None;
                        using (FileStream fsCrypt = new FileStream(DBPath, FileMode.Open, FileAccess.ReadWrite))
                        {
                            Console.WriteLine("Reading input file.");
                            using (FileStream fsOut = new FileStream(DBPath + ".tmp", FileMode.Create, FileAccess.ReadWrite))
                            {
                                Console.WriteLine("Creating output file.");
                                using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                                {
                                    Console.WriteLine("Decryption beginning.");
                                    using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                    {
                                        int data;
                                        while ((data = cs.ReadByte()) != -1)
                                        {
                                            fsOut.WriteByte((byte)data);
                                        }
                                        cs.Close();
                                        Console.WriteLine("Success!");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("This exception was caught: " + ex.ToString());
                    MessageBox.Show("Incorrect password!");
                    loginFail();
                    return;
                }
                readDB();
                updatePasswords();
            }
            loginSuccess();
        } //User login screen

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string aboutMessage = string.Empty;
            try
            {
                _assembly = Assembly.GetExecutingAssembly();
                _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("PassTools.about.txt"));
                try
                {
                    while (true)
                    {
                        if (_textStreamReader.Peek() != -1)
                        {
                            aboutMessage += _textStreamReader.ReadLine();
                            aboutMessage += Environment.NewLine;
                        }
                        else 
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("This exception was caught: {0}", ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("This exception was caught: {0}", ex.ToString());
                aboutMessage = "Pass Tools, a simple password generator & manager, Copyright (C) 2014  Timur Lavrenti Kiyivinski. This program is distributed under the GNU GPL, available at: https://www.gnu.org/copyleft/gpl.html.";
            }
            MessageBox.Show(aboutMessage, "Pass Tools Version 0.1");
        } //The about button

        private void btnInclude_Click(object sender, EventArgs e)
        {
            if (hasSpace(txtGInclude.Text) == false)
            {
                cbInclude.Items.Add(txtGInclude.Text);
                includeValues.Add(txtGInclude.Text);
                cbInclude.SelectedIndex = cbInclude.Items.Count - 1; //Focuses the include combobox on the newest added value
            }
            else
            {
                MessageBox.Show("Cannot include: '" + txtGInclude.Text + "'.");
                txtGInclude.Focus();
                txtGInclude.SelectAll();
            }
        } //Include values

        private void btnUninclude_Click(object sender, EventArgs e)
        {
            if (cbInclude.SelectedIndex > -1)
            {
                includeValues.RemoveAt(cbInclude.SelectedIndex);
                cbInclude.Items.RemoveAt(cbInclude.SelectedIndex);
                if (cbInclude.Items.Count != 0)
                {
                    cbInclude.SelectedIndex = 0;
                }
            }
        } //Uninclude values

        private void txtGLength_TextChanged(object sender, EventArgs e)
        {
            if (testStringREGEX(txtGLength.Text, 2) == false)
            {
                lblGLComment.Text = "Length must be numeric!";
            }
            else if (txtGLength.Text == string.Empty)
            {
                lblGLComment.Text = "...";
            }
            else
            {
                double passwordLength = Convert.ToDouble(txtGLength.Text.ToString());
                if (passwordLength == 0)
                {
                    lblGLComment.Text = "Password cannot be nothing";
                }
                else if (passwordLength < 8)
                {
                    lblGLComment.Text = "Short password (discouraged)";
                }
                else if (passwordLength < 12)
                {
                    lblGLComment.Text = "Average password";
                }
                else
                {
                    if (passwordLength == 69)
                    {
                        lblGLComment.Text = "Cheeky bastard ;)";
                    }
                    else
                    {
                        lblGLComment.Text = "Long password (recommended)";
                    }
                }
            }
        } //Updates comments based on desired password length

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            //Test password seed
            if (hasSpace(txtGPassSeed.Text) == true)
            {
                MessageBox.Show("Password seed cannot contain any whitespaces!");
            }
            else if (testStringREGEX(txtGPassSeed.Text, 0) == false)
            {
                MessageBox.Show("Password seed must be alphanumeric!");
            }
            else if(txtGPassSeed.Text == string.Empty)
            {
                MessageBox.Show("Password seed cannot be empty!");
            }
            else
            {
                //Test password length
                if (hasSpace(txtGLength.Text) == true)
                {
                    MessageBox.Show("Length cannot contain any whitespaces!");
                    return;
                }
                else if (testStringREGEX(txtGLength.Text, 2) == false)
                {
                    MessageBox.Show("Length must be numeric!");
                    return;
                }
                else if (txtGLength.Text == string.Empty)
                {
                    MessageBox.Show("Length cannot be nothing!");
                    return;
                }
                //Test length of include values
                if (includeLength() >= Convert.ToDouble(txtGLength.Text.ToString()))
                {
                    MessageBox.Show("Too many include values!");
                    return;
                }
                //Generate the password
                string generatedPassword = string.Empty;
                switch (cbAlgorithm.SelectedIndex)
                {
                    case 0:
                        generatedPassword = genSimple(txtGPassSeed.Text, Convert.ToDouble(txtGLength.Text.ToString()));
                        break;
                    case 1:
                        generatedPassword = genComplex(txtGPassSeed.Text, Convert.ToDouble(txtGLength.Text.ToString()));
                        break;
                    default:
                        Console.WriteLine("If you see this line, you should reconsider living.");
                        break;
                }
                //Add include values
                generatedPassword = addInclude(generatedPassword, generatedPassword.Length);
                txtGOutputPassword.Text = generatedPassword;
            }
        } //Generates the password

        private void btnTips_Click(object sender, EventArgs e)
        {
            List<string> tipsList = new List<string>();
            tipsList.Add("Try making super long passwords. The longer they are, the harder to crack.");
            tipsList.Add("Avoid using common words in passwords or things people would associate with you.");
            tipsList.Add("An easy way to remember passwords is to only remember a random sequence like A&Kl_oP@. For every site you use, you could just do something like A&Kl_oP@mysite.");
            tipsList.Add("Use a password manager to remember! Like this.");
            tipsList.Add("Use different passwords for each site. Keep your hardest passwords for mission critical sites like your email or payment sites.");
            tipsList.Add("Use two-step authentication on sites that offer it. In this case, you will receive an SMS with a code every time someone logs onto your account.");
            tipsList.Add("Whenever possible, use other services to log into a site so you wouldn't have to create yet another password.");
            Random randGen = new Random();
            int someTip = randGen.Next(0, 6);
            MessageBox.Show(tipsList[someTip]);
        } //Gives password tips

        private void btnCopy_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(txtGOutputPassword.Text);
        } //Copies password to clipboard

        private void txtEnter(object sender, EventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            senderTextBox.Focus();
            senderTextBox.SelectAll();
        } //Highlights text in selected textbox

        private void btnCopyGen_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(passDPass.Text);
        } //Copies generated password to clipboard
        #endregion
    }
}
