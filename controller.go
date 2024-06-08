package main

import rl "github.com/gen2brain/raylib-go/raylib"

func Up() bool {
	return rl.IsKeyDown(rl.KeyW)
}

func Left() bool {
	return rl.IsKeyDown(rl.KeyA)
}

func Down() bool {
	return rl.IsKeyDown(rl.KeyS)
}

func Right() bool {
	return rl.IsKeyDown(rl.KeyD)
}

func SpeedModifier() bool {
	return rl.IsKeyDown(rl.KeyLeftShift) || rl.IsKeyDown(rl.KeyRightShift)
}
