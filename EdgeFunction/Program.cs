using System.Numerics;
namespace EdgeFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vector2 v0 = new(491.407f, 411.407f);
            Vector2 v1 = new(148.593f, 68.5928f);
            Vector2 v2 = new(148.593f, 411.407f);

            Vector3 c0 = new(1, 0, 0);
            Vector3 c1 = new(0, 1, 0);
            Vector3 c2 = new(0, 0, 1);

            //image size
            int imgWidth = 512;
            int imgHeight = 512;

            //output .ppm File
            Console.WriteLine("P3");
            Console.WriteLine(imgWidth + " " + imgHeight);
            Console.WriteLine(255);

            float area = EdgeCheck(v0, v1, v2);
            //find pixel
            for (int j = 0; j < imgHeight; j++)
            {
                for (int i = 0; i < imgWidth; i++)
                {
                    Vector3 color = new Vector3(0, 0, 0);
                    Vector2 point = new(j + 0.5f, i + 0.5f);

                    // check inside
                    float w0, w1, w2;
                    w0 = EdgeCheck(v1, v2, point);
                    if (w0 >= 0)
                    {
                        w1 = EdgeCheck(v2, v0, point);
                        if (w1 >= 0)
                        {
                            w2 = EdgeCheck(v0, v1, point);
                            if (w2 >= 0)
                            {
                                // color
                                w0 /= area;
                                w1 /= area;
                                w2 /= area;

                                float r0 = w0 * c0.X + w1 * c1.X + w2 * c2.X;
                                float r1 = w0 * c0.Y + w1 * c1.Y + w2 * c2.Y;
                                float r2 = w0 * c0.Z + w1 * c1.Z + w2 * c2.Z;
                                int r = Math.Clamp((int)(r0 * 255), 0, 255);
                                int g = Math.Clamp((int)(r1 * 255), 0, 255);
                                int b = Math.Clamp((int)(r2 * 255), 0, 255);
                                color.X = r;
                                color.Y = g;
                                color.Z = b;
                            }
                        }
                    }
                    Console.Write(color.X + " " + color.Y + " " + color.Z + "\n");
                }
                Console.WriteLine();
            }


        }

        /// <summary>
        /// 检测点p是否在向量v1v2的右侧。
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static float EdgeCheck(Vector2 v1, Vector2 v2, Vector2 p)
        {
            return (p.X - v1.X) * (v2.Y - v1.Y) - (v2.X - v1.X) * (p.Y - v1.Y);
        }
    }
}