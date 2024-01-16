import { Passenger } from "./Passenger.js";

    let RegisterBtn = document.querySelector(".addPassenger");
    RegisterBtn.addEventListener("click", function(){
            let name = document.querySelector(".name").value;
            if (name == "") {
                console.log("Izaberite dolazni gate");
                return;
            }
            let surname = document.querySelector(".surname").value;
            if (surname == "") {
                console.log("Izaberite dolazni gate");
                return;
            }
            let email = document.querySelector(".email").value;
            if (email == "") {
                console.log("Izaberite dolazni gate");
                return;
            }
            let password = document.querySelector(".password").value;
            if(password == "")
            {
                console.log("Izaberite dolazni gate");
                return;
            }
            let nationality = document.querySelector(".nationality").value;
            if(nationality == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }

            let phone = document.querySelector(".phone").value;
            if(phone == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let birthday = document.querySelector(".birthday").value;
            if(birthday == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let passport = document.querySelector(".passport").value;
            if(passport == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let street = document.querySelector(".street").value;
            if(street == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            let stnumber = document.querySelector(".stnumber").value;
            if(stnumber == "")
            {
                console.log("Izaberite kapacitet");
                return;
            }
            console.log(name, surname);

        /*fetch(`http://localhost:5163/Passenger/AddPassenger/`, {
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
            }
            else
            {
                console.log("nesto je poslo po zlu iks de");
            }
        }).catch(errorMsg=>console.log(errorMsg));*/
});
