// Show Modals Banner Menu Tag
const bannerMenuTab = document.querySelector(".banner-menu__tab");
const modalBannerTag = document.querySelector(".modal-bannertag");
const modalBannerTagOverlay = document.querySelector(
  ".modal-bannertag__overlay"
);
const modalBannerCloseIcon = document.querySelector(
  ".modal-banner__close-icon"
);

bannerMenuTab.addEventListener("click", (e) => {
  modalBannerTag.classList.add("show-tag");
});

modalBannerTagOverlay.addEventListener("click", (e) => {
  modalBannerTag.classList.remove("show-tag");
});

modalBannerCloseIcon.addEventListener("click", (e) => {
  modalBannerTag.classList.remove("show-tag");
});