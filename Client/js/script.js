import { AvioCompany } from "./AvioCompany.js";

document.getElementById('button').addEventListener("click", function() {
	document.querySelector('.bg-modal').style.display = "flex";
});

let avioCompany;
let promAvioCompany = await fetch(`http://localhost:5163/AvioCompany/GetAvioCompany/user@example.com`); // email avio kompanije
await promAvioCompany.json().then(ac=>{
		avioCompany = new AvioCompany(ac.email, ac.password, ac.name, ac.phone, ac.state, ac.expiredFlights, ac.flights, ac.feedbacks, ac.planes);
});
//console.log(avioCompany);

let flightsTable = document.querySelector(".flightsTable");
avioCompany.flights.forEach(flight=>{
	let tableRow = document.createElement("tr");
	tableRow.value = flight.serial_number;
	let tableData = document.createElement("td");
	tableData.innerHTML= flight.takeOffAirport.city;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= flight.landAirport.city;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= flight.plane.type;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	let date = new Date(flight.dateTimeTakeOff);
	tableData.innerHTML= `${date.getDate()}.${(date.getMonth()+1)}.${date.getFullYear()}.`;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= date.toLocaleTimeString();
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= flight.capacity;
	tableRow.appendChild(tableData);
	flightsTable.appendChild(tableRow);
});

let feedbackList = document.querySelector(".feedbackList");
avioCompany.feedbacks.forEach(fb=>{
	let div1 = document.createElement("div");
	div1.classList.add("feedback-item");
	let div2 = document.createElement("div");
	div2.classList.add("comment-container");
	let p1 = document.createElement("p");
	p1.classList.add("comment");
	p1.innerHTML=fb.comment;
	div2.appendChild(p1);
	div1.appendChild(div2);
	let p2 = document.createElement("p");
	p2.classList.add("rate");
	p2.innerHTML=`Rating: ${fb.rate}`;
	div1.appendChild(p2);
	let p3 = document.createElement("p");
	p3.classList.add("date");
	p3.innerHTML=`Date: ${fb.date}`;
	div1.appendChild(p3);
	let p4 = document.createElement("p");
	p4.classList.add("user");
	p4.innerHTML=`User: ${fb.passenger.email}`;
	div1.appendChild(p4);
	feedbackList.appendChild(div1);
});

let takeOffAirportList = document.querySelector(".takeOffAirportList");
let takeOffAirportTitle = document.createElement("option");
takeOffAirportTitle.innerHTML= "Airport";
takeOffAirportTitle.value = 0;
takeOffAirportList.appendChild(takeOffAirportTitle);

let landAirportList = document.querySelector(".landAirportList");
let landAirportTitle = document.createElement("option");
landAirportTitle.innerHTML= "Airport";
landAirportTitle.value = 0;
landAirportList.appendChild(landAirportTitle);

let promAirports = await fetch(`http://localhost:5163/Airport/GetAirports`);
await promAirports.json().then(airports=>{
		airports.forEach(airport=>{
			takeOffAirportTitle = document.createElement("option");
			takeOffAirportTitle.innerHTML= airport.name;
			takeOffAirportTitle.value = airport.pib;
			takeOffAirportList.appendChild(takeOffAirportTitle);
			landAirportTitle = document.createElement("option");
			landAirportTitle.innerHTML= airport.name;
			landAirportTitle.value = airport.pib;
			landAirportList.appendChild(landAirportTitle);
		});
});

let planeList = document.querySelector(".planeList");
let planeTitle = document.createElement("option");
planeTitle.innerHTML= "Plane";
planeTitle.value = 0;
planeList.appendChild(planeTitle);
avioCompany.planes.forEach(plane =>{
	planeTitle = document.createElement("option");
	planeTitle.innerHTML= `${plane.serialNumber} (${plane.type})`;
	planeTitle.value = plane.serialNumber;
	planeList.appendChild(planeTitle);
});

// dugme za dodavanje leta
let AddFlightBtn = document.querySelector(".addFlight");
AddFlightBtn.addEventListener("click", function(){
	let airportTakeOff = takeOffAirportList.options[takeOffAirportList.selectedIndex].value;
	if(airportTakeOff == 0)
	{
		console.log("Izaberite polazni aerodrom");
		return;
	}
	let airportLand = landAirportList.options[landAirportList.selectedIndex].value;
	if(airportLand == 0)
	{
		console.log("Izaberite dolazni aerodrom");
		return;
	}
	let plane = planeList.options[planeList.selectedIndex].value;
	if(plane == 0)
	{
		console.log("Izaberite avion");
		return;
	}
	let dateTakeOff = document.querySelector(".takeOffDate").value;
	if(dateTakeOff == "")
	{
		console.log("Izaberite polazni datum");
		return;
	}
	let dateLand = document.querySelector(".landDate").value;
	if(dateLand == "")
	{
		console.log("Izaberite dolazni aerodrom");
		return;
	}
	let gateTakeOff = document.querySelector(".takeOffGate").value;
	if(gateTakeOff == "")
	{
		console.log("Izaberite polazni gate");
		return;
	}
	let gateLand = document.querySelector(".landGate").value;
	if(gateLand == "")
	{
		console.log("Izaberite dolazni gate");
		return;
	}
	let capacity = document.querySelector(".capacity").value;
	if(capacity == "")
	{
		console.log("Izaberite kapacitet");
		return;
	}
	//console.log(avioCompany.email, airportTakeOff, airportLand,plane,capacity,dateLand, dateTakeOff, gateLand, gateTakeOff);
	fetch(`http://localhost:5163/Flight/AddFlight/${avioCompany.email}/${airportTakeOff}/${airportLand}/${plane}`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify({
			capacity: capacity,
        	available_seats: capacity,
			dateTimeLand: dateLand,
        	dateTimeTakeOff: dateTakeOff,
			gateLand: gateLand,
        	gateTakeOff: gateTakeOff
		})
	}).then(p=>{
		if(p.ok){
			location.reload();
		}
		else
		{
			console.log("nesto je poslo po zlu iks de");
		}
	}).catch(errorMsg=>console.log(errorMsg));
});


// dugme za dodavanje leta
let AddPlaneBtn = document.querySelector(".addPlane");
AddPlaneBtn.addEventListener("click", function(){
	let fuelList = document.querySelector(".fuelList");
	let fuel = fuelList.options[fuelList.selectedIndex].value;
	if(fuel == "jetFuel") // ???
	{
		console.log("Izaberite gorivo");
		return;
	}
	let type = document.querySelector(".planeType").value;
	if(type == "")
	{
		console.log("Izaberite tip aviona");
		return;
	}

	fetch(`http://localhost:5163/Plane/AddPlane/${avioCompany.email}`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify({
			fuel: fuel,
        	type: type
		})
	}).then(p=>{
		if(p.ok){
			location.reload();
		}
		else
		{
			console.log("nesto je poslo po zlu iks de");
		}
	}).catch(errorMsg=>console.log(errorMsg));
});

