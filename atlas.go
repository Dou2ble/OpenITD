package main

import (
	"github.com/Dou2ble/OpenITD/pkg/utils"
	rl "github.com/gen2brain/raylib-go/raylib"
)

const (
	tileSize  = 64
	atlasSize = 128
)

type Atlas struct {
	Tiles [atlasSize][atlasSize]Tile
}

func NewAtlas() Atlas {
	var tiles [atlasSize][atlasSize]Tile

	for x, row := range tiles {
		for y := range row {
			tiles[x][y] = NewTile()
		}
	}

	return Atlas{
		Tiles: tiles,
	}
}

func (atlas *Atlas) Update() {
	mousePos := rl.GetMousePosition()
	worldMousePos := rl.GetScreenToWorld2D(mousePos, game.Camera)
	atlasMousePos := rl.Vector2Divide(worldMousePos, rl.NewVector2(tileSize, tileSize))
	atlasMousePosX := int(atlasMousePos.X)
	atlasMousePosY := int(atlasMousePos.Y)

	// Handle using the various tools for building
	if atlasMousePosX >= 0 && atlasMousePosX < atlasSize &&
		atlasMousePosY >= 0 && atlasMousePosY < atlasSize &&
		!utils.Vector2InRec(mousePos, barRec) &&
		rl.IsMouseButtonDown(rl.MouseButtonLeft) {
		switch activeTool {
		case ToolBulldozer:
			atlas.Tiles[atlasMousePosX][atlasMousePosY].Build = nil
		case ToolRoad:
			building := BuildingRoad
			atlas.Tiles[atlasMousePosX][atlasMousePosY].Build = &building
		}
	}
}

func (atlas *Atlas) Draw() {
	for x, row := range atlas.Tiles {
		for y, tile := range row {
			position := rl.NewVector2(float32(x*tileSize), float32(y*tileSize))

			// TODO: we actually need to draw tiles that are off the screen

			tile.Draw(position)
		}
	}
}
