using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using Tesseract;

namespace open0322
{
    public partial class Image_win : Form
    {
        // 현재 열어둔 파일 주소
        string NowImagePath;

        // 현재의 이미지(800*800)
        Bitmap NowImg;

        // 처리가 가해진 이미지
        Bitmap processedImg;

        public Image_win()
        {
            InitializeComponent();
        }

        private void MenuOpenImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "이미지 파일(png, jpg)|*.png;*.jpg"; // open 필터 설정

            if (open.ShowDialog() == DialogResult.OK)
            {
                NowImagePath = open.FileName; // NowImagePath를 초기화
                Bitmap Image = new Bitmap(NowImagePath); // 비트맵 타입 변수 Image 선언 및 초기화
                                                         // Mat Image = Cv2.ImRead(NowImagePath); // Mat 선언 및 초기화


                /* 0322 : 이미지 크기 변경을 수정해야함. 정사각 화면에 띄우기 위해 이미지를 자르는 방안도 고려해야 할 것 같음. */
                NowImg = Method.ResizeImage(Image, 800, 800); // 이미지 크기 변경
                this.picOriginal.Image = NowImg; // 창에 이미지 설정

            }
        }

        /* this.picOriginal에 설정된 이미지를 Mat 객체로 변환하여 OpenCv 메서드로 처리한 후 picResult에 재설정  */
        /* picResult에 이미지가 있으면 해당 이미지를 변환하고, 없으면 picOriginal에 설정된 이미지를 변환 : 메서드로 만들 수 있을듯*/

        /* 1)이미지 파일로 저장
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            bmp.Save("output.png", ImageFormat.Jpeg);
        2): 이미지를 byte[] 로 변경
            ImageConverter converter = new ImageConverter();
            imageBytes = (byte[])converter.ConvertTo(pictureBox1.Image, typeof(byte[]));*/

/*        public Mat SelectImg()
        {
            Mat temp = new Mat();
            *//* 이미지 선택 메서드, 메모리 누수 방지를 위한 using 사용 *//*
            if (processedImg is null)
            {
                temp = OpenCvSharp.Extensions.BitmapConverter.ToMat(NowImg);
            } else
            {
                temp = OpenCvSharp.Extensions.BitmapConverter.ToMat(processedImg);
            }
            return temp;
        }
*/
        public Bitmap SelectImg()
        {
            /* 이미지 선택 메서드, 메모리 누수 방지를 위한 using 사용 */
            if (processedImg is null)
            {
                return NowImg;
            }
            else
            {
                return processedImg;
            }
        }


        private void btnBlur_Click(object sender, EventArgs e)
        {
            Mat img = OpenCvSharp.Extensions.BitmapConverter.ToMat(SelectImg()); // 편집할 이미지
            Mat result = new Mat(); // 편집 후 이미지

            /* 가우시안 */
            Cv2.GaussianBlur(img, result, new OpenCvSharp.Size(3, 3), 0);
            // 편집 후 이미지 할당
            processedImg = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
            this.picResult.Image = processedImg;

            // 메모리 해제
            img.Dispose();
            result.Dispose();
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            Mat img = OpenCvSharp.Extensions.BitmapConverter.ToMat(SelectImg()); // 편집할 이미지
            Mat result = new Mat(); // 편집 후 이미지

            /* 그레이 스케일 : 이미 그레이스케일을 적용한 이미지에는 그레이스케일 불가 */
            if(img.Channels() != 1)
            {
                Cv2.CvtColor(img, result, ColorConversionCodes.BGR2GRAY);

                // 편집 후 이미지 할당
                processedImg = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
                this.picResult.Image = processedImg;

                // 메모리 해제
                img.Dispose();
                result.Dispose();

            }
        }

        private void btnBin_Click(object sender, EventArgs e)
        {
            Mat img = OpenCvSharp.Extensions.BitmapConverter.ToMat(SelectImg()); // 편집할 이미지
            Mat result = new Mat(); // 편집 후 이미지

            /* 이진화 : 그레이스케일이 적용되지 않은 이미지는 이진화 하지 않음 */
            if (img.Channels() == 1)
            {
                Cv2.AdaptiveThreshold(img, result, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 15, 2);
                // 편집 후 이미지 할당
                processedImg = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
                this.picResult.Image = processedImg;

                // 메모리 해제
                img.Dispose();
                result.Dispose();
            }
        }

        private void btnEdge_Click(object sender, EventArgs e)
        {
            Mat img = OpenCvSharp.Extensions.BitmapConverter.ToMat(SelectImg()); // 편집할 이미지
            Mat result = new Mat(); // 편집 후 이미지

            /* 경계 추출 */
            Cv2.Canny(img, result, 100, 200, 3);
            // 편집 후 이미지 할당
            processedImg = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
            this.picResult.Image = processedImg;

            // 메모리 해제
            img.Dispose();
            result.Dispose();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            /* 초기화 */
            processedImg = null;
            this.picResult.Image = null;
            this.txtReadbox.Text = null;
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("인식 버튼 클릭");

            /* 글자 인식 */
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "kor", EngineMode.Default))
                using (Pix pix = PixConverter.ToPix(SelectImg()))
                using (var page = engine.Process(pix))
                {
                    this.txtReadbox.Text = page.GetText();
                    Console.WriteLine(page.GetText());
                }
            }
            catch (Exception ex)
            {
                this.txtReadbox.Text = ex.ToString();
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}