import * as Common from "../js/common.js";

if (Common.getCookie("user") !== undefined)
    window.location = "Main/Index";

const button = document.querySelector(".login100-form-btn");

button.addEventListener("click", onLoginClick);

const login = document.querySelector('input[name="login"]');
const password = document.querySelector('input[name="pass"]');

const onLoginInput = () => {
    if (login.value.length < 6)
    {
        login.parentNode.setAttribute('data-validate', "Мінімум 6 символів");
        login.parentNode.classList.add('alert-validate');
    }
    else login.parentNode.classList.remove('alert-validate');
}

const onPasswordInput = () => {
    if (password.value.length < 8) {
        password.parentNode.setAttribute('data-validate', "Мінімум 8 символів");
        password.parentNode.classList.add('alert-validate');
    }
    else password.parentNode.classList.remove('alert-validate');
}

login.addEventListener("input", onLoginInput);
password.addEventListener("input", onPasswordInput);

 async function onLoginClick()
{
     const res = await fetch("https://localhost:7113/User/LogIn?login=" + login.value + "&password=" + password.value);
     if(res.status !== 200)
     {
         const data = await res.text();
         login.parentNode.setAttribute('data-validate', data);
         login.parentNode.classList.add('alert-validate');
         password.parentNode.setAttribute('data-validate', data);
         password.parentNode.classList.add('alert-validate');
     }
     else
     {
         const data = await res.json();
         let date = new Date(Date.now() + 86400e3);
         date = date.toUTCString();
         Common.setCookie("user", JSON.stringify(data), { secure: true, 'expires': date });
         window.location = "Main/Index";
     }
}
