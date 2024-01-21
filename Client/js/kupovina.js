import { Passenger } from "./Passenger.js";
import { Luggage } from "./Luggage.js";

let email = window.localStorage.getItem("emailPass");

let passenger=null;
let luggages = [];
let dropDownProfil = document.querySelector(".dropDownProfil");
let signOutBtn = document.createElement("button");
if(email!=null)
{
    let promPassenger = await fetch(`http://localhost:5163/Passenger/GetPassenger/${email}`);
    await promPassenger.json().then(p=>{
        passenger = new Passenger(p.email, p.password, p.passport, p.phone, p.birth_date, p.nationality, p.first_name, p.last_name, p.addr_street, p.addr_stNo, p.feedbacks, p.tickets);
    });

    let li = document.createElement("li");
    let a = document.createElement("a");
    a.classList.add("dropdown-item");
    a.href="profile.html";
    a.innerHTML="Profile";
    li.appendChild(a);
    dropDownProfil.appendChild(li);

    signOutBtn.classList.add("dropdown-item");
    signOutBtn.innerHTML="Sign Out";
    dropDownProfil.appendChild(signOutBtn);

    signOutBtn.addEventListener("click", function () {
        window.localStorage.removeItem("emailPass");
        let url = "./login-register.html";
        location.href = url;
    });
}
else
{
    signOutBtn.classList.add("dropdown-item");
    signOutBtn.innerHTML="Sing in";
    dropDownProfil.appendChild(signOutBtn);

    signOutBtn.addEventListener("click", function () {
        let url = "./login-register.html";
        location.href = url;
    });
}

function toggleSection(sectionId) {
    $(sectionId).toggle();
  }

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
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/o/o`)
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
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/${landAirport}/o`)
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
			console.log("greska preuzimanje letova sa izabranim polaznim aerodromom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
};

//ovo treba posle biranja datum da se odradi
let selectedFlight;
avioCompanyField.onchange=()=>{
    let landAirport = landField.options[landField.selectedIndex].value;
    let takeoffAirport = takeOffField.options[takeOffField.selectedIndex].value;
    let avioCompany = avioCompanyField.options[avioCompanyField.selectedIndex].value;
    if(landAirport==0 || takeoffAirport==0 || avioCompany==0)
    {
        return;
    }
    //console.log(landAirport);
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/${landAirport}/${avioCompany}`)
    .then(p=>{
		if(p.ok){
			p.json().then(flights=>{
                flights.forEach(flight=>{
                    console.log(flight);
                    selectedFlight=flight;
                });
            });
		}
		else
		{
			console.log("greska preuzimanje letova sa izabranim polaznim aerodromom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
};

let dateField = document.querySelector(".dateField");
let nextBtn1 = document.querySelector(".nextBtn1");

nextBtn1.addEventListener("click", function () {
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
    toggleSection('#section_2');
});


let smallLuggage = document.querySelector(".smallLuggage");
let bigLuggage = document.querySelector(".bigLuggage");
let nextBtn2 = document.querySelector(".nextBtn2");

let avioCompanyView = document.querySelector(".avioCompanyView");
let airportTakeOffView = document.querySelector(".airportTakeOffView");
let dateTakeOffView = document.querySelector(".dateTakeOffView");
let airportLandView = document.querySelector(".airportLandView");
let dateLandView = document.querySelector(".dateLandView");
let ticketPriceView = document.querySelector(".ticketPriceView");
let luggageView = document.querySelector(".luggageView");
let luggagePriceView = document.querySelector(".luggagePriceView");
let priceTotalView = document.querySelector(".priceTotalView");

let passNameView = document.querySelector(".passNameView");
let passPassportView = document.querySelector(".passPassportView");
let passNationalityView = document.querySelector(".passNationalityView");
let passPhoneView = document.querySelector(".passPhoneView");

let totalPrice = 0;
let ticketPrice = 450;
let lugPrice=5*40;
nextBtn2.addEventListener("click", function () {
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
    let luggageNameList = "";
    let l = new Luggage(5, "10x10x20", 40); //hardkodirana cena po kilu
    luggageNameList += "1 light";
    luggages.push(l);
    let lugNum=0;
    for (let i=0;i<parseInt(smallLuggage.value);i++)
    {
        l = new Luggage(8, "20x20x20", 30);
        lugNum++;
        lugPrice+=8*30;
        luggages.push(l);
    }
    if(lugNum!=0)
    {
        luggageNameList += `, ${lugNum} small`;
        lugNum=0;
    }
    for (let i=0;i<parseInt(bigLuggage.value);i++)
    {
        l = new Luggage(23, "50x20x20", 20);
        lugNum++;
        lugPrice+=23*20;
        luggages.push(l);
    }
    if(lugNum!=0)
        luggageNameList += `, ${lugNum} big`;

    totalPrice=ticketPrice+lugPrice;
    avioCompanyView.innerHTML=`Company: ${selectedFlight.avioCompany.name}`;
    airportTakeOffView.innerHTML=`Airport: ${selectedFlight.takeOffAirport.city} (${selectedFlight.takeOffAirport.name})`;
    dateTakeOffView.innerHTML=`Date and time: ${selectedFlight.dateTimeTakeOff}`;
    airportLandView.innerHTML=`Airport: ${selectedFlight.landAirport.city} (${selectedFlight.landAirport.name})`;
    dateLandView.innerHTML=`Date and time: ${selectedFlight.dateTimeLand}`;
    ticketPriceView.innerHTML=`Ticket price: ${ticketPrice}`; // dodati atribut cena u flight-ove (:
    luggageView.innerHTML=`My luggage: ${luggageNameList}`;
    luggagePriceView.innerHTML=`Luggage price: ${lugPrice}`;
    priceTotalView.innerHTML=`Price total: ${totalPrice}`;
    
    passNameView.innerHTML=`Passenger: ${passenger.first_name} ${passenger.last_name}`;
    passPassportView.innerHTML=`Passport number: ${passenger.passport}`;
    passNationalityView.innerHTML=`Nationality: ${passenger.nationality}`;
    passPhoneView.innerHTML=`Phone: ${passenger.phone}`;
    


});

let buyBtn = document.querySelector(".buyBtn");
buyBtn.addEventListener("click", function () {
    let date = new Date();
    console.log("kupi", passenger.email, selectedFlight.serial_number, date.toISOString(), (ticketPrice+lugPrice), selectedFlight.available_seats.toString());

    fetch(`http://localhost:5163/TicketCass/AddTicket/${passenger.email}/${selectedFlight.serial_number}`, {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
    body: JSON.stringify({
        purchaseDate: date.toISOString(),
        price: parseFloat(totalPrice),
        seatNumber: selectedFlight.available_seats.toString()
    })
    }).then(p=>{
        if(p.ok){
            p.text().then(ticketID=>{
                let i=1;
                luggages.forEach(luggage => {
                    fetch(`http://localhost:5163/LuggageCass/AddLuggage/${ticketID}`, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        number: (i++).toString(),
                        weight: luggage.weight,
                        dimension: luggage.dimension,
                        pricePerKG: luggage.pricePerKG
                    })
                    }).then(p=>{
                        if(p.ok){
                            console.log("Uspesno, otvaraj sampanjac");
                        }
                        else
                        {
                            console.log("greska dodavanje prtljaga za kartu");
                        }
                    }).catch(errorMsg=>console.log(errorMsg));
                });
                location.reload();
            });
        }
        else
        {
            console.log("greska kupovina karte");
        }
    }).catch(errorMsg=>console.log(errorMsg));
});