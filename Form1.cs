using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class frmNotepad : Form
    {
        /* 布尔变量b用于判断文件是新建的还是从磁盘打开的，
   true表示文件是从磁盘打开的，false表示文件是新建的，默认值为false*/
        bool b = false;
        /* 布尔变量s用于判断文件件是否被保存，
           true表示文件是已经被保存了，false表示文件未被保存，默认值为true*/
        bool s = true;
        public frmNotepad()
        {
            InitializeComponent();
        }

        private void 打开0CtrlOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (b == true || rtxtNotepad.Text.Trim() != "")
            {
                if (s == false)
                {
                    string result;
                    result = MessageBox.Show("文件尚未保存,是否保存?","保存文件", MessageBoxButtons.YesNoCancel).ToString();
                    switch (result)
                    {
                        case "Yes":
                            if (b == true)
                            {
                                rtxtNotepad.SaveFile(odlgNotepad.FileName);
                            }
                            else if (sdlgNotepad.ShowDialog() == DialogResult.OK)
                            {
                                rtxtNotepad.SaveFile(sdlgNotepad.FileName);
                            }
                            s = true;
                            break;
                        case "No":
                            b = false;
                            rtxtNotepad.Text = "";
                            break;
                    }
                }
            }
            odlgNotepad.RestoreDirectory = true;
            if ((odlgNotepad.ShowDialog() == DialogResult.OK) && odlgNotepad.FileName != "")
            {
                rtxtNotepad.LoadFile(odlgNotepad.FileName);//打开代码语句
                b = true;
            }
            s = true;
        }

        private void 保存SCtrlSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (b == true && rtxtNotepad.Modified == true)
            {
                rtxtNotepad.SaveFile(odlgNotepad.FileName);
                s = true;
            }
            else if (b == false && rtxtNotepad.Text.Trim() != "" &&
                sdlgNotepad.ShowDialog() == DialogResult.OK)
            {
                rtxtNotepad.SaveFile(sdlgNotepad.FileName);//保存语句
                s = true;
                b = true;
                odlgNotepad.FileName = sdlgNotepad.FileName;
            }
        }

        private void tsmiNew_Click(object sender, EventArgs e)
        {
            // 判断当前文件是否从磁盘打开，或者新建时文档不为空，并且文件未被保存
            if (b == true || rtxtNotepad.Text.Trim() != "")
            {
                // 若文件未保存
                if (s == false)
                {
                    string result;
                    result = MessageBox.Show("文件尚未保存,是否保存?",
                        "保存文件", MessageBoxButtons.YesNoCancel).ToString();
                    switch (result)
                    {
                        case "Yes":
                            // 若文件是从磁盘打开的
                            if (b == true)
                            {
                                // 按文件打开的路径保存文件
                                rtxtNotepad.SaveFile(odlgNotepad.FileName);
                            }
                            // 若文件不是从磁盘打开的
                            else if (sdlgNotepad.ShowDialog() == DialogResult.OK)
                            {
                                rtxtNotepad.SaveFile(sdlgNotepad.FileName);
                            }
                            s = true;
                            rtxtNotepad.Text = "";
                            break;
                        case "No":
                            b = false;
                            rtxtNotepad.Text = "";
                            break;
                    }
                }
            }
        }

        private void 黏贴PCtrlPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtNotepad.Paste();//粘贴
        }

        private void tssLbl2_Click(object sender, EventArgs e)
        {

        }

        private void tssLbl1_Click(object sender, EventArgs e)
        {

        }

        private void rtxtNotepad_TextChanged(object sender, EventArgs e)
        {
            s = false;
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            tsmiNew_Click(sender, e);
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            打开0CtrlOToolStripMenuItem_Click(sender, e);
        }

        private void tsmiSaveAs_Click(object sender, EventArgs e)
        {
            if (sdlgNotepad.ShowDialog() == DialogResult.OK)
            {
                rtxtNotepad.SaveFile(sdlgNotepad.FileName);
                s = true;
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            Application.Exit();//程序结束
        }

        private void tsmiUndo_Click(object sender, EventArgs e)
        {
            rtxtNotepad.Undo();//撤销
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            rtxtNotepad.Copy();//复制
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            rtxtNotepad.Cut();//剪切
        }

        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            rtxtNotepad.SelectAll();//全选
        }

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            rtxtNotepad.AppendText(System.DateTime.Now.ToString());//显示当前日期
        }

        private void tsmiAuto_Click(object sender, EventArgs e)
        {
            if (tsmiAuto.Checked == false)
            {
                tsmiAuto.Checked = true;            // 选中该菜单项
                rtxtNotepad.WordWrap = true;        // 设置为自动换行
            }
            else
            {
                tsmiAuto.Checked = false;
                rtxtNotepad.WordWrap = false;
            }
        }

        private void tsmiFont_Click(object sender, EventArgs e)
        {
            fdlgNotepad.ShowColor = true;
            if (fdlgNotepad.ShowDialog() == DialogResult.OK)
            {
                rtxtNotepad.SelectionColor = fdlgNotepad.Color;
                rtxtNotepad.SelectionFont = fdlgNotepad.Font;
            }
        }

        private void tsmiToolStrip_Click(object sender, EventArgs e)
        {
            Point point;
            if (tsmiToolStrip.Checked == true)
            {
                // 隐藏工具栏时，把坐标设为（0，24）,因为菜单的高度为24
                point = new Point(0, 24);
                tsmiToolStrip.Checked = false;
                tlsNotepad.Visible = false;
                // 设置多格式文本框左上角位置
                rtxtNotepad.Location = point;
                // 隐藏工具栏后，增加文本框高度
                rtxtNotepad.Height += tlsNotepad.Height;
            }
            else
            {
                /* 显示工具栏时，多格式文本框左上角位置的位置为（0，49），
                   因为工具栏的高度为25，加上菜单的高度24后为49 */
                point = new Point(0, 49);
                tsmiToolStrip.Checked = true;
                tlsNotepad.Visible = true;
                rtxtNotepad.Location = point;
                rtxtNotepad.Height -= tlsNotepad.Height;
            }
        }

        private void tsmiStatusStrip_Click(object sender, EventArgs e)
        {
            if (tsmiStatusStrip.Checked == true)
            {
                tsmiStatusStrip.Checked = false;
                stsNotepad.Visible = false;
                rtxtNotepad.Height += stsNotepad.Height;
            }
            else
            {
                tsmiStatusStrip.Checked = true;
                stsNotepad.Visible = true;
                rtxtNotepad.Height -= stsNotepad.Height;
            }
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            frmAbout ob_FrmAbout = new frmAbout();
            ob_FrmAbout.Show();
        }

        private void tlsNotepad_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void tmrNotepad_Tick(object sender, EventArgs e)
        {
            tssLbl2.Text = System.DateTime.Now.ToString();
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            保存SCtrlSToolStripMenuItem_Click(sender, e);
        }

        private void 剪切UToolStripButton_Click(object sender, EventArgs e)
        {
            tsmiCut_Click(sender, e);
        }

        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            tsmiCopy_Click(sender, e);
        }

        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            黏贴PCtrlPToolStripMenuItem_Click(sender, e);
        }

        private void 帮助LToolStripButton_Click(object sender, EventArgs e)
        {
            tsmiAbout_Click(sender, e);
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {

        }
    }
}
