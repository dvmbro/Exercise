# Exercise

## Exercise 1
Target class is located in file `Exercise\Domain\Product.cs`

## Exercise 3
Function is located in `Exercise\Repositories\ProductRepository.cs` and named GetFilteredProductsAsync

## Exercise 4
```
--a)
select product.Name as ProductName, category.Name as CategoryName from Product product
join Category category on  category.Id = product.CategoryId

--b)
select product.Name as TheMostExpensiveProductName, max(product.Price) as TheMostExpensiveProductPrice from Product product
join Category category on  category.Id = product.CategoryId
group by product.CategoryId

--c)
select product.Name as ProductName, sum(sale.Quantity) as QuantitySold from Product product
left join Sales sale on sale.ProductId = product.Id 
group by product.CategoryId
order by sum(sale.Quantity) desc

--d)
select category.Name as CategoryName, cast(STRFTIME('%u', sale.SaleDate) as integer) as SaleDayOfWeek , ROUND(AVG(sale.Quantity)) as AverageQuantitySold from Product product
join Category category on  category.Id = product.CategoryId
left join Sales sale on sale.ProductId = product.Id 
where SaleDayOfWeek != 7 AND cast(STRFTIME('%Y', sale.SaleDate) as integer) = 2023
group by SaleDayOfWeek
order by category.Name, SaleDayOfWeek, sum(sale.Quantity) desc
```