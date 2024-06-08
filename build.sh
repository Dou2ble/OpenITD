#!/bin/sh

go build ./cmd/buildings || exit 1
go run . || exit 1

