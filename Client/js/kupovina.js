import { Passenger } from "./Passenger.js";
import { Luggage } from "./Luggage.js";

const urlString = window.location.search;
const urlParam=new URLSearchParams(urlString);
const pib1 = urlParam.get('pib1');
const pib2 = urlParam.get('pib2');
const ac = urlParam.get('ac');
const date = urlParam.get('date');
//console.log(pib1, pib2, ac, date);



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
        let index;
        let i=0;
		airports.forEach(airport=>{
			takeOffTitle = document.createElement("option");
			takeOffTitle.innerHTML= `${airport.city} (${airport.name})`;
			takeOffTitle.value = airport.pib;
			takeOffField.appendChild(takeOffTitle);
            i++;
            if(airport.pib==pib1)
            {
                index=i;
                
            }
		});
        if(pib1!=null)
        {
            takeOffField.selectedIndex=index;
        }
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
                while(landField.lastChild.value!=0){
                    landField.removeChild(landField.lastChild);
                }
                let unique=[];
                flights.forEach(flight=>{
                    //console.log(flight);
                    if(unique.indexOf(flight.landAirport.pib)<0)
                    {
                        unique.push(flight.landAirport.pib);
                        landTitle = document.createElement("option");
                        landTitle.innerHTML= `${flight.landAirport.city} (${flight.landAirport.name})`;
                        landTitle.value = flight.landAirport.pib;
                        landField.appendChild(landTitle);
                    }
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
                while(avioCompanyField.lastChild.value!=0){
                    avioCompanyField.removeChild(avioCompanyField.lastChild);
                }
                let unique=[];
                flights.forEach(flight=>{
                    if(unique.indexOf(flight.avioCompany.email)<0)
                    {
                        unique.push(flight.avioCompany.email);
                        avioCompanyTitle = document.createElement("option");
                        avioCompanyTitle.innerHTML= `${flight.avioCompany.name}`;
                        avioCompanyTitle.value = flight.avioCompany.email;
                        avioCompanyField.appendChild(avioCompanyTitle);
                    }
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
                while(dateField.lastChild.value!=0){
                    dateField.removeChild(dateField.lastChild);
                }
                flights.forEach(flight=>{
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

let selectedFlight;
dateField.onchange=()=>{
    let landAirport = landField.options[landField.selectedIndex].value;
    let takeoffAirport = takeOffField.options[takeOffField.selectedIndex].value;
    let avioCompany = avioCompanyField.options[avioCompanyField.selectedIndex].value;
    let date = dateField.options[dateField.selectedIndex].value;
    if(landAirport==0 || takeoffAirport==0 || avioCompany==0 || date==0)
    {
        return;
    }
    //console.log(landAirport);
    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${takeoffAirport}/${landAirport}/${avioCompany}/${date}`)
    .then(p=>{
		if(p.ok){
			p.json().then(flights=>{
                flights.forEach(flight=>{
                    //console.log(flight);
                    selectedFlight=flight;
                });
            });
		}
		else
		{
			console.log("greska preuzimanje letova sa izabranim polaznim datumom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
};

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
    if(passenger==null){
        location.href="./login-register.html";
    }
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
    let dateTakeOff= new Date(selectedFlight.dateTimeTakeOff);
    let dateTimeString = `${dateTakeOff.getDate()}.${(dateTakeOff.getMonth()+1)}.${dateTakeOff.getFullYear()}. ${dateTakeOff.toLocaleTimeString()}`;
    dateTakeOffView.innerHTML=`Date and time: ${dateTimeString}`;
    airportLandView.innerHTML=`Airport: ${selectedFlight.landAirport.city} (${selectedFlight.landAirport.name})`;
    let dateLand= new Date(selectedFlight.dateTimeLand);
    dateTimeString = `${dateLand.getDate()}.${(dateLand.getMonth()+1)}.${dateLand.getFullYear()}. ${dateLand.toLocaleTimeString()}`;
    dateLandView.innerHTML=`Date and time: ${dateTimeString}`;
    ticketPriceView.innerHTML=`Ticket price: ${ticketPrice}`; // dodati atribut cena u flight-ove (:
    luggageView.innerHTML=`My luggage: ${luggageNameList}`;
    luggagePriceView.innerHTML=`Luggage price: ${lugPrice}`;
    priceTotalView.innerHTML=`Price total: ${totalPrice}`;
    //console.log(luggages);
    passNameView.innerHTML=`Passenger: ${passenger.first_name} ${passenger.last_name}`;
    passPassportView.innerHTML=`Passport number: ${passenger.passport}`;
    passNationalityView.innerHTML=`Nationality: ${passenger.nationality}`;
    passPhoneView.innerHTML=`Phone: ${passenger.phone}`;
});

let buyBtn = document.querySelector(".buyBtn");
buyBtn.addEventListener("click", function () {
    let date = new Date();
	date.setTime( date.getTime() - date.getTimezoneOffset()*60*1000 );
    //console.log("kupi", passenger.email, selectedFlight.serial_number, date.toISOString(), (ticketPrice+lugPrice), selectedFlight.available_seats.toString());
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
                let sendTicketEmail = document.querySelector(".sendTicketEmail");
                if(sendTicketEmail.checked)
                {
                    let poruka = `Ticket ID: ${ticketID}, From: ${selectedFlight.takeOffAirport.city} (${selectedFlight.takeOffAirport.name}), To: ${selectedFlight.landAirport.city} (${selectedFlight.landAirport.name}), Date: ${selectedFlight.dateTimeTakeOff}, AvioCompany: ${selectedFlight.avioCompany.name}, Seat No.: ${selectedFlight.available_seats}`;
                    sendEmail(poruka);
                }
                location.reload();
            });
        }
        else
        {
            console.log("greska kupovina karte");
        }
    }).catch(errorMsg=>console.log(errorMsg));
});

if(pib1!=null)
{
    await fetch(`http://localhost:5163/Flight/GetFlightsSearch/${pib1}/${pib2}/${ac}/${date}`)
    .then(p=>{
		if(p.ok){
			p.json().then(flights=>{
                //console.log(flights);
                flights.forEach(flight=>{
                    //console.log(flight);
                    selectedFlight=flight;
                });
                fetch(`http://localhost:5163/Flight/GetFlightsSearch/${selectedFlight.takeOffAirport.pib}/o/o/o`)
                .then(p=>{
                    if(p.ok){
                        p.json().then(flights=>{
                            while(landField.lastChild.value!=0){
                                landField.removeChild(landField.lastChild);
                            }
                            let indeks;
                            let i = 0;
                            flights.forEach(flight=>{
                                //console.log(flight);
                                landTitle = document.createElement("option");
                                landTitle.innerHTML= `${flight.landAirport.city} (${flight.landAirport.name})`;
                                landTitle.value = flight.landAirport.pib;
                                landField.appendChild(landTitle);
                                i++;
                                if(flight.landAirport.pib == pib2)
                                {
                                    indeks = i;
                                }
                            });
                            if(pib2!=null)
                            {
                                landField.selectedIndex=i;
                            }
                            fetch(`http://localhost:5163/Flight/GetFlightsSearch/${selectedFlight.takeOffAirport.pib}/${selectedFlight.landAirport.pib}/o/o`)
                            .then(p=>{
                                if(p.ok){
                                    p.json().then(flights=>{
                                        while(avioCompanyField.lastChild.value!=0){
                                            avioCompanyField.removeChild(avioCompanyField.lastChild);
                                        }
                                        let indeks;
                                        let i = 0;
                                        flights.forEach(flight=>{
                                            avioCompanyTitle = document.createElement("option");
                                            avioCompanyTitle.innerHTML= `${flight.avioCompany.name}`;
                                            avioCompanyTitle.value = flight.avioCompany.email;
                                            avioCompanyField.appendChild(avioCompanyTitle);
                                            i++;
                                            if(flight.avioCompany.email == ac)
                                            {
                                                indeks = i;
                                            }
                                        });
                                        if(ac!=null)
                                        {
                                            avioCompanyField.selectedIndex=i;
                                        }
                                    });
                                    fetch(`http://localhost:5163/Flight/GetFlightsSearch/${selectedFlight.takeOffAirport.pib}/${selectedFlight.landAirport.pib}/${selectedFlight.avioCompany.email}/o`)
                                    .then(p=>{
                                        if(p.ok){
                                            p.json().then(flights=>{
                                                while(dateField.lastChild.value!=0){
                                                    dateField.removeChild(dateField.lastChild);
                                                }
                                                let indeks;
                                                let i = 0;
                                                flights.forEach(flight=>{
                                                    let dateView = new Date(flight.dateTimeTakeOff);
                                                    dateTitle = document.createElement("option");
                                                    dateTitle.innerHTML= `${dateView.getDate()}.${(dateView.getMonth()+1)}.${dateView.getFullYear()}.`;
                                                    dateTitle.value = flight.dateTimeTakeOff;
                                                    dateField.appendChild(dateTitle);
                                                    i++;
                                                    if(flight.dateTimeTakeOff == date)
                                                    {
                                                        indeks = i;
                                                    }
                                                });
                                                if(date!=null)
                                                {
                                                    dateField.selectedIndex=i;
                                                }
                                            });
                                        }
                                        else
                                        {
                                            console.log("greska preuzimanje letova sa izabranom avio kompanijom");
                                        }
                                    }).catch(errorMsg=>console.log(errorMsg));
                                }
                                else
                                {
                                    console.log("greska preuzimanje letova sa izabranim dolaznim aerodromom");
                                }
                            }).catch(errorMsg=>console.log(errorMsg));
                        });
                    }
                    else
                    {
                        console.log("greska preuzimanje letova sa izabranim polaznim aerodromom");
                    }
	            }).catch(errorMsg=>console.log(errorMsg));
            });
		}
		else
		{
			console.log("greska preuzimanje letova sa izabranim polaznim datumom");
		}
	}).catch(errorMsg=>console.log(errorMsg));
}

var Email = { send: function (a) { return new Promise(function (n, e) { a.nocache = Math.floor(1e6 * Math.random() + 1),
    a.Action = "Send"; var t = JSON.stringify(a); Email.ajaxPost("https://smtpjs.com/v3/smtpjs.aspx?", 
    t, function (e) { n(e) }) }) }, ajaxPost: function (e, n, t) 
    { var a = Email.createCORSRequest("POST", e); a.setRequestHeader("Content-type", "application/x-www-form-urlencoded"), 
    a.onload = function () { var e = a.responseText; null != t && t(e) }, a.send(n) }, 
    ajax: function (e, n) { var t = Email.createCORSRequest("GET", e); t.onload = function () 
    { var e = t.responseText; null != n && n(e) }, t.send() }, createCORSRequest: function (e, n) 
    { var t = new XMLHttpRequest; return "withCredentials" in t ? t.open(e, n, !0) : "undefined" != typeof XDomainRequest ? (t = new XDomainRequest).open(e, n) : t = null, t } };

function sendEmail(poruka){
    //function send email
    Email.send({
        Host : "smtp.elasticemail.com",
        Username : "samanoku@elfak.rs",
        Password : "87FCB833ABCDB11D7B729D833B989545D9C7",
        To : "lukaluxy01@gmail.com", // passenger.email
        From : "samanoku@elfak.rs",
        Subject : "Informacije o kupljenoj karti",
        Body : JSON.stringify(poruka)
    })
}