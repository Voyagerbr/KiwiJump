using Microsoft.VisualBasic;
using FFImageLoading.Maui;

namespace Kiiwi;

public partial class MainPage : ContentPage
{
	bool estanoChao = true;
	bool estanoAr = false;
	
	int Velocity = 0;
	int Velocity1 = 0;
	int Velocity2 = 0;
	int Velocity3 = 0;
	
	int AirTime = 0;


	int TimeBeteweenFrames = 25;

	bool IsDied = true;
	int GravityForce = 35;
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
	Inimigos inimigos;
	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocity(w);
		Inimigos = new inimigos(-w);
		Inimigos.Add(new Inimigo(imgInimigo1));
		Inimigos.Add(new Inimigo(imgInimigo2));
		Inimigos.Add(new Inimigo(imgInimigo3));
		Inimigos.Add(new Inimigo(imgInimigo4));
	}
	void CalculaVelocity(double w)
	{
		Velocity1 = (int)(w * 0.001);
		Velocity2 = (int)(w * 0.004);
		Velocity3 = (int)(w * 0.008);
		Velocity = (int)(w * 0.01);

	}
	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var a in layerFundo01.Children)
			(a as Image).WidthRequest = w;
		foreach (var a in layerFundo02.Children)
			(a as Image).WidthRequest = w;
		foreach (var a in layerFundo03.Children)
			(a as Image).WidthRequest = w;
		foreach (var a in layerChao.Children)
			(a as Image).WidthRequest = w;

		layerFundo01.WidthRequest = w * 1.5;
		layerFundo02.WidthRequest = w * 1.5;
		layerFundo03.WidthRequest = w * 1.5;
		layerChao.WidthRequest = w * 1.5;
	}
	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenarios(layerFundo01);
		GerenciaCenarios(layerFundo02);
		GerenciaCenarios(layerFundo03);
		GerenciaCenarios(layerChao);

	}
	void MoveCenario()
	{
		layerFundo01.TranslationX -= Velocity1;
		layerFundo02.TranslationX -= Velocity2;
		layerFundo03.TranslationX -= Velocity3;
	}
	void GerenciaCenarios(HorizontalStackLayout horizontalStackLayout)
	{
		var view = (horizontalStackLayout.Children.First() as Image);
		if (view.WidthRequest + horizontalStackLayout.TranslationX < 0)
		{
			horizontalStackLayout.Children.Remove(view);
			horizontalStackLayout.Children.Add(view);
			horizontalStackLayout.TranslationX = view.TranslationX;
		}
	}

	async Task Desenha()
	{
		while (!IsDied)
		{
			GerenciaCenarios();
			if (Inimigos != null)
				Inimigos.Desenha(Velocity);
			if (!IsJumping && !estanoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
			else
				AplicaPulo();
			await Task.Delay(TimeBeteweenFrames);
		}


	}
	void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.MoveY(GravityForce);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			estanoChao = true;
		}
	}
	void AplicaPulo()
	{
		estanoChao = false;
		if (IsJumping && JumpTime >= maxJumpTime)
		{
			IsJumping = false;
			estanoAr = true;
			AirTime = 0;
		}
		else if (estanoAr && AirTime >= maxAirTime)
		{
			IsJumping = false;
			estanoAr = false;
			JumpTime = 0;
			AirTime = 0;
		}
		else if (IsJumping && JumpTime < maxJumpTime)
		{
			player.MoveY(-JumpForce);
			JumpTime++;
		}
		else if (estanoAr)
			AirTime++;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	void OnGridClicked(object o, TappedEventArgs a)
	{
		if (estanoChao)
			IsJumping = true;
	}


}
