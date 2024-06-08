package main

import (
	"github.com/Dou2ble/OpenITD/pkg/menu"
	rg "github.com/gen2brain/raylib-go/raygui"
	rl "github.com/gen2brain/raylib-go/raylib"
)

type Tool uint8

var (
	barRec        = rl.NewRectangle(0, 0, float32(width), 24)
	barButtonRecs [barButtonCount]rl.Rectangle
)

const (
	ToolBulldozer Tool = iota
	ToolRoad
)

const (
	barButtonCount = 2
)

func bar() Tool {
	result := activeTool
	menu.Button()

	barRec.Width = barRec.Height * barButtonCount
	barRec.X = float32(width)/2 - barRec.Width/2

	for i := 0; i < barButtonCount; i++ {
		barButtonRecs[i] = rl.NewRectangle(barRec.X+float32(i)*barRec.Height, barRec.Y, barRec.Height, barRec.Height)
	}

	if rg.Button(barButtonRecs[0], rg.IconText(rg.ICON_BIN, "")) {
		result = ToolBulldozer
	}
	if rg.Button(barButtonRecs[1], rg.IconText(rg.ICON_HOUSE, "")) {
		SpawnBuildingsWindow(barButtonRecs[1].X, barButtonRecs[1].Y)
		result = ToolRoad
	}

	return result
}
