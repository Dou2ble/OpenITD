package main

import (
	rl "github.com/gen2brain/raylib-go/raylib"
)

var activeTool Tool
var game Game

func main() {
	rl.InitWindow(int32(width), int32(height), "raylib [core] example - basic window")
	defer rl.CloseWindow()

	rl.BeginDrawing()
	rl.ClearBackground(rl.Black)
	rl.DrawText("Loading game...", 200, 200, 36, rl.LightGray)
	rl.EndDrawing()

	game = NewGame()
	debugEnabled := false

	for !rl.WindowShouldClose() {
		rl.BeginDrawing()

		if rl.IsKeyPressed(rl.KeyF3) {
			debugEnabled = !debugEnabled
		}

		game.Update()
		game.Draw()
		activeTool = bar()

		if debugEnabled {
			Debug()
		}

		rl.EndDrawing()
	}
}
