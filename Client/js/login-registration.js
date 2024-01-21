
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
            //console.log(name,surname,email,passport,nationality,phone,birthday, passport,stnumber, street);
            fetch(`http://localhost:5163/Passenger/AddPassenger/`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    email: email,
                    password: password,
                    passport: passport,
                    phone:phone,
                    birth_date:birthday,
                    nationality:nationality,
                    first_name: name,
                    last_name:surname,
                    addr_street:street,
                    addr_stNo:stnumber
                })
                }).then(p=>{
                    if(p.ok){
                        window.localStorage.setItem("emailPass", email);
                        location.reload();
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
            
        //console.log(nameac,emailac,passwordac,state,phoneac);
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
                window.localStorage.setItem("emailAC", emailac);
                location.reload();
            }
            else
            {
                console.log("nesto je poslo po zlu iks de");
            }
        }).catch(errorMsg=>console.log(errorMsg));

       
});


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
            });
            if (response.status===200) {
                window.localStorage.setItem("emailPass", email);
                /*let buying = window.localStorage.getItem("buying");
                if(buying === "yes")*/
                history.back();
                /*let url ="./index.html";
                location.href=url;*/
            } else {
                response = await fetch(`http://localhost:5163/AvioCompany/LoginAvioCompany/${email}/${password}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json"
                }
                });

                if (response.status===200) {
                    window.localStorage.setItem("emailAC", email);
                    let url ="./companies.html";
                    location.href=url;
                } else {
                    console.log("Login failed. Please check your credentials.");
                }
            }
        } catch (error) {
            console.error("Error during login:", error);
        }
    });



