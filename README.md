# CoffeeShop-POS
A point of sale system for a coffee shop.

# Tasks Completed

- [ ] REQUIRED: For any given drink order, provide a correct price to the customer. This price should account for the variety of options applicable to a drink such as type, size, and add-ons. As part of the input file, you will be provided with information about the price options for different sized drinks, types of drinks and add-ons such as an extra shot of espresso, flavored syrup, or whipped cream. For any given combination of these things, your program should be able to output a price for the order. A few constraints do apply: customers shouldn't be able to order an add-on without ordering a drink and customers must specify a size for their drink.

   - [X] Get input from customer via console
   - [X] Give price to customer via console
   - [X] Output receipt to console
   - [X] Give price to customer via `./outputs/ADAM.CLARK.X.json`

- [ ] Additional Tasks

   - [X] Implement the ability to use food items. The input for food items has been defined in the Inputs section.

   - [ ] Allow purchase of "meals" that include a food and a drink item as a combo. Allow your program to apply a 10% discount automatically when items can be grouped into combos. This discount should apply for each pair of drink and food items in the order. For example, if a customer orders two large coffees and one croissant, the discount should only apply to one of the coffees and the croissant. To be generous with your customers, you should discount the most expensive drink when calculating the total order amount.

   - [X] Allow the system to optionally pass a payment method. Provide a 5% discount to customers that pay with cash, instead of a credit card. This discount would be in addition to the discounts given for combo meals and should be applied after other discounts have been calculated.
      - Discount isn't implemented

- [X] Extra Credit:  
Create a command line interface to add, delete, or modify your shop's menu. Please be certain to clearly document your extra credit in the write up.

   - [X] Delete Items From Menu  
      See `examples/operation.4.txt` for an example  

   - [X] Add items to menu  
      See `examples/operation.5.txt` for an example  

# Build and Run Console

        cd Source/Console-App
        dotnet run ../../examples/input.1.json

# Make Targets
Run these on a `Unix-Like` platform to run a series of tests or clean up build products.

        make test
        make clean

# Build and Run Web

        cd Source/Web-App
        dotnet run

Open a web browser and go to http://localhost:5000