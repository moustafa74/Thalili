// Start Global Functions
document.addEventListener('click', (e) => {
    let menu = document.querySelector('.arrowMenu');
    if (menu != null && e.target.getAttribute('id') !== "arrowBtn" && e.target.getAttribute('id') !== "arrow") {
        menu.classList.remove(menu.getAttribute('data-Todisplay'));
    }
});
function isDigit(char) {
    let asciiOfChar = char.charCodeAt(0);
    if ((asciiOfChar >= 48 && asciiOfChar <= 57)) {
        return true;
    }
    return false;
}
function isChar(char) {
    let asciiOfChar = char.charCodeAt(0);
    if (asciiOfChar >= 1569 && asciiOfChar <= 1610) {
        return true;
    }
    if ((asciiOfChar >= 65 && asciiOfChar <= 90) || (asciiOfChar >= 97 && asciiOfChar <= 122)) {
        return true;
    }
    return false;
}
function isSpecial(char) {
    if (char === ' ' || char === '#' || char === '@' || char === '$' || char === '*' || char === "+") {
        return true;
    }
    return false;
}
function checkPasswordValidation(pass, small) {
    if (pass.length == 0) {
        small.style.visibility = "hidden";
    }
    else if (pass.length < 8) {
        small.textContent = "كلمة المرور يجب ان تكون اكبر من 8 احرف";
        small.style.color = "red";
        small.style.visibility = "visible";
    }
    else {
        let ischar = false, isdigit = false, isspecial = false, isnot = true;
        for (let it of pass) {
            if (isDigit(it)) {
                isdigit = true;
            }
            else if (isChar(it)) {
                ischar = true;
            }
            else if (isSpecial(it)) {
                isspecial = true;
            }
            else {
                isnot = false;
                small.textContent = "كلمة المرور تحتوي علي علامات غير مسموح بها";
                small.style.visibility = "visible";
            }
        }
        if (isnot === false) {
            small.style.color = "red";
            small.textContent = "كلمة المرور تحتوي علي علامات غير مسموح بها";
        }
        else {
            if (ischar && isdigit && isspecial) {
                small.textContent = "كلمة المرور قوية"
                small.style.color = "green";
                return true;
            }
            else {
                small.style.color = "red";
                small.textContent = "كلمة المرور يجب ان تحتوي علي احرف وارقام وعلامات (@,#,$,*, ,+)";
            }
        }
    }
    return false;
}
function checkPasswordEquality(pass1, pass2, small) {
    if (pass1.length === 0) {
        small.style.visibility = "hidden";
    }
    else if (pass1 !== pass2) {
        small.style.color = "red";
        small.textContent = "كلمة المرور غير متطابقة";
        small.style.visibility = "visible";
    }
    else {
        small.style.color = "green";
        small.textContent = "كلمتا المرور متطابقتان";
        small.style.visibility = "visible";
        return true;
    }
    return false;
}
// End Global Functions

// Start  Functions of Navbar
function arrow_clicked() {
    let menu = document.querySelector('.arrowMenu');
    let classname = menu.getAttribute('data-Todisplay');
    menu.classList.toggle(classname);
}
// End  Functions of Navbar

//Start Functions of Login Page 
function changeLogin(h3text, formaction, idofbutton, idofanother, backgroundclass, removeoldbackgroundclass) {
    let element = document.querySelector(".loginPageForm");
    let h3InElement = element.querySelector('h3');
    let forminElement = element.querySelector('form');
    h3InElement.textContent = h3text;
    forminElement.setAttribute('action', formaction);
    document.querySelector(idofbutton).classList.add("loginChangeBtnsActive");
    document.querySelector(idofanother).classList.remove("loginChangeBtnsActive");
    document.querySelector(".aLoginPage").classList.add(backgroundclass);
    document.querySelector(".aLoginPage").classList.remove(removeoldbackgroundclass);
}
function changeLoginUserButton() {
    changeLogin("مستخدم عادي", "userLogin.php", "#user-btn", "#lab-owner-btn", "aLoginPageForUser", "aLoginPageForLabOwner");
}
function changeLoginLabOwnerButton() {
    changeLogin("صاحب معمل", "labOwnerLogin.php", "#lab-owner-btn", "#user-btn", "aLoginPageForLabOwner", "aLoginPageForUser");
}
//End Functions of Login Page 


// Start check Password on Forms
let formofpage = document.forms[0];
if (formofpage != null) {
    let passwordFields = document.querySelectorAll('input[type="password"]');
    if (passwordFields.length > 0) {
        let pass; let valid1 = false; let valid2 = -1;
        if (passwordFields.length > 1) {

            passwordFields[0].addEventListener('input', (e) => {
                valid1 = checkPasswordValidation(e.target.value, formofpage.querySelector('input[type="password"] ~ small'));
                if (passwordFields[1] != null) {
                    valid2 = checkPasswordEquality(passwordFields[1].value, e.target.value, document.querySelectorAll('input[type="password"] ~ small')[1]);
                }
                pass = e.target.value;
            });
            passwordFields[1].addEventListener('input', (e) => {
                let small2 = document.querySelectorAll('input[type="password"] ~ small')[1];
                valid2 = checkPasswordEquality(e.target.value, pass, small2);
            });
            console.log(valid1 + "    " + valid2);
        }

        formofpage.onsubmit = (event) => {
            if (valid2 !== -1) {

                if (!(valid1 && valid2)) {
                    event.preventDefault();
                }
                else {
                    formofpage.submit();
                }
            }
            // else{
            //     if(!valid1){
            //         event.preventDefault();
            //     }
            //     else{
            //         formofpage.submit();
            //     }
            // }
        };
    }
}
// End check Password on Forms