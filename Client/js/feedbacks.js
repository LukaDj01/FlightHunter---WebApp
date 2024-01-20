import { Feedback } from "./Feedback.js";
import { Passenger } from "./Passenger.js";
let allFeedbacks = [];
let promAllFeedbacks = await fetch(`http://localhost:5163/Feedback/GetAllFeedbacks/`, {
  method: "GET",
  headers: {
	  "Content-Type": "application/json"
  }
});

// Use CRUD Read operation to get all feedbacks
await promAllFeedbacks.json().then(feedbackData => {
  feedbackData.forEach(feedback => {
	  console.log("OCEM");
	  
	// Create Feedback instance and push it to the array
	let feedbacks = new Feedback(feedback.rate, feedback.comment, feedback.date);
	allFeedbacks.push(feedbacks);

	console.log(feedbacks);
	/*let nameSurname = document.querySelector(".user");
	nameSurname.innerText=`${feedbacks.passenger.first_name} ${feedbacks.passenger.last_name}`;
	console.log(nameSurname);*/
  });

  
});


let email = window.localStorage.getItem("emailPass");
let passenger;
let promPassenger = await fetch(`http://localhost:5163/Passenger/GetPassenger/${email}`);
await promPassenger.json().then(p=>{
    passenger = new Passenger(p.email, p.password, p.passport, p.phone, p.birth_date, p.nationality, p.first_name, p.last_name, p.addr_street, p.addr_stNo, p.feedbacks, p.tickets);
	console.log(passenger);
});


// Render the feedbacks in the UI
renderFeedbacks(allFeedbacks);

function renderFeedbacks(feedbacks) {
	let feedbackList = document.querySelector(".feedbacksL");

	/*let feedbacksL = document.querySelector(".feedbacksL");
feed.feedbacks.forEach(fb=>{
	let div1 = document.createElement("div");
	div1.classList.add("feedback-items");
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
});*/
	// Clear existing content
	feedbackList.innerHTML = "";
  
	// Loop through feedbacks and create elements
	allFeedbacks.forEach(fb => {
	  let div1 = document.createElement("div");
	  div1.classList.add("feedback-items");
  
	  let div2 = document.createElement("div");
	  div2.classList.add("comment-container");
	  let p1 = document.createElement("p");
	  p1.classList.add("comment");
	  p1.innerHTML = fb.comment;
	  div2.appendChild(p1);
	  div1.appendChild(div2);
  
	  let p2 = document.createElement("p");
	  p2.classList.add("rate");
	  p2.innerHTML = `Rating: ${fb.rate}`;
	  div1.appendChild(p2);
  
	  let p3 = document.createElement("p");
	  p3.classList.add("date");
	  p3.innerHTML = `Date: ${fb.date}`;
	  div1.appendChild(p3);
  
	 /* let p4 = document.createElement("p");
	  p4.classList.add("user");
	  p4.innerHTML = `User: ${fb.passenger.email}`;
	  div1.appendChild(p4);*/
  
	  feedbackList.appendChild(div1);
	});
  }




  
