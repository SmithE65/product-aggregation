## Challenge Description:

### Level 1:

You are given a CSV file containing data about sales transactions. Each row represents a single transaction with the following columns:

1. Transaction ID
2. Date
3. Product ID
4. Product Name
5. Quantity
6. Price per Unit

Your task is to write a program that reads this CSV file, aggregates the data, and produces a new CSV file containing the following information:

1. Total sales revenue for each product.
2. Total quantity sold for each product.

### Input Schema:

The input CSV file should have the following schema:

```
Transaction ID, Date, Product ID, Product Name, Quantity, Price per Unit
```

 - Transaction ID: UTF8 max 64 bytes
 - Date: format 1970-01-01T00:00:00.0000000Z
 - Product ID: UTF8 max 64 bytes
 - Product Name: UTF8 max 512 bytes
 - Quantity: positive integer fitting in a signed 32-bit value
 - Price per Unit: positive double float

### Required Output:

The output CSV file should have the following schema:

```
Product ID, Product Name, Total Quantity Sold, Total Revenue
```

### Example:

#### Example Input CSV (input.csv):

```
Transaction ID, Date, Product ID, Product Name, Quantity, Price per Unit
"1", "2024-01-01T08:00:00.0000000Z", "1001", "Product A", 10, 20.00
"2", "2024-01-01T08:00:00.0000000Z", "1002", "Product B", 5, 15.00
"3", "2024-01-02T08:00:00.0000000Z", "1001", "Product A", 15, 20.00
"4", "2024-01-02T08:00:00.0000000Z", "1003", "Product C", 8, 25.00
"5", "2024-01-03T08:00:00.0000000Z", "1002", "Product B", 3, 15.00
```

#### Example Output CSV (output.csv):

```
Product ID, Product Name, Total Quantity Sold, Total Revenue
"1001", "Product A", 25, 500.00
"1002", "Product B", 8, 120.00
"1003", "Product C", 8, 200.00
```

### Constraints:

- The input CSV file can be up to 4 GB in size.
- The product ID is a unique identifier for each product.
- All columns are non-empty and contain valid data.
- All columns may or may not be quoted and text fields may contain escaped quotes
- Prices and quantities are non-negative integers.
- The output CSV should be sorted by Product ID.

Your task is to implement a program in your preferred programming language that reads the input CSV file, performs the required aggregations, and writes the output CSV file according to the specified schema.
