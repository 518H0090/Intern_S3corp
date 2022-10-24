// Show DropDown Location And Category In Search Food Detail
const searchFilterFirstItem = document.querySelector('.searchfilter__item:nth-child(1)');
const searchFilterFirstToggle = document.querySelector('.searchfilter__item:nth-child(1) .searchfilter__toggle');
const searchFilterFirstBoxlist = document.querySelector('.searchfilter__item:nth-child(1) .searchfilter__boxlist'); 

const searchFilterSecondItem = document.querySelector('.searchfilter__item:nth-child(2)');
const searchFilterSecondToggle = document.querySelector('.searchfilter__item:nth-child(2) .searchfilter__toggle');
const searchFilterSecondBoxlist = document.querySelector('.searchfilter__item:nth-child(2) .searchfilter__boxlist'); 

const searchFilterTag = document.querySelectorAll('.searchfilter__tag');

const searchMenuItem = document.querySelectorAll(".layout-searchmenu .searchmenu__item")

searchFilterFirstItem.firstElementChild.addEventListener('click', (e) => {
    searchFilterFirstToggle.classList.toggle('show')
    searchFilterSecondToggle.classList.remove('show')
    searchFilterSecondToggle.classList.remove('active')

    if (searchFilterFirstToggle.classList.contains('active')) {
        searchFilterFirstToggle.classList.remove('active')

        searchFilterTag.forEach(element => {
            element.style.zIndex = "2";
        })

        searchMenuItem.forEach(element => {
            element.style.zIndex = "1"
        })

    } else {
        searchFilterFirstToggle.classList.add('active')

        searchFilterTag.forEach(element => {
            element.style.zIndex = "-1";
        })

        searchMenuItem.forEach(element => {
            element.style.zIndex = "-2"
        })
    }
});

searchFilterFirstBoxlist.addEventListener('click',(e) => {
    searchFilterFirstToggle.classList.toggle('show')
    searchFilterSecondToggle.classList.remove('show')
    searchFilterSecondToggle.classList.remove('active')


    if (searchFilterFirstToggle.classList.contains('active')) {
        searchFilterFirstToggle.classList.remove('active')


    } else {
        searchFilterFirstToggle.classList.add('active')
    }
})

searchFilterSecondItem.firstElementChild.addEventListener('click', (e) => {
    searchFilterSecondToggle.classList.toggle('show')
    searchFilterFirstToggle.classList.remove('show')
    searchFilterFirstToggle.classList.remove('active')


    if (searchFilterSecondToggle.classList.contains('active')) {
        searchFilterSecondToggle.classList.remove('active')

        searchFilterTag.forEach(element => {
            element.style.zIndex = "2";
        })

        searchMenuItem.forEach(element => {
            element.style.zIndex = "1"
        })
    } else {
        searchFilterSecondToggle.classList.add('active')

        searchFilterTag.forEach(element => {
            element.style.zIndex = "-1";
        })

        searchMenuItem.forEach(element => {
            element.style.zIndex = "-2"
        })
    }
});

searchFilterSecondBoxlist.addEventListener('click',(e) => {
    searchFilterSecondToggle.classList.toggle('show')
    searchFilterFirstToggle.classList.remove('show')
    searchFilterFirstToggle.classList.remove('active')

    if (searchFilterSecondToggle.classList.contains('active')) {
        searchFilterSecondToggle.classList.remove('active')

        
    } else {
        searchFilterSecondToggle.classList.add('active')
    }
})


// Process Caculate Checkbox
const calculateFilterLocation = document.querySelectorAll('.searchfilter__item:nth-child(1) .searchfilter__item-checkbox');
const calculateFilterCategory = document.querySelectorAll('.searchfilter__item:nth-child(2) .searchfilter__item-checkbox');

const textFilterLocation = document.querySelector('.searchfilter__tag:first-child .searchfilter__paragraph');

const textFilterCategory = document.querySelector('.searchfilter__tag:last-child .searchfilter__paragraph');

let countLocation = 0;

calculateFilterLocation.forEach(element => {

    if (element.checked) {
        countLocation++
        console.log(countLocation)
    }

    element.addEventListener('change', (e) => {
        if(e.target.checked === true) {
            countLocation++
        } else if (e.target.checked === false) {
            countLocation--
        }

        textFilterLocation.innerText = `Khu vực : ${countLocation}`
    })
   
});

let countCategory = 0;

calculateFilterCategory.forEach(element => {

    if (element.checked) {
        countCategory++
        console.log(countCategory)
    }

    element.addEventListener('change', (e) => {
        if(e.target.checked === true) {
            countCategory++
        } else if (e.target.checked === false) {
            countCategory--
        }

        textFilterCategory.innerText = `Phân loại : ${countCategory}`
    })
   
});


