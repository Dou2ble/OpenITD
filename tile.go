package main

import (
	rl "github.com/gen2brain/raylib-go/raylib"
)

type TileKind uint8
type Building uint8

const (
	TileKindGrass TileKind = iota
)

const (
	BuildingRoad Building = iota
)

type Tile struct {
	Kind  TileKind
	Build *Building
}

func NewTile() Tile {
	return Tile{
		Kind:  TileKindGrass,
		Build: nil,
	}
}

func (tile *Tile) Draw(pos rl.Vector2) {
	rec := rl.NewRectangle(pos.X, pos.Y, tileSize, tileSize)
	rl.DrawRectangleRec(rec, rl.Lime)

	if tile.Build != nil {
		switch *tile.Build {
		case BuildingRoad:
			rl.DrawRectangleRec(rec, rl.DarkGray)
		}
	}

	rl.DrawRectangleLinesEx(rec, 1, rl.ColorAlpha(rl.DarkGray, 0.2))
}
