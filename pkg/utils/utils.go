package utils

import rl "github.com/gen2brain/raylib-go/raylib"

func Vector2InRec(vec rl.Vector2, rec rl.Rectangle) bool {
	return vec.X > rec.X && vec.Y > rec.Y && vec.X < rec.X+rec.Width && vec.Y < rec.Y+rec.Height
}
