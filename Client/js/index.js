import { Passenger } from "./Passenger.js";

let email = window.localStorage.getItem("emailPass");

let passenger;
let signOutBtn = document.querySelector(".signOut");
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

  let dropDownProfil = document.querySelector(".dropDownProfil");

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
    /*window.localStorage.removeItem("emailPass");
        let url = "./login-register.html";
        location.href = url;*/
    console.log("profilllll");
  });
} else {
  let dropDownProfil = document.querySelector(".dropDownProfil");

  let signOutBtn = document.createElement("button");
  signOutBtn.classList.add("dropdown-item");
  signOutBtn.innerHTML = "Sing in";
  dropDownProfil.appendChild(signOutBtn);

  signOutBtn.addEventListener("click", function () {
    let url = "./login-register.html";
    location.href = url;
  });
}

// Get the token from local storage
let token = window.localStorage.getItem("emailAc");

// Check if the token exists
if (token) {
  // Parse the JSON object
  let tokenObject = JSON.parse(token);

  // Access the email and type properties
  let email = tokenObject.email;
  let userType = tokenObject.userType;

  // Check the userType and perform actions accordingly
  if (userType === "aircompany") {
    // Code for aircompany click
    console.log("Clicked by air company");
  } else if (userType === "airport") {
    // Code for airport click
    console.log("Clicked by airport");
  } else {
    // Handle other cases if needed
    console.log("Unknown user type");
  }
} else {
  // Handle the case where the token is not found
  console.log("Token not found in local storage");
}
