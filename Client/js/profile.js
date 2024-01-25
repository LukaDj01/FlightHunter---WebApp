import { Passenger } from "./Passenger.js";

/*const urlString = window.location.search;
const urlParam=new URLSearchParams(urlString);
const email = urlParam.get('email');*/
let email = window.localStorage.getItem("emailPass");

let passenger;
let promPassenger = await fetch(`http://localhost:5163/Passenger/GetPassenger/${email}`);
await promPassenger.json().then(p=>{
    passenger = new Passenger(p.email, p.password, p.passport, p.phone, p.birth_date, p.nationality, p.first_name, p.last_name, p.addr_street, p.addr_stNo, p.feedbacks, p.tickets);
});
//console.log(passenger);

let tickcetsTable = document.querySelector(".ticket_flightsTable");
passenger.tickets.forEach(ticket=>{
	let tableRow = document.createElement("tr");
	tableRow.value = ticket.id;
	let tableData = document.createElement("td");
	tableData.innerHTML= ticket.id;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= `${ticket.flight.takeOffAirport.city} (${ticket.flight.takeOffAirport.name})`;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= `${ticket.flight.landAirport.city} (${ticket.flight.landAirport.name})`;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	let date = new Date(ticket.flight.dateTimeTakeOff);
	tableData.innerHTML= `${date.getDate()}.${(date.getMonth()+1)}.${date.getFullYear()}.`;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= ticket.flight.avioCompany.name;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= ticket.seatNumber;
	tableRow.appendChild(tableData);
	tickcetsTable.appendChild(tableRow);
});

let airportAClist = document.querySelector('.airportAClist');
let op = document.createElement("option");
op.innerHTML = "Izaberi";
op.value = "Izaberi";
airportAClist.appendChild(op);
let radioBtns = document.querySelectorAll('input[name="tripType"]');
radioBtns.forEach(radioBtn => {
    radioBtn.addEventListener("click", function () {
        while(airportAClist.lastChild.value!="Izaberi")
            airportAClist.removeChild(airportAClist.lastChild);
        if(radioBtn.value=="AC")
        {
            fetch(`http://localhost:5163/AvioCompany/GetAvioCompaniesForRate/${passenger.email}`)
            .then(p=>{
                if(p.ok){
                    p.json().then(acs=>{
                        let unique=[];
                        acs.forEach(ac => {
                            if(unique.indexOf(ac.email)<0)
                            {
                                unique.push(ac.email);
                                let op = document.createElement("option");
                                op.innerHTML = ac.name;
                                op.value = ac.email;
                                airportAClist.appendChild(op);
                            }
                        });
                    })
                }
                else
                {
                    console.log("desila se greska kod preuzimanja avio kompanija");
                }
            }).catch(errorMsg=>console.log(errorMsg));
            
        }
        else
        {
            fetch(`http://localhost:5163/Airport/GetAirportsForRate/${passenger.email}`)
            .then(p=>{
                if(p.ok){
                    p.json().then(as=>{
                        let unique=[];
                        as.forEach(a => {
                            if(unique.indexOf(a.pib)<0)
                            {
                                unique.push(a.pib);
                                let op = document.createElement("option");
                                    op.innerHTML = a.name;
                                    op.value = a.pib;
                                    airportAClist.appendChild(op);
                            }
                        });
                    })
                }
                else
                {
                    console.log("desila se greska kod preuzimanja avio kompanija");
                }
            }).catch(errorMsg=>console.log(errorMsg));
        }
    });
});

let signOutBtn = document.querySelector(".signOut");

signOutBtn.addEventListener("click", function () {
    window.localStorage.removeItem("emailPass");
	let url = "./index.html";
    location.href = url;
});

let nameSurname = document.querySelector(".nameSurname");
nameSurname.innerHTML=`${passenger.first_name} ${passenger.last_name}`;

let emailTitle = document.querySelector(".emailTitle");
emailTitle.innerHTML=`${passenger.email}`;

