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
        public List<string> PasswordAlgorithm = new List<string>();
        public List<string> PasswordFunction_Random = new List<string>();
        public List<string> PasswordFunction_Linear = new List<string>();
        public List<string> PasswordFunction_Embed = new List<string>();
        public List<string> PasswordFunction_SimpleHint = new List<string>();
        public List<string> PasswordFunction_ComplexHint = new List<string>();
        public List<string> PasswordDifficulty = new List<string>();
        public string DBPath = Directory.GetCurrentDirectory() + "\\passDB";
        public string userPassWord;
        #endregion
        public frmMain()
        {
            //PassListUpdate();
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
        private void PassListUpdate()
        {
            //Algorithm List
            PasswordAlgorithm.Add("Random");
            PasswordAlgorithm.Add("Linear");
            PasswordAlgorithm.Add("Embed");
            PasswordAlgorithm.Add("Simple Hint");
            PasswordAlgorithm.Add("Complex Hint");
            //Random
            PasswordFunction_Random.Add("Alphabetic");
            PasswordFunction_Random.Add("Numeric");
            PasswordFunction_Random.Add("AlphaNumeric");
            //Linear
            PasswordFunction_Linear.Add("Alphabetic");
            PasswordFunction_Linear.Add("Numeric");
            PasswordFunction_Linear.Add("AlphaNumeric");
            //Embed
            PasswordFunction_Embed.Add("Front");
            PasswordFunction_Embed.Add("Back");
            PasswordFunction_Embed.Add("Middle");
            //Simple Hint
            PasswordFunction_SimpleHint.Add("Split");
            PasswordFunction_SimpleHint.Add("Block");
            //Complex Hint
            PasswordFunction_ComplexHint.Add("Trash");
            PasswordFunction_ComplexHint.Add("Split");
            PasswordFunction_ComplexHint.Add("Random");
            //Difficulty
            PasswordDifficulty.Add("Easy");
            PasswordDifficulty.Add("Moderate");
            PasswordDifficulty.Add("Hard");
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
                GLtxtPass.Focus();
            }
            else
            {
                GL.Text = "Login";
            }
        }
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
        }
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
        }
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
        }
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
        #endregion

        private void btnPass_Click(object sender, EventArgs e)
        {
            gGen.Visible = false;
            gPass.Visible = true;
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This section is not yet complete as per alpha release!", "Error");
            return;
            //gGen.Visible = true;
            //gPass.Visible = false;
        }

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
        }

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
        }

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
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Alpha 1 - Timur Lavrenti Kiyivinski 2014", "Pass Tools");
        }

    }
}
