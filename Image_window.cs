using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using Tesseract;
using Point = OpenCvSharp.Point;

namespace open0322
{
    public partial class Image_win : Form
    {
        // 현재 열어둔 파일 주소
        string NowImagePath;

        // 현재의 이미지(800*800)
        Bitmap NowImg;

        // 처리가 가해진 이미지 (800*800)
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
                // Cv2.AdaptiveThreshold(img, result, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 15, 2);
                Cv2.AdaptiveThreshold(img, result, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 15, 9);

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
        // ********* 여기수정해야돼
        
        private void btnContours_Click(object sender, EventArgs e)
        {
            Mat img = OpenCvSharp.Extensions.BitmapConverter.ToMat(SelectImg()); // 편집할 이미지
            Mat result = img.Clone(); // 편집 후 이미지

            /* 윤곽선 찾기 */
            Point[][] contours; // 윤곽선의 실젝 값이 저장될 contours 1)
            HierarchyIndex[] hierarchyIndexes; // 등고선 계층 정보를 저장할 배열 2) 

            /* 
             * 1) contours의 차원 구조는 점 좌표(x, y)의 묶음과, 그 좌표들을 한 번 더 묶는 구조
            좌표를 저장하기 위해서 Point 형식이며, 좌표들을 하나로 묶어 윤곽선을 구성하기 위해 Point[]
            윤곽선은 n개 이상 발생할 수 있으므로, Point[]를 묶는 Point[][]
             * 2) Node 정보가 담김
            다음 윤곽선, 이전 윤곽선, 자식 노드(자신의 안쪽에 있는 윤곽선), 부모 노드(자신 바깥쪽 윤곽선)
            */


            // 윤곽선 검출 함수
            Cv2.FindContours(img,
                             out contours, // out 키워드로 검출된 윤곽선 저장 
                             out hierarchyIndexes, // out 키워드로 계층구조 저장
                             RetrievalModes.Tree, // 검색 방법: 어떤 계층 구조의 형태를 사용할지 설정
                             ContourApproximationModes.ApproxSimple); // 윤곽점의 근사법을 설정

            /*List<List<Point>> contours_poly = new List<List<Point>>(contours.Count);
            List<Rect> boundRect = new List<Rect>(contours.Count);
            List<Rect> boundRect2 = new List<Rect>(contours.Count);

            for (int i = 0; i < contours.Count; i++)
            {
                CvInvoke.ApproxPolyDP(new VectorOfPoint(contours[i]), contours_poly[i], 1, true);
                boundRect[i] = CvInvoke.BoundingRectangle(new VectorOfPoint(contours_poly[i]));
            }*/

            /*vector<vector<Point>> contours_poly(contours.size());
            vector<Rect> boundRect(contours.size());
            vector<Rect> boundRect2(contours.size());

            for(int i = 0; i < contours.IsFixedSize(); i++)
            {
                approxPolyDP(Mat(contours[i]), contours_poly[i], 1, true);
                boundRect[i] = boundingRect(Mat(contours poly[i]));
            }*/


            // 불필요한 윤곽선을 제거하기 위한 List 형태의 배열 선언
            List<Point[]> new_contours = new List<Point[]>(); 
            foreach (Point[] p in contours)
            {
                double length = Cv2.ArcLength(p, true); // 윤곽선 길이 함수
                if (length > 200)
                {
                    // 윤곽선 길이가 200 이상인 값만 new_contours에 추가
                    new_contours.Add(p);
                }
            }

            // 이거 흑백으로 출력되는 문제가 있음 - 아마도 그리는 비트맵이 1채널이라 그런듯
            //Cv2.DrawContours(result, contours, -1, new Scalar(51, 255, 255), 2, LineTypes.AntiAlias, hierarchyIndexes, 3);

            // 새로운 윤곽선 배열을 그린다
            Cv2.DrawContours(result, new_contours, -1, new Scalar(255, 0, 0), 2, LineTypes.AntiAlias, null, 1);

            // 편집 후 이미지 할당
            /*processedImg = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(result);
            this.picResult.Image = processedImg;*/
            Cv2.ImShow("윤곽선", result);

            // 메모리 해제
            img.Dispose();
            result.Dispose();

        }

        // 번호판 자르기 만들어야함
        // 번호판 구역인식해야함

        private void btnBoxCheck_Click(object sender, EventArgs e)
        {
            Mat img = OpenCvSharp.Extensions.BitmapConverter.ToMat(SelectImg()); // 편집할 이미지
            Mat result = new Mat(); // 편집 후 이미지
            /* 윤곽선 중 사각형 찾기 */

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
                // 경로(폴더 내 tessdata폴더), traineddata파일명, 모드
                using (var engine = new TesseractEngine(@"./tessdata", "kor", EngineMode.Default))
                // Failed to initialise tesseract engine
                //  See https://github.com/charlesw/tesseract/wiki/Error-1 for details.
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