let passEmailEdit = document.querySelector(".passEmailEdit");
passEmailEdit.value=`${passenger.email}`;
let passBirthdayEdit = document.querySelector(".passBirthdayEdit");
passBirthdayEdit.value=`${passenger.birth_date}`;
let passNationalityEdit = document.querySelector(".passNationalityEdit");
passNationalityEdit.value=`${passenger.nationality}`;
let passNameEdit = document.querySelector(".passNameEdit");
passNameEdit.value=`${passenger.first_name}`;
let passSurnameEdit = document.querySelector(".passSurnameEdit");
passSurnameEdit.value=`${passenger.last_name}`;
let passPassportEdit = document.querySelector(".passPassportEdit");
passPassportEdit.value=`${passenger.passport}`;
let passPhoneEdit = document.querySelector(".passPhoneEdit");
passPhoneEdit.value=`${passenger.phone}`;
let passStreetEdit = document.querySelector(".passStreetEdit");
passStreetEdit.value=`${passenger.addr_street}`;
let passStreetNumEdit = document.querySelector(".passStreetNumEdit");
passStreetNumEdit.value=`${passenger.addr_stNo}`;

let updateBtn = document.querySelector(".updateBtn");
updateBtn.addEventListener("click", function () {
    if(passNameEdit.value=="")
    {
        console.log("polje za ime je prazno");
        return;
    }
    if(passSurnameEdit.value=="")
    {
        console.log("polje za prezime je prazno");
        return;
    }
    if(passPassportEdit.value=="")
    {
        console.log("polje za pasos je prazno");
        return;
    }
    if(passPhoneEdit.value=="")
    {
        console.log("polje za telefon je prazno");
        return;
    }
    if(passStreetEdit.value=="")
    {
        console.log("polje za ulicu je prazno");
        return;
    }
    if(passStreetNumEdit.value=="")
    {
        console.log("polje za broj ulice je prazno");
        return;
    }

    fetch(`http://localhost:5163/Passenger/UpdatePassenger/${passenger.email}`, {
		method: "PUT",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify({
			passport: passPassportEdit.value,
        	phone: passPhoneEdit.value,
			first_name: passNameEdit.value,
        	last_name: passSurnameEdit.value,
			addr_street: passStreetEdit.value,
        	addr_stNo: passStreetNumEdit.value
		})
	}).then(p=>{
		if(p.ok){
			location.reload();
		}
		else
		{
			console.log("greska azuriranje putnika");
		}
	}).catch(errorMsg=>console.log(errorMsg));
});
let numStarsRate = document.querySelector(".rating");
numStarsRate.value="0";
let AddFeedbackBtn = document.querySelector(".addFeedback");
AddFeedbackBtn.addEventListener("click", function () {
    let comment = document.querySelector(".comment").value;
    if (comment === "") {
        console.log("Napisite komentar");
        return;
    }

    let rate = numStarsRate.value;
    if (rate == "0") {
        console.log("Unesite ocenu od 1 do 5");
        return;
    }

    let feedbackType;
    if (document.getElementById("rbAC").checked) {
        feedbackType = "Avio Company";
    } else if (document.getElementById("rbAirport").checked) {
        feedbackType = "Airport";
    } else {
        console.log("Please select the type of feedback (Avio Company or Airport).");
        return;
    }
    let emailACorA = airportAClist.options[airportAClist.selectedIndex].value;
    
    if (emailACorA === "Izaberi") {
        console.log("Izaberite ponudjeno");
        return;
    }
    let passEmailForUrl = passenger.email;
    let currentDate = new Date();
	currentDate.setTime( currentDate.getTime() - currentDate.getTimezoneOffset()*60*1000 );
    
    //console.log("Podaci: ", parseInt(rate) ,comment, emailACorA , passEmailForUrl);
    // Assuming passEmail, acEmail, and airportPib are defined somewhere in your code
    let url;
    let requestBody = {
        date: currentDate.toISOString(),
        comment: comment,
        rate: parseInt(rate)
    };
    
    if (feedbackType === "Avio Company") {
        url = `http://localhost:5163/Feedback/AddFeedbackPassAC/${passEmailForUrl}/${emailACorA}`;
    } else if (feedbackType === "Airport") {
        url = `http://localhost:5163/Feedback/AddFeedbackPassAirport/${passEmailForUrl}/${emailACorA}`;
    }


    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(requestBody)
    })
    .then((p) => {
        if (p.ok) {
            location.reload();
        } else {
            console.log("Something went wrong while adding feedback.");
        }
    })
    .catch((errorMsg) => console.log(errorMsg));
});

document.querySelector(".rating").addEventListener("click", handleRating);

function handleRating(event) {
    if (event.target.classList.contains("bi-star-fill")) {
        let clickedIndex = event.target.getAttribute("data-index");
        numStarsRate.value=clickedIndex;
    }
}

// uklanjanje zastarelih letova iz cassandre i dodavanje u neo4j zbog istorije letova
fetch("http://localhost:5163/Flight/DeleteFlightsOutdated",{ method: 'DELETE' });