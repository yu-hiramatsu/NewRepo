
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ExifLib;


namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        Bitmap[] say = new Bitmap[100];
        Bitmap[] list = new Bitmap[100];
        string[] fname = new string[100];

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            list[i] = ImageFileOpen();
            //pictureBox1.Image = bmp;


        }

        private static OpenFileDialog ofd;


        private Bitmap ImageFileOpen()
        {

            ofd = new OpenFileDialog();
            ofd.Filter = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            ofd.Multiselect = true;


            if (ofd.ShowDialog() == DialogResult.Cancel) return null;

            return ImageFileOpen(ofd.FileName);
        }

        private static int i;
        //private static int j;

        private Bitmap ImageFileOpen(string fileNames)
        {




            // 指定したファイルが存在するか？確認
            if (System.IO.File.Exists(fileNames) == false) return null;

            // 拡張子の確認
            var ext = System.IO.Path.GetExtension(fileNames).ToLower();



            // ファイルの拡張子が対応しているファイルかどうか調べる
            if (
            (ext != ".bmp") &&
            (ext != ".jpg") &&
            (ext != ".png") &&
            (ext != ".tif")
            )
            {
                return null;
            }







            // ファイルストリームでファイルを開く
            using (var fs = new System.IO.FileStream(
                fileNames,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read))
            {

                //int i = 0;
                //int j = 0;

                foreach (string strFilePath in ofd.FileNames)
                {

                    string strFileName = System.IO.Path.GetFileName(strFilePath);

                    //string pathvar = fs.Name;

                    list[i] = new Bitmap(strFilePath);
                    say[i] = list[i];







                    foreach (System.Drawing.Imaging.PropertyItem item in list[i].PropertyItems)
                    {
                        if (item.Type == 2)
                        {
                            //ASCII文字の場合は、文字列に変換する
                            string val = System.Text.Encoding.ASCII.GetString(item.Value);
                            val = val.Trim(new char[] { '\0' });
                            int len = val.Length;


                            if (len >= 15)
                            {
                                //表示する
                                //System.Windows.Forms.MessageBox.Show($"{item.Id:132}: {item.Type}: {val}");
                                listBox1.Items.Add($"{val}, {i}, {strFileName}");
                                fname[i] = val;

                                break;
                            }
                        }
                    }
                    i++;

                }
            }

            pictureBox1.Image = list[0];

            return list[0];


        }











        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //［Ctrl］+［Shift］+［T］が押されたらキャッチする
            if (e.KeyData == (Keys.Control | Keys.Oemplus))
            {
                System.Windows.Forms.MessageBox.Show("Ctrl + ");
            }
            //［Ctrl］+［T］が押されたらキャッチする
            if (e.KeyData == (Keys.Control | Keys.OemMinus))
            {
                System.Windows.Forms.MessageBox.Show("Ctrl -");
            }
        }



        private void writeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            i = 0;



            for (i = 0; i < list.Length; i++)
            {
                try
                {

                    //Bitmap bmp = list[i];

                    // Create a rectangle for the entire bitmap
                    RectangleF rectf = new RectangleF(0, 0, list[i].Width, list[i].Height);

                    Graphics g = Graphics.FromImage(list[i]);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near
                    };
                    g.DrawString($"{fname[i]}", new Font("Arial", 200), Brushes.White, rectf, format);


                    g.Flush();

                    //bmp = list[i];
                    pictureBox1.Image = list[i];


                }

                catch (Exception)
                {
                    return;
                }



            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int num = listBox1.SelectedIndex;

            pictureBox1.Image = list[num];
            // if (num != i) {
            //     pictureBox1.Image = list[0];
            // }


        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

           
                // Prepare a dummy string, thos would appear in the dialog
                string dummyFileName = "Save Here";

                SaveFileDialog sf = new SaveFileDialog();
                // Feed the dummy name to the save dialog
                sf.FileName = dummyFileName;

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    // Now here's our save folder
                    string savePath = Path.GetDirectoryName(sf.FileName);
                // Do whatever


             

                try
                {

                    for (i = 0; i < list.Length; i++)
                    {
                        // Bitmap bmp = say[i];

                        list[i].Save($"{savePath}\\img{i}.jpg", ImageFormat.Jpeg);


                    }


                }

                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("saved");
                    return;
                }

                System.Windows.Forms.MessageBox.Show("saved");

            }

          

        }
    }





}



