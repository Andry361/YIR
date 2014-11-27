using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whush.Demo
{
  class FFT
  {
    private const double pi = 3.14159265358979323846;
    private int ii;
    private int jj;
    private int n;
    private int mmax;
    private int m;
    private int j;
    private int istep;
    private int i;
    private int isign;
    private double wtemp;
    private double wr;
    private double wpr;
    private double wpi;
    private double wi;
    private double theta;
    private double tempr;
    private double tempi;

    public FFT()
    {
    }

    public void BildFFT(double[] a, int nn, bool inversefft) 
    {
      isign = inversefft ? -1 : 1;
      n = 2 * nn;
      j = 1;
      for (ii = 1; ii <= nn; ii++)
      {
        i = 2 * ii - 1;
        if (j > i)
        {
          tempr = a[j - 1];
          tempi = a[j];
          a[j - 1] = a[i - 1];
          a[j] = a[i];
          a[i - 1] = tempr;
          a[i] = tempi;
        }
        m = n / 2;
        while (m >= 2 && j > m)
        {
          j = j - m;
          m = m / 2;
        }
        j = j + m;
      }
      mmax = 2;
      while (n > mmax)
      {
        istep = 2 * mmax;
        theta = 2.0 * pi / (isign * mmax);
        wpr = -2.0 * Math.Pow(Math.Sin(0.5 * theta), 2);
        wpi = Math.Sin(theta);
        wr = 1.0;
        wi = 0.0;
        for (ii = 1; ii <= mmax / 2; ii++)
        {
          m = 2 * ii - 1;
          for (jj = 0; jj <= (n - m) / istep; jj++)
          {
            i = m + jj * istep;
            j = i + mmax;
            tempr = wr * a[j - 1] - wi * a[j];
            tempi = wr * a[j] + wi * a[j - 1];
            a[j - 1] = a[i - 1] - tempr;
            a[j] = a[i] - tempi;
            a[i - 1] = a[i - 1] + tempr;
            a[i] = a[i] + tempi;
          }
          wtemp = wr;
          wr = wr * wpr - wi * wpi + wr;
          wi = wi * wpr + wtemp * wpi + wi;
        }
        mmax = istep;
      }
      if (inversefft)
      {
        for (i = 1; i <= 2 * nn; i++)
        {
          a[i - 1] = a[i - 1] / nn;
        }
      }
      return;
      ///////
    }
  }
}
