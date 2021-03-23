/**
 * Flight management system using pointers and doubly linked list:
 * The user should be able to add/remove destination cities, list all possible cities,
 * add/remove flights, list flight times and their corresponding flight capacities, and schedule/unschedule seats
 * on said flights.
 **/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>
#include <assert.h>

// Limit constants
#define MAX_CITY_NAME_LEN 20
#define MAX_FLIGHTS_PER_CITY 5
#define MAX_DEFAULT_SCHEDULES 50

// Time definitions
#define TIME_MIN 0
#define TIME_MAX ((60 * 24)-1)
#define TIME_NULL -1


/******************************************************************************
 * Structure and Type definitions                                             *
 ******************************************************************************/
typedef int time_t;                        // integers used for time values
typedef char city_t[MAX_CITY_NAME_LEN+1];; // null terminate fixed length city
 
// Structure to hold all the information for a single flight
//   A city's schedule has an array of these
struct flight {
  time_t time;       // departure time of the flight
  int available;  // number of seats currently available on the flight
  int capacity;   // maximum seat capacity of the flight
};

// Structure for an individual flight schedule
// The main data structure of the program is an Array of these structures
// Each structure will be placed on one of two linked lists:
//                free or active
// Initially the active list will be empty and all the schedules
// will be on the free list.  Adding a schedule is finding the first
// free schedule on the free list, removing it from the free list,
// setting its destination city and putting it on the active list
struct flight_schedule {
  city_t destination;                          // destination city name
  struct flight flights[MAX_FLIGHTS_PER_CITY]; // array of flights to the city
  struct flight_schedule *next;                // link list next pointer
  struct flight_schedule *prev;                // link list prev pointer
};

/******************************************************************************
 * Global / External variables                                                *
 ******************************************************************************/
// This program uses two global linked lists of Schedules.  See comments
// of struct flight_schedule above for details
struct flight_schedule *flight_schedules_free = NULL;
struct flight_schedule *flight_schedules_active = NULL;


/******************************************************************************
 * Function Prototypes                                                        *
 ******************************************************************************/
// Misc utility io functions
int city_read(city_t city);           
bool time_get(time_t *time_ptr);      
bool flight_capacity_get(int *capacity_ptr);
void print_command_help(void);

/****************************************************************
 * Message functions so that your messages match what we expect *
 ****************************************************************/
void msg_city_bad(char *city) {
  printf("No schedule for %s\n", city);
}

void msg_city_exists(char *city) {
  printf("There is a schedule of %s already.\n", city);
}

void msg_schedule_no_free(void) {
  printf("Sorry no more free schedules.\n");
}

void msg_city_flights(char *city) {
  printf("The flights for %s are:", city);
}

void msg_flight_info(int time, int avail, int capacity) {
  printf(" (%d, %d, %d)", time, avail, capacity);
}

void msg_city_max_flights_reached(char *city) {
  printf("Sorry we cannot add more flights on this city.\n");
}

void msg_flight_bad_time(void) {
  printf("Sorry there's no flight scheduled on this time.\n");
}

void msg_flight_no_seats(void) {
    printf("Sorry there's no more seats available!\n");
}

void msg_flight_all_seats_empty(void) {
  printf("All the seats on this flights are empty!\n");
}

void msg_time_bad() {
  printf("Invalid time value\n");
}

void msg_capacity_bad() {
  printf("Invalid capacity value\n");
}

int flight_compare_time(const void *a, const void *b) 
{
  const struct flight *af = a;
  const struct flight *bf = b;
  
  return (af->time - bf->time);
}

void flight_schedule_sort_flights_by_time(struct flight_schedule *fs) 
{
  qsort(fs->flights, MAX_FLIGHTS_PER_CITY, sizeof(struct flight),
	flight_compare_time);
}
// Core functions of the program

void flight_schedule_initialize(struct flight_schedule array[], int n);

struct flight_schedule * flight_schedule_find(city_t city){
  // assign find to active pointer
  // check if city is in the active list
  struct flight_schedule * find = flight_schedules_active;
  if (flight_schedules_active != NULL) {
    while (strcmp(find->destination, city) != 0) {
      if (find->next == NULL) {
        break;
      }
      find = find->next;
    }
  }
    return find;
}

struct flight_schedule * flight_schedule_allocate(void){
  // Take empty element from free list and put it into the active list
  // return the pointer for the moved element
  struct flight_schedule * p1 = flight_schedules_free; // assign p1 to the first element
  flight_schedules_free = flight_schedules_free->next; // change the free pointer to point to second element in free
  
  flight_schedules_free->prev = NULL;
  
  // check if flight_schedules_active is empty or not
  // then connect p1 to p2
  if (flight_schedules_active == NULL) {
    flight_schedules_active = p1;
    flight_schedules_active->prev = NULL;
    flight_schedules_active->next = NULL;
  }
  else {
    struct flight_schedule * p2 = flight_schedules_active;
    while (p2->next != NULL) {
      p2 = p2->next;
    }
    p2->next = p1;
    p1->next = NULL;
    p1->prev = p2;
  }

  return p1;
}
void flight_schedule_free(struct flight_schedule *fs){
    // take an active node off the list
    // assign it back to free
    // reset fs

    // check if the node is also flight_schedules_active
    if (fs == flight_schedules_active && flight_schedules_active->next != NULL) {
      flight_schedules_active = flight_schedules_active->next;
    }

    fs->destination[0] = 0;
    for (int i=0; i<MAX_FLIGHTS_PER_CITY; i++) {
      fs->flights[i].time = TIME_NULL;
      fs->flights[i].available = 0;
      fs->flights[i].capacity = 0;
    }
    if (fs->prev != NULL) {
      fs->prev->next = NULL;
    }
    fs->next = NULL;
    fs->prev = NULL;

    struct flight_schedule * temp = flight_schedules_free;

    if (flight_schedules_free != NULL) { // check if there is a free list
      while (temp->next != NULL) {
        temp = temp->next;
      }
      temp->next = fs;
      fs->prev = temp;
      fs->next = NULL;
    }
    else {
      flight_schedules_free = fs;  // no list exist, assign free pointer
    }                              
}

void flight_schedule_add(city_t city){
  // add a new node to active list
      struct flight_schedule * temp = flight_schedule_allocate();
      strcpy(temp->destination, city); 
}

void flight_schedule_listAll(void) {
  // list every flight destination in the active list
  struct flight_schedule * temp = flight_schedules_active;
  if (flight_schedules_active != NULL) {
    if (temp->next == NULL) {
      printf("%s\n", temp->destination);  // if there is only one city
    }
    else {
      while (temp->next != NULL) {        // loop the rest of cities
        printf("%s\n", temp->destination);
        temp = temp->next; 
      }
      if (temp->next == NULL) {
        printf("%s\n", temp->destination);
      }
    }
  }
}

void flight_schedule_list(city_t city){
  // list the flights in a city
  struct flight_schedule * temp = flight_schedule_find(city);
  time_t input_time;
  int input_capacity;
  int seat;
  msg_city_flights(city);

  for (int i=0; i<MAX_FLIGHTS_PER_CITY; i++) { // print out all the flights except the empty ones
    if (temp->flights[i].time != TIME_NULL) {
      input_time = temp->flights[i].time;
      input_capacity = temp->flights[i].capacity;
      seat = temp->flights[i].available;
      msg_flight_info(input_time, temp->flights[i].available, input_capacity);
    }
  }
  printf("\n");
}

void flight_schedule_add_flight(city_t city){
  // add a flight to a city in existing shcedule
  struct flight_schedule * temp = flight_schedule_find(city);

  time_t input_time;
  int input_capacity;
  int seat;

  if (time_get(&input_time) && flight_capacity_get(&input_capacity)){
    for (int i = 0; i < MAX_FLIGHTS_PER_CITY; i++) { // get input, find an empty flight and replace it
      if (temp->flights[i].time == TIME_NULL){
        temp->flights[i].time = input_time;
        temp->flights[i].capacity = input_capacity;
        temp->flights[i].available = input_capacity;
        break;
      }
      if (i == MAX_FLIGHTS_PER_CITY - 1) {
        msg_city_max_flights_reached(city);
      }
    }
  }
}

void flight_schedule_remove_flight(city_t city){
  // remove a flight from a schedule through the time input
  struct flight_schedule * temp = flight_schedule_find(city);
  time_t input_time;

  if (time_get(&input_time)) {
    for (int i = 0; i < MAX_FLIGHTS_PER_CITY; i++) { // check which flight equals to input time
      if (temp->flights[i].time == input_time) {     // then reset
        temp->flights[i].time = TIME_NULL;
        temp->flights[i].available = 0;
        temp->flights[i].capacity = 0;
        break;
      }
      if (i == MAX_FLIGHTS_PER_CITY - 1) {
        msg_flight_bad_time();  // no flight
      }
    }
  }
}

void flight_schedule_schedule_seat(city_t city){
  // find available seat for a flight closest (but after) the input time
  struct flight_schedule * temp = flight_schedule_find(city);
  flight_schedule_sort_flights_by_time(temp);  // call helper function to sort the flights in time order
  time_t input_time;

  if (time_get(&input_time)) {
    for (int i = 0;  i < MAX_FLIGHTS_PER_CITY; i++) { // check if the input time is before the next closest flight
      if (i == MAX_FLIGHTS_PER_CITY - 1 && input_time > temp->flights[i].time) {
        msg_flight_no_seats();  // no flight available
      }
      if (input_time <= temp->flights[i].time) {      
        if (temp->flights[i].available == 0) {
          msg_flight_no_seats();   // no more seats
        }
        else {
          temp->flights[i].available--;
        }
        break;
      }
    }
  }
}

void flight_schedule_unschedule_seat(city_t city){
  struct flight_schedule * temp = flight_schedule_find(city);
  time_t input_time;

  if (time_get(&input_time)) {
    for (int i = 0;  i < MAX_FLIGHTS_PER_CITY; i++) {
      if (temp->flights[i].time == input_time) {
        if (temp->flights[i].capacity == temp->flights[i].available) {
          msg_flight_all_seats_empty();
          break;
        }
        temp->flights[i].available++;
        break;
      }
      if (i == MAX_FLIGHTS_PER_CITY - 1 && temp->flights[i].time != input_time) {
        msg_flight_bad_time();  // no schedule at this time
      }
    }
  }
}
void flight_schedule_remove(city_t city){
  // remove a flight from active list
  struct flight_schedule * temp = flight_schedule_find(city);
  if (temp != NULL) {
    flight_schedule_free(temp);        // then use free() to free the schedule
  }
}

void flight_schedule_sort_flights_by_time(struct flight_schedule *fs);
int  flight_compare_time(const void *a, const void *b);

int main(int argc, char *argv[]) 
{
  long n = MAX_DEFAULT_SCHEDULES;
  char command;
  city_t city;

  if (argc > 1) {
    // If the program was passed an argument then try and convert the first
    // argument in the a number that will override the default max number
    // of schedule we will support
    char *end;
    n = strtol(argv[1], &end, 10); // CPAMA p 787
    if (n==0) {
      printf("ERROR: Bad number of default max scedules specified.\n");
      exit(EXIT_FAILURE);
    }
  }


  struct flight_schedule flight_schedules[n];
 
  // Initialize our global lists of free and active schedules using
  // the elements of the flight_schedules array
  flight_schedule_initialize(flight_schedules, n);

  // DEFENSIVE PROGRAMMING:  Write code that avoids bad things from happening.
  //  When possible, if we know that some particular thing should have happened
  //  we think of that as an assertion and write code to test them.
  // Use the assert function (CPAMA p749) to be sure the initilization has set
  // the free list to a non-null value and the the active list is a null value.
  assert(flight_schedules_free != NULL && flight_schedules_active == NULL);

  // Print the instruction in the beginning
  print_command_help();

  // Command processing loop
  while (scanf(" %c", &command) == 1) {
    switch (command) {
    case 'A': 
      //  Add an active flight schedule for a new city eg "A Toronto\n"
      city_read(city);
      flight_schedule_add(city);

      break;
    case 'L':
      // List all active flight schedules eg. "L\n"
      flight_schedule_listAll();
      break;
    case 'l': 
      // List the flights for a particular city eg. "l\n"
      city_read(city);
      flight_schedule_list(city);
      break;
    case 'a':
      // Adds a flight for a particular city "a Toronto\n
      //                                      360 100\n"
      city_read(city);
      flight_schedule_add_flight(city);
      break;
    case 'r':
      // Remove a flight for a particular city "r Toronto\n
      //                                        360\n"
      city_read(city);
      flight_schedule_remove_flight(city);
	    break;
    case 's':
      // schedule a seat on a flight for a particular city "s Toronto\n
      //                                                    300\n"
      city_read(city);
      flight_schedule_schedule_seat(city);
      break;
    case 'u':
      // unschedule a seat on a flight for a particular city "u Toronto\n
      //                                                      360\n"
        city_read(city);
        flight_schedule_unschedule_seat(city);
        break;
    case 'R':
      // remove the schedule for a particular city "R Toronto\n"
      city_read(city);
      flight_schedule_remove(city);  
      break;
    case 'h':
        print_command_help();
        break;
    case 'q':
      goto done;
    default:
      printf("Bad command. Use h to see help.\n");
    }
  }
 done:
  return EXIT_SUCCESS;
}

/**********************************************************************
 * city_read: Takes in and processes a given city following a command *
 *********************************************************************/
int city_read(city_t city) {
  int ch, i=0;

  // skip leading non letter characters
  while (true) {
    ch = getchar();
    if ((ch >= 'A' && ch <= 'Z') || (ch >='a' && ch <='z')) {
      city[i++] = ch;
      break;
    }
  }
  while ((ch = getchar()) != '\n') {
    if (i < MAX_CITY_NAME_LEN) {
      city[i++] = ch;
    }
  }
  city[i] = '\0';
  return i;
}

void print_command_help()
{
  printf("Here are the possible commands:\n"
	 "A <city name>     - Add an active empty flight schedule for\n"
	 "                    <city name>\n"
	 "L                 - List cities which have an active schedule\n"
	 "l <city name>     - List the flights for <city name>\n"
	 "a <city name>\n"
         "<time> <capacity> - Add a flight for <city name> @ <time> time\n"
	 "                    with <capacity> seats\n"  
	 "r <city name>\n"
         "<time>            - Remove a flight form <city name> whose time is\n"
	 "                    <time>\n"
	 "s <city name>\n"
	 "<time>            - Attempt to schedule seat on flight to \n"
	 "                    <city name> at <time> or next closest time on\n"
	 "                    which their is an available seat\n"
	 "u <city name>\n"
	 "<time>            - unschedule a seat from flight to <city name>\n"
	 "                    at <time>\n"
	 "R <city name>     - Remove schedule for <city name>\n"
	 "h                 - print this help message\n"
	 "q                 - quit\n"
);
}


/****************************************************************
 * Resets a flight schedule                                     *
 ****************************************************************/
void flight_schedule_reset(struct flight_schedule *fs) {
    fs->destination[0] = 0;
    for (int i=0; i<MAX_FLIGHTS_PER_CITY; i++) {
      fs->flights[i].time = TIME_NULL;
      fs->flights[i].available = 0;
      fs->flights[i].capacity = 0;
    }
    fs->next = NULL;
    fs->prev = NULL;
}

/******************************************************************
* Initializes the flight_schedule array that will hold any flight *
* schedules created by the user. This is called in main for you.  *
 *****************************************************************/

void flight_schedule_initialize(struct flight_schedule array[], int n)
{
  flight_schedules_active = NULL;
  flight_schedules_free = NULL;

  // takes care of empty array case
  if (n==0) return;

  // Loop through the Array connecting them
  // as a linear doubly linked list
  array[0].prev = NULL;
  for (int i=0; i<n-1; i++) {
    flight_schedule_reset(&array[i]);
    array[i].next = &array[i+1];
    array[i+1].prev = &array[i];
  }
  // Takes care of last node.  
  flight_schedule_reset(&array[n-1]); // reset clears all fields
  array[n-1].next = NULL;
  array[n-1].prev = &array[n-2];
  flight_schedules_free = &array[0];
}

/***********************************************************
 * time_get: read a time from the user
   Time in this program is a minute number 0-((24*60)-1)=1439
   -1 is used to indicate the NULL empty time 
   This function should read in a time value and check its 
   validity.  If it is not valid eg. not -1 or not 0-1439
   It should print "Invalid Time" and return false.
   othewise it should return the value in the integer pointed
   to by time_ptr.
 ***********************************************************/
bool time_get(int *time_ptr) {
  if (scanf("%d", time_ptr)==1) {
    return (TIME_NULL == *time_ptr || 
	    (*time_ptr >= TIME_MIN && *time_ptr <= TIME_MAX));
  } 
  msg_time_bad();
  return false;
}

/***********************************************************
 * flight_capacity_get: read the capacity of a flight from the user
   This function should read in a capacity value and check its 
   validity.  If it is not greater than 0, it should print 
   "Invalid capacity value" and return false. Othewise it should 
   return the value in the integer pointed to by cap_ptr.
 ***********************************************************/
bool flight_capacity_get(int *cap_ptr) {
  if (scanf("%d", cap_ptr)==1) {
    return *cap_ptr > 0;
  }
  msg_capacity_bad();
  return false;
}