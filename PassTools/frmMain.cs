using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            string[] alphabet = new string[26] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            return alphabet;
        }
        private void PassListUpdate()
        {
            cbAlgorithm.Items.Clear();
            cbAlgorithm.Items.Add("Simple");
            cbAlgorithm.Items.Add("Complex");
            cbAlgorithm.SelectedIndex = 0;
        } //Updates the password list
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
                if (sequenceSeed > 25)
                {
                    sequenceSeed -= 25;
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
            for (int loopVar = 0; loopVar < 26; loopVar++ )
            {
                sequenceSeed = randomGen.Next(0, 26);
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
        private string addInclude(string generatedPassword, double passwordLength)
        {
            return generatedPassword;
        }
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
        } //Decrypts the database
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
            System.IO.File.Delete(DBPath);
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
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(userPassWord);
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes("1234567890qwerty");
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
        public bool testString(string testString, int regextype)
        {
            Regex r0REGEX = new Regex("^[a-zA-Z0-9]*$");
            Regex r1REGEX = new Regex("^[a-zA-Z]*$");
            Regex r2REGEX = new Regex("^[0-9]*$");
            Regex testREGEX;
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
        }
        #endregion

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
            bool allowPassName = testString(newName, 0);
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
            }
        } //When the form closes

        private void GLbtnGo_Click(object sender, EventArgs e)
        {
            userPassWord = GLtxtPass.Text;
            if (File.Exists(DBPath) == false)
            {
                if(userPassWord.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters!");
                    loginFail();
                    return;
                }
                if(testString(userPassWord, 0) == false)
                {
                    MessageBox.Show("Password must be alphanumeric!");
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
                        byte[] IV = ASCIIEncoding.UTF8.GetBytes("1234567890qwerty");
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
            MessageBox.Show("Alpha 2 - Timur Lavrenti Kiyivinski 2014", "Pass Tools");
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
        }

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
        }

        private void txtGLength_TextChanged(object sender, EventArgs e)
        {
            if (testString(txtGLength.Text, 2) == false)
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
                    lblGLComment.Text = "Long password (recommended)";
                }
            }
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            //Test password seed
            if (hasSpace(txtGPassSeed.Text) == true)
            {
                MessageBox.Show("Password seed cannot contain any whitespaces!");
            }
            else if (testString(txtGPassSeed.Text, 0) == false)
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
                }
                else if (testString(txtGLength.Text, 2) == false)
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
                string includeLength = string.Empty;
                foreach (string includeString in includeValues)
                {
                    includeLength += includeString;
                }
                if (includeLength.Length >= Convert.ToDouble(txtGLength.Text.ToString()))
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
                txtGOutputPassword.Text = generatedPassword;
            }
        } 

    }
}
