// Move To Register Page Or Login Page
const layoutFormSwitch = document.querySelector(".layout-form__switch");

layoutFormSwitch.addEventListener("click", (e) => {
  if (layoutFormSwitch.classList.contains("switch-register")) {
    window.location.href = "./register.html";
  } else if (layoutFormSwitch.classList.contains("switch-login")) {
    window.location.href = "./login.html";
  }
});
