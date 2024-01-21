import { Passenger } from "./Passenger.js";

let email = window.localStorage.getItem("emailPass");
let passenger;
if (email != null) {
  let promPassenger = await fetch(
    `http://localhost:5163/Passenger/GetPassenger/${email}`
  );
  await promPassenger.json().then((p) => {
    passenger = new Passenger(
      p.email,
      p.password,
      p.passport,
      p.phone,
      p.birth_date,
      p.nationality,
      p.first_name,
      p.last_name,
      p.addr_street,
      p.addr_stNo,
      p.feedbacks,
      p.tickets
    );
  });
}
let dropDownProfil = document.querySelector(".dropDownProfil");
if(email!=null)
{
    let promPassenger = await fetch(`http://localhost:5163/Passenger/GetPassenger/${email}`);
    await promPassenger.json().then(p=>{
        passenger = new Passenger(p.email, p.password, p.passport, p.phone, p.birth_date, p.nationality, p.first_name, p.last_name, p.addr_street, p.addr_stNo, p.feedbacks, p.tickets);
    });

  let li = document.createElement("li");
  let a = document.createElement("a");
  a.classList.add("dropdown-item");
  a.href = "profile.html";
  a.innerHTML = "Profile";
  li.appendChild(a);
  dropDownProfil.appendChild(li);

  let signOutBtn = document.createElement("button");
  signOutBtn.classList.add("dropdown-item");
  signOutBtn.innerHTML = "Sign Out";
  dropDownProfil.appendChild(signOutBtn);

    signOutBtn.addEventListener("click", function () {
        window.localStorage.removeItem("emailPass");
        let url = "./index.html";
        location.href = url;
    });
}
else
{
  let signOutBtn = document.createElement("button");
    signOutBtn.classList.add("dropdown-item");
    signOutBtn.innerHTML="Sing in";
    dropDownProfil.appendChild(signOutBtn);

  signOutBtn.addEventListener("click", function () {
    let url = "./login-register.html";
    location.href = url;
  });
}


//logika za pretrazivanje letova
let takeOffField = document.querySelector(".takeOffField");
let takeOffTitle = document.createElement("option");
takeOffTitle.innerHTML= "Airport";
takeOffTitle.value = 0;
takeOffField.appendChild(takeOffTitle);
let promAirports = await fetch(`http://localhost:5163/Airport/GetAirports`);
await promAirports.json().then(airports=>{
		airports.forEach(airport=>{
			takeOffTitle = document.createElement("option");
			takeOffTitle.innerHTML= `${airport.city} (${airport.name})`;
			takeOffTitle.value = airport.pib;
			takeOffField.appendChild(takeOffTitle);
		});
});

let landField = document.querySelector(".landField");
let landTitle = document.createElement("option");
landTitle.innerHTML= "Airport";
landTitle.value = 0;
landField.appendChild(landTitle);

takeOffField.onchange=()=>{
    let takeoffAirport = takeOffField.options[takeOffField.selectedIndex].value;
    if(takeoffAirport==0)
    {
        return;
    }
    //console.log(takeoffAirport);
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/o/o/o`)
    .then(p=>{
		if(p.ok){
			p.json().then(flights=>{
                flights.forEach(flight=>{
                    //console.log(flight);
                    while(landField.lastChild.value!=0){
                        landField.removeChild(landField.lastChild);
                    }
                    landTitle = document.createElement("option");
                    landTitle.innerHTML= `${flight.landAirport.city} (${flight.landAirport.name})`;
                    landTitle.value = flight.landAirport.pib;
                    landField.appendChild(landTitle);
                });
            });
		}
		else
		{
			console.log("greska preuzimanje letova sa izabranim polaznim aerodromom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
};


let avioCompanyField = document.querySelector(".avioCompanyField");
let avioCompanyTitle = document.createElement("option");
avioCompanyTitle.innerHTML= "Avio Company";
avioCompanyTitle.value = 0;
avioCompanyField.appendChild(avioCompanyTitle);

landField.onchange=()=>{
    let landAirport = landField.options[landField.selectedIndex].value;
    let takeoffAirport = takeOffField.options[takeOffField.selectedIndex].value;
    if(landAirport==0 || takeoffAirport==0)
    {
        return;
    }
    //console.log(landAirport);
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/${landAirport}/o/o`)
    .then(p=>{
		if(p.ok){
			p.json().then(flights=>{
                flights.forEach(flight=>{
                    while(avioCompanyField.lastChild.value!=0){
                        avioCompanyField.removeChild(avioCompanyField.lastChild);
                    }
                    avioCompanyTitle = document.createElement("option");
                    avioCompanyTitle.innerHTML= `${flight.avioCompany.name}`;
                    avioCompanyTitle.value = flight.avioCompany.email;
                    avioCompanyField.appendChild(avioCompanyTitle);
                });
            });
		}
		else
		{
			console.log("greska preuzimanje letova sa izabranim dolaznim aerodromom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
};

let dateField = document.querySelector(".dateField");
let dateTitle = document.createElement("option");
dateTitle.innerHTML= "Departure date";
dateTitle.value = 0;
dateField.appendChild(dateTitle);

avioCompanyField.onchange=()=>{
    let landAirport = landField.options[landField.selectedIndex].value;
    let takeoffAirport = takeOffField.options[takeOffField.selectedIndex].value;
    let avioCompany = avioCompanyField.options[avioCompanyField.selectedIndex].value;
    if(landAirport==0 || takeoffAirport==0 || avioCompany==0)
    {
        return;
    }
    //console.log(landAirport);
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/${landAirport}/${avioCompany}/o`)
    .then(p=>{
		if(p.ok){
			p.json().then(flights=>{
                flights.forEach(flight=>{
                    while(dateField.lastChild.value!=0){
                        dateField.removeChild(dateField.lastChild);
                    }
                    let dateView = new Date(flight.dateTimeTakeOff);
                    dateTitle = document.createElement("option");
	                  dateTitle.innerHTML= `${dateView.getDate()}.${(dateView.getMonth()+1)}.${dateView.getFullYear()}.`;
                    dateTitle.value = flight.dateTimeTakeOff;
                    dateField.appendChild(dateTitle);
                });
            });
		}
		else
		{
			console.log("greska preuzimanje letova sa izabranom avio kompanijom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
};

let searchBtn = document.querySelector(".searchBtn");
searchBtn.addEventListener("click", function () {
    let takeoffAirport = takeOffField.options[takeOffField.selectedIndex].value;
    let landAirport = landField.options[landField.selectedIndex].value;
    let avioCompany = avioCompanyField.options[avioCompanyField.selectedIndex].value;
    let date = dateField.value;
    if(takeoffAirport==0)
    {
        console.log("unesite pocetnu destinaciju");
        return;
    }
    if(landAirport==0)
    {
        console.log("unesite zavrsnu destinaciju");
        return;
    }
    if(avioCompany==0)
    {
        console.log("unesite aviokompaniju");
        return;
    }
    if(date=="")
    {
        console.log("unesite datum polaska");
        return;
    }
    //console.log(takeoffAirport, landAirport, avioCompany, date);
    location.href=`../Client/kupovina.html?pib1=${takeoffAirport}&pib2=${landAirport}&ac=${avioCompany}&date=${date}`;
});
