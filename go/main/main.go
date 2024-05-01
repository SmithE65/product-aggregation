package main

import (
	"encoding/csv"
	"fmt"
	"io"
	"os"
	"strconv"
)

type Product struct {
	ProductId   string
	ProductName string
}

type ProductAggregate struct {
	Product    Product
	Quantity   int
	TotalPrice float64
}

func main() {
	fmt.Print("Enter file path: ")
	var path string
	fmt.Scanln(&path)
	fmt.Println("File path is: ", path)

	file, err := os.Open(path)

	if err != nil {
		fmt.Println("Error: ", err)
		return
	}

	reader := csv.NewReader(file)
	aggregates := make(map[string]ProductAggregate)
	totalUnitsSold, totalRevenue := aggregateAllTransactions(reader, aggregates)

	productCount := len(aggregates)
	fmt.Println("Number of products: ", productCount)
	fmt.Println("Total units sold: ", totalUnitsSold)
	fmt.Println("Total revenue: ", totalRevenue)
}

func aggregateAllTransactions(reader *csv.Reader, aggregates map[string]ProductAggregate) (int, float64) {
	reader.Read()

	totalUnitsSold := 0
	totalRevenue := 0.0

	for {
		record, err := reader.Read()
		if err == io.EOF {
			break
		}
		if err != nil {
			fmt.Println("Error: ", err)
			break
		}

		quantity, err := strconv.Atoi(record[4])
		if err != nil {
			fmt.Println("Error parsing quantity: ", err)
			break
		}
		price, err := strconv.ParseFloat(record[5], 64)
		if err != nil {
			fmt.Println("Error parsing price: ", err)
			break
		}

		totalUnitsSold += quantity
		totalRevenue += float64(quantity) * price

		existing, ok := aggregates[record[2]]
		if !ok {
			aggregates[record[2]] = ProductAggregate{
				Product: Product{
					ProductId:   record[2],
					ProductName: record[3],
				},
				Quantity:   quantity,
				TotalPrice: float64(quantity) * price,
			}
		} else {
			existing.Quantity += quantity
			existing.TotalPrice += float64(quantity) * price
			aggregates[record[2]] = existing
		}
	}
	return totalUnitsSold, totalRevenue
}
