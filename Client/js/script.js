import { AvioCompany } from "./AvioCompany.js";

document.getElementById('button').addEventListener("click", function() {
	document.querySelector('.bg-modal').style.display = "flex";
});

/*document.querySelector('.close').addEventListener("click", function() {
	document.querySelector('.bg-modal').style.display = "none";
});*/

let avioCompany;
let promAvioCompany = await fetch(`http://localhost:5163/AvioCompany/GetAvioCompany/user@example.com`); // email avio kompanije
await promAvioCompany.json().then(ac=>{
		avioCompany = new AvioCompany(ac.email, ac.password, ac.name, ac.phone, ac.state, ac.expiredFlights, ac.flights, ac.feedbacks, ac.planes);
});
//console.log(avioCompany);

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


let planeList = document.querySelector(".planeList");
let planeTitle = document.createElement("option");
planeTitle.innerHTML= "Plane";
planeTitle.value = 0;
planeList.appendChild(planeTitle);

// dugme za dodavanje leta
let AddFlightBtn = document.querySelector(".addFlight");
AddFlightBtn.addEventListener("click", function(){
	
});