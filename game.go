package main

import rl "github.com/gen2brain/raylib-go/raylib"

var (
	width     int          = 1280
	height    int          = 720
	screenRec rl.Rectangle = rl.NewRectangle(0, 0, float32(width), float32(height))
	dt        float32      = 0
)

const (
	cameraSpeed     = 500
	cameraZoomSpeed = 0.125
)

type Game struct {
	Atlas  Atlas
	Camera rl.Camera2D
}

func NewGame() Game {
	return Game{
		Atlas:  NewAtlas(),
		Camera: rl.NewCamera2D(rl.NewVector2(-float32(width)/2, -float32(height)/2), rl.Vector2Zero(), 0, 1),
		// Camera: rl.NewCamera2D(rl.Vector2Zero(), rl.Vector2Zero(), 0, 1),
	}
}

func (game *Game) Update() {
	width = rl.GetScreenWidth()
	height = rl.GetScreenHeight()
	screenRec = rl.NewRectangle(0, 0, float32(width), float32(height))

	dt = rl.GetFrameTime()

	game.Camera.Zoom += game.Camera.Zoom * cameraZoomSpeed * rl.GetMouseWheelMoveV().Y

	// Control the camera
	game.Camera.Offset.X = float32(width) / 2
	game.Camera.Offset.Y = float32(height) / 2

	var speed float32 = cameraSpeed

	if SpeedModifier() {
		speed *= 5
	}

	if Up() {
		game.Camera.Target.Y -= speed / game.Camera.Zoom * dt
	}
	if Left() {
		game.Camera.Target.X -= speed / game.Camera.Zoom * dt
	}
	if Down() {
		game.Camera.Target.Y += speed / game.Camera.Zoom * dt
	}
	if Right() {
		game.Camera.Target.X += speed / game.Camera.Zoom * dt
	}

	game.Atlas.Update()
}

func (game *Game) Draw() {
	rl.ClearBackground(rl.RayWhite)

	rl.BeginMode2D(game.Camera)

	rl.DrawCircleV(rl.Vector2Zero(), 25, rl.Red)
	game.Atlas.Draw()

	rl.EndMode2D()
}
