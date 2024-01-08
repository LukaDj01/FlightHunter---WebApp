CREATE TABLE "Flight" (
    serial_number text,
    capacity int,
    available_seats int,
    "avioCompanyId" text,
    "landAirportPib" text,
    "takeOffAirportPib" text,
    "planeSerialNumber" text,
    dateTimeLand text,
    dateTimeTakeOff text,
    gateLand text,
    gateTakeOff text,
    PRIMARY KEY ("stateLand", "stateTakeOff")
);

CREATE TABLE "Ticket" (
    "passengerEmail" text,
    "flightSerialNumber" text,
    purchaseDate text,
    id text,
    price text,
    seatNumber text,
    PRIMARY KEY ("passengerEmail", "flightSerialNumber" purchaseDate)
);

CREATE TABLE "Luggage" (
    "ticketId" text,
    number text,
    weight float,
    dimension float,
    pricePerKG float,
    PRIMARY KEY ("ticketId", "number")
);

CREATE TABLE "SearchFlight" (
    "stateLand" text,
    "stateTakeOff" text,
    "avioCompanyId" text,
    "landAirportPib" text,
    "takeOffAirportPib" text,
    "planeSerialNumber" text,
    "flightSerialNumber" text,
    PRIMARY KEY ("stateLand", "stateTakeOff")
);
