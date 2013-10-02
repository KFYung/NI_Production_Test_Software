using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NI_Test_Software.Instruction_Operation;

namespace NI_Test_Software
{
    public partial class Test_Template_Form : Form
    {
        private Dictionary<int, List<string>> Left_Button_Action            = new Dictionary<int,List<string>>();
        private Dictionary<int, List<string>> Right_Button_Action           = new Dictionary<int, List<string>>();
        private Dictionary<int, List<string>> Question_Instruction_List     = new Dictionary<int, List<string>>();

        private Instruction_List_Creator Instruction_Creation;
        private Instruction_Executor Instruction_Exe ;
        private Flag_Control Test_Template_Flag;

        public Flag_Control template_flag
        {
            set { Test_Template_Flag = value; }
            get { return Test_Template_Flag; } 
        }

        public Test_Template_Form(Flag_Control System_Flag, Instruction_List_Creator Created_List, Instruction_Executor import_Instruction_Exe)
        {
            Test_Template_Flag = System_Flag;

            InitializeComponent();
            
            //Pass the whole list to the local area for this AREA gobal use only
            try
            {
                string result_msg;
                Instruction_Creation = Created_List;
                Instruction_Exe = import_Instruction_Exe;

                Dictionary<int, List<string>> instruction_list = Created_List.Read_Instruction_List;
                Dictionary<int, int[]> question_instruction_loction = Created_List.Read_Instruction_Question_List;
                
                //Copy the Question List out
                Question_Instruction_List.Clear();
                for(
                    int cnt = question_instruction_loction[Instruction_Exe.question_step][(int)Instruction_Executor.question_loc.start];
                    cnt <= question_instruction_loction[Instruction_Exe.question_step][(int)Instruction_Executor.question_loc.end];
                    cnt++
                   )

                {
                    Question_Instruction_List.Add(cnt, new List<string>((instruction_list[cnt])));
                }
                

                result_msg = Instruction_Exe.list_executation(Question_Instruction_List, Instruction_Creation, this);
                if (result_msg.Contains(Instruction_Executor.executation_result[(int)Instruction_Executor.exe_result.Fail]))
                {
                    MessageBox.Show("Question Instruction Error" + "\n" + result_msg);
                    Instruction_Exe.instruction_step = 0;       //Reset the Test Step
                    template_flag.Flag_Reset();
                    this.Close();
                }

                Instruction_Exe.instruction_step = question_instruction_loction[Instruction_Exe.question_step][(int)Instruction_Executor.question_loc.end] + 1;

                //Setup the Instructio Progress This is Base on overall Instruction which has been though
                Test_Progress_Bar.Minimum = 0;
                Test_Progress_Bar.Maximum = instruction_list.Count;
                Test_Progress_Bar.TabIndex = import_Instruction_Exe.instruction_step;

                //Test Label
                Test_Label.Text = Created_List.Test_Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Question List Error");
                this.Close();
            }

        }

        private void Test_Label_Click(object sender, EventArgs e)
        {

        }

        private void Test_Progress_Bar_Click(object sender, EventArgs e)
        {

        }

        private void Instruction_and_Result_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Instruction_and_Result_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button_Left_Click(object sender, EventArgs e)
        {
            try
            {
                string result_msg = Instruction_Exe.list_executation(Left_Button_Action, Instruction_Creation, this);
                if (
                    result_msg.Contains(Instruction_Executor.executation_result[(int)Instruction_Executor.exe_result.Fail]) ||
                    result_msg.Contains(Instruction_Executor.executation_result[(int)Instruction_Executor.exe_result.Exe_Err])
                    )
                {
                    MessageBox.Show("Left Button Action Error" + "\n" + result_msg);
                    Instruction_Exe.instruction_step = 0;       //Reset the Test Step
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Left Button Action List Error");
            }

            this.Close(); 
        }

        private void Button_Right_Click(object sender, EventArgs e)
        {
            try
            {
                string result_msg = Instruction_Exe.list_executation(Right_Button_Action, Instruction_Creation, this);
                if (
                    result_msg.Contains(Instruction_Executor.executation_result[(int)Instruction_Executor.exe_result.Fail]) ||
                    result_msg.Contains(Instruction_Executor.executation_result[(int)Instruction_Executor.exe_result.Exe_Err])
                    )
                {
                    MessageBox.Show("Right Button Action Error" + "\n" + result_msg);
                    Instruction_Exe.instruction_step = 0;       //Reset the Test Step
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Right Button Action List Error");
            }

            this.Close(); 
        }

        private void Result_Pass_Click(object sender, EventArgs e)
        {

        }

        private void Result_Fail_Click(object sender, EventArgs e)
        {

        }

        private void Button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Test_Template_Flag.Flag_Reset();
            //Test_Template_Flag.mode = Flag_Control.Mode_Flag_Name[(int)Flag_Control.Mode_Flag.Start];  //This Action is use to fall back to the test select menu
        }

        private void Test_Template_Form_Closed(object sender, FormClosedEventArgs e)
        {
            Test_Template_Flag.Test_status = (int)Flag_Control.Test_status_Lib.resume;  //Pause the Main Instruction Running Thread   
        }

        /*---------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------Sub Command Operation---------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------*/

        public Label p_Left_Panel_txt
        {
            get { return this.Left_Panel_txt; }
            set { this.Left_Panel_txt = value; }
        }

        public Label p_Right_Panel_txt
        {
            get { return this.Right_Panel_txt; }
            set { this.Right_Panel_txt = value; }
        }

        public Button p_Button_Right
        {
            get { return this.Button_Right; }
            set { this.Button_Right = value; }
        }

        public Button p_Button_Left
        {
            get { return this.Button_Left; }
            set { this.Button_Left = value; }
        }

        public Button p_Button_Exit
        {
            get { return this.Button_Exit; }
            set { this.Button_Exit = value; }
        }

        public SplitContainer p_Instruction_and_Result
        {
            get { return this.Instruction_and_Result; }
            set { this.Instruction_and_Result = value; }
        }

        public PictureBox p_Test_Image
        {
            get { return this.Test_Image; }
            set { this.Test_Image = value; }
        }

        public Dictionary<int, List<string>> p_Left_Button_Action           
        {
            get { return Left_Button_Action; }
            set { Left_Button_Action = value; }
        }
        public Dictionary<int, List<string>> p_Right_Button_Action           
        {
            get { return Right_Button_Action; }
            set { Right_Button_Action = value; }
        }
        
        public Dictionary<int, List<string>> p_Question_Instruction_List
        {
            get { return Question_Instruction_List; }
            set { Question_Instruction_List = value; }
        }
        
    }
}
