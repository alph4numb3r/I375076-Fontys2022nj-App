package fontys2022nj

import (
	"fmt"
	"time"
)

type DaySegment byte

const (
	Night     DaySegment = 0 //00-06 midnight to 6 AM
	Morning   DaySegment = 1 //06-12 6 AM to noon
	Afternoon DaySegment = 2 //12-18	noon to 6 PM
	Evening   DaySegment = 3 //18-24 6 PM to midnight
)

func main() {
	now := time.Now()
	fmt.Println("Hello World" + getDaySegment(now).String())
}

func getDaySegment(t time.Time) DaySegment {
	return DaySegment(t.Hour() / 6)
}
