package gui

import (
	"github.com/Dou2ble/OpenITD/pkg/utils"
	rl "github.com/gen2brain/raylib-go/raylib"
)

const (
	fontSize   = 20
	Margin     = 5
	ButtonSize = 20
)

func Button(rec rl.Rectangle, label string, active bool) bool {
	result := false

	mousePos := rl.GetMousePosition()
	hover := utils.Vector2InRec(mousePos, rec)

	if hover && rl.IsMouseButtonDown(rl.MouseButtonLeft) {
		if rl.IsMouseButtonPressed(rl.MouseButtonLeft) {
			result = true
		}
		rl.DrawRectangleRec(rec, rl.White)
	} else if active || hover {
		rl.DrawRectangleRec(rec, rl.LightGray)
	} else {
		rl.DrawRectangleRec(rec, rl.DarkGray)
	}

	rl.DrawRectangleLinesEx(rec, 2, rl.Black)
	rl.DrawText(label, int32(rec.X)+1, int32(rec.Y)+1, fontSize, rl.Black)

	return result
}
