// Sıkça Sorulan Sorular Accordion
var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
	acc[i].addEventListener("click", function () {
		this.classList.toggle("active");
		var panel = this.nextElementSibling;
		if (panel.style.maxHeight) {
			panel.style.maxHeight = null;
		} else {
			panel.style.maxHeight = panel.scrollHeight + "px";
		}
	});
}

// Video Controller

function stopVideo() {
	var video = document.getElementById("videoPlayer");
	if (video) {
		video.pause();
		video.currentTime = 0;
	}
}

// Modal Controller
function modalOpen(modalId) {
	var modal = document.getElementById(modalId);

	modal.style.display = "block";
}

function modalClose(modalId) {
	var modal = document.getElementById(modalId);

	modal.style.display = "none";
}

window.onclick = function (event) {
	var modal1 = document.getElementById("kagitBoyutu");
	var modal2 = document.getElementById("videoModal");
	var modal3 = document.getElementById("siparisTakip");
	var modal4 = document.getElementById("siparisDurumu");
	var modal5 = document.getElementById("fotokopiHesaplama");
	var modal6 = document.getElementById("renkSecimi");
	var modal7 = document.getElementById("baskiSekli");
	var modal8 = document.getElementById("kombinasyonSecimi");
	var modal9 = document.getElementById("ciltSecimi");
	var modal10 = document.getElementById("kaliteSecimi");

	if (
		event.target == modal1 ||
		event.target == modal2 ||
		event.target == modal3 ||
		event.target == modal4 ||
		event.target == modal5 ||
		event.target == modal6 ||
		event.target == modal7 ||
		event.target == modal8 ||
		event.target == modal9 ||
		event.target == modal10
	) {
		if (modal1) {
			modal1.style.display = "none";
		}
		if (modal2) {
			modal2.style.display = "none";
			stopVideo();
		}
		if (modal3) {
			modal3.style.display = "none";
		}
		if (modal4) {
			modal4.style.display = "none";
		}
		if (modal5) {
			modal5.style.display = "none";
		}
		if (modal6) {
			modal6.style.display = "none";
		}
		if (modal7) {
			modal7.style.display = "none";
		}
		if (modal8) {
			modal8.style.display = "none";
		}
		if (modal9) {
			modal9.style.display = "none";
		}
		if (modal10) {
			modal10.style.display = "none";
		}
	}
};

// File Input
let fileInput = document.getElementById("siparis-config-file-input");
let fileInputText = document.getElementById("fileInputText");
let uploadFileBtn = document.getElementById("uploadFileBtn");
let sepeteEkleDosyaAdi = document.getElementById("sepeteEkleDosyaAdi");
//let inputItems = document.getElementsByClassName("input-item-button");
let dosyaBasligi = document.getElementById("dosyaBasligi");
//let btnContainer = document.getElementById("btnContainer");
if (fileInput) {
	fileInput.onchange = e => {
		let fileList = e.target.files;
		if (e.target.files?.length) {
			fileInputText.innerText = fileList?.[0]?.name;
			fileInput.style.visibility = "hidden";
			uploadFileBtn.style.display = "inline-block";
			dosyaBasligi.innerText = fileList?.[0]?.name;
			//btnContainer.style.display = "block";
			//[...inputItems].map(item => {
			//	item.disabled = false;
			//});
		}
	};
}

let fileUploadArea = document.getElementById("fileUploadArea");

if (uploadFileBtn) {
	uploadFileBtn.onclick = () => {
		fileUploadArea.style.display = "none";

		dosyaBasligi.style.display = "flex";
	};
}

// Mobile Menu Btn
let menuCollapseBtn = document.getElementById("menu-collapse-btn");
let mobileMenuNav = document.getElementById("navbar");
let mobileMenuCta = document.getElementById("cta");

menuCollapseBtn.onclick = () => {
	if (mobileMenuNav.style.display === "flex") {
		mobileMenuNav.style.display = "none";
		mobileMenuCta.style.display = "none";
	} else {
		mobileMenuNav.style.display = "flex";
		mobileMenuCta.style.display = "flex";
	}
};

// Mobile BlogPost Slider
var swiper = new Swiper(".mySwiper", { spaceBetween: 10 });

// İletişim Form
let iletisimForm = document.getElementById("iletisim-form");
let iletisimMailInput = document.getElementById("iletisim-mail-input");
let mailError = document.getElementById("mail-error");

// E-mail Validator
const validateEmail = email => {
	return String(email)
		.toLowerCase()
		.match(
			/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
		);
};

if (iletisimForm) {
	iletisimForm.addEventListener("click", e => {
		e.preventDefault();

		if (!validateEmail(iletisimMailInput.value)) {
			iletisimMailInput.classList.add("invalid");
			mailError.style.display = "block";
		}
	});
	iletisimMailInput.addEventListener("focus", () => {
		iletisimMailInput.classList.remove("invalid");
		mailError.style.display = "none";
	});
}
