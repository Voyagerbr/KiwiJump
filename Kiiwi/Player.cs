using FFImageLoading.Maui;

namespace Kiiwi;
public delegate void Callback();

public class Player : Animacao

{
    public Player (CachedImageView a) : base (a)
    {
        for (int i = 1; i <= 20; i++)
            Animacao1.Add ($"bicho{i.ToString("D2")}.png");
         
         for (int i = 1; i <= 20; i++)
            Animacao2.Add ($"morto{i.ToString("D2")}.png");
        
    }
    public void Die()
    {
        Loop = false;
        SetAnimacaoAtiva(2); 
    }
    public void Run()
    {
        Loop=true;
        SetAnimacaoAtiva(1);
        Play();
    }
   
}