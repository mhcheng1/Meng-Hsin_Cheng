# Flight Mangement System

The program processes commands from user and schedule flights accordingly. <br>
The user should be able to add/remove destination cities, list all possible cities,
add/remove flights, list flight times and their corresponding flight capacities, and schedule/unschedule seats
on said flights <br>

## List of commands for the functions:

* A add - takes a city and add to schedule
* R remove - remove a city from schedule
* L listAll - list all existing schedules
* l list - list schedules of a given city
* a add flight - add a flight with given time and capacity
* r remove flight - remove a flight with given city
* s schedule seat - schedule a seat on a flight with given city
* u unschedule seat - unschedule seat on a flight with given city

For example, the user can enter the following to register a flight schedule to Boston at time 300: <br>
* a Boston\n 300 100\n

Then the user can enter the following to schedule a seat on the earliest flight to Boston: <br>
* s Boston\n 200 \n

To find all the flights currently available the user can enter: <br>
* L\n
