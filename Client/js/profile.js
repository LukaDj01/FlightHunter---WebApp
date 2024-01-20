import { Passenger } from "./Passenger.js";

const urlString = window.location.search;
const urlParam=new URLSearchParams(urlString);
const email = urlParam.get('email');
//let email = window.localStorage.getItem("emailPass");

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
	tableData.innerHTML= ticket.flight.takeOffAirport.city;
	tableRow.appendChild(tableData);
	tableData = document.createElement("td");
	tableData.innerHTML= ticket.flight.landAirport.city;
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
                console.log(p);
                if(p.ok){
                    p.json().then(acs=>{
                        acs.forEach(ac => {
                            let op = document.createElement("option");
                            op.innerHTML = ac.name;
                            op.value = ac.email;
                            airportAClist.appendChild(op);
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
                        as.forEach(a => {
                            let op = document.createElement("option");
                            op.innerHTML = a.name;
                            op.value = a.pib;
                            airportAClist.appendChild(op);
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

signOutBtn.addEventListener("click", function (event) {
    event.preventDefault();
    // Clear stored user credentials or perform any other sign-out actions
    window.localStorage.removeItem("emailPass"); // Assuming you stored the email in localStorage
    window.localStorage.removeItem("emailAC");   // Assuming you stored the email in localStorage

    // Redirect to the login page or any other appropriate page
    let url = "./login-register.html";
    location.href = url;
});

let AddFeedbackBtn = document.querySelector(".addFeedback");
AddFeedbackBtn.addEventListener("click", function () {
    let comment = document.querySelector(".comment").value;
    if (comment === "") {
        console.log("Napisite komentar");
        return;
    }

    let rate = getSelectedRating();
    if (rate < 0 || rate > 5 || isNaN(rate)) {
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
    let list = document.querySelector(".airportAClist");
    let emailACorA = list.options[list.selectedIndex].value;
    
    if (list === "" || list === "Izaberi") {
        console.log("Izaberite ponudjeno");
        return;
    }
    let passEmailForUrl = passenger.email;
    let currentDate = new Date();
    
    console.log("Podaci: " + rate + "" + comment + "" + date + "" + emailACorA + "" + passEmailForUrl + "" + list);
    // Assuming passEmail, acEmail, and airportPib are defined somewhere in your code
    let url;
    let requestBody = {
        comment: comment,
        rate: rate,
        date: currentDate.toISOString(),
    };

    if (feedbackType === "Avio Company") {
        url = `http://localhost:5163/AvioCompany/AddFeedback/${passEmailForUrl}/${emailACorA}`;
    } else if (feedbackType === "Airport") {
        url = `http://localhost:5163/Airport/AddFeedback/${passEmailForUrl}/${emailACorA}`;
    }

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(requestBody),
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

function getSelectedRating() {
    
    let selectedStarIndex = document.querySelector(".rating .bi-star-fill.selected");
    return selectedStarIndex ? parseInt(selectedStarIndex.getAttribute("data-index")) : NaN;
}

document.querySelector(".rating").addEventListener("click", handleRating);

function handleRating(event) {
    if (event.target.classList.contains("bi-star-fill")) {
        let clickedIndex = event.target.getAttribute("data-index");
        // logika za hendlovanje kliknutih zvezda
    }
}
