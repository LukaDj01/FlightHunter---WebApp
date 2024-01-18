import { Passenger } from "./Passenger.js";

const urlString = window.location.search;
const urlParam=new URLSearchParams(urlString);
const email = urlParam.get('email');
//let email = window.localStorage.getItem("emailAC");

let passenger;
let promPassenger = await fetch(`http://localhost:5163/Passenger/GetPassenger/${email}`);
await promPassenger.json().then(p=>{
    passenger = new Passenger(p.email, p.password, p.passport, p.phone, p.birth_date, p.nationality, p.first_name, p.last_name, p.addr_street, p.addr_stNo, p.feedbacks, p.tickets);
});
console.log(passenger);