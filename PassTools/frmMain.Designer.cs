namespace PassTools
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnPass = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.gPass = new System.Windows.Forms.GroupBox();
            this.gPassNew = new System.Windows.Forms.GroupBox();
            this.passNSave = new System.Windows.Forms.Button();
            this.passNPass = new System.Windows.Forms.TextBox();
            this.passNName = new System.Windows.Forms.TextBox();
            this.passNDetails = new System.Windows.Forms.TextBox();
            this.lblNDetails = new System.Windows.Forms.Label();
            this.lblNPassword = new System.Windows.Forms.Label();
            this.lblNName = new System.Windows.Forms.Label();
            this.gPassDetails = new System.Windows.Forms.GroupBox();
            this.btnCopyGen = new System.Windows.Forms.Button();
            this.passDDetails = new System.Windows.Forms.TextBox();
            this.lblDDetails = new System.Windows.Forms.Label();
            this.passDPass = new System.Windows.Forms.Label();
            this.lblDPass = new System.Windows.Forms.Label();
            this.passDName = new System.Windows.Forms.Label();
            this.lblDName = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbPass = new System.Windows.Forms.ComboBox();
            this.GL = new System.Windows.Forms.GroupBox();
            this.GLbtnGo = new System.Windows.Forms.Button();
            this.GLlblPass = new System.Windows.Forms.Label();
            this.GLtxtPass = new System.Windows.Forms.TextBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.gGen = new System.Windows.Forms.GroupBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnTips = new System.Windows.Forms.Button();
            this.txtGOutputPassword = new System.Windows.Forms.TextBox();
            this.lblGPassword = new System.Windows.Forms.Label();
            this.lblGLComment = new System.Windows.Forms.Label();
            this.btnGeneratePassword = new System.Windows.Forms.Button();
            this.txtGLength = new System.Windows.Forms.TextBox();
            this.lblGLength = new System.Windows.Forms.Label();
            this.btnUninclude = new System.Windows.Forms.Button();
            this.btnInclude = new System.Windows.Forms.Button();
            this.cbInclude = new System.Windows.Forms.ComboBox();
            this.txtGInclude = new System.Windows.Forms.TextBox();
            this.lblGInclude = new System.Windows.Forms.Label();
            this.txtGPassSeed = new System.Windows.Forms.TextBox();
            this.lblGSeed = new System.Windows.Forms.Label();
            this.cbAlgorithm = new System.Windows.Forms.ComboBox();
            this.lblGAlgorithm = new System.Windows.Forms.Label();
            this.gPass.SuspendLayout();
            this.gPassNew.SuspendLayout();
            this.gPassDetails.SuspendLayout();
            this.GL.SuspendLayout();
            this.gGen.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPass
            // 
            this.btnPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPass.Location = new System.Drawing.Point(12, 12);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(140, 23);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Passwords";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // btnGen
            // 
            this.btnGen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGen.Location = new System.Drawing.Point(158, 12);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(214, 23);
            this.btnGen.TabIndex = 1;
            this.btnGen.Text = "Password Generator";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // gPass
            // 
            this.gPass.Controls.Add(this.gPassNew);
            this.gPass.Controls.Add(this.gPassDetails);
            this.gPass.Controls.Add(this.btnRemove);
            this.gPass.Controls.Add(this.cbPass);
            this.gPass.Location = new System.Drawing.Point(12, 41);
            this.gPass.Name = "gPass";
            this.gPass.Size = new System.Drawing.Size(420, 309);
            this.gPass.TabIndex = 3;
            this.gPass.TabStop = false;
            this.gPass.Text = "Passwords";
            // 
            // gPassNew
            // 
            this.gPassNew.Controls.Add(this.passNSave);
            this.gPassNew.Controls.Add(this.passNPass);
            this.gPassNew.Controls.Add(this.passNName);
            this.gPassNew.Controls.Add(this.passNDetails);
            this.gPassNew.Controls.Add(this.lblNDetails);
            this.gPassNew.Controls.Add(this.lblNPassword);
            this.gPassNew.Controls.Add(this.lblNName);
            this.gPassNew.Location = new System.Drawing.Point(212, 46);
            this.gPassNew.Name = "gPassNew";
            this.gPassNew.Size = new System.Drawing.Size(200, 257);
            this.gPassNew.TabIndex = 4;
            this.gPassNew.TabStop = false;
            this.gPassNew.Text = "New Password";
            // 
            // passNSave
            // 
            this.passNSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.passNSave.Location = new System.Drawing.Point(9, 228);
            this.passNSave.Name = "passNSave";
            this.passNSave.Size = new System.Drawing.Size(185, 23);
            this.passNSave.TabIndex = 6;
            this.passNSave.Text = "Save";
            this.passNSave.UseVisualStyleBackColor = true;
            this.passNSave.Click += new System.EventHandler(this.passNSave_Click);
            // 
            // passNPass
            // 
            this.passNPass.Location = new System.Drawing.Point(68, 39);
            this.passNPass.Name = "passNPass";
            this.passNPass.Size = new System.Drawing.Size(126, 20);
            this.passNPass.TabIndex = 3;
            this.passNPass.Text = "...";
            this.passNPass.Click += new System.EventHandler(this.txtEnter);
            this.passNPass.Enter += new System.EventHandler(this.txtEnter);
            // 
            // passNName
            // 
            this.passNName.Location = new System.Drawing.Point(68, 13);
            this.passNName.Name = "passNName";
            this.passNName.Size = new System.Drawing.Size(126, 20);
            this.passNName.TabIndex = 1;
            this.passNName.Text = "...";
            this.passNName.Click += new System.EventHandler(this.txtEnter);
            this.passNName.Enter += new System.EventHandler(this.txtEnter);
            // 
            // passNDetails
            // 
            this.passNDetails.Location = new System.Drawing.Point(9, 79);
            this.passNDetails.Multiline = true;
            this.passNDetails.Name = "passNDetails";
            this.passNDetails.Size = new System.Drawing.Size(185, 143);
            this.passNDetails.TabIndex = 5;
            this.passNDetails.Text = "...";
            this.passNDetails.Click += new System.EventHandler(this.txtEnter);
            this.passNDetails.Enter += new System.EventHandler(this.txtEnter);
            // 
            // lblNDetails
            // 
            this.lblNDetails.AutoSize = true;
            this.lblNDetails.Location = new System.Drawing.Point(6, 63);
            this.lblNDetails.Name = "lblNDetails";
            this.lblNDetails.Size = new System.Drawing.Size(42, 13);
            this.lblNDetails.TabIndex = 4;
            this.lblNDetails.Text = "Details:";
            // 
            // lblNPassword
            // 
            this.lblNPassword.AutoSize = true;
            this.lblNPassword.Location = new System.Drawing.Point(6, 42);
            this.lblNPassword.Name = "lblNPassword";
            this.lblNPassword.Size = new System.Drawing.Size(56, 13);
            this.lblNPassword.TabIndex = 2;
            this.lblNPassword.Text = "Password:";
            // 
            // lblNName
            // 
            this.lblNName.AutoSize = true;
            this.lblNName.Location = new System.Drawing.Point(6, 16);
            this.lblNName.Name = "lblNName";
            this.lblNName.Size = new System.Drawing.Size(38, 13);
            this.lblNName.TabIndex = 0;
            this.lblNName.Text = "Name:";
            // 
            // gPassDetails
            // 
            this.gPassDetails.Controls.Add(this.btnCopyGen);
            this.gPassDetails.Controls.Add(this.passDDetails);
            this.gPassDetails.Controls.Add(this.lblDDetails);
            this.gPassDetails.Controls.Add(this.passDPass);
            this.gPassDetails.Controls.Add(this.lblDPass);
            this.gPassDetails.Controls.Add(this.passDName);
            this.gPassDetails.Controls.Add(this.lblDName);
            this.gPassDetails.Location = new System.Drawing.Point(6, 46);
            this.gPassDetails.Name = "gPassDetails";
            this.gPassDetails.Size = new System.Drawing.Size(200, 257);
            this.gPassDetails.TabIndex = 3;
            this.gPassDetails.TabStop = false;
            this.gPassDetails.Text = "Password Details";
            // 
            // btnCopyGen
            // 
            this.btnCopyGen.Location = new System.Drawing.Point(9, 63);
            this.btnCopyGen.Name = "btnCopyGen";
            this.btnCopyGen.Size = new System.Drawing.Size(75, 23);
            this.btnCopyGen.TabIndex = 6;
            this.btnCopyGen.Text = "Copy";
            this.btnCopyGen.UseVisualStyleBackColor = true;
            this.btnCopyGen.Click += new System.EventHandler(this.btnCopyGen_Click);
            // 
            // passDDetails
            // 
            this.passDDetails.Location = new System.Drawing.Point(9, 108);
            this.passDDetails.Multiline = true;
            this.passDDetails.Name = "passDDetails";
            this.passDDetails.ReadOnly = true;
            this.passDDetails.Size = new System.Drawing.Size(185, 143);
            this.passDDetails.TabIndex = 5;
            this.passDDetails.Text = "...";
            // 
            // lblDDetails
            // 
            this.lblDDetails.AutoSize = true;
            this.lblDDetails.Location = new System.Drawing.Point(6, 89);
            this.lblDDetails.Name = "lblDDetails";
            this.lblDDetails.Size = new System.Drawing.Size(42, 13);
            this.lblDDetails.TabIndex = 4;
            this.lblDDetails.Text = "Details:";
            // 
            // passDPass
            // 
            this.passDPass.AutoSize = true;
            this.passDPass.Location = new System.Drawing.Point(68, 42);
            this.passDPass.Name = "passDPass";
            this.passDPass.Size = new System.Drawing.Size(16, 13);
            this.passDPass.TabIndex = 3;
            this.passDPass.Text = "...";
            this.passDPass.UseMnemonic = false;
            // 
            // lblDPass
            // 
            this.lblDPass.AutoSize = true;
            this.lblDPass.Location = new System.Drawing.Point(6, 42);
            this.lblDPass.Name = "lblDPass";
            this.lblDPass.Size = new System.Drawing.Size(56, 13);
            this.lblDPass.TabIndex = 2;
            this.lblDPass.Text = "Password:";
            // 
            // passDName
            // 
            this.passDName.AutoSize = true;
            this.passDName.Location = new System.Drawing.Point(68, 16);
            this.passDName.Name = "passDName";
            this.passDName.Size = new System.Drawing.Size(16, 13);
            this.passDName.TabIndex = 1;
            this.passDName.Text = "...";
            // 
            // lblDName
            // 
            this.lblDName.AutoSize = true;
            this.lblDName.Location = new System.Drawing.Point(6, 16);
            this.lblDName.Name = "lblDName";
            this.lblDName.Size = new System.Drawing.Size(38, 13);
            this.lblDName.TabIndex = 0;
            this.lblDName.Text = "Name:";
            // 
            // btnRemove
            // 
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(339, 19);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(73, 21);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cbPass
            // 
            this.cbPass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPass.FormattingEnabled = true;
            this.cbPass.Items.AddRange(new object[] {
            "Passwords"});
            this.cbPass.Location = new System.Drawing.Point(6, 19);
            this.cbPass.Name = "cbPass";
            this.cbPass.Size = new System.Drawing.Size(327, 21);
            this.cbPass.TabIndex = 0;
            this.cbPass.SelectedIndexChanged += new System.EventHandler(this.cbPass_SelectedIndexChanged);
            // 
            // GL
            // 
            this.GL.Controls.Add(this.GLbtnGo);
            this.GL.Controls.Add(this.GLlblPass);
            this.GL.Controls.Add(this.GLtxtPass);
            this.GL.Location = new System.Drawing.Point(12, 12);
            this.GL.Name = "GL";
            this.GL.Size = new System.Drawing.Size(420, 338);
            this.GL.TabIndex = 5;
            this.GL.TabStop = false;
            this.GL.Text = "Login";
            // 
            // GLbtnGo
            // 
            this.GLbtnGo.Location = new System.Drawing.Point(68, 159);
            this.GLbtnGo.Name = "GLbtnGo";
            this.GLbtnGo.Size = new System.Drawing.Size(75, 23);
            this.GLbtnGo.TabIndex = 2;
            this.GLbtnGo.Text = "Let\'s Begin";
            this.GLbtnGo.UseVisualStyleBackColor = true;
            this.GLbtnGo.Click += new System.EventHandler(this.GLbtnGo_Click);
            // 
            // GLlblPass
            // 
            this.GLlblPass.AutoSize = true;
            this.GLlblPass.Location = new System.Drawing.Point(6, 136);
            this.GLlblPass.Name = "GLlblPass";
            this.GLlblPass.Size = new System.Drawing.Size(56, 13);
            this.GLlblPass.TabIndex = 1;
            this.GLlblPass.Text = "Password:";
            // 
            // GLtxtPass
            // 
            this.GLtxtPass.Location = new System.Drawing.Point(68, 133);
            this.GLtxtPass.Name = "GLtxtPass";
            this.GLtxtPass.PasswordChar = 'X';
            this.GLtxtPass.Size = new System.Drawing.Size(338, 20);
            this.GLtxtPass.TabIndex = 0;
            this.GLtxtPass.Enter += new System.EventHandler(this.txtEnter);
            // 
            // btnAbout
            // 
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Location = new System.Drawing.Point(378, 12);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(54, 23);
            this.btnAbout.TabIndex = 2;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // gGen
            // 
            this.gGen.Controls.Add(this.btnCopy);
            this.gGen.Controls.Add(this.btnTips);
            this.gGen.Controls.Add(this.txtGOutputPassword);
            this.gGen.Controls.Add(this.lblGPassword);
            this.gGen.Controls.Add(this.lblGLComment);
            this.gGen.Controls.Add(this.btnGeneratePassword);
            this.gGen.Controls.Add(this.txtGLength);
            this.gGen.Controls.Add(this.lblGLength);
            this.gGen.Controls.Add(this.btnUninclude);
            this.gGen.Controls.Add(this.btnInclude);
            this.gGen.Controls.Add(this.cbInclude);
            this.gGen.Controls.Add(this.txtGInclude);
            this.gGen.Controls.Add(this.lblGInclude);
            this.gGen.Controls.Add(this.txtGPassSeed);
            this.gGen.Controls.Add(this.lblGSeed);
            this.gGen.Controls.Add(this.cbAlgorithm);
            this.gGen.Controls.Add(this.lblGAlgorithm);
            this.gGen.Location = new System.Drawing.Point(12, 41);
            this.gGen.Name = "gGen";
            this.gGen.Size = new System.Drawing.Size(420, 309);
            this.gGen.TabIndex = 4;
            this.gGen.TabStop = false;
            this.gGen.Text = "Generate a Password";
            this.gGen.Visible = false;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(369, 180);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(45, 23);
            this.btnCopy.TabIndex = 20;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnTips
            // 
            this.btnTips.Location = new System.Drawing.Point(369, 17);
            this.btnTips.Name = "btnTips";
            this.btnTips.Size = new System.Drawing.Size(45, 23);
            this.btnTips.TabIndex = 19;
            this.btnTips.Text = "?";
            this.btnTips.UseVisualStyleBackColor = true;
            this.btnTips.Click += new System.EventHandler(this.btnTips_Click);
            // 
            // txtGOutputPassword
            // 
            this.txtGOutputPassword.Location = new System.Drawing.Point(113, 183);
            this.txtGOutputPassword.Name = "txtGOutputPassword";
            this.txtGOutputPassword.Size = new System.Drawing.Size(248, 20);
            this.txtGOutputPassword.TabIndex = 18;
            this.txtGOutputPassword.Click += new System.EventHandler(this.txtEnter);
            this.txtGOutputPassword.Enter += new System.EventHandler(this.txtEnter);
            // 
            // lblGPassword
            // 
            this.lblGPassword.AutoSize = true;
            this.lblGPassword.Location = new System.Drawing.Point(51, 185);
            this.lblGPassword.Name = "lblGPassword";
            this.lblGPassword.Size = new System.Drawing.Size(56, 13);
            this.lblGPassword.TabIndex = 17;
            this.lblGPassword.Text = "Password:";
            // 
            // lblGLComment
            // 
            this.lblGLComment.AutoSize = true;
            this.lblGLComment.Location = new System.Drawing.Point(240, 131);
            this.lblGLComment.Name = "lblGLComment";
            this.lblGLComment.Size = new System.Drawing.Size(16, 13);
            this.lblGLComment.TabIndex = 16;
            this.lblGLComment.Text = "...";
            // 
            // btnGeneratePassword
            // 
            this.btnGeneratePassword.Location = new System.Drawing.Point(113, 154);
            this.btnGeneratePassword.Name = "btnGeneratePassword";
            this.btnGeneratePassword.Size = new System.Drawing.Size(248, 23);
            this.btnGeneratePassword.TabIndex = 15;
            this.btnGeneratePassword.Text = "Generate Password";
            this.btnGeneratePassword.UseVisualStyleBackColor = true;
            this.btnGeneratePassword.Click += new System.EventHandler(this.btnGeneratePassword_Click);
            // 
            // txtGLength
            // 
            this.txtGLength.Location = new System.Drawing.Point(113, 128);
            this.txtGLength.Name = "txtGLength";
            this.txtGLength.Size = new System.Drawing.Size(121, 20);
            this.txtGLength.TabIndex = 14;
            this.txtGLength.Click += new System.EventHandler(this.txtEnter);
            this.txtGLength.TextChanged += new System.EventHandler(this.txtGLength_TextChanged);
            this.txtGLength.Enter += new System.EventHandler(this.txtEnter);
            // 
            // lblGLength
            // 
            this.lblGLength.AutoSize = true;
            this.lblGLength.Location = new System.Drawing.Point(64, 131);
            this.lblGLength.Name = "lblGLength";
            this.lblGLength.Size = new System.Drawing.Size(43, 13);
            this.lblGLength.TabIndex = 13;
            this.lblGLength.Text = "Length:";
            // 
            // btnUninclude
            // 
            this.btnUninclude.Location = new System.Drawing.Point(240, 99);
            this.btnUninclude.Name = "btnUninclude";
            this.btnUninclude.Size = new System.Drawing.Size(121, 23);
            this.btnUninclude.TabIndex = 10;
            this.btnUninclude.Text = "Remove";
            this.btnUninclude.UseVisualStyleBackColor = true;
            this.btnUninclude.Click += new System.EventHandler(this.btnUninclude_Click);
            // 
            // btnInclude
            // 
            this.btnInclude.Location = new System.Drawing.Point(113, 99);
            this.btnInclude.Name = "btnInclude";
            this.btnInclude.Size = new System.Drawing.Size(121, 23);
            this.btnInclude.TabIndex = 9;
            this.btnInclude.Text = "Add";
            this.btnInclude.UseVisualStyleBackColor = true;
            this.btnInclude.Click += new System.EventHandler(this.btnInclude_Click);
            // 
            // cbInclude
            // 
            this.cbInclude.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInclude.FormattingEnabled = true;
            this.cbInclude.Location = new System.Drawing.Point(240, 72);
            this.cbInclude.Name = "cbInclude";
            this.cbInclude.Size = new System.Drawing.Size(121, 21);
            this.cbInclude.TabIndex = 8;
            // 
            // txtGInclude
            // 
            this.txtGInclude.Location = new System.Drawing.Point(113, 73);
            this.txtGInclude.Name = "txtGInclude";
            this.txtGInclude.Size = new System.Drawing.Size(121, 20);
            this.txtGInclude.TabIndex = 7;
            this.txtGInclude.Click += new System.EventHandler(this.txtEnter);
            this.txtGInclude.Enter += new System.EventHandler(this.txtEnter);
            // 
            // lblGInclude
            // 
            this.lblGInclude.AutoSize = true;
            this.lblGInclude.Location = new System.Drawing.Point(32, 75);
            this.lblGInclude.Name = "lblGInclude";
            this.lblGInclude.Size = new System.Drawing.Size(75, 13);
            this.lblGInclude.TabIndex = 6;
            this.lblGInclude.Text = "Include Value:";
            // 
            // txtGPassSeed
            // 
            this.txtGPassSeed.Location = new System.Drawing.Point(113, 46);
            this.txtGPassSeed.Name = "txtGPassSeed";
            this.txtGPassSeed.Size = new System.Drawing.Size(248, 20);
            this.txtGPassSeed.TabIndex = 3;
            this.txtGPassSeed.Click += new System.EventHandler(this.txtEnter);
            this.txtGPassSeed.Enter += new System.EventHandler(this.txtEnter);
            // 
            // lblGSeed
            // 
            this.lblGSeed.AutoSize = true;
            this.lblGSeed.Location = new System.Drawing.Point(20, 49);
            this.lblGSeed.Name = "lblGSeed";
            this.lblGSeed.Size = new System.Drawing.Size(84, 13);
            this.lblGSeed.TabIndex = 2;
            this.lblGSeed.Text = "Password Seed:";
            // 
            // cbAlgorithm
            // 
            this.cbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlgorithm.FormattingEnabled = true;
            this.cbAlgorithm.Location = new System.Drawing.Point(113, 19);
            this.cbAlgorithm.Name = "cbAlgorithm";
            this.cbAlgorithm.Size = new System.Drawing.Size(248, 21);
            this.cbAlgorithm.TabIndex = 1;
            // 
            // lblGAlgorithm
            // 
            this.lblGAlgorithm.AutoSize = true;
            this.lblGAlgorithm.Location = new System.Drawing.Point(54, 23);
            this.lblGAlgorithm.Name = "lblGAlgorithm";
            this.lblGAlgorithm.Size = new System.Drawing.Size(53, 13);
            this.lblGAlgorithm.TabIndex = 0;
            this.lblGAlgorithm.Text = "Algorithm:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 362);
            this.Controls.Add(this.GL);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.btnPass);
            this.Controls.Add(this.gPass);
            this.Controls.Add(this.gGen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pass Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.gPass.ResumeLayout(false);
            this.gPassNew.ResumeLayout(false);
            this.gPassNew.PerformLayout();
            this.gPassDetails.ResumeLayout(false);
            this.gPassDetails.PerformLayout();
            this.GL.ResumeLayout(false);
            this.GL.PerformLayout();
            this.gGen.ResumeLayout(false);
            this.gGen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.GroupBox gPass;
        private System.Windows.Forms.GroupBox gPassNew;
        private System.Windows.Forms.TextBox passNPass;
        private System.Windows.Forms.TextBox passNName;
        private System.Windows.Forms.TextBox passNDetails;
        private System.Windows.Forms.Label lblNDetails;
        private System.Windows.Forms.Label lblNPassword;
        private System.Windows.Forms.Label lblNName;
        private System.Windows.Forms.GroupBox gPassDetails;
        private System.Windows.Forms.TextBox passDDetails;
        private System.Windows.Forms.Label lblDDetails;
        private System.Windows.Forms.Label passDPass;
        private System.Windows.Forms.Label lblDPass;
        private System.Windows.Forms.Label passDName;
        private System.Windows.Forms.Label lblDName;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cbPass;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox gGen;
        private System.Windows.Forms.Button btnUninclude;
        private System.Windows.Forms.Button btnInclude;
        private System.Windows.Forms.ComboBox cbInclude;
        private System.Windows.Forms.TextBox txtGInclude;
        private System.Windows.Forms.Label lblGInclude;
        private System.Windows.Forms.TextBox txtGPassSeed;
        private System.Windows.Forms.Label lblGSeed;
        private System.Windows.Forms.ComboBox cbAlgorithm;
        private System.Windows.Forms.Label lblGAlgorithm;
        private System.Windows.Forms.TextBox txtGLength;
        private System.Windows.Forms.Label lblGLength;
        private System.Windows.Forms.Label lblGLComment;
        private System.Windows.Forms.Button btnGeneratePassword;
        private System.Windows.Forms.TextBox txtGOutputPassword;
        private System.Windows.Forms.Label lblGPassword;
        private System.Windows.Forms.Button passNSave;
        private System.Windows.Forms.GroupBox GL;
        private System.Windows.Forms.TextBox GLtxtPass;
        private System.Windows.Forms.Button GLbtnGo;
        private System.Windows.Forms.Label GLlblPass;
        private System.Windows.Forms.Button btnTips;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCopyGen;
    }
}

