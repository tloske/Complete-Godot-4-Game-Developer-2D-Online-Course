using Godot;
using System;
using System.Collections;

public partial class Game : Node2D
{
    private int _lives = 3;
    private int _score = 0;

    private Player _player;
    private HUD _hud;
    private CanvasLayer _ui;
    private AudioStreamPlayer _enemyHitSound;
    private AudioStreamPlayer _playerHitSound;


    [Export] private PackedScene _gameOverScene;

    public override void _Ready()
    {
        base._Ready();
        _player = GetNode<Player>("%Player");

        _hud = GetNode<HUD>("%HUD");
        _hud.SetScoreLabel(_score);
        _hud.SetLives(_lives);

        _ui = GetNode<CanvasLayer>("UI");
        _enemyHitSound = GetNode<AudioStreamPlayer>("EnemyHitSound");
        _playerHitSound = GetNode<AudioStreamPlayer>("PlayerHitSound");
    }
    public async void OnPlayerTookDamage()
    {
        _lives--;
        _hud.SetLives(_lives);
        _playerHitSound.Play();
        if (_lives <= 0)
        {
            _player.Die();

            await ToSignal(GetTree().CreateTimer(1.5), SceneTreeTimer.SignalName.Timeout);

            GameOverScreen gameOverScreen = _gameOverScene.Instantiate<GameOverScreen>();
            _ui.AddChild(gameOverScreen);
            gameOverScreen.SetScore(_score);
        }
    }

    public void OnEnemySpawnerEnemySpawned(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
        AddChild(enemy);
    }

    public void OnEnemySpawnerPathEnemySpawned(PathEnemy pathEnemy)
    {
        AddChild(pathEnemy);
        pathEnemy.Enemy.Died += OnEnemyDied;
    }

    public void OnEnemyDied()
    {
        _score += 100;
        _hud.SetScoreLabel(_score);
        _enemyHitSound.Play();
    }

}
