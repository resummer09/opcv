using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace open0322
{
    class Method
    {
        #region 이미지 크기 변경
        public static bool ResizeImageFile(string imageFilePath, in int newWidth, in int newHeight, in bool keepSizeRatio = true)
        {
            /* bool 타입 리턴 (이미지파일주소, 새 가로 사이즈, 새 세로 사이즈, 종횡비 고정=true) */

            try
            {
                byte[] byteArr = File.ReadAllBytes(imageFilePath); // byte 배열 선언 및 초기화

                using (var stream = new System.IO.MemoryStream(byteArr))
                {
                    Bitmap bitmap = new Bitmap(stream); // Bitmap 선언 및 초기화

                    int applyWidth = newWidth;
                    int applyHeight = newHeight;

                    /* 종횡비를 고정한 채 이미지 사이즈를 변경하는 코드 */
                    if (keepSizeRatio)
                    {
                        double percentW = 0;
                        double percentH = 0;
                        double targetPercent = 0;

                        /* 기존 크기 / 이미지의 크기를 퍼센트W, H에 할당 */
                        percentW = (double)newWidth / bitmap.Width;
                        percentH = (double)newHeight / bitmap.Height;

                        /* 더 작은 쪽을 targetPercent에 할당 */
                        if (percentW < percentH) targetPercent = percentW;
                        else targetPercent = percentH;

                        applyWidth = (int)(bitmap.Width * targetPercent);
                        applyHeight = (int)(bitmap.Height * targetPercent);

                        if (applyWidth > newWidth) applyWidth = newWidth;
                        if (applyHeight > newHeight) applyHeight = newHeight;
                    }

                    bitmap = ResizeImage(bitmap, applyWidth, applyHeight); // 리사이즈 이미지 생성
                    // bitmap.Save(imageFilePath);
                    bitmap.Dispose(); // 이미지 메모리 해제
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            /* Bitmap을 반환하는 이미지 크기 변경 함수 */

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        #endregion





    }
}
