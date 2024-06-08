package main

import (
	"fmt"

	rl "github.com/gen2brain/raylib-go/raylib"
	"github.com/go-cmd/cmd"
)

func SpawnBuildingsWindow(x, y float32) {
	currentWinPos := rl.GetWindowPosition()
	cmd := cmd.NewCmd("./buildings", fmt.Sprint(int(currentWinPos.X+x)), fmt.Sprint(int(currentWinPos.Y+y+barRec.Height)))
	// cmd := cmd.NewCmd("./buildings")
	cmd.Start()
}
