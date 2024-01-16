import { Passenger } from "./Passenger.js";
import { AvioCompany } from "./AvioCompany.js";

    let RegisterBtn = document.querySelector(".addPassenger");
    RegisterBtn.addEventListener("click", function(){
            let name = document.querySelector(".name").value;
            if (name == "") {
                console.log("Unesite ime");
                return;
            }
            let surname = document.querySelector(".surname").value;
            if (surname == "") {
                console.log("Unesite prezime");
                return;
            }
            let email = document.querySelector(".email").value;
            if (email == "") {
                console.log("Unesite email adresu");
                return;
            }
            let password = document.querySelector(".password").value;
            if(password == "")
            {
                console.log("Unesite lozinku");
                return;
            }
            let nationality = document.querySelector(".nationality").value;
            if(nationality == "")
            {
                console.log("INacionalnost");
                return;
            }

            let phone = document.querySelector(".phone").value;
            if(phone == "")
            {
                console.log("Unesite broj telefona");
                return;
            }
            let birthday = document.querySelector(".birthday").value;
            if(birthday == "")
            {
                console.log("Unesite datum rodjenja");
                return;
            }
            let passport = document.querySelector(".passport").value;
            if(passport == "")
            {
                console.log("Unesite broj vaseg pasosa");
                return;
            }
            let street = document.querySelector(".street").value;
            if(street == "")
            {
                console.log("Unesite ulicu");
                return;
            }
            let stnumber = document.querySelector(".stnumber").value;
            if(stnumber == "")
            {
                console.log("Unesite broj");
                return;
            }
            

        fetch(`http://localhost:5163/Passenger/AddPassenger/`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify({
			email: email,
            password: password,
            name: name,
            surname:surname,
            nationality:nationality,
            passport: passport,
            birthday:birthday,
            phone:phone,
            street:street,
            stnumber:stnumber
		})
        }).then(p=>{
            if(p.ok){
                location.reload();
                console.log(name, surname);
            }
            else
            {
                console.log("nesto je poslo po zlu iks de");
            }
        }).catch(errorMsg=>console.log(errorMsg));

       
});

let RegisterACBtn = document.querySelector(".addAvioCompany");
RegisterACBtn.addEventListener("click", function(){
            let nameac = document.querySelector(".nameac").value;
            if (nameac == "") {
                console.log("Unesite ime");
                return;
            }
            let emailac = document.querySelector(".emailac").value;
            if (emailac == "") {
                console.log("Unesite email adresu");
                return;
            }
            let passwordac = document.querySelector(".passwordac").value;
            if(passwordac == "")
            {
                console.log("Unesite lozinku");
                return;
            }
            let state = document.querySelector(".state").value;
            if(state == "")
            {
                console.log("state");
                return;
            }

            let phoneac = document.querySelector(".phoneac").value;
            if(phoneac == "")
            {
                console.log("Unesite broj telefona");
                return;
            }
            

        fetch(`http://localhost:5163/AvioCompany/AddAvioCompany/`, {
		method: "POST",
		headers: {
			"Content-Type": "application/json"
		},
		body: JSON.stringify({
			email: emailac,
            password: passwordac,
            name: nameac,
            state:state,
            phone:phoneac
		})
        }).then(p=>{
            if(p.ok){
                location.reload();
                console.log(nameac);
            }
            else
            {
                console.log("nesto je poslo po zlu iks de");
            }
        }).catch(errorMsg=>console.log(errorMsg));

       
});


// Declare passengerLogIn outside the event listener
let passengerLogIn;

document.addEventListener("DOMContentLoaded", function () {
    let loginBtn = document.querySelector(".LogIn");
    loginBtn.addEventListener("click", async function () {
        let email = document.getElementById("email").value;
        let password = document.getElementById("password").value;

        if (email === "" || password === "") {
            console.log("Please enter both email and password.");
            return;
        }

        try {
            let response = await fetch(`http://localhost:5163/Passenger/LoginPassenger/${email}/${password}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            })
            if (response.ok) {
                let responseData = await response.json();
                console.log("Response Data:", responseData);

                let passengerData = responseData?.first_name; //.data
                let passengerSurname = responseData?.last_name;
                let passengerEmail = responseData?.email;
                let passengerNationality = responseData?.nationality;

                console.log("Pass data " + passengerData + passengerSurname);
                if (passengerData && passengerSurname) {
                    passengerLogIn = new Passenger(passengerData, passengerSurname);
                    console.log("Login successful:", passengerLogIn);
                } else {
                    console.log("Invalid response format. Please check the server response.");
                }
            } else {
                console.log("Login failed. Please check your credentials.");
            }
        } catch (error) {
            console.error("Error during login:", error);
        }
    });
});

let avioCompanyLogIn; // Change the variable name to indicate it's an AvioCompany

document.addEventListener("DOMContentLoaded", function () {
    let loginACBtn = document.querySelector(".LogInAC");
    loginACBtn.addEventListener("click", async function () {
        let email = document.getElementById("email").value;
        let password = document.getElementById("password").value;

        if (email === "" || password === "") {
            console.log("Please enter both email and password.");
            return;
        }

        try {
            let response = await fetch(`http://localhost:5163/AvioCompany/LoginAvioCompany/${email}/${password}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
            });

            if (response.ok) {
                let responseData = await response.json();
                console.log("Response Data:", responseData);

                let avioCompanyData = responseData?.nameac; //.data
                let avioCompanyEmail = responseData?.emailac;

                console.log("AvioCompany data " + avioCompanyData + avioCompanyEmail);
                if (avioCompanyData && avioCompanyEmail) {
                    avioCompanyLogIn = new AvioCompany(avioCompanyData, avioCompanyEmail);
                    console.log("Login successful:", avioCompanyLogIn);
                } else {
                    console.log("Invalid response format. Please check the server response.");
                }
            } else {
                console.log("Login failed. Please check your credentials.");
            }
        } catch (error) {
            console.error("Error during login:", error);
        }
    });
});


