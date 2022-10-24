const layoutBanner = document.querySelector('.layout-banner');

const layoutBannerSearch = document.querySelector('.layout-banner__search');

window.addEventListener('scroll',(e) => {
  if (window.pageYOffset > 10) {
    layoutBannerSearch.classList.add('layout-banner__search--stuck')
    layoutBanner.style.overflowY = "hidden"
  } else {
    layoutBannerSearch.classList.remove('layout-banner__search--stuck')
    layoutBanner.style.overflowY = "scroll"
  } 
})

window.addEventListener('load',(e) => {

  if (window.pageYOffset > 10) {
    layoutBannerSearch.classList.add('layout-banner__search--stuck')
    layoutBanner.style.overflowY = "hidden"
    window.scroll(0,0)
  }
})