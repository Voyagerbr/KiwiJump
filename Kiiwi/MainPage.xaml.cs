namespace Kiiwi;

public partial class MainPage : ContentPage
{
	double HeightWindow = 0;
	double WidthtWindow = 0;
	const int Velocity = 15;
	const int Velocity1 = 15;
	const int Velocity2 = 15;
	const int Velocity3 = 15;
	const int Velocity4 = 15;
	const int Gravity = 5;


	const int TimeBeteweenFrames = 25;

	bool IsDied = true;
	const int JumpForce = 35;
	const int maxJumpTime = 5;
	bool IsJumping = false;
	int JumpTime = 0;
	const int minOpening = 100;
	int Score = 0;



	public MainPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	async Task Desenha()
	{
		while (!IsDied)
		{
			ManageCenary();
			await Task.Delay(TimeBeteweenFrames);
		}
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		VelocityCalc(w);
	}

	void VelocityCalc(double w)
	{
		Velocity1 = (int)(w * 0.001);
		Velocity2 = (int)(w * 0.004);
		Velocity3 = (int)(w * 0.007);
		Velocity4 = (int)(w * .009);
		Velocity = (int)(w * 0.01);
	}

	void ManageCenary()
	{
		MoveCenario();
		GerenciaCenario(layerFundo);
		GerenciaCenario(layerCidade);
		GerenciaCenario(layerSemaforo);
		GerenciaCenario(layerAsfalto);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in layerFundo.Children)
			(a as Image).WidthRequest = w;

		foreach (var a in layerCidade.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerSemaforo.Children)
			(a as Image).WidthRequest = w;
		
		foreach (var a in layerAsfalto.Children)
			(a as Image).WidthRequest = w;

		layerFundo.WidthRequest = w * 1.5;
		layerCidade.WidthRequest = w * 1.5;
		layerSemaforo.WidthRequest = w * 1.5;
		layerAsfalto.WidthRequest = w * 1.5;
	}

	void MoveCenario()
	{
		layerFundo.TranslationX -= velocidade1;
		layerCidade.TranslationX -= velocidade2;
		layerSemaforo.TranslationX -= velocidade3;
		layerAsfalto.TranslationX -= velocidade;
	}

	void GerenciaCenario(HorizontalStackLayout hsl)
	{
		var view = (hsl.Children.First() as Image);

		if(view.WidthRequest + hsl.TranslationX < 0)
		{
			hsl.Children.Remove(view);
			hsl.Children.Add(view);
			hsl.TranslationX = view.TranslationX;
		}
        
    }







}

