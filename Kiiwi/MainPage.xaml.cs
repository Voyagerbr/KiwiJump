
namespace Kiiwi;

public partial class MainPage : ContentPage
{
	double HeightWindow = 0;
	double WidthtWindow = 0;
 	int Velocity = 0;
	 int Velocity1 = 0;
	 int Velocity2 = 0;
	 int Velocity3 = 0;
	 int Velocity4 = 0;
	 int Gravity = 5;


	 int TimeBeteweenFrames = 25;

	bool IsDied = true;
	 int JumpForce = 35;
	 int maxJumpTime = 5;
	bool IsJumping = false;
	int JumpTime = 0;
	 int minOpening = 100;
	int Score = 0;



	Player player;

	public MainPage()
	{
		InitializeComponent();
		player = new Player(obrabo);
		player.Run();
	}

	async Task Desenha()
	{
		while (!IsDied)
		{
			GerenciaCenarios();
			player.Desenha();
			await Task.Delay(TimeBeteweenFrames);
		}
	}

    protected override void OnSizeAllocated(double w, double h)
    {
        base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w,h);
		CalculaVelocity(w);
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
	
	void CalculaVelocity(double w)
	{
		Velocity1=(int)(w*0.001);
		Velocity2=(int)(w*0.004);
		Velocity3=(int)(w*0.008);
		Velocity = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach(var a in layerFundo1.Children)
		(a as Image ).WidthRequest = w;
		foreach(var a in layerFundo2.Children)
		(a as Image ).WidthRequest = w;
		foreach(var a in layerFundo3.Children)
		(a as Image ).WidthRequest = w;
		foreach( var a in layerChao.Children)
		(a as Image ).WidthRequest = w;

		layerFundo1.WidthRequest=w*1.5;
		layerFundo2.WidthRequest=w*1.5;
		layerFundo3.WidthRequest=w*1.5;
		layerChao.WidthRequest=w*1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(layerFundo1);
		GerenciaCenario(layerFundo2);
		GerenciaCenario(layerFundo3);
		GerenciaCenario(layerChao);
	}

	void MoveCenario()
	{
		layerFundo1.TranslationX -= Velocity1;
		layerFundo2.TranslationX -= Velocity2;
		layerFundo3.TranslationX -= Velocity3;
		layerChao.TranslationX -= Velocity;
	}
	
	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);

		if(view.WidthRequest+hsl.TranslationX < 0)
		{
			hsl.Children.Remove(view);
			hsl.Children.Add(view);
			hsl.TranslationX = view.TranslationX;
		}
	}

	
}