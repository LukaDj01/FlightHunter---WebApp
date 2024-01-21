CREATE KEYSPACE "FlightHunter" WITH replication = {'class':'SimpleStrategy', 'replication_factor': 1};

CREATE TABLE "Flight" (
    serial_number text,
    capacity int,
    available_seats int,
    "avioCompanyEmail" text,
    "landAirportPib" text,
    "takeOffAirportPib" text,
    "planeSerialNumber" text,
    "dateTimeLand" text,
    "dateTimeTakeOff" text,
    "gateLand" text,
    "gateTakeOff" text,
    PRIMARY KEY ("serial_number")
);

CREATE TABLE "FlightAC" (
    "avioCompanyEmail" text,
    serial_number text,
    capacity int,
    available_seats int,
    "landAirportPib" text,
    "takeOffAirportPib" text,
    "planeSerialNumber" text,
    "dateTimeLand" text,
    "dateTimeTakeOff" text,
    "gateLand" text,
    "gateTakeOff" text,
    PRIMARY KEY ("avioCompanyEmail", serial_number)
);

CREATE TABLE "TicketFlight" (
    "flightSerialNumber" text,
    "passengerEmail" text,
    "purchaseDate" text,
    id text,
    price float,
    "seatNumber" text,
    PRIMARY KEY ("flightSerialNumber", "passengerEmail", "purchaseDate")
);

CREATE TABLE "TicketPass" (
    "passengerEmail" text,
    "flightSerialNumber" text,
    "purchaseDate" text,
    id text,
    price float,
    "seatNumber" text,
    PRIMARY KEY ("passengerEmail", "flightSerialNumber", "purchaseDate")
);

CREATE TABLE "Luggage" (
    "ticketId" text,
    number text,
    weight float,
    dimension text,
    "pricePerKG" float,
    PRIMARY KEY ("ticketId", number)
);

CREATE TABLE "SearchFlight" (
    "takeOffAirportPib" text,
    "landAirportPib" text,
    "avioCompanyEmail" text,
    "dateTimeTakeOff" text,
    "flightSerialNumber" text,
    PRIMARY KEY ("takeOffAirportPib", "landAirportPib", "avioCompanyEmail", "dateTimeTakeOff", "flightSerialNumber")
);


// LAKSE ZA KOPIRANJE U CMD!!!

CREATE TABLE "Flight" (serial_number text,capacity int,available_seats int,"avioCompanyEmail" text,"landAirportPib" text,"takeOffAirportPib" text,"planeSerialNumber" text,"dateTimeLand" text,"dateTimeTakeOff" text,"gateLand" text,"gateTakeOff" text,PRIMARY KEY ("serial_number"));

CREATE TABLE "FlightAC" ("avioCompanyEmail" text,serial_number text,capacity int,available_seats int,"landAirportPib" text,"takeOffAirportPib" text,"planeSerialNumber" text,"dateTimeLand" text,"dateTimeTakeOff" text,"gateLand" text,"gateTakeOff" text,PRIMARY KEY ("avioCompanyEmail", serial_number));

CREATE TABLE "TicketFlight" ("flightSerialNumber" text,"passengerEmail" text,"purchaseDate" text,id text,price float,"seatNumber" text,PRIMARY KEY ("flightSerialNumber", "passengerEmail", "purchaseDate"));

CREATE TABLE "TicketPass" ("passengerEmail" text,"flightSerialNumber" text,"purchaseDate" text,id text,price float,"seatNumber" text,PRIMARY KEY ("passengerEmail", "flightSerialNumber", "purchaseDate"));

CREATE TABLE "Luggage" ("ticketId" text,number text,weight float,dimension text,"pricePerKG" float,PRIMARY KEY ("ticketId", number));

CREATE TABLE "SearchFlight" ("takeOffAirportPib" text,"landAirportPib" text,"avioCompanyEmail" text,"dateTimeTakeOff" text,"flightSerialNumber" text,PRIMARY KEY ("takeOffAirportPib", "landAirportPib", "avioCompanyEmail", "dateTimeTakeOff", "flightSerialNumber"));