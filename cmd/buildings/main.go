package main

import (
	"os"
	"strconv"

	"github.com/Dou2ble/OpenITD/pkg/gui"
	rl "github.com/gen2brain/raylib-go/raylib"
)

func main() {
	rl.InitWindow(100, 100, "raylib [core] example - basic window")
	defer rl.CloseWindow()
	x, err := strconv.Atoi(os.Args[1])
	if err != nil {
		panic(err)
	}
	y, err := strconv.Atoi(os.Args[2])
	if err != nil {
		panic(err)
	}

	rl.SetWindowPosition(x, y)

	for !rl.WindowShouldClose() {
		rl.BeginDrawing()
		gui.Button(rl.NewRectangle(10, 10, 100, 100), "wuu", false)

		rl.ClearBackground(rl.DarkGray)

		rl.EndDrawing()
	}
}
