import { Feedback } from "./Feedback.js";
import { Passenger } from "./Passenger.js";
let email = window.localStorage.getItem("emailPass");
let dropDownProfil = document.querySelector(".dropDownProfil");
let passenger;
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
let feedbackAirportAC = window.localStorage.getItem("feedback");
if (feedbackAirportAC == "airport") {
  //console.log("USAO U IF");
  let allFeedbacks = [];
  let promAllFeedbacks = await fetch(
    `http://localhost:5163/Feedback/GetAllFeedbacksAirport/`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  await promAllFeedbacks.json().then((feedbackData) => {
    feedbackData.forEach((feedback) => {
      let feedbacks = new Feedback(
        feedback.rate,
        feedback.comment,
        feedback.date,
        feedback.passenger,
        feedback.airport
      );
      allFeedbacks.push(feedbacks);

      //console.log(feedbacks);
    });
  });
  renderFeedbacks(allFeedbacks);
  function renderFeedbacks(feedbacks) {
    let feedbackList = document.querySelector(".feedbacksL");
    feedbackList.innerHTML = "";

    allFeedbacks.forEach((fb) => {
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

      let p4 = document.createElement("p");
      p4.classList.add("user");
      p4.innerHTML = `User: ${fb.passenger.email}`;
      div1.appendChild(p4);

      let p5 = document.createElement("p");
      p5.classList.add("airport");
      p5.innerHTML = `Airport: ${fb.airport.name}`;
      div1.appendChild(p5);

      feedbackList.appendChild(div1);
    });
  }
} else {
  //console.log("USAO U ELSE");
  let allFeedbacksAC = [];
  let promAllFeedbacksAC = await fetch(
    `http://localhost:5163/Feedback/GetAllFeedbacksAC/`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  await promAllFeedbacksAC.json().then((feedbackDataAC) => {
    feedbackDataAC.forEach((feedbackac) => {
      let feedbacksac = new Feedback(
        feedbackac.rate,
        feedbackac.comment,
        feedbackac.date,
        feedbackac.passenger,
        feedbackac.airport,
        feedbackac.avioCompany
      );
      allFeedbacksAC.push(feedbacksac);

      //console.log(feedbacksac);
    });
  });

  renderFeedbacksAC(allFeedbacksAC);
  function renderFeedbacksAC() {
    let feedbackListAC = document.querySelector(".feedbacksL");
    feedbackListAC.innerHTML = "";

    allFeedbacksAC.forEach((fbac) => {
      let div1ac = document.createElement("div");
      div1ac.classList.add("feedback-items");

      let div2 = document.createElement("div");
      div2.classList.add("comment-container");
      let p1ac = document.createElement("p");
      p1ac.classList.add("comment");
      p1ac.innerHTML = fbac.comment;
      div2.appendChild(p1ac);
      div1ac.appendChild(div2);

      let p2ac = document.createElement("p");
      p2ac.classList.add("rate");
      p2ac.innerHTML = `Rating: ${fbac.rate}`;
      div1ac.appendChild(p2ac);

      let p3ac = document.createElement("p");
      p3ac.classList.add("date");
      p3ac.innerHTML = `Date: ${fbac.date}`;
      div1ac.appendChild(p3ac);

      let p4ac = document.createElement("p");
      p4ac.classList.add("user");
      p4ac.innerHTML = `User: ${fbac.passenger.email}`;
      div1ac.appendChild(p4ac);

      let p5ac = document.createElement("p");
      p5ac.classList.add("avioCompany");
      p5ac.innerHTML = `Avio company: ${fbac.avioCompany.name}`;
      div1ac.appendChild(p5ac);

      feedbackListAC.appendChild(div1ac);
    });
  }
}
