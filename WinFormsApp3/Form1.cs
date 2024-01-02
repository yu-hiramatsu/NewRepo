
using System.Drawing.Imaging;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bmp = ImageFileOpen();
            pictureBox1.Image = bmp;
        }

        private Bitmap ImageFileOpen()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.Cancel) return null;
            return ImageFileOpen(ofd.FileName);
        }

        private Bitmap ImageFileOpen(string fileName)
        {
            // 指定したファイルが存在するか？確認
            if (System.IO.File.Exists(fileName) == false) return null;

            // 拡張子の確認
            var ext = System.IO.Path.GetExtension(fileName).ToLower();

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

            Bitmap bmp;


            // ファイルストリームでファイルを開く
            using (var fs = new System.IO.FileStream(
            fileName,
            System.IO.FileMode.Open,
            System.IO.FileAccess.Read))
            {
                bmp = new Bitmap(fs);
                string pathvar = fs.Name;

                foreach (System.Drawing.Imaging.PropertyItem item in bmp.PropertyItems)
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
                            listBox1.Items.Add($"{val}");
                            break;
                        }
                    }


                }

            }

            return bmp;

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

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }





}



