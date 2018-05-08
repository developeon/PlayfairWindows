using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3102
{
    public partial class Form1 : Form
    {
        string encryptionKey; //암호키
        string plainText; //평문
        char[,] cipher_plate = new char[5, 5]; //암호판
        bool isaddX = false; //홀수여서 X추가해줬는지 아닌지 여부
        List<char> removeDuplicates;
        List<char> splitPlainText;
        List<int> rememberIndex; //X추가해준 index 기억
        List<char> cipher_text;
        List<char> plain_text;

        public Form1()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e) //암호화키 누르면
        {
            encryptionKey = encryptionKeyBox.Text.ToLower();
            plainText = plainTextBox.Text.ToLower();

            removeDuplicates = new List<char>(encryptionKey); //중복 제거한 암호키
            for (int i = (int)'a'; i < (int)'z'; i++)
            {
                removeDuplicates.Add((char)i);
            }
            removeDuplicates = removeDuplicates.Distinct().ToList();

            int tmpCnt = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cipher_plate[i, j] = removeDuplicates[tmpCnt++]; //암호판 완성 
                }
            }

            //암호판 출력
            textBox0.Text = cipher_plate[0, 0].ToString();
            textBox1.Text = cipher_plate[0, 1].ToString();
            textBox2.Text = cipher_plate[0, 2].ToString();
            textBox3.Text = cipher_plate[0, 3].ToString();
            textBox4.Text = cipher_plate[0, 4].ToString();

            textBox5.Text = cipher_plate[1, 0].ToString();
            textBox6.Text = cipher_plate[1, 1].ToString();
            textBox7.Text = cipher_plate[1, 2].ToString();
            textBox8.Text = cipher_plate[1, 3].ToString();
            textBox9.Text = cipher_plate[1, 4].ToString();

            textBox10.Text = cipher_plate[2, 0].ToString();
            textBox11.Text = cipher_plate[2, 1].ToString();
            textBox12.Text = cipher_plate[2, 2].ToString();
            textBox13.Text = cipher_plate[2, 3].ToString();
            textBox14.Text = cipher_plate[2, 4].ToString();

            textBox15.Text = cipher_plate[3, 0].ToString();
            textBox16.Text = cipher_plate[3, 1].ToString();
            textBox17.Text = cipher_plate[3, 2].ToString();
            textBox18.Text = cipher_plate[3, 3].ToString();
            textBox19.Text = cipher_plate[3, 4].ToString();

            textBox20.Text = cipher_plate[4, 0].ToString();
            textBox21.Text = cipher_plate[4, 1].ToString();
            textBox22.Text = cipher_plate[4, 2].ToString();
            textBox23.Text = cipher_plate[4, 3].ToString();
            textBox24.Text = cipher_plate[4, 4].ToString();

            //평문 띄어쓴거 없애고 SS같은거 있으면 중간에 X넣기
            splitPlainText = new List<char>(plainText);
            rememberIndex = new List<int>(); //X추가해준 index 기억
            do
            {
                splitPlainText.Remove(' ');
            }
            while (splitPlainText.Contains(' '));

            tmpCnt = 0;
            while (true)
            {
                if (splitPlainText[tmpCnt] == splitPlainText[tmpCnt + 1])
                {
                    splitPlainText.Insert(tmpCnt + 1, 'x');
                    rememberIndex.Add(tmpCnt + 1);
                }
                tmpCnt += 2;
                if (tmpCnt == splitPlainText.Count() - 1 || tmpCnt >= splitPlainText.Count()) break;
            }

            //홀수면 x 추가해주기
            if (splitPlainText.Count() % 2 == 1)
            {
                splitPlainText.Add('x');
                isaddX = true;
            }

            for (int i = 0; i < splitPlainText.Count; i++)
            {
                if (splitPlainText[i].Equals('z'))
                    splitPlainText[i] = 'q';
            } //암호화활 문자열에 z가 있으면 q로 바꿔줌 (q랑z를 같은곳에 둔것처럼 할 수 있음)

            //암호문 생성
            cipher_text = new List<char>();
            int coltemp1 = 0, rowtemp1 = 0, coltemp2 = 0, rowtemp2 = 0;
            for (int i = 0; i < splitPlainText.Count(); i += 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {

                        if (cipher_plate[j, k] == splitPlainText[i])
                        {

                            coltemp1 = j;
                            rowtemp1 = k;
                        }
                        if (cipher_plate[j, k] == splitPlainText[i + 1])
                        {
                            coltemp2 = j;
                            rowtemp2 = k;
                        }
                        //바꿀문자에 z있으면 Q랑같은 컬럼,열 번호 넣어주고 비교하기
                        //그냥 splitPlainText[i]에 z있으면 q로바꿔줌 
                    }
                }
                //같은행, 같은열에 있을때 처리 
                if (coltemp1 == coltemp2)
                {
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp1, rowtemp1 + 1]);

                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[coltemp1, 0]);

                    }
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp2, rowtemp2 + 1]);
                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[coltemp2, 0]);
                    }
                    continue;
                }
                if (rowtemp1 == rowtemp2)
                {
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp1 + 1, rowtemp1]);

                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[0, rowtemp1]);
                    }
                    try
                    {
                        cipher_text.Add(cipher_plate[coltemp2 + 1, rowtemp2]);
                    }
                    catch
                    {
                        cipher_text.Add(cipher_plate[0, rowtemp2]);
                    }

                    continue;
                }
                cipher_text.Add(cipher_plate[coltemp2, rowtemp1]);
                cipher_text.Add(cipher_plate[coltemp1, rowtemp2]);
            } //81 ~153라인함수로 만들 수 있을듯?그래서 복호화랑 암호화랑 같이 진행하자!

            //암호문 출력
            string tmpCihperText = "";
            foreach (char item in cipher_text)
            {
                tmpCihperText += item;
            }
            CipherTextBox.Text = tmpCihperText;

            
        }

        private void button2_Click(object sender, EventArgs e) //복호화키 누르면
        {
            //복호화하기
            plain_text = new List<char>();
            int coltemp1 = 0, rowtemp1 = 0, coltemp2 = 0, rowtemp2 = 0;
            for (int i = 0; i < cipher_text.Count(); i += 2)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (cipher_plate[j, k] == cipher_text[i])
                        {

                            coltemp1 = j;
                            rowtemp1 = k;
                        }
                        if (cipher_plate[j, k] == cipher_text[i + 1])
                        {
                            coltemp2 = j;
                            rowtemp2 = k;
                        }
                    }
                }
                if (coltemp1 == coltemp2)
                {
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp1, rowtemp1 - 1]);
                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[coltemp1, 4]);

                    }
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp2, rowtemp2 - 1]);
                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[coltemp2, 4]);
                    }
                    continue;
                }
                if (rowtemp1 == rowtemp2)
                {
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp1 - 1, rowtemp1]);

                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[4, rowtemp1]);
                    }
                    try
                    {
                        plain_text.Add(cipher_plate[coltemp2 - 1, rowtemp2]);
                    }
                    catch
                    {
                        plain_text.Add(cipher_plate[4, rowtemp2]);
                    }

                    continue;
                }
                plain_text.Add(cipher_plate[coltemp2, rowtemp1]);
                plain_text.Add(cipher_plate[coltemp1, rowtemp2]);
            }

            //홀수일때 추가해준 x삭제
            if (isaddX)
            {

                plain_text.RemoveAt((plain_text.Count() - 1));
            }
            //중간에 넣어준 x 삭제
            for (int i = rememberIndex.Count() - 1; i >= 0; i--)
            {
                plain_text.RemoveAt(rememberIndex[i]);
            }
            //복호문 출력
            string tmpdecryptedText = "";
            foreach (char item in plain_text)
            {
                tmpdecryptedText += item;
            }
            decryptedTextBox.Text = tmpdecryptedText;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            encryptionKeyBox.Text = "";
            plainTextBox.Text = "";
            textBox0.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            CipherTextBox.Text = "";
            decryptedTextBox.Text = "";

        }

      
    }
}
