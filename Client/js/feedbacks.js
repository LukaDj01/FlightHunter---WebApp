import { Feedback } from "./Feedback.js";
let feedbackAirportAC = window.localStorage.getItem("feedback");
if (feedbackAirportAC == "airport") {
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

      console.log(feedbacks);
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
        feedbackac.avioCompany
      );
      allFeedbacksAC.push(feedbacksac);

      console.log(feedbacksac);
    });
  });
  renderFeedbacksAC(allFeedbacksAC);
  function renderFeedbacksAC(feedbacks) {
    let feedbackListAC = document.querySelector(".feedbacksL");
    feedbackListAC.innerHTML = "";

    allFeedbacksAC.forEach((fb) => {
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
      p5.classList.add("avioCompany");
      p5.innerHTML = `Airport: ${fb.avioCompany.name}`;
      div1.appendChild(p5);

      feedbackListac.appendChild(div1);
    });
  }
}
