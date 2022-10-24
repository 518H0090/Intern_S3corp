// DropDown Location Toggle

const navbarDropDownToggle = document.querySelector('.navbar .navbar-dropdown__toggle');
const navbarDropDownMenu = document.querySelector('.navbar-dropdown__menu');

navbarDropDownToggle.addEventListener('click',(e) => {
    navbarDropDownMenu.classList.toggle('show');
});

// Show Navbar Search Modal
const navbarSearchBtn = document.querySelector('.navbar-search__btn');
const modalNavbarSearch = document.querySelector('.modal-navbarsearch');

navbarSearchBtn.addEventListener('click',(e) => {
    modalNavbarSearch.classList.add('showModals');
});

// Close Navbar Search Modal
const modalNavBarSearchOverlay = document.querySelector('.modal-navbarsearch__overlay');

modalNavBarSearchOverlay.addEventListener('click', (e) => {
    modalNavbarSearch.classList.remove('showModals');
});

const modalNavbarSearchCloseIcon = document.querySelector('.modal-navbarsearch__close-icon');

modalNavbarSearchCloseIcon.addEventListener('click', (e) => {
    modalNavbarSearch.classList.remove('showModals');
});

// Add Switch Language
const dropdownSwitchToggle = document.querySelector('.dropdown-switch__toggle');
const dropdownSwitchMenu = document.querySelector('.dropdown-switch__menu ');

dropdownSwitchToggle.addEventListener('click',(e) => {
    dropdownSwitchMenu.classList.toggle('showSwitch');
});