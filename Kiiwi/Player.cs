using FFImageLoading.Maui;

namespace Kiiwi;
public delegate void Callback();
public class Player : Animacao

{
    public Player (CachedImageView a) : base (a)
    {
        for (int i = 1; i <= 6; i++)
            Animacao1.Add ($"bicho{i.ToString("D2")}.png");
        // for (int i = 1; i <= ; i++)
        //     animacao2.Add ($"{i.ToString("D2")}.png");
        SetAnimacaoAtiva(1);
    }
    public void Die()
    {
        Loop = false;
        
    }
    public void Run()
    {
        Loop=true;
        SetAnimacaoAtiva(1);
        Play();
    }
    public void MoveY (int s)
     {
        ImageView.TranslationY += s;
     }
     public double GetY()
     {
        return ImageView.TranslationY;
     }

     public void SetY(double a)
     {
        ImageView.TranslationY=a;
     }
}