# WGU-C968-Final
Project for C968 - Software I
Built with Visual Studio Community Edition
.NET Framework 4.7.2
 
## Scenario

You are working for a small manufacturing organization that has outgrown its current inventory system. They have been using a spreadsheet program to manually enter inventory additions, deletions, and other data from a paper-based system but would now like you to develop a more sophisticated inventory program.

They have provided you with a mock-up of the user interface to use in the design and development of the system and a class diagram to assist you in your work. The organization also has specific business requirements that must be included as part of the application. A system analyst from your company created the solution statements outlined in the requirements section based on the manufacturing organization’s business requirements. You will use these solution statements to develop your application.

## Instructions

_Create a C# application with a graphical user interface (GUI) based on the attached “GUI Mock-Up.” Write code to display each of the following forms in the GUI:_

__A.  A main form, showing the following controls:__
* buttons for “Add,” “Modify,” “Delete,” “Search” for parts and products, and “Exit”
* lists for parts and products
* text boxes for searching for parts and products
* title labels for parts, products, and the application title

__B.  An add part form, showing the following controls:__
* radio buttons for “In-House” and “Outsourced” parts
* buttons for “Save” and “Cancel”
* text boxes for ID, name, inventory level, price, max and min values, and company name or machine ID
* labels for ID, name, inventory level, price/cost, max and min values, the application title, and company name or machine ID

__C.  A modify part form, with fields that populate with data from an existing Part, showing the following controls:__
* radio buttons for “In-House” and “Outsourced” parts
* buttons for “Save” and “Cancel”
* text boxes for ID, name, inventory level, price, max and min values, and company name or machine ID
labels for ID, name, inventory level, price, max and min values, the application title, and company name or machine ID

__D .  An add product form, showing the following controls:__
* buttons for “Save,” “Cancel,” “Add” part, and “Delete” part
* text boxes for ID, name, inventory level, price, and max and min values
* labels for ID, name, inventory level, price, max and min values, and the application
* a grid view for all parts
* a grid view for parts associated with the product
* a “Search” button and a text field with an associated list for displaying the results of the search

__E.  A modify product form, with fields that populate with data from an existing product, showing the following controls:__
* buttons for “Save,” “Cancel,” “Add” part, and “Delete” part
* text boxes for ID, name, inventory level, price, and max and min values
* labels for ID, name, inventory level, price, max and min values, and the application “all candidate parts”
* a grid view for parts associated with the product
* a “Search” button and a text box with associated list for displaying the results of the search

_Now that you’ve created the GUI, write code to create the class structure provided in the attached “UML (unified modeling language) Class Diagram.” Enable each of the following capabilities in the application:_

__F.  Using the attached “UML Class Diagram,” create appropriate classes and instance variables with the following criteria:__
* five classes with the all associated properties
* variables are accessible/modifiable through properties

__G.  Add the following functionalities to the main form, using the methods provided in the attached “UML Class Diagram”:__
* redirect the user to the “Add Part,” “Modify Part,” “Add Product,” or “Modify Product” forms
* delete a selected part or product from the grid view
* search for a part or product and display matching results
* exit the main form

__H.  Add the following functionalities to the part forms, using the methods provided in the attached “UML Class Diagram”:__

_1.   “Add Part” form_

* select “In-House” or “Outsourced”
* enter name, inventory level, price, max and min values, and company name or machine ID
* save the data and then redirect to the main form
* cancel or exit out of this form and go back to the main form

_2.   “Modify Part” form_

* select “In-House” or “Outsourced”
* modify or change data values
* save modifications to the data and then redirect to the main form
* cancel or exit out of this form and go back to the main form

__I.  Add the following functionalities to the product forms, using the methods provided in the attached “UML Class Diagram”:__

_1.   “Add Product” form_

* enter name, inventory level, price, and max and min values
* save the data and then redirect to the main form
* associate one or more parts with a product
* remove or disassociate a part from a product
* cancel or exit out of this form and go back to the main form

_2.   “Modify Product” form_

* modify or change data values
* save modifications to the data and then redirect to the main form
* associate one or more parts with a product
* remove or disassociate a part from a product
* cancel or exit out of this form and go back to the main form

__J.  Write code to address the following conditions with exception handling code:__

* Detect non-numeric values in textboxes that expect numeric values
* Min should be less than Max; and Inv should be between those two values
* Prevent the user from deleting a product that has a Part associated with it
* Confirm “Delete” actions
