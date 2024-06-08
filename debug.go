package main

import (
	"fmt"

	rl "github.com/gen2brain/raylib-go/raylib"
)

func Debug() {
	text := fmt.Sprintf("FPS: %d", rl.GetFPS())
	rl.DrawText(text, 12, 12, 20, rl.Black)
}
