### Challenge Level 2: CPU-Intensive Aggregation

In this level of the challenge, you'll still be working with the same input schema and required outputs as in the previous challenge. However, you'll be introducing additional processing steps and a new output file.

### Additional Requirements:

1. **Advanced Aggregation**: In addition to calculating total sales revenue and quantity sold for each product, you'll also need to compute additional metrics for each product, such as:

    - Average price per unit.
    - Maximum and minimum prices per unit.
    - Standard deviation of prices per unit.

2. **Parallel Processing**: Implement parallel processing techniques to improve performance, such as using multithreading or multiprocessing, to distribute the workload across multiple CPU cores.

3. **Additional Output** Generate a secondary output file that aggregates data on an hourly basis. The secondary output file should contain the following information:

1. Hourly Timestamp: Representing the starting hour of each time period.
2. Total Products Sold: Total quantity of products sold within the hour.
3. Total Revenue: Total revenue generated within the hour.
4. Average Unit Price: Average price per unit of products sold within the hour.


### Example Expanded Output:

Consider the same input CSV file provided in the previous challenge:

```
Transaction ID, Date, Product ID, Product Name, Quantity, Price per Unit
"1", "2024-01-01T08:00:00.0000000Z", "1001", "Product A", 10, 20.00
"2", "2024-01-01T08:00:00.0000000Z", "1002", "Product B", 5, 15.00
"3", "2024-01-02T08:00:00.0000000Z", "1001", "Product A", 15, 20.00
"4", "2024-01-02T08:00:00.0000000Z", "1003", "Product C", 8, 25.00
"5", "2024-01-03T08:00:00.0000000Z", "1002", "Product B", 3, 15.00
```

This should produce the expanded output:

```
Product ID, Product Name, Total Quantity Sold, Total Revenue, Average Price per Unit, Max Price per Unit, Min Price per Unit, Standard Deviation of Prices per Unit
"1001", "Product A", 25, 500.00, 20.00, 20.00, 20.00, 0.00
"1002", "Product B", 8, 120.00, 15.00, 15.00, 15.00, 0.00
"1003", "Product C", 8, 200.00, 25.00, 25.00, 25.00, 0.00
```

### Example Secondary Output CSV (hourly_output.csv):

```
Hourly Timestamp, Total Products Sold, Total Revenue, Average Unit Price
"2024-01-01T08:00:00.0000000Z", 15, 300.00, 20.00
"2024-01-01T09:00:00.0000000Z", 5, 75.00, 15.00
"2024-01-02T08:00:00.0000000Z", 8, 200.00, 25.00
```

You're required to perform the following additional tasks:

- Transform the 'Date' column into a numerical representation (e.g., number of days since a reference date).
- Calculate the average price per unit, maximum and minimum prices per unit, and standard deviation of prices per unit for each product.
- Implement parallel processing to speed up data processing.

### Output:

The output CSV file should include the following additional columns:

```
Product ID, Product Name, Total Quantity Sold, Total Revenue, Average Price per Unit, Max Price per Unit, Min Price per Unit, Standard Deviation of Prices per Unit
```

The `hourly_output.csv` file should have the following columns:

```
Hourly Timestamp, Total Products Sold, Total Revenue, Average Unit Price
```

### Constraints:

- Implementations should be optimized for CPU performance, utilizing parallel processing where applicable to handle large datasets efficiently.

This level of the challenge requires not only efficient disk I/O handling but also optimized CPU utilization for data transformation, advanced aggregation, and parallel processing to handle extremely large datasets effectively